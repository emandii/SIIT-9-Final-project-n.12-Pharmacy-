using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pharmacy_first_setup
{
    internal class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public Product(string name, int quantity, decimal price, DateTime? expirationDate)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            ExpirationDate = expirationDate;
        }
    }
}
