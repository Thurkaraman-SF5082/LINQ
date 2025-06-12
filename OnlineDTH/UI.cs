using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDTH
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
                System.Console.WriteLine("1. User Registration\n2. User Login\n3. Exit\nEnter option:");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        {
                            //user registration
                            Registration();
                            break;
                        }
                    case 2:
                        {
                            //user login
                            UserLogin();
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
            //get name, mobile number, email id, balance from user
            System.Console.WriteLine("Enter your name:");
            string userName = Console.ReadLine();
            System.Console.WriteLine("Enter your mobile number:");
            string mobileNumber = Console.ReadLine();
            System.Console.WriteLine("Enter your email ID:");
            string emailID = Console.ReadLine();
            System.Console.WriteLine("Enter your wallet balance:");
            double walletBalance = double.Parse(Console.ReadLine());

            UserRegistration user = new UserRegistration(userName, mobileNumber, emailID, walletBalance);

            System.Console.WriteLine($"Registration was successful. Your user ID is {operation.AddUser(user)}");
        }

        public void UserLogin()
        {
            //get user id from user
            System.Console.WriteLine("Enter your user ID:");
            string userID = Console.ReadLine().ToUpper();
            //traverse user list for entered user id
            if (operation.CheckUser(userID))
            {
                System.Console.WriteLine("Login successful");
                SubMenu();

            }
            else
            {
                System.Console.WriteLine("Invalid user ID");
            }
        }

        public void SubMenu()
        {
            bool flag = true;
            do
            {
                System.Console.WriteLine("1. Current Pack\n2. Pack Recharge\n3. Wallet Recharge\n4. View Pack Recharge History\n5. Show Wallet balance\n6. Exit\nEnter option:");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        {
                            //current pack
                            CurrentPack();
                            break;
                        }
                    case 2:
                        {
                            //pack recharge
                            PackRecharge();
                            break;
                        }
                    case 3:
                        {
                            //wallet recharge
                            WalletRecharge();
                            break;
                        }
                    case 4:
                        {
                            //view pack recharge history
                            RechargeHistory();
                            break;
                        }
                    case 5:
                        {
                            //wallet balance
                            WalletBalance();
                            break;
                        }
                    case 6:
                        {
                            //exit
                            flag = false;
                            break;
                        }
                }
            } while (flag);
        }

        public void CurrentPack()
        {
            //traverse recharge history and check user id
            //check pack which is used currently
            //show the pack details
            if (operation.CurrentPack().Count == 0)
            {
                System.Console.WriteLine("There is no current pack");
            }
            else
            {
                operation.CurrentPack().ForEach(recharge=> System.Console.WriteLine($"{recharge.RechargeID,-15} |{recharge.UserID,-15} |{recharge.PackID,-15} |{recharge.RechargeDate.ToString("dd/MM/yyyy"),-15} |{recharge.StartDate.ToString("dd/MM/yyyy"),-15} |{recharge.ValidTill.ToString("dd/MM/yyyy"),-15} |{recharge.RechargeAmount,-15} |{recharge.NoOfChannels,-15}"));
                // foreach (RechargeHistory recharge in operation.CurrentPack())
                // {
                //     System.Console.WriteLine($"{recharge.RechargeID,-15} |{recharge.UserID,-15} |{recharge.PackID,-15} |{recharge.RechargeDate.ToString("dd/MM/yyyy"),-15} |{recharge.StartDate.ToString("dd/MM/yyyy"),-15} |{recharge.ValidTill.ToString("dd/MM/yyyy"),-15} |{recharge.RechargeAmount,-15} |{recharge.NoOfChannels,-15}");
                // }
            }
        }

        public void WalletRecharge()
        {
            //Ask and get the amount from user to WalletRecharge.
            System.Console.WriteLine("Enter amount you want to recharge:");
            double amount = double.Parse(Console.ReadLine());
            System.Console.WriteLine($"Recharge was successful. Your wallet bamlance is {operation.WalletRecharge(amount)}");
        }

        public void WalletBalance()
        {
            //show wallet balance
            System.Console.WriteLine($"Your wallet balance is {operation.WalletBalance()}");
        }

        public void RechargeHistory()
        {
            //check recharge history by traversing recharge list
            //show history if present else show History is not found
            if (operation.IsHistory())
            {
                operation.GetRechargeHistory().ForEach(recharge=>  System.Console.WriteLine($"{recharge.RechargeID,-15} |{recharge.UserID,-15} |{recharge.PackID,-15} |{recharge.RechargeDate.ToString("dd/MM/yyyy"),-15} |{recharge.StartDate.ToString("dd/MM/yyyy"),-15} |{recharge.ValidTill.ToString("dd/MM/yyyy"),-15} |{recharge.RechargeAmount,-15} |{recharge.NoOfChannels,-15}"));
                // foreach (RechargeHistory recharge in operation.GetRechargeHistory())
                // {
                //     System.Console.WriteLine($"{recharge.RechargeID,-15} |{recharge.UserID,-15} |{recharge.PackID,-15} |{recharge.RechargeDate.ToString("dd/MM/yyyy"),-15} |{recharge.StartDate.ToString("dd/MM/yyyy"),-15} |{recharge.ValidTill.ToString("dd/MM/yyyy"),-15} |{recharge.RechargeAmount,-15} |{recharge.NoOfChannels,-15}");
                // }
            }
            else
            {
                System.Console.WriteLine("History is not found");
            }
        }

        public void PackRecharge()
        {
            //traverse pack list and show it
            // foreach (PackDetails pack in operation.GetPackDetails())
            // {
            //     System.Console.WriteLine($"{pack.PackID,-15} |{pack.PackName,-15} |{pack.Price,-15} |{pack.Validity,-15} |{pack.NoOfChannels,-15}");
            // }
            operation.GetPackDetails().ForEach(pack=>System.Console.WriteLine($"{pack.PackID,-15} |{pack.PackName,-15} |{pack.Price,-15} |{pack.Validity,-15} |{pack.NoOfChannels,-15}"));
            //ask user for pack id
            System.Console.WriteLine("Enter pack ID you want to recharge:");
            string packID = Console.ReadLine().ToUpper();

            switch (operation.PackRecharge(packID))
            {
                case 0:
                    {
                        System.Console.WriteLine("This Pack ID is not available.");
                        break;
                    }
                case 1:
                    {
                        System.Console.WriteLine("Your balance is insufficient. Please recharge your wallet.");
                        break;
                    }
                case 2:
                    {
                        System.Console.WriteLine("Pack Recharge is Successful ...!");
                        break;
                    }
            }
            //check if pack id is there in list - else show This Pack ID is not available.
            //check user balance >= recharge - else show Your balance is insufficient. Please recharge your wallet.
            //check already recharged & valid till date of previous recharge
            //if pre pack expired, new pack start from today - valid till= today to pack duration
            //if not expired, recharge date=today , start date= previous pack expiry date+1 & valid=pack duration from start date
            //deduct amount from balance , show Pack Recharge is Successful ...!
        }
    }
}