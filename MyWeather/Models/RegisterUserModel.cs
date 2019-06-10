using System.ComponentModel.DataAnnotations;

namespace MyWeather.Models
{
    public class RegisterUserModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password is not matched")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}