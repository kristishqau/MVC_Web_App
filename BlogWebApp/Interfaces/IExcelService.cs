using BlogWebApp.Models;

namespace BlogWebApp.Interfaces
{
    public interface IExcelService
    {
        byte[] GenerateExcelFile(IEnumerable<Post> posts);
    }
}
