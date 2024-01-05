using Microsoft.EntityFrameworkCore;
using ReadXmlInvoce.Models;

namespace ReadXmlInvoce.DB
{
    public class MssqlConnect : DbContext
    {
        public MssqlConnect(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Invoce> Invoces { get; set; }
        public DbSet<Product> products {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Invoce>().HasKey(a => a.numDock);

            modelBuilder.Entity<Invoce>()
                .HasOne(a => a.product)
                .WithOne(a => a.Invoce)
                .HasForeignKey<Product>(a => a.invoceNumber)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
