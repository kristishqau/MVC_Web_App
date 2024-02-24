using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email{ get; set; }
        [Required(ErrorMessage = "Please select a role")]
        [Display(Name = "Role")]
        public string SelectedRole { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
