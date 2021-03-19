namespace PMG.Services.Bookmark
{
    using Domain;
    using System.Threading.Tasks;

    public interface IBookmarkService
    {
        Task AddBookmark(Bookmark bookmark);
        Task<Bookmark> GetBookmarkByUsername(string username);
        Task Delete(string BookmarId, decimal mark, string subject);
    }
}