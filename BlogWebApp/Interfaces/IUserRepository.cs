using BlogWebApp.Data;
using BlogWebApp.Models;

namespace BlogWebApp.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);
        List<string> GetUserRoles();
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
