using System.ComponentModel.DataAnnotations;

namespace GrowDT.MvcApplication.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名")]
        public string Username { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        [Display(Name = "密 码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string MsgNamePwdWrong
        {
            get { return "用户名或密码错误"; }
        }
    }
}