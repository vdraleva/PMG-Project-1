namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PMG.App.Models.Teacher;
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
        private string studentUsername;

        public TeacherController(IMarkService markService,
            IUserService userService,
            IBookmarkService bookmarkService)
        {
            this.markService = markService;
            this.userService = userService;
            this.bookmarkService = bookmarkService;
        }
        [HttpGet("Teacher/Marks")]
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
        public IActionResult Marks(MarkBindingModel model)
        {
            var user = userService
                .GetUserWithBookmarkByUsrename(model.StudentUsername)
                .GetAwaiter()
                .GetResult();

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

        [HttpPost("Teacher/Bookmark")]
        public IActionResult Bookmark(MarkBindingModel model)
        {
            var marks = new Dictionary<string, string>();
            var bookmark = bookmarkService.GetBookmarkByUsername(model.StudentUsername).GetAwaiter().GetResult();

            studentUsername = model.StudentUsername;

            if (bookmark == null)
            {
                return this.View();
            }
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

        [HttpGet("Teacher/Delete")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Delete(UpdateDeleteBindingModel model)
        {
            var bookmark = bookmarkService.GetBookmarkByUsername(studentUsername).GetAwaiter().GetResult();
            bookmarkService.Delete(bookmark.Id, model.Mark, model.Subject);

            return Redirect("/Home/Index");
        }

        [HttpGet("Teacher/DeleteOrUdpate/{username}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteOrUpdate(string username)
        {
            ViewData["Student"] = username;
            return this.View();
        }
    }
}