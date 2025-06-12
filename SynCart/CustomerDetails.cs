using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynCart
{
    public class CustomerDetails
    {
        /*•	CustomerID (Auto Increment -CID3001) 
•	CustomerName 
•	City 
•	MobileNumber 
•	WalletBalance 
•	EmailID 
*/
        public double _balance;
        private static int s_customerID = 3000;
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }

        public CustomerDetails(string customerName, string city, string mobileNumber, double walletBalance, string emailID)
        {
            CustomerID = $"CID{++s_customerID}";
            CustomerName = customerName;
            City = city;
            MobileNumber = mobileNumber;
            WalletBalance = walletBalance;
            EmailID = emailID;
        }

        public double WalletBalance
        {
            get {return _balance;
            } set {_balance=value;} 
        }

        public void WalletRecharge(double amount)
        {
            WalletBalance += amount; 
        }

        public void DeductBalance(double amount)
        {
            WalletBalance -= amount;
        }
    }
}