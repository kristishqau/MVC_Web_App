using BlogWebApp.Data;
using BlogWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApp.ViewModels
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? URL { get; set; }
        public IFormFile ImageUrl { get; set; }
        public Category Category { get; set; }
    }
}
