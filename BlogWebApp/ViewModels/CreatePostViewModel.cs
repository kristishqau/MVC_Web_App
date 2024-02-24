using BlogWebApp.Data;
using BlogWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.ViewModels
{
    public class CreatePostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public IFormFile ImageUrl { get; set; }
        public string AppUserId { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }
    }
}
