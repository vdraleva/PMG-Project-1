namespace PMG.Services.Marks
{
    using PMG.Data;
    using PMG.Domain;
    using PMG.Domain.SchoolSubjects;
    using System;
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
            English english= new English
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
    }
}