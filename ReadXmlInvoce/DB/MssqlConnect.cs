using Microsoft.EntityFrameworkCore;
using ReadXmlInvoce.Models;

namespace ReadXmlInvoce.DB
{
    public class MssqlConnect : DbContext
    {
        public MssqlConnect(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> products {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Invoice>().HasKey(a => a.numDock);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.product)
                .WithOne(a => a.Invoice)
                .HasForeignKey(a => a.invoceNumber)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
