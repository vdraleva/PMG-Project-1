namespace PMG.Services.Fact
{
    using PMG.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFactService
    {
        Task<IEnumerable<Fact>> GetPhilosophyFacts();
    }
}