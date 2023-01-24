using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using No5.Models;

namespace No5.Context
{
    public class AnyarDbContext:IdentityDbContext<AppUser>
    {
        public AnyarDbContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Icon> Icons { get; set; }  
    }
}
