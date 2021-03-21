namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PMG.App.Models.Teacher;
    using PMG.Domain.SchoolSubjects;
    using PMG.Services.Bookmark;
    using PMG.Services.Marks;
    using PMG.Services.User;
    using System.Collections.Generic;
    using System.Linq;

    public class TeacherController : Controller
    {
        private readonly IMarkService markService;
        private readonly IUserService userService;
        private readonly IBookmarkService bookmarkService;
        private static string studentUsername;

        public TeacherController(IMarkService markService,
            IUserService userService,
            IBookmarkService bookmarkService)
        {
            this.markService = markService;
            this.userService = userService;
            this.bookmarkService = bookmarkService;
        }
        [HttpGet("Teacher/Marks")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Marks()
        {
            ViewData["Students"] = userService.GetAllUsers()
                .GetAwaiter()
                .GetResult()
                .Where(x => x.Role == "Student")
                .Select(x => x.UserName).ToList();

            return this.View();
        }
        [HttpPost("Teacher/Marks")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Marks(MarkBindingModel model)
        {
            var user = userService
                .GetUserWithBookmarkByUsrename(model.StudentUsername)
                .GetAwaiter()
                .GetResult();

            studentUsername = model.StudentUsername;
            ViewData["StudentUsername"] = model.StudentUsername;

            if (model.Subject == "Philosophy")
            {
                markService
                    .CreateMarkPhilosophy(model.Mark, user)
                    .GetAwaiter()
                    .GetResult();
            }

            else if (model.Subject == "Matematics")
            {
                markService
                    .CreateMarkMathematics(model.Mark, user)
                    .GetAwaiter()
                    .GetResult();
            }

            else if (model.Subject == "English")
            {
                markService
                    .CreateMarkEnglish(model.Mark, user)
                    .GetAwaiter()
                    .GetResult();
            }

            return this.Redirect("/Home/Index");

        }
        [HttpGet("Teacher/DeleteOrUpdate")]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteOrUpdate()
        {
            return this.View();
        }

        [HttpPost("Teacher/Bookmark")]
        public IActionResult Bookmark(MarkBindingModel model)
        {
            var marks = new Dictionary<string, IEnumerable<ISubject>>();
            var bookmark = bookmarkService.GetBookmarkByUsername(model.StudentUsername).GetAwaiter().GetResult();

            studentUsername = model.StudentUsername;
            ViewData["StudentUsername"] = studentUsername;

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
        [HttpGet("/Teacher/Delete/{MarkId}/{BookmarkId}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(string MarkId, string BookmarkId)
        {
            markService.DeleteMark(MarkId, BookmarkId);
            return this.Redirect("/Home/Index");
        }
    }
}