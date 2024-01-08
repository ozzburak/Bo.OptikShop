using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace OptikShop.WebUI.Models
{
    public class AddContactViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu alan doldurulmak zorundadır.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Bu alan doldurulmak zorundadır.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Bu alan doldurulmak zorundadır.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu alan doldurulmak zorundadır.")]
        public string Message { get; set; }
    }
}
