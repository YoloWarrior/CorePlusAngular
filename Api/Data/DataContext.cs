using Api.Models;
using CorePlusAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace CorePlusAngular.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
            Database.EnsureCreated();
        }

        
        public DbSet<User> Users { get; set; }
        public DbSet<Value> Values { get; set; }
    }
}
