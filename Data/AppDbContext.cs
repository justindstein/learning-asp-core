using learning_asp_core.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace learning_asp_core.Data
{
    public class AppDbContext : DbContext
    {
        public IConfiguration _config { get; set; }

        public AppDbContext(IConfiguration config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        }

        public DbSet<Workflow> Workflows { get; set; }
    }
}
