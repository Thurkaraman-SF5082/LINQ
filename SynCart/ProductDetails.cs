using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynCart
{
    public class ProductDetails
    {
        /*
        •	ProductID (Auto Increment – PID2001) 
•	ProductName 
•	Stock 
•	Price  
•	ShippingDuration 
*/
        private static int s_productID = 2000;
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public double ShippingDuration { get; set; }

        public ProductDetails(string productName, int stock, double price, double shippingDuration)
        {
            ProductID = $"PID{++s_productID}";
            ProductName = productName;
            Stock = stock;
            Price = price;
            ShippingDuration = shippingDuration;
        }
    }
}