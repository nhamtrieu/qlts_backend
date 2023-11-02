using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher062023.Demo.Application
{
    public class UserDto
    {

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Required(ErrorMessage ="Tên đăng nhập không được phép để trống")]
        public string UserName { get; set; }

        /// <summary>
        /// mật khẩu
        /// </summary>
        [Required(ErrorMessage ="Mật khẩu không được phép để trống")]
        public string Password { get; set; }
    }
}
