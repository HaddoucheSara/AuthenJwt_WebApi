using Authentification.JWT.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentification.JWT.DAL.Data.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public  DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=OUJDKZJN53;Database=AuthDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
