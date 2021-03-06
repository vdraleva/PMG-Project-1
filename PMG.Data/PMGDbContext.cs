namespace PMG.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using Domain;

    public class PMGDbContext : IdentityDbContext<PMGUser, IdentityRole, string>
    {
        public PMGDbContext(DbContextOptions options)
            : base(options)
        {

        }
        
    }
}