namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PMG.App.Models;
    using PMG.App.Models.Home;
    using PMG.Data;
    using PMG.Services.Message;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IMessageService messageService;

        public HomeController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            var tables = new Dictionary<string, IEnumerable<IBindingModel>>();

            IEnumerable<MessagesBindingModel> messages = messageService
                .GetAll()
                .GetAwaiter()
                .GetResult()
                .Select(x => new MessagesBindingModel
                {
                    Id = x.Id,
                    Content = x.Content.TrimStart('"').TrimEnd('"'),
                    PublishedOn = x.PublishedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                })
             .ToList();

            tables["Messages"] = messages;
            tables["Table"] = new List<Timetable>();

            return View(tables);

        }

        public IActionResult Privacy()
        {
            return View();
        }        
        public IActionResult Facts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}