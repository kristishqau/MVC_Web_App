using BlogWebApp.Data;
using BlogWebApp.Interfaces;
using BlogWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Comment comment)
        {
            _context.Add(comment);
            return Save();
        }
        public bool Delete(Comment comment)
        {
            _context.Remove(comment);
            return Save();
        }
        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetCommentsByUserId(string userId)
        {
            return await _context.Comments
                .Where(c => c.AppUserId == userId)
                .ToListAsync();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}