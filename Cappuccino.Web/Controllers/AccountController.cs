using Cappuccino.Common;
using Cappuccino.Common.Enum;
using Cappuccino.Common.Helper;
using Cappuccino.Common.Util;
using Cappuccino.IBLL;
using Cappuccino.Model;
using Cappuccino.Web.Core;
using System;
using System.Linq;
using System.Web.Mvc;
using Cappuccino.ViewModel;
using Cappuccino.Common.Log;
using Cappuccino.Common.Caching;
using System.Collections.Generic;

namespace Cappuccino.Web.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController(ISysUserService sysUserService, ISysLogLogonService sysLogLogonService)
        {
            SysUserService = sysUserService;
            SysLogLogonService = sysLogLogonService;
            this.AddDisposableObject(SysUserService);
            this.AddDisposableObject(SysLogLogonService);

        }

        [SkipCheckLogin]
        public ActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                LoginName = "admin",
                IsMember = true
            };

            if (Request.Cookies[KeyManager.IsMember] != null)
            {
                loginViewModel.IsMember = true;
            }
            return View(loginViewModel);
        }

        [HttpPost, SkipCheckLogin]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return WriteError("实体验证失败");
                }
                if (loginViewModel.VerifyCode.ToLower() != (string)TempData["verifyCode"])
                {
                    return WriteError("验证码失败");
                }
                bool result = SysUserService.CheckLogin(loginViewModel.LoginName, loginViewModel.LoginPassword);
                if (result)
                {
                    var user = SysUserService.GetList(x => x.UserName == loginViewModel.LoginName).FirstOrDefault();
                    string userLoginId = Guid.NewGuid().ToString();
                    if (loginViewModel.IsMember)
                    {
                        List<string> list = new List<string>
                        {
                            userLoginId,
                            "0"
                        };
                        CookieHelper.Set(KeyManager.IsMember, DESUtils.Encrypt(list.ToJson()));
                        CacheManager.Set(userLoginId, user, new TimeSpan(10, 0, 0, 0));
                    }
                    else
                    {
                        CookieHelper.Remove(KeyManager.IsMember);
                        List<string> list = new List<string>
                        {
                            userLoginId,
                            "1"
                        };
                        CookieHelper.Set(KeyManager.IsMember, DESUtils.Encrypt(list.ToJson()), 30);
                        CacheManager.Set(userLoginId, user, new TimeSpan(0, 30, 0));
                    }
                    SysLogLogonService.WriteDbLog(new SysLogLogon
                    {
                        LogType = DbLogType.Login.ToString(),
                        Account = user.UserName,
                        RealName = user.NickName,
                        Description = "登陆成功",
                    });
                    return WriteSuccess("登录成功");
                }
                else
                {
                    return WriteError("用户名或者密码错误");
                }
            }
            catch (Exception ex)
            {
                SysLogLogonService.WriteDbLog(new SysLogLogon
                {
                    LogType = DbLogType.Exception.ToString(),
                    Account = loginViewModel.LoginName,
                    RealName = loginViewModel.LoginName,
                    Description = "登录失败，" + ex.Message
                });
                return WriteError(ex);
            }
        }

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <returns></returns>
        [SkipCheckLogin]
        public ActionResult CreateVerifyCode()
        {
            string verifyCode = VerifyCodeUtils.CreateVerifyCode(4);
            TempData["verifyCode"] = verifyCode.ToLower();
            return File(VerifyCodeUtils.GenerateImage(verifyCode), @"image/Gif");
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            SysLogLogonService.WriteDbLog(new SysLogLogon
            {
                LogType = DbLogType.Exit.ToString(),
                Account = UserManager.GetCurrentUserInfo().UserName,
                RealName = UserManager.GetCurrentUserInfo().NickName,
                Description = "安全退出系统",
            });
            CacheManager.Remove(UserManager.GetCurrentUserCacheId());
            CookieHelper.Remove(KeyManager.IsMember);
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            ViewBag.UserName = UserManager.GetCurrentUserInfo().UserName;
            return View();
        }

        [HttpPost]
        public ActionResult ModifyUserPwd(ChangePasswordViewModel viewModel)
        {
            int userId = UserManager.GetCurrentUserInfo().Id;
            var result = WriteError("出现异常，密码修改失败");
            if (!SysUserService.CheckLogin(viewModel.UserName, viewModel.OldPassword))
            {
                return WriteError("旧密码不正常");
            }
            else
            {
                if (SysUserService.ModifyUserPwd(userId, viewModel))
                {
                    result = WriteSuccess("密码修改成功");
                    List<string> list = DESUtils.Decrypt(CookieHelper.Get(KeyManager.IsMember)).ToList<string>();
                    if (list == null || list.Count() != 2)
                    {
                        //获取缓存的用户信息
                        SysUser userinfo = CacheManager.Get<SysUser>(list[0]);
                        //删除缓存的用户信息
                        CacheManager.Remove(list[0]);
                        //更新缓存用户信息的KEY
                        list[0] = Guid.NewGuid().ToString();
                        if (list[1] == "0")
                        {
                            CacheManager.Set(list[0], userinfo, new TimeSpan(10, 0, 0, 0));
                            CookieHelper.Set(KeyManager.IsMember, DESUtils.Encrypt(list.ToJson()));
                        }
                        else if (list[1] == "1")
                        {
                            CacheManager.Set(list[0], userinfo, new TimeSpan(0, 30, 0));
                            CookieHelper.Set(KeyManager.IsMember, DESUtils.Encrypt(list.ToJson()), 30);
                        }
                    }
                }
                else
                {
                    result = WriteError("密码修改失败");
                }
            }
            return result;
        }

        public ActionResult Person()
        {
            return View();
        }
    }
}