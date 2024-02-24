using BlogWebApp.Models;

namespace BlogWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Post>> GetAllUserPosts();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
