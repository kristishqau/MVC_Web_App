using BlogWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public List<string> AvailableRoles { get; set; }
    }
}
