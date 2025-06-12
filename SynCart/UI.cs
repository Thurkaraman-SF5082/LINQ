using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynCart
{
    public class UI
    {
        Operations operation = new Operations();

        public void MainMenu()
        {
            operation.DefaultData();

            bool flag = true;
            do
            {
                System.Console.WriteLine("Enter option:\n1. Customer Registration\n2. Login\n3. Exit");
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        {
                            //customer registration
                            Registration();
                            break;
                        }
                    case 2:
                        {
                            //login
                            Login();
                            break;
                        }
                    case 3:
                        {
                            //exit
                            flag = false;
                            break;
                        }
                }
            } while (flag);
        }

        public void Registration()
        {
            System.Console.WriteLine("Enter your Name:");
            string customerName = Console.ReadLine();
            System.Console.WriteLine("Enter your city:");
            string city = Console.ReadLine();
            System.Console.WriteLine("Enter your mobile number:");
            string mobileNumber = Console.ReadLine();
            System.Console.WriteLine("Enter your wallet balance:");
            double walletBalance = double.Parse(Console.ReadLine());
            System.Console.WriteLine("Enter your email ID:");
            string emailID = Console.ReadLine();

            CustomerDetails customer = new CustomerDetails(customerName, city, mobileNumber, walletBalance, emailID);

            System.Console.WriteLine($"Registration was successful. Your customer ID is {operation.AddUser(customer)}");
        }

        public void Login()
        {
            System.Console.WriteLine("Enter your customer ID:");
            string customerID = Console.ReadLine().ToUpper();
            //check customer id
            if (operation.CheckCustomer(customerID))
            {
                //submenu
                System.Console.WriteLine("Login successful");
                SubMenu();

            }
            else
            {
                System.Console.WriteLine("Invalid customer ID");
            }

        }

        public void SubMenu()
        {
            bool flag = true;

            do
            {
                System.Console.WriteLine("Enter your selection:\n1. Purchase\n2. OrderHistory\n3. CancelOrder\n4. WalletBalance\n5. WalletRecharge\n6. Exit SubMenu");
                int selection = int.Parse(Console.ReadLine());
                switch (selection)
                {
                    case 1:
                        {
                            //purchase
                            Purchase();
                            break;
                        }
                    case 2:
                        {
                            //orderhistory
                            ShowOrderHistory();
                            break;
                        }
                    case 3:
                        {
                            //cancelorder
                            CancelOrder();
                            break;
                        }
                    case 4:
                        {
                            //wallet balance
                            WalletBalance();
                            break;
                        }
                    case 5:
                        {
                            //wallet recharge
                            WalletRecharge();
                            break;
                        }
                    case 6:
                        {
                            System.Console.WriteLine("Exit SubMenu");
                            flag = false;
                            break;
                        }
                }
            } while (flag);
        }


        public void ShowOrderHistory()
        {
            //check for order count
            if (operation.OrderHistory().Count > 0)
            {
                operation.OrderHistory().ForEach(order=> System.Console.WriteLine($"{order.OrderID} |{order.CustomerID} |{order.ProductID} |{order.TotalPrice} |{order.PurchaseDate} |{order.Quantity} |{order.Order}"));
                // foreach (OrderDetails order in operation.OrderHistory())
                // {
                //     System.Console.WriteLine($"{order.OrderID} |{order.CustomerID} |{order.ProductID} |{order.TotalPrice} |{order.PurchaseDate} |{order.Quantity} |{order.Order}");
                // }
            }
            else
            {
                System.Console.WriteLine("No order history was found");
            }
        }

        public void WalletBalance()
        {
            //1.	Show the wallet balance of the current logged-in customer.
            System.Console.WriteLine($"Your wallet balance = {operation.WalletBalance()}");
        }

        public void WalletRecharge()
        {
            System.Console.WriteLine("Do you wish to continue recharge:");
            string wish = Console.ReadLine().ToUpper();
            if (wish == "YES")
            {
                System.Console.WriteLine("Enter amount you want to recharge:");
                double amount = double.Parse(Console.ReadLine());
                operation.WalletRecharge(amount);
                System.Console.WriteLine($"Wallet Recharge was successful. Your Wallet balance is {operation.WalletBalance()}");
            }
        }



        public void Purchase()
        {
            //shoe product details by traversing product list
            // foreach (ProductDetails product in operation.GetProductDetails())
            // {
            //     System.Console.WriteLine($"{product.ProductID} |{product.ProductName} |{product.Price} |{product.Stock} |{product.ShippingDuration}");
            // }
            operation.GetProductDetails().ForEach(product=> System.Console.WriteLine($"{product.ProductID} |{product.ProductName} |{product.Price} |{product.Stock} |{product.ShippingDuration}"));
            //ask and get product ID
            System.Console.WriteLine("Enter the product ID:");
            string productID = Console.ReadLine().ToUpper();
            System.Console.WriteLine("Enter the count:");
            int count = int.Parse(Console.ReadLine());

            switch (operation.Purchase(productID, count))
            {
                case 0:
                    {
                        System.Console.WriteLine("Invalid product ID");
                        break;
                    }
                case 1:
                    {
                        System.Console.WriteLine($"Required product count not available. Available count is {operation.StockAvailability(productID)}");
                        break;
                    }
                case 2:
                    {
                        System.Console.WriteLine($"Insufficient balance. Please recharge and continue");
                        break;
                    }
                case 3:
                    {
                        System.Console.WriteLine($"Order placed successfully. Your order ID is , Delivery date is {DateTime.Now}");
                        break;
                    }
            }
        }

        public void CancelOrder()
        {
            //show all orders by traversing order list by checking customer id - else return 0 ,“No Orders Found to Cancel”.
            if (operation.GetOrderDetails().Count != 0)
            {
                operation.GetOrderDetails().ForEach(order=> System.Console.WriteLine($"{order.OrderID} |{order.CustomerID} |{order.ProductID} |{order.TotalPrice} |{order.PurchaseDate} |{order.Quantity} |{order.Order}"));
                // foreach (OrderDetails order in operation.GetOrderDetails())
                // {
                //     System.Console.WriteLine($"{order.OrderID} |{order.CustomerID} |{order.ProductID} |{order.TotalPrice} |{order.PurchaseDate} |{order.Quantity} |{order.Order}");
                // }

                //ask customer for order id to cancel
                System.Console.WriteLine("Enter order ID to cancel:");
                string orderID = Console.ReadLine().ToUpper();

                switch (operation.CancelOrder(orderID))
                {
                    case 1:
                        {
                            System.Console.WriteLine("“Invalid OrderID to Cancel Order”");
                            break;
                        }
                    case 2:
                        {
                            System.Console.WriteLine("“Order Cancelled Successfully”");
                            break;
                        }
                }
            }
            else
            {
                System.Console.WriteLine("“No Orders Found to Cancel”");
            }


            //check presence of order id in order list and order status (ordered) - return 1 , “Invalid OrderID to Cancel Order”.
            //increase stock quantity by count number that was cancelled
            //add totalprice of cancelled order to wallet
            //change order status cancelled
            //show “Order Cancelled Successfully”.
        }
    }
}