using System.ComponentModel.DataAnnotations;

namespace OptikShop.WebUI.Models
{
    public class LoginViewModel
    {

		[Required(ErrorMessage ="Email gereklidir.")]
		public string Email { get; set; }

		[Required(ErrorMessage ="Parala gereklidir.")]
		public string Password { get; set; }
	}
}
