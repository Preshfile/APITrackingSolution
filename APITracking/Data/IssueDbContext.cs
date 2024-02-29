using APITracking.Models;
using Microsoft.EntityFrameworkCore;

namespace APITracking.Data
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Issue> issues { get; set; }
    }
}
