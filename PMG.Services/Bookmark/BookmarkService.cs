namespace PMG.Services.Bookmark
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using PMG.Domain.SchoolSubjects;
    using System.Linq;
    using System.Threading.Tasks;

    public class BookmarkService : IBookmarkService
    {
        private readonly PMGDbContext context;
        public BookmarkService(PMGDbContext context)
        {
            this.context = context;
        }
        public async Task AddBookmark(Bookmark bookmark)
        {
            await context.Bookmarks.AddAsync(bookmark);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string BookmarId, decimal mark, string subject)
        {
            var bookmark = await context.Users
               .Where(x => x.Id == BookmarId)
               .Select(x => x.Bookmark)
               .Include(x => x.EnglishMarks)
               .Include(x => x.PhilosophyMarks)
               .Include(x => x.MathematicsMarks)
               .SingleOrDefaultAsync();

            if (subject == "Philosophy")
            {
                var _mark  = bookmark.PhilosophyMarks.Where(x => x.Mark == mark).FirstOrDefault();
                if (_mark !=null) { bookmark.PhilosophyMarks.Remove(_mark); }
            }
            else if (subject == "Mathematics")
            {
                var _mark = bookmark.MathematicsMarks.Where(x => x.Mark == mark).FirstOrDefault();
                if (_mark != null) { bookmark.MathematicsMarks.Remove(_mark); }
            }
            else if (subject == "English")
            {
                var _mark = bookmark.EnglishMarks.Where(x => x.Mark == mark).FirstOrDefault();
                if (_mark != null) { bookmark.EnglishMarks.Remove(_mark); }
            }

            await context.SaveChangesAsync();
        }

        public async Task<Bookmark> GetBookmarkByUsername(string username)
        {
            var bookmark = await context.Users
                .Where(x => x.UserName == username)
                .Select(x => x.Bookmark)
                .Include(x => x.EnglishMarks)
                .Include(x => x.PhilosophyMarks)
                .Include(x => x.MathematicsMarks)
                .SingleOrDefaultAsync();

            return bookmark;
        }

    }
}