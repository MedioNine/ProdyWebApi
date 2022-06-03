using Microsoft.EntityFrameworkCore;
using Prody.DAL.Entities;

namespace Prody.DAL
{
    public class ProdyDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<ShoppingList> ShoppingList { get; set; }

        public ProdyDbContext(DbContextOptions<ProdyDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
