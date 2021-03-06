namespace PMG.Data.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System;
    using System.Threading.Tasks;

    using PMG.Domain;

    public class SeedAdmin : ISeeder
    {
        private readonly UserManager<PMGUser> _userManager;
        private readonly PMGDbContext context;

        public SeedAdmin(UserManager<PMGUser> userManager, PMGDbContext context)
        {
            this._userManager = userManager;
            this.context = context;
        }
        public async Task SeedAsync()
        {
            //var user = context.Users.SingleOrDefault(x => x.Id == )
            //if ()
            //{

            //}
            var admin = new PMGUser
            {
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@gmail.com",
                DateOfBirth = DateTime.UtcNow,
                UCN = "1111111111",
            };

            await this._userManager.CreateAsync(admin, "Admin");
            await this.context.SaveChangesAsync();
        }
    }
}