using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystemFunctions.Models
{
    public class Inventory
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitPrice { get; set; }
        public string Remarks { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2, {3}, {4}",
                this.ProductId, this.ProductTitle, this.UnitsInStock, this.UnitPrice, this.Remarks);
        }
    }
}
