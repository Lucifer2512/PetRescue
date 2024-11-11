using System.ComponentModel.DataAnnotations;

namespace PetRescueFE.Pages.Model
{
    public class AccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }

    public class RegisterAccountViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }

        public string? Gender { get; set; }

        //[Required(ErrorMessage = "Role is required")]
        public int? RoleId { get; set; }
    }
}
