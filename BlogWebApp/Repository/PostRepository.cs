using BlogWebApp.Data;
using BlogWebApp.Interfaces;
using BlogWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Add(Post post)
        {
            _context.Add(post);
            return Save();
        }

        public bool Delete(Post post)
        {
            _context.Remove(post);
            return Save();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts
                .Include(u => u.AppUser)
                .Include(c => c.Comments)
                .OrderByDescending(p => p.CreatedDate)
                .Take(10)
                .ToListAsync();
        }
        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(u => u.AppUser)
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Post?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Posts
                .Include(u => u.AppUser)
                .Include(c => c.Comments)
                    .ThenInclude(u => u.AppUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(string userId)
        {
            return await _context.Posts
                .Where(c => c.AppUserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetPostByCategory(Category category)
        {
            return await _context.Posts.Where(c => c.Category == category).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Post post)
        {
            _context.Update(post);
            return Save();
        }

        public async Task<IEnumerable<Post>> GetSliceAsync(int offset, int size)
        {
            return await _context.Posts
                .Include(u => u.AppUser)
                .Include(c => c.Comments)
                .OrderByDescending(p => p.CreatedDate)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Posts.CountAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryAndSliceAsync(Category category, int offset, int size)
        {
            return await _context.Posts
                .Include(u => u.AppUser)
                .Include(c => c.Comments)
                .Where(c => c.Category == category)
                .OrderByDescending(p => p.CreatedDate)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetCountByCategoryAsync(Category category)
        {
            return await _context.Posts.CountAsync(c => c.Category == category);
        }

        public async Task<IEnumerable<Post>> GetPostsByKeywordAndSliceAsync(string keyword, int start, int count)
        {
            return await _context.Posts
                .Include(u => u.AppUser)
                .Include(c => c.Comments)
                .Where(post => post.Title.Contains(keyword) || post.Description.Contains(keyword))
                .OrderByDescending(post => post.CreatedDate) 
                .Skip(start)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> GetCountByKeywordAsync(string keyword)
        {
            return await _context.Posts
                .CountAsync(post => post.Title.Contains(keyword) || post.Description.Contains(keyword));
        }
    }
}
