using Microsoft.EntityFrameworkCore;
using UseAzureKeyVault.Models;

namespace UseAzureKeyVault
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
    }
}
