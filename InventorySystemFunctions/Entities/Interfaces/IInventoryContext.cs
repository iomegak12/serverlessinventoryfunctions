using InventorySystemFunctions.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystemFunctions.Entities.Interfaces
{
    public interface IInventoryContext : IDisposable
    {
        DbSet<Inventory> Inventories { get; set; }
    }
}
