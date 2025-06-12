using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDTH
{
    public class Operations
    {
        List<UserRegistration> users = new List<UserRegistration>();
        List<PackDetails> packs = new List<PackDetails>();
        List<RechargeHistory> recharges = new List<RechargeHistory>();

        UserRegistration currentUser;

        public void DefaultData()
        {
            //adding default values by creating object and adding it to list
            UserRegistration user1 = new UserRegistration("John", "9746646466", "john@gmail.com", 500);
            UserRegistration user2 = new UserRegistration("Merlin", "9782136543", "merlin@gmail.com", 150);

            users.AddRange(new List<UserRegistration>() { user1, user2 });

            PackDetails pack1 = new PackDetails("RC150", "Pack1", 150, 28, 50);
            PackDetails pack2 = new PackDetails("RC300", "Pack2", 300, 56, 75);
            PackDetails pack3 = new PackDetails("RC500", "Pack3", 500, 28, 200);
            PackDetails pack4 = new PackDetails("RC1500", "Pack4", 1500, 365, 200);

            packs.AddRange(new List<PackDetails>() { pack1, pack2, pack3, pack4 });

            RechargeHistory recharge1 = new RechargeHistory("UID1001", "RC150", new DateTime(2021, 11, 30), new DateTime(2021, 11, 30), new DateTime(2021, 12, 27), 150, 50);
            RechargeHistory recharge2 = new RechargeHistory("UID1001", "RC300", new DateTime(2021, 11, 30), new DateTime(2021, 12, 28), new DateTime(2022, 02, 22), 300, 75);
            RechargeHistory recharge3 = new RechargeHistory("UID1002", "RC150", new DateTime(2025, 01, 01), new DateTime(2025, 01, 01), new DateTime(2025, 03, 24), 150, 50);

            recharges.AddRange(new List<RechargeHistory>() { recharge1, recharge2, recharge3 });
        }

        public string AddUser(UserRegistration user)
        {
            //add user to list
            users.Add(user);
            return user.UserID;
        }

        public bool CheckUser(string userID)
        {
            //user authentication
            // foreach (UserRegistration user in users)
            // {
            UserRegistration user = (from user1 in users where user1.UserID == userID select user1).FirstOrDefault();
            if (user != null)
            {
                currentUser = user;
                return true;
            }
            // }
            return false;
        }

        public List<RechargeHistory> CurrentPack()
        {
            // List<RechargeHistory> histories = new List<RechargeHistory>();
            // foreach (RechargeHistory recharge in recharges)
            // {
            //     if (recharge.UserID == currentUser.UserID)
            //     {
            //         //check pack which is used currently
            //         if (recharge.StartDate <= DateTime.Now && recharge.ValidTill >= DateTime.Now)
            //         {
            //             histories.Add(recharge);
            //         }
            //     }
            // }
            List<RechargeHistory> histories1 = (from recharge1 in recharges
                                                where recharge1.UserID == currentUser.UserID && recharge1.StartDate <= DateTime.Now && recharge1.ValidTill >= DateTime.Now
                                                select recharge1).ToList();
            //return histories;
            return histories1;

        }

        public bool IsHistory()
        {
            //checking recharge history
            // foreach (RechargeHistory recharge in recharges)
            // {
            //     if (recharge.UserID == currentUser.UserID)
            //     {
            //         return true;
            //     }
            // }
            if (recharges.Any(user => user.UserID == currentUser.UserID))
            {
                return true;
            }
            return false;
        }

        public List<RechargeHistory> GetRechargeHistory()
        {
            //returning list of current user
            // List<RechargeHistory> rechargeHistories=new List<RechargeHistory>();
            // foreach(RechargeHistory recharge in recharges)
            // {
            //     if(recharge.UserID==currentUser.UserID)
            //     {
            //         rechargeHistories.Add(recharge);
            //     }
            // }
            List<RechargeHistory> rechargeHistories = (from recharge in recharges where recharge.UserID == currentUser.UserID select recharge).ToList();
            return rechargeHistories;
        }

        public double WalletRecharge(double amount)
        {
            //Update the WalletBalance to the current user
            currentUser.WalletRecharge(amount);
            return currentUser.WalletBalance;
        }

        public double WalletBalance()
        {
            return currentUser.WalletBalance;
        }

        public List<PackDetails> GetPackDetails()
        {
            return packs;
        }

        public int PackRecharge(string packID)
        {
            //check if pack id is there in list - else return 0
            foreach (PackDetails pack in packs)
            {
                if (pack.PackID == packID)
                {
                    //check user balance >= recharge - else show Your balance is insufficient. Please recharge your wallet.
                    if (pack.Price <= currentUser.WalletBalance)
                    {
                        //check already recharged & valid till date of previous recharge
                        if (IsHistory())
                        {
                            foreach (RechargeHistory recharge in CurrentPack())
                            {
                                //if not expired, recharge date=today , start date= previous pack expiry date+1 & valid=pack duration from start date
                                //deduct amount from balance
                                RechargeHistory recharge2 = new RechargeHistory(currentUser.UserID, pack.PackID, DateTime.Now, GetRechargeHistory().Last().ValidTill.AddDays(1)/*recharge.ValidTill.AddDays(1)*/, GetRechargeHistory().Last().ValidTill.AddDays(pack.Validity + 1), pack.Price, pack.NoOfChannels);
                                recharges.Add(recharge2);
                                currentUser.DeductBalance(pack.Price);
                                return 2;
                            }
                            //if pre pack expired, new pack start from today - valid till= today to pack duration
                            //deduct amount from balance
                            RechargeHistory recharge1 = new RechargeHistory(currentUser.UserID, pack.PackID, DateTime.Now, DateTime.Now, DateTime.Now.AddDays(pack.Validity), pack.Price, pack.NoOfChannels);
                            recharges.Add(recharge1);
                            currentUser.DeductBalance(pack.Price);
                            return 2;
                        }
                        else
                        {
                            //proceed recharge which applies from today
                            //deduct amount from balance
                            RechargeHistory recharge = new RechargeHistory(currentUser.UserID, pack.PackID, DateTime.Now, DateTime.Now, DateTime.Now.AddDays(pack.Validity), pack.Price, pack.NoOfChannels);
                            recharges.Add(recharge);
                            currentUser.DeductBalance(pack.Price);
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
        }

    }
}