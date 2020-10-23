using Cappuccino.Model;
using Cappuccino.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.IBLL
{
    public interface ISysUserService : IBaseService<SysUser>
    {
        bool CheckLogin(string loginName, string loginPassword);

        bool ModifyUserPwd(int userId, ChangePasswordViewModel viewModel);
    }
}
