using AutoMapper;
using Cappuccino.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    public static class EntityMapper
    {
        /// <summary>
        /// 负责将所有实体做一次映射操作
        /// </summary>
        static EntityMapper()
        {
            //1.将Model和ViewModel中的所有实体类在AutoMapper内部建立一个关联
            Mapper.CreateMap<SysUser, SysUserViewModel>();
            Mapper.CreateMap<SysRole, SysRoleViewModel>();
            Mapper.CreateMap<SysAction, SysActionViewModel>();
            Mapper.CreateMap<SysActionMenu, SysActionMenuViewModel>();
            Mapper.CreateMap<SysActionButton, SysActionButtonViewModel>();
            Mapper.CreateMap<SysUserAction, SysUserActionViewModel>();
            Mapper.CreateMap<SysDict, SysDictViewModel>();
            Mapper.CreateMap<SysDictType, SysDictTypeViewModel>();
            Mapper.CreateMap<SysLogLogon, SysLogLogonViewModel>();

            //2.将ViewModel和Model中的所有实体类在AutoMapper内部建立一个关联
            Mapper.CreateMap<SysUserViewModel, SysUser>();
            Mapper.CreateMap<SysRoleViewModel, SysRole>();
            Mapper.CreateMap<SysActionViewModel, SysAction>();
            Mapper.CreateMap<SysActionMenuViewModel, SysActionMenu>();
            Mapper.CreateMap<SysActionButtonViewModel, SysActionButton>();
            Mapper.CreateMap<SysUserActionViewModel, SysUserAction>();
            Mapper.CreateMap<SysDictViewModel, SysDict>();
            Mapper.CreateMap<SysDictTypeViewModel, SysDictType>();
            Mapper.CreateMap<SysLogLogonViewModel, SysLogLogon>();
        }

        #region SysUser
        public static SysUserViewModel EntityMap(this SysUser model)
        {
            return Mapper.Map<SysUser, SysUserViewModel>(model);
        }

        public static SysUser EntityMap(this SysUserViewModel model)
        {
            return Mapper.Map<SysUserViewModel, SysUser>(model);
        }

        #endregion

        #region SysRole
        public static SysRoleViewModel EntityMap(this SysRole model)
        {
            return Mapper.Map<SysRole, SysRoleViewModel>(model);
        }

        public static SysRole EntityMap(this SysRoleViewModel model)
        {
            return Mapper.Map<SysRoleViewModel, SysRole>(model);
        }
        #endregion

        #region SysAction
        public static SysActionViewModel EntityMap(this SysAction model)
        {
            return Mapper.Map<SysAction, SysActionViewModel>(model);
        }

        public static SysAction EntityMap(this SysActionViewModel model)
        {
            return Mapper.Map<SysActionViewModel, SysAction>(model);
        }
        #endregion

        #region SysActionMenu
        public static SysActionMenuViewModel EntityMap(this SysActionMenu model)
        {
            return Mapper.Map<SysActionMenu, SysActionMenuViewModel>(model);
        }

        public static SysActionMenu EntityMap(this SysActionMenuViewModel model)
        {
            return Mapper.Map<SysActionMenuViewModel, SysActionMenu>(model);
        }
        #endregion

        #region SysActionButton
        public static SysActionButtonViewModel EntityMap(this SysActionButton model)
        {
            return Mapper.Map<SysActionButton, SysActionButtonViewModel>(model);
        }

        public static SysActionButton EntityMap(this SysActionButtonViewModel model)
        {
            return Mapper.Map<SysActionButtonViewModel, SysActionButton>(model);
        }
        #endregion

        #region SysUserAction
        public static SysUserActionViewModel EntityMap(this SysUserAction model)
        {
            return Mapper.Map<SysUserAction, SysUserActionViewModel>(model);
        }

        public static SysUserAction EntityMap(this SysUserActionViewModel model)
        {
            return Mapper.Map<SysUserActionViewModel, SysUserAction>(model);
        }
        #endregion

        #region SysDict
        public static SysDictViewModel EntityMap(this SysDict model)
        {
            return Mapper.Map<SysDict, SysDictViewModel>(model);
        }

        public static SysDict EntityMap(this SysDictViewModel model)
        {
            return Mapper.Map<SysDictViewModel, SysDict>(model);
        }
        #endregion

        #region SysDictType
        public static SysDictTypeViewModel EntityMap(this SysDictType model)
        {
            return Mapper.Map<SysDictType, SysDictTypeViewModel>(model);
        }

        public static SysDictType EntityMap(this SysDictTypeViewModel model)
        {
            return Mapper.Map<SysDictTypeViewModel, SysDictType>(model);
        }
        #endregion

        #region SysLogLogon
        public static SysLogLogonViewModel EntityMap(this SysLogLogon model)
        {
            return Mapper.Map<SysLogLogon, SysLogLogonViewModel>(model);
        }

        public static SysLogLogon EntityMap(this SysLogLogonViewModel model)
        {
            return Mapper.Map<SysLogLogonViewModel, SysLogLogon>(model);
        }
        #endregion
    }
}
