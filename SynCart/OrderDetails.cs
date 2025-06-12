using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynCart
{
    public class OrderDetails
    {
        /*
        •	OrderID (Auto Increment – OID1001) 
•	CustomerID 
•	ProductID 
•	TotalPrice  
•	PurchaseDate 
•	Quantity 
•	OrderStatus – (Enum- Default, Ordered, Cancelled) 
*/

        private static int s_orderID = 1000;
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string ProductID { get; set; }
        public double TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Order { get; set; }

        public OrderDetails(string customerID,string productID, double totalPrice, DateTime purchaseDate, int quantity, OrderStatus order)
        {
            OrderID = $"OID{++s_orderID}";
            CustomerID=customerID;
            ProductID = productID;
            TotalPrice = totalPrice;
            PurchaseDate = purchaseDate;
            Quantity = quantity;
            Order = order;
        }
    }
}