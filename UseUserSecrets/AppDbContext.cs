using Microsoft.EntityFrameworkCore;
using UseUserSecrets.Models;

namespace UseUserSecrets
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
    }
}
