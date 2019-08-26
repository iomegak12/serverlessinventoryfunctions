using InventorySystemFunctions.Entities.Interfaces;
using InventorySystemFunctions.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystemFunctions.Entities.Implementations
{
    public class InventoryContext : DbContext, IInventoryContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Inventory>(
                new InventoryEntityTypeConfiguration());
        }
    }
}
