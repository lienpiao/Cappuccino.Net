using Cappuccino.Common.Enum;
using Cappuccino.Common.Util;
using Cappuccino.ViewModel;
using Cappuccino.IBLL;
using Cappuccino.IDAL;
using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.BLL
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        #region 依赖注入
        ISysUserDao dao;
        public SysUserService(ISysUserDao dao)
        {
            this.dao = dao;
            base.CurrentDao = dao;
            this.AddDisposableObject(this.CurrentDao);
        }
        #endregion

        public bool CheckLogin(string loginName, string loginPassword)
        {
            var user = dao.GetList(x => x.UserName == loginName && x.EnabledMark == (int)EnabledMarkEnum.Valid).SingleOrDefault(x => x.UserName == loginName);
            if (user == null)
            {
                return false;
            }
            else
            {
                string dbPwdHash = user.PasswordHash;
                string salt = user.PasswordSalt;
                string userPwdHash = Md5Utils.EncryptTo32(salt + loginPassword);
                return dbPwdHash == userPwdHash;
            }
        }

        public bool ModifyUserPwd(int userId, ChangePasswordViewModel viewModel)
        {
            string salt = VerifyCodeUtils.CreateVerifyCode(5);
            string passwordHash = Md5Utils.EncryptTo32(salt + viewModel.Password);
            var user = dao.GetList(x => x.Id == userId).FirstOrDefault();
            user.PasswordSalt = salt;
            user.PasswordHash = passwordHash;
            user.UpdateUserId = userId;
            user.UpdateTime = DateTime.Now;
            return dao.SaveChanges() >= 1;
        }
    }
}
