using BlogWebApp.Data;
using BlogWebApp.Models;

namespace BlogWebApp.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAll();
        Task<Comment?> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByPostId(int postId);
        Task<IEnumerable<Comment>> GetCommentsByUserId(string userId);
        //Comment GetLatestComment(int postId);
        bool Add(Comment comment);
        bool Delete(Comment comment);
        bool Save();
    }
}
