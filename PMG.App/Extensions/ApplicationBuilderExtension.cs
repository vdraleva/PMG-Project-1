namespace PMG.App.Extensions
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Data;
    using Data.Seeders;

    public static class ApplicationBuilderExtension
    {
        public static void UseDatabaseSeeding(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<PMGDbContext>())
                {

                    context.Database.Migrate();

                    var seeders = Assembly.GetAssembly(typeof(PMGDbContext))
                       .GetTypes()
                       .Where(type => typeof(ISeeder).IsAssignableFrom(type))
                       .Where(type => type.IsClass)
                       .Select(type => (ISeeder)scope.ServiceProvider.GetRequiredService(type))
                       .ToList();

                    foreach (var seeder in seeders)
                    {
                        seeder.SeedAsync().GetAwaiter().GetResult();
                    }
                }
            }
        }
    }
}