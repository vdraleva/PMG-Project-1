namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PMG.App.Models.Facts;
    using PMG.Services.Fact;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FactsController : Controller
    {
        private readonly IFactService factService;
        private Random random;

        public FactsController(IFactService factService)
        {
            this.factService = factService;
            random = new Random();
        }

        public IActionResult Facts()
        {
            var facts = new Dictionary<string, IFacts>();

            var philosophy = factService.GetPhilosophyFacts()
                .GetAwaiter()
                .GetResult()
                .Select(f => new PhilosophyBindingModel
                {
                    Content = f.Content,
                    Author = f.Author
                })
                .ToList();

            IFacts philosophyFact = philosophy[1];

            facts["Philosophy"] = philosophyFact;

            return View(facts);
        }
        public IFacts GetPhilosophyFact()
        {
            var philosophy = factService
                .GetPhilosophyFacts()
                .GetAwaiter()
                .GetResult()
                .Select(f => new PhilosophyBindingModel
                {
                    Content = f.Content,
                    Author = f.Author
                })
               .ToList();

            var philosophyIndex = random.Next(1, philosophy.Count());

            return philosophy[philosophyIndex];
        }
    }
}