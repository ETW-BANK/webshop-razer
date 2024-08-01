using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEbshopnew.Models;


namespace WEbshopnew.DataAccess.Data
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Catagory> Catagories { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

