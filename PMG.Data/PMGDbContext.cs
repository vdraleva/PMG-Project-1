namespace PMG.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using Domain;
    using PMG.Domain.Home;
    using PMG.Domain.SchoolSubjects;

    public class PMGDbContext : IdentityDbContext<PMGUser, IdentityRole, string>
    {
        public PMGDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Fact> PhilosophyFacts { get; set; }
        public DbSet<Philosophy> Philosophy { get; set; }
        public DbSet<English> English { get; set; }
        public DbSet<Mathematics> Mathematics { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Messages> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}