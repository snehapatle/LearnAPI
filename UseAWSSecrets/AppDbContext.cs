using Microsoft.EntityFrameworkCore;
using UseAWSSecrets.Models;

namespace UseAWSSecrets
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
    }
}
