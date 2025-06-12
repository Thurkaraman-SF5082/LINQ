using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDTH
{
    public class UserRegistration
    {
        /*
        •	_walletBalance (Private field)
        Properties: 
        •	UserID (Auto Incremented which is start from UID1001) 
        •	UserName 
        •	MobileNumber 
        •	EmailID 
        •	WalletBalance 
            Methods:
        •	WalletRecharge
        •	DeductBalance

        */

        private static int s_userID = 1000;
        public double _walletBalance;
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public double WalletBalance
        {
            get { return _walletBalance; }
            set { _walletBalance = value; }
        }

        public UserRegistration(string userName, string mobileNumber, string emailID, double walletBalance)
        {
            UserID = $"UID{++s_userID}";
            UserName = userName;
            MobileNumber = mobileNumber;
            EmailID = emailID;
            WalletBalance = walletBalance;
        }

        public void WalletRecharge(double amount)
        {
            _walletBalance += amount;
        }

        public void DeductBalance(double amount)
        {
            _walletBalance -= amount;
        }

    }
}