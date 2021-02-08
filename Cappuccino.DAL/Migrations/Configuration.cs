using Cappuccino.Common.Enum;
using Cappuccino.Common.Util;
using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Cappuccino.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Cappuccino.DAL.EfDbContext>
    {
        public Configuration()
        {
            //数据
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Cappuccino.DAL.EfDbContext context)
        {
            #region 用户数据
            string salt = VerifyCodeUtils.CreateVerifyCode(5);
            string passwordHash = Md5Utils.EncryptTo32(salt + "123456");
            var sysUsers = new List<SysUser>
            {
              new SysUser{UserName="admin",NickName="超级管理员",HeadIcon="/Content/admin/images/avatar.jpg",PasswordSalt=salt,PasswordHash=passwordHash,
                          Email="admin@Cappuccino.com",EnabledMark=(int)EnabledMarkEnum.Valid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysUser{UserName="system",NickName="系统管理员",HeadIcon="/Content/admin/images/avatar.jpg",PasswordSalt=salt,PasswordHash=passwordHash,
                          Email="system@Cappuccino.com",EnabledMark=(int)EnabledMarkEnum.Invalid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysUser{UserName="user",NickName="普通用户",HeadIcon="/Content/admin/images/avatar.jpg",PasswordSalt=salt,PasswordHash=passwordHash,
                          Email="user@Cappuccino.com",EnabledMark=(int)EnabledMarkEnum.Invalid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysUser{UserName="test",NickName="测试用户",HeadIcon="/Content/admin/images/avatar.jpg",PasswordSalt=salt,PasswordHash=passwordHash,
                          Email="test@Cappuccino.com",EnabledMark=(int)EnabledMarkEnum.Invalid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
            };
            sysUsers.ForEach(s => context.Set<SysUser>().Add(s));
            context.SaveChanges();
            #endregion

            #region 角色数据
            var sysRoles = new List<SysRole>
            {
              new SysRole{  Name="超级管理员",Code="Administrator",EnabledMark=(int)EnabledMarkEnum.Valid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysRole{  Name="系统管理员",Code="system",EnabledMark=(int)EnabledMarkEnum.Invalid,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysRole{  Name="普通用户",Code="user",EnabledMark=(int)EnabledMarkEnum.Invalid,Remark="只有查看页面的权限",CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
              new SysRole{  Name="测试用户",Code="test",EnabledMark=(int)EnabledMarkEnum.Invalid,Remark="用来测试的",CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

            };
            sysRoles.ForEach(s => context.Set<SysRole>().Add(s));
            context.SaveChanges();
            #endregion

            #region 用户角色数据
            var adminUser = context.Set<SysUser>().Single(s => s.UserName == "admin");
            var adminRole = context.Set<SysRole>().Single(s => s.Code == "Administrator");
            adminUser.SysRoles.Add(adminRole);
            var systemUser = context.Set<SysUser>().Single(s => s.UserName == "system");
            var systemRole = context.Set<SysRole>().Single(s => s.Code == "system");
            systemUser.SysRoles.Add(systemRole);
            var user = context.Set<SysUser>().Single(s => s.UserName == "user");
            var userRole = context.Set<SysRole>().Single(s => s.Code == "user");
            user.SysRoles.Add(userRole);
            var testUser = context.Set<SysUser>().Single(s => s.UserName == "test");
            var testRole = context.Set<SysRole>().Single(s => s.Code == "test");
            testUser.SysRoles.Add(testRole);
            context.SaveChanges();
            #endregion

            #region 权限管理
            //目录
            var sysActionsByCatalog = new List<SysAction>
            {
                 new SysAction{Name="系统管理",Code="system",ParentId=0,Type=0,SortCode=1,
                 SysActionMenu=new SysActionMenu{Icon="layui-icon-set-fill" },
                 CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1}
            };
            sysActionsByCatalog.ForEach(s => context.Set<SysAction>().Add(s));
            context.SaveChanges();

            //菜单
            var systemMenu = context.Set<SysAction>().Single(s => s.Code == "system");
            var sysActionByMenus = new List<SysAction>
            {
                new SysAction{Id=2,Name="用户管理",Code="system.user.list",ParentId=systemMenu.Id,Type=0,SortCode=1
                ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysUser" }
                ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Id=3,Name="角色管理",Code="system.role.list",ParentId=systemMenu.Id,Type=0,SortCode=2
                ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysRole" }
                ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Id=4,Name="权限管理",Code="system.action.list",ParentId=systemMenu.Id,Type=0,SortCode=3
                ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysAction" }
                ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Id=5,Name="数据字典",Code="system.dict.list",ParentId=systemMenu.Id,Type=0,SortCode=4
                ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysDict" }
                ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Id=6,Name="日志管理",Code="system.log",ParentId=systemMenu.Id,Type=0,SortCode=5
                ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry"}
                ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=systemMenu.Id,UpdateUserId=1},
            };
            sysActionByMenus.ForEach(s => context.Set<SysAction>().Add(s));
            context.SaveChanges();

            //子级菜单
            var systemMenuSystemLog = context.Set<SysAction>().Single(s => s.Code == "system.log");
            var sysActionByMenuSystemLogs = new List<SysAction>
            {
                new SysAction{Id=7,Name="登录日志",Code="system.log.logon",ParentId=systemMenuSystemLog.Id,Type=0,SortCode=6
                ,SysActionMenu=new SysActionMenu{Icon="layui-icon-face-cry",Url="/System/SysLogLogon" }
                ,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
            };
            sysActionByMenuSystemLogs.ForEach(s => context.Set<SysAction>().Add(s));
            context.SaveChanges();

            //按钮
            var systemMenuSystemUser = context.Set<SysAction>().Single(s => s.Code == "system.user.list");
            var systemMenuSystemRole = context.Set<SysAction>().Single(s => s.Code == "system.role.list");
            var systemMenuSystemAction = context.Set<SysAction>().Single(s => s.Code == "system.action.list");
            var systemMenuSystemDict = context.Set<SysAction>().Single(s => s.Code == "system.dict.list");
            var systemMenuSystemLogLogon = context.Set<SysAction>().Single(s => s.Code == "system.log.logon");
            var sysActionByButton = new List<SysAction>
            {
                new SysAction{Name="新增",Code="system.user.create",ParentId=systemMenuSystemUser.Id,Type=ActionTypeEnum.Button,SortCode=1,
                SysActionButton=new SysActionButton{ButtonCode="create",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-add-1" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="编辑",Code="system.user.edit",ParentId=systemMenuSystemUser.Id,Type=ActionTypeEnum.Button,SortCode=2,
                SysActionButton=new SysActionButton{ButtonCode="edit",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-primary pear-btn-sm",ButtonIcon="layui-icon-edit" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="删除",Code="system.user.delete",ParentId=systemMenuSystemUser.Id,Type=ActionTypeEnum.Button,SortCode=3,
                SysActionButton=new SysActionButton{ButtonCode="delete",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-danger pear-btn-sm",ButtonIcon="layui-icon-delete" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="批量删除",Code="system.user.batchDel",ParentId=systemMenuSystemUser.Id,Type=ActionTypeEnum.Button,SortCode=4,
                SysActionButton=new SysActionButton{ButtonCode="batchDel",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-danger pear-btn-md",ButtonIcon="layui-icon-delete" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="分配权限",Code="system.user.assign",ParentId=systemMenuSystemUser.Id,Type=ActionTypeEnum.Button,SortCode=5,
                SysActionButton=new SysActionButton{ButtonCode="assign",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-warming pear-btn-sm",ButtonIcon="layui-icon-vercode" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="重置密码",Code="system.user.initPwd",ParentId=systemMenuSystemUser.Id,Type=ActionTypeEnum.Button,SortCode=6,
                SysActionButton=new SysActionButton{ButtonCode="initPwd",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-warming pear-btn-sm",ButtonIcon="layui-icon-refresh" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                new SysAction{Name="新增",Code="system.role.create",ParentId=systemMenuSystemRole.Id,Type=ActionTypeEnum.Button,SortCode=1,
                SysActionButton=new SysActionButton{ButtonCode="create",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-add-1" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="编辑",Code="system.role.edit",ParentId=systemMenuSystemRole.Id,Type=ActionTypeEnum.Button,SortCode=2,
                SysActionButton=new SysActionButton{ButtonCode="edit",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-primary pear-btn-sm",ButtonIcon="layui-icon-edit" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="删除",Code="system.role.delete",ParentId=systemMenuSystemRole.Id,Type=ActionTypeEnum.Button,SortCode=3,
                SysActionButton=new SysActionButton{ButtonCode="delete",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-danger pear-btn-sm",ButtonIcon="layui-icon-delete" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="批量删除",Code="system.role.batchDel",ParentId=systemMenuSystemRole.Id,Type=ActionTypeEnum.Button,SortCode=4,
                SysActionButton=new SysActionButton{ButtonCode="batchDel",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-danger pear-btn-md",ButtonIcon="layui-icon-delete" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="分配权限",Code="system.role.assign",ParentId=systemMenuSystemRole.Id,Type=ActionTypeEnum.Button,SortCode=5,
                SysActionButton=new SysActionButton{ButtonCode="assign",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-warming pear-btn-sm",ButtonIcon="layui-icon-vercode" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                new SysAction{Name="新增",Code="system.action.create",ParentId=systemMenuSystemAction.Id,Type=ActionTypeEnum.Button,SortCode=1,
                SysActionButton=new SysActionButton{ButtonCode="create",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-add-1" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="编辑",Code="system.action.edit",ParentId=systemMenuSystemAction.Id,Type=ActionTypeEnum.Button,SortCode=2,
                SysActionButton=new SysActionButton{ButtonCode="edit",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-primary pear-btn-sm",ButtonIcon="layui-icon-edit" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="删除",Code="system.action.delete",ParentId=systemMenuSystemAction.Id,Type=ActionTypeEnum.Button,SortCode=3,
                SysActionButton=new SysActionButton{ButtonCode="delete",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-danger pear-btn-sm",ButtonIcon="layui-icon-delete" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="批量删除",Code="system.action.batchDel",ParentId=systemMenuSystemAction.Id,Type=ActionTypeEnum.Button,SortCode=4,
                SysActionButton=new SysActionButton{ButtonCode="batchDel",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-danger pear-btn-md",ButtonIcon="layui-icon-delete" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                new SysAction{Name="新增",Code="system.dict.create",ParentId=systemMenuSystemDict.Id,Type=ActionTypeEnum.Button,SortCode=1,
                SysActionButton=new SysActionButton{ButtonCode="create",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-add-1" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="编辑",Code="system.dict.edit",ParentId=systemMenuSystemDict.Id,Type=ActionTypeEnum.Button,SortCode=2,
                SysActionButton=new SysActionButton{ButtonCode="edit",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-primary pear-btn-sm",ButtonIcon="layui-icon-edit" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="删除",Code="system.dict.delete",ParentId=systemMenuSystemDict.Id,Type=ActionTypeEnum.Button,SortCode=3,
                SysActionButton=new SysActionButton{ButtonCode="delete",Location=PositionEnum.FormInside,ButtonClass="pear-btn pear-btn-danger pear-btn-sm",ButtonIcon="layui-icon-delete" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysAction{Name="批量删除",Code="system.dict.batchDel",ParentId=systemMenuSystemDict.Id,Type=ActionTypeEnum.Button,SortCode=4,
                SysActionButton=new SysActionButton{ButtonCode="batchDel",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-danger pear-btn-md",ButtonIcon="layui-icon-delete" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},

                new SysAction{Id=26,Name="导出",Code="system.log.logon.export",ParentId=systemMenuSystemLogLogon.Id,Type=ActionTypeEnum.Button,SortCode=1,
                SysActionButton=new SysActionButton{ButtonCode="export",Location=PositionEnum.FormRightTop,ButtonClass="pear-btn pear-btn-primary pear-btn-md",ButtonIcon="layui-icon-export" },
                CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
            };

            sysActionByButton.ForEach(s => context.Set<SysAction>().Add(s));
            context.SaveChanges();
            #endregion

            #region 分配权限
            //超级管理员
            adminRole = context.Set<SysRole>().Single(s => s.Code == "Administrator");
            var adminAction = context.Set<SysAction>().ToList();
            adminAction.ForEach(x => adminRole.SysActions.Add(x));
            //普通用户
            userRole = context.Set<SysRole>().Single(s => s.Code == "user");
            adminAction = context.Set<SysAction>().Where(x => x.Type == ActionTypeEnum.Menu).ToList();
            adminAction.ForEach(x => adminRole.SysActions.Add(x));
            context.SaveChanges();
            #endregion

            #region 数据字典
            var sysDictTypes = new List<SysDictType>
            {
                new SysDictType{Name="机构类型",Code="OrganizeCategory",SortCode=1,SysDicts=new List<SysDict> {
                    new SysDict{Name="公司",Code="Company",SortCode=1,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                    new SysDict{Name="部门",Code="Department",SortCode=2,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                    new SysDict{Name="小组",Code="WorkGroup",SortCode=3,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1}},
                    CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                new SysDictType{Name="性别",Code="Sex",SortCode=2,SysDicts=new List<SysDict> {
                    new SysDict{Name="男",Code="Male",SortCode=1,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
                    new SysDict{Name="女",Code="Female",SortCode=2,CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1}},
                    CreateTime=DateTime.Now,UpdateTime=DateTime.Now,CreateUserId=1,UpdateUserId=1},
            };
            sysDictTypes.ForEach(s => context.Set<SysDictType>().Add(s));
            context.SaveChanges();
            #endregion
        }
    }
}
