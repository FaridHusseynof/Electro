using Electro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Electro.Data
{
    public class ElectroDbContext : IdentityDbContext<AppUser>
    {
        public ElectroDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> products { get; set; }
    }
}
