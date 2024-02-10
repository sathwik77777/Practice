using Microsoft.EntityFrameworkCore;
using Practice_test1.Entities;

namespace Practice_test1.Database
{
    public class Mycontext : DbContext
    {
        private IConfiguration config;
        public Mycontext(IConfiguration cfg)
        {
            config = cfg;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config["ConnectionString"]);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
