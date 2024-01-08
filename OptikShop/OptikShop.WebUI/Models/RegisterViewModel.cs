using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace OptikShop.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad giriniz.")]
		[MaxLength(30)]
		public string  FirstName { get; set; }

        [Required(ErrorMessage = "Soyad giriniz.")]
		[MaxLength(30)]
		public string  LastName { get; set; }

        [EmailAddress(ErrorMessage ="Lütfen bir email formatı giriniz.")]
        [Required(ErrorMessage = "Email adresi gereklidir")]
		[MaxLength(50)]
		public string Email { get; set; }

        [Required(ErrorMessage = "Parola alanı gereklidir.")]
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage = "Parolalar eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
        

    }
}
