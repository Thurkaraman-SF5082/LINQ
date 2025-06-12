using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynCart
{
    public class Operations
    {
        List<ProductDetails> products = new List<ProductDetails>();
        List<CustomerDetails> customers = new List<CustomerDetails>();
        List<OrderDetails> orders = new List<OrderDetails>();

        CustomerDetails currentCustomer;

        public void DefaultData()
        {
            //create customer object
            CustomerDetails customer1 = new CustomerDetails("Ravi", "Chennai ", "9885858588", 50000, "ravi@mail.com");
            CustomerDetails customer2 = new CustomerDetails("Baskaran", "Chennai", "9888475757", 60000, "baskaran@mail.com");

            customers.AddRange(new List<CustomerDetails>() { customer1, customer2 });

            //create order object
            OrderDetails order1 = new OrderDetails("CID3001", "PID2001", 20000, DateTime.Now, 2, OrderStatus.Ordered);
            OrderDetails order2 = new OrderDetails("CID3002", "PID2003", 40000, DateTime.Now, 2, OrderStatus.Ordered);

            orders.AddRange(new List<OrderDetails>() { order1, order2 });

            //create product object
            ProductDetails product1 = new ProductDetails("Mobile (Samsung)", 10, 10000, 3);
            ProductDetails product2 = new ProductDetails("Tablet (Lenovo)", 5, 15000, 2);
            ProductDetails product3 = new ProductDetails("Camara (Sony)", 3, 20000, 4);
            ProductDetails product4 = new ProductDetails("iPhone", 5, 50000, 6);
            ProductDetails product5 = new ProductDetails("Laptop (Lenovo I3)", 3, 40000, 3);
            ProductDetails product6 = new ProductDetails("HeadPhone (Boat) ", 5, 1000, 2);
            ProductDetails product7 = new ProductDetails("Speakers (Boat)", 4, 500, 2);

            products.AddRange(new List<ProductDetails>() { product1, product2, product3, product4, product5, product6, product7 });
        }

        public string AddUser(CustomerDetails customer)
        {
            customers.Add(customer);
            return customer.CustomerID;
        }

        public bool CheckCustomer(string customerID)
        {
            // foreach (CustomerDetails customer in customers)
            // {
            CustomerDetails customer =(from customer1 in customers where customer1.CustomerID == customerID select customer1).FirstOrDefault();
            if (customer!=null)
                {
                    currentCustomer = customer;
                    return true;
                }
            //}
            
            return false;

        }

        public List<OrderDetails> OrderHistory()
        {
            //traverse through orderdetail list
            // List<OrderDetails> orderHistory = new List<OrderDetails>();
            // foreach (OrderDetails order in orders)
            // {
            //     if (currentCustomer.CustomerID == order.CustomerID)
            //     {
            //         orderHistory.Add(order);
            //     }
            // }
            List<OrderDetails> orderHistory1=(from order in orders where order.CustomerID==currentCustomer.CustomerID select order).ToList();
            return orderHistory1;
        }

        public double WalletBalance()
        {
            return currentCustomer.WalletBalance;
        }

        public void WalletRecharge(double amount)
        {
            currentCustomer.WalletRecharge(amount);
        }

        public List<ProductDetails> GetProductDetails()
        {
            return products;
        }



        public int StockAvailability(string productID)
        {
            // foreach (ProductDetails product in products)
            // {
                int stock=(from product in products where product.ProductID==productID select product.Stock).FirstOrDefault();
                //if (productID == product.ProductID)
                if(stock!=null)
                {
                    return stock;
                    //return product.Stock;
                }
            //}
            return 1;
        }
        //purchase

        public int Purchase(string productID, int count)
        {
            //traverse and check if product is there - else return 0
            foreach (ProductDetails product in products)
            {
                if (productID == product.ProductID)
                {
                    //check the product cnt - else return 1
                    if (count <= product.Stock)
                    {
                        //cal tot price of product
                        double totalPrice = (count * product.Price) + 50;
                        //check user has enough balance for purchase- return 2
                        if (WalletBalance() >= totalPrice)
                        {
                            //reduce product cnt
                            product.Stock -= count;
                            //reduce user's bal
                            currentCustomer.DeductBalance(totalPrice);
                            //create instance for this order and add it to list - return3
                            OrderDetails order = new OrderDetails(currentCustomer.CustomerID, product.ProductID, totalPrice, DateTime.Now, count, OrderStatus.Ordered);
                            orders.Add(order);
                            return 3;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 0;

            //if not present print invalid id
            //ask customer for cnt they wish to purchase
            //check for product cnt if cnt was less show required cnt not available and show cnt available
            //if available cal tot price = (cnt * product price) + delivery charge(50)
            //check customer wallet for enough balance by comparing
            //if balance was low show insufficient bal,pls recharge and continue
            //if bal was enough deduct tot price from wallet of current customer & deduct cnt from stock availability
            //create order instance and make order status as ordered and add order to order list
            //cal delivery date comparing purchase date and shipping duration
            //show order placed successfully. ur order is ____ ur delivery date____
        }

        /*public string DeliveryDate()
        {
            foreach(OrderDetails order in orders)
            {
                if(currentCustomer.CustomerID==order.CustomerID)
                {
                    foreach(ProductDetails product in products)
                    {
                        if(order.ProductID==product.ProductID)
                        {
                            return (order.PurchaseDate.AddDays(product.ShippingDuration)).ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
        }*/

        public List<OrderDetails> GetOrderDetails()
        {
            // List<OrderDetails> orderbycustomer = new List<OrderDetails>();
            // foreach (OrderDetails order in orders)
            // {
            //     if (order.CustomerID == currentCustomer.CustomerID)
            //     {
            //         orderbycustomer.Add(order);
            //     }
            // }
            List<OrderDetails> orderdet=(from order in orders where order.CustomerID==currentCustomer.CustomerID select order).ToList();
            return orderdet;
            //return orderbycustomer;
        }

        public int CancelOrder(string orderID)
        {
            //show all orders by traversing order list by checking customer id - else return 0 ,“No Orders Found to Cancel”.
            foreach (OrderDetails order in orders)
            {
                //check presence of order id in order list and order status (ordered) - return 1 , “Invalid OrderID to Cancel Order”.
                if (order.OrderID == orderID && OrderStatus.Ordered == order.Order)
                {
                    foreach (ProductDetails product in products)
                    {
                        if (order.ProductID == product.ProductID)
                        {
                            //increase stock quantity by count number that was cancelled
                            product.Stock += ((int)order.TotalPrice - 50) / (int)product.Price;
                            //add totalprice of cancelled order to wallet
                            WalletRecharge(order.TotalPrice);
                            //change order status cancelled
                            order.Order = OrderStatus.Cancelled;
                            return 2;
                        }
                    }
                }
            }
            
            return 1;
        }
        //ask customer for order id to cancel


        //show “Order Cancelled Successfully”.
    }
}