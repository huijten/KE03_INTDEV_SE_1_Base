using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class MatrixIncDbContext : DbContext
    {
        #region Initialize DbContext
        public MatrixIncDbContext(DbContextOptions<MatrixIncDbContext> options) : base(options)
        {
        }
        #endregion
        
        #region Database sets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Part> Parts { get; set; }
        #endregion
        
        #region Model configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired(); // An Order must always have a Customer
            
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Parts)
                .WithMany();
            
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}