using Microsoft.EntityFrameworkCore;
using XUnitAssessment.Models;

namespace XUnitAssessment.Data
{
    public class XunitDbContext:DbContext
    {
        public XunitDbContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Appuser> XunitAppuser { get; set; }

        public DbSet<Usertype> XunitUsertype { get; set; }
    }
}
