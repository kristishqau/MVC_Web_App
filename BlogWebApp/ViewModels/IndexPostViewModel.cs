using BlogWebApp.Models;

namespace BlogWebApp.ViewModels
{
    public class IndexPostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalPosts { get; set; }
        public int Category { get; set; }
        public bool HasPreviousPage => Page > 1;
        public string? Keyword { get; set; }
        public bool HasNextPage => Page < TotalPages;
    }
}
