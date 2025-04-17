using Authentification.JWT.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentification.JWT.DAL.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=tcp:mymicroappserver.database.windows.net,1433;Initial Catalog=MyMicroAppDb;Persist Security Info=False;User ID=sara;Password=azureAzure@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
