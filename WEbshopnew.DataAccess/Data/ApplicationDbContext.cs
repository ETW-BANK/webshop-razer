using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEbshopnew.Models;


namespace WEbshopnew.DataAccess.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Catagory> Catagories { get; set; }

        public DbSet<Products> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

