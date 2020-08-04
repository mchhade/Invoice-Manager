using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.Models
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<Invoice>().ToTable("invoice");
            modelBuilder.Entity<InvoiceDetail>().ToTable("detail");
        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<InvoiceDetail> details { get; set; }
    }
}
