using BlogWebApp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApp.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
