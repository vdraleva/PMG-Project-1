namespace PMG.Data.Seeders
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    public class RoleSeeder : ISeeder
    {
        private readonly PMGDbContext context;

        public RoleSeeder(PMGDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
                await context.Roles.AddAsync(new IdentityRole { Name = "Teacher", NormalizedName = "TEACHER" });
                await context.Roles.AddAsync(new IdentityRole { Name = "Student", NormalizedName = "STUDENT" });

                await context.SaveChangesAsync();
            }
        }
    }
}