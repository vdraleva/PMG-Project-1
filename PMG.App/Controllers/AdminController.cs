namespace PMG.App.Controllers
{
    using PMG.Domain.Home;
    using Microsoft.AspNetCore.Mvc;
    using Models.Admin;
    using System;
    using Microsoft.AspNetCore.Authorization;
    using PMG.Services.Message;

    public class AdminController : Controller
    {
        private readonly IMessageService messageService;

        public AdminController(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        [HttpGet("Admin/Create")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("Admin/Create")]
        public IActionResult Create(MessageCreateBindingModel model)
        {
            Messages message = new Messages
            {
                Content = model.Content,
                PublishedOn = DateTime.UtcNow
            };

            messageService.Create(message).GetAwaiter().GetResult();

            return this.Redirect("/Home/Index");
        }

        [HttpGet("Home/Delete/{Id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            messageService.RemoveById(id);
            return this.Redirect("/Home/Index");
        }
    }
}