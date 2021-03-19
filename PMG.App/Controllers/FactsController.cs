namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PMG.App.Models.Facts;
    using PMG.Data;
    using PMG.Services.Fact;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FactsController : Controller
    {
        private readonly PMGDbContext context;
        private readonly IFactService factSerice;
        private Random random;

        public FactsController(PMGDbContext context, IFactService factSerice)
        {
            this.context = context;
            this.factSerice = factSerice;
            random = new Random();
        }

        public IActionResult Facts()
        {
            var facts = new Dictionary<string, IFacts>();

            var philosophy = factSerice.GetPhilosophyFacts()
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
            var philosophy = factSerice
                .GetPhilosophyFacts()
                .GetAwaiter()
                .GetResult()
               .Select(f => new PhilosophyBindingModel
               {
                   Content = f.Content,
                   Author = f.Author
               })
               .ToList();

            var philosophyIndex = random.Next(1, philosophy.Count);

            return philosophy[philosophyIndex];
        }
    }
}