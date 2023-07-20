using E2.Models;
using Microsoft.EntityFrameworkCore;
namespace E2.Data
{
    public class E2DbContext : DbContext 
    {
        public E2DbContext(DbContextOptions options ) : base(options)
        {

        }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<LocationModel> Location { get; set; }
    }
}
