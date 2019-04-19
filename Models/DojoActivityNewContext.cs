using Microsoft.EntityFrameworkCore;
 
namespace DojoActivityNew.Models
{
    public class DojoActivityNewContext : DbContext
    {
        public DojoActivityNewContext(DbContextOptions<DojoActivityNewContext> options) : base(options) { }
        public DbSet<User> users {get;set;}
        public DbSet<Activities> activities {get;set;}

        public DbSet<Guest> guests {get;set;}
    }
}
