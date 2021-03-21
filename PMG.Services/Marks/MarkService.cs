namespace PMG.Services.Marks
{
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using PMG.Domain;
    using PMG.Domain.SchoolSubjects;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MarkService : IMarkService
    {
        private readonly PMGDbContext context;
        public MarkService(PMGDbContext context)
        {
            this.context = context;
        }

        public async Task CreateMarkEnglish(decimal mark, PMGUser user)
        {
            English english = new English
            {
                Mark = mark,
                Day = DateTime.UtcNow
            };
            user.Bookmark.EnglishMarks.Add(english);
            await context.SaveChangesAsync();
        }

        public async Task CreateMarkMathematics(decimal mark, PMGUser user)
        {
            Mathematics math = new Mathematics
            {
                Mark = mark,
                Day = DateTime.UtcNow
            };
            user.Bookmark.MathematicsMarks.Add(math);
            await context.SaveChangesAsync();
        }

        public async Task CreateMarkPhilosophy(decimal mark, PMGUser user)
        {
            Philosophy philosophy = new Philosophy
            {
                Mark = mark,
                Day = DateTime.UtcNow
            };
            user.Bookmark.PhilosophyMarks.Add(philosophy);
            await context.SaveChangesAsync();
        }
        public async Task DeleteMark(string MarkId, string bookmarkId)
        {
            var bookmark = context.Bookmarks
                .Include(x => x.EnglishMarks)
                .Include(x => x.PhilosophyMarks)
                .Include(x => x.MathematicsMarks)
                .SingleOrDefault(x => x.Id == bookmarkId);

            var english = bookmark.EnglishMarks.SingleOrDefault(x => x.Id == MarkId);
            var philosophy = bookmark.PhilosophyMarks.SingleOrDefault(x => x.Id == MarkId);
            var mathematics = bookmark.MathematicsMarks.SingleOrDefault(x => x.Id == MarkId);

            if (english != null)
            {
                context.English.Remove(english);
            }
            else if (philosophy != null)
            {
                context.Philosophy.Remove(philosophy);
            }
            else if (mathematics != null)
            {
                context.Mathematics.Remove(mathematics);
            }

            await context.SaveChangesAsync();
        }
        public async Task UpdateMark(decimal mark, string subject)
        {
            if (subject == "Philosophy")
            {
                var _mark = context.Philosophy.Where(x => x.Mark == mark).FirstOrDefault();
                _mark.Mark = mark;
                _mark.Day = DateTime.UtcNow;
                if (_mark != null)
                {
                    _mark.Mark = mark;
                    _mark.Day = DateTime.UtcNow;
                    context.Philosophy.Update(_mark);
                }
            }
            else if (subject == "Mathematics")
            {
                var _mark = context.Mathematics.Where(x => x.Mark == mark).FirstOrDefault();

                if (_mark != null)
                {
                    _mark.Mark = mark;
                    _mark.Day = DateTime.UtcNow;
                    context.Mathematics.Update(_mark);
                }
            }
            else if (subject == "English")
            {
                var _mark = context.English.Where(x => x.Mark == mark).FirstOrDefault();
                _mark.Mark = mark;
                _mark.Day = DateTime.UtcNow;
                if (_mark != null)
                {
                    _mark.Mark = mark;
                    _mark.Day = DateTime.UtcNow;
                    context.English.Update(_mark);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}