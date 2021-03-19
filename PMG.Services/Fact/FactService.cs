namespace PMG.Services.Fact
{
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using PMG.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FactService : IFactService
    {
        private readonly PMGDbContext context;

        public FactService(PMGDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Fact>> GetPhilosophyFacts()
        {
            var facts = await context.PhilosophyFacts.ToListAsync();
            return facts;
        }
    }
}