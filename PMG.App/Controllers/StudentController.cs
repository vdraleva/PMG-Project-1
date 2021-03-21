namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    using Services.Bookmark;
    using PMG.Domain.SchoolSubjects;

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
            var marks = new Dictionary<string, IEnumerable<ISubject>>();
            var bookmark = bookmarkService.GetBookmarkByUsername(this.User.Identity.Name).GetAwaiter().GetResult();

            ViewData["StudentUsername"] = this.User.Identity.Name;

            if (bookmark == null)
            {
                return this.View();
            }

            var philosophy = bookmark.PhilosophyMarks.ToList();

            var english = bookmark.EnglishMarks.ToList();

            var mathematics = bookmark.MathematicsMarks.ToList();

            marks["Philosophy"] = philosophy;
            marks["Mathematics"] = mathematics;
            marks["English"] = english;

            return this.View(marks);
        }
    }
}