using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
