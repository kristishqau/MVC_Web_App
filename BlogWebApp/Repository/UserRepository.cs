using BlogWebApp.Data;
using BlogWebApp.Interfaces;
using BlogWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(AppUser user)
        {
            user =  _context.Users
                .Include(u => u.Posts)
                .Include(u => u.Comments)
                .FirstOrDefault(u => u.Id == user.Id);

            if (user == null)
            {
                return false;
            }

            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public List<string> GetUserRoles()
        {
            return new List<string> { UserRoles.Admin, UserRoles.Editor, UserRoles.Member };
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
