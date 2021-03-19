namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    using Services.Bookmark;

    public class StudentController : Controller
    {
        private readonly IBookmarkService bookmarkService;

        public StudentController(IBookmarkService bookmarkService)
        {
            this.bookmarkService = bookmarkService;
        }

        [HttpGet]
        public IActionResult Bookmark()
        {
            var marks = new Dictionary<string, string>();
            var bookmark = bookmarkService.GetBookmarkByUsername(this.User.Identity.Name).GetAwaiter().GetResult();
            ViewData["StudentUsername"] = this.User.Identity.Name;

            var philosophy = bookmark.PhilosophyMarks.Select(x => x.Mark.ToString()).ToList();
            var philosophyMarks = string.Join(", ", philosophy);

            var english = bookmark.EnglishMarks.Select(x => x.Mark.ToString()).ToList();
            var englishMarks = string.Join(", ", english);

            var mathematics = bookmark.MathematicsMarks.Select(x => x.Mark.ToString()).ToList();
            var mathematicsMarks = string.Join(", ", mathematics);

            marks["Philosophy"] = philosophyMarks;
            marks["Mathematics"] = mathematicsMarks;
            marks["English"] = englishMarks;

            return this.View(marks);
        }
    }
}