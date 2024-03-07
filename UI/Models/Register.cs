using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class Register
    {
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        public string UserName { get; set; }
        //[Required]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{12,}$", ErrorMessage = "Minimum length 12 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; } 
        [Compare("Password")]
        public string PasswordConfirm { get; set; } 
        [Required]
        public string Role { get; set; } 
    }
}
