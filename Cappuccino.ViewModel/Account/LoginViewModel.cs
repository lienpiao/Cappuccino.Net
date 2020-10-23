using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.ViewModel
{
    public class LoginViewModel
    {
        [DisplayName("帐号"), Required(ErrorMessage = "帐号非空")]
        public string LoginName { get; set; }
        [DisplayName("密码"), Required(ErrorMessage = "帐号非空")]
        public string LoginPassword { get; set; }
        [DisplayName("验证码"), Required(ErrorMessage = "帐号非空")]
        public string VerifyCode { get; set; }
        [DisplayName("验证码")]
        public bool IsMember { get; set; }
    }
}
