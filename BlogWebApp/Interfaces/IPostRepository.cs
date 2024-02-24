using BlogWebApp.Data;
using BlogWebApp.Models;

namespace BlogWebApp.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post?> GetByIdAsync(int id);
        Task<Post?> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Post>> GetPostByCategory(Category category);
        Task<IEnumerable<Post>> GetPostsByUserId(string userId);
        Task<IEnumerable<Post>> GetSliceAsync(int offset, int size);
        Task<int> GetCountAsync();
        Task<IEnumerable<Post>> GetPostsByCategoryAndSliceAsync(Category category, int offset, int size);
        Task<int> GetCountByCategoryAsync(Category category);
        Task<IEnumerable<Post>> GetPostsByKeywordAndSliceAsync(string keyword, int start, int count);
        Task<int> GetCountByKeywordAsync(string keyword);
        bool Add(Post post);
        bool Update(Post post);
        bool Delete(Post post);
        bool Save();
    }
}
