namespace PMG.Tests.Factory
{
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using System;
    public class PMGDbContextInMemoryFactory
    {
        public static PMGDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<PMGDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            return new PMGDbContext(options);
        }
    }
}