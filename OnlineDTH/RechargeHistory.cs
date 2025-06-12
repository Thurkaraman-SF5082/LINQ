using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDTH
{
    public class RechargeHistory
    {
        /*
        •	RechargeID (Auto increment which is start from RP101) 
        •	UserID 
        •	PackID 
        •	RechargeDate (Current Date) 
        •	StartDate
        •	ValidTill
        •	RechargeAmount 
        •	NumberOfChannels 

        */
        private static int s_rechargeID = 100;
        public string RechargeID { get; set; }
        public string UserID { get; set; }
        public string PackID { get; set; }
        public DateTime RechargeDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ValidTill { get; set; }
        public double RechargeAmount { get; set; }
        public int NoOfChannels { get; set; }

        public RechargeHistory(string userID, string packID, DateTime rechargeDate, DateTime startDate, DateTime validTill, double rechargeAmount, int noOfChannels)
        {
            RechargeID = $"RP{++s_rechargeID}";
            UserID = userID;
            PackID = packID;
            RechargeDate = rechargeDate;
            StartDate = startDate;
            ValidTill = validTill;
            RechargeAmount = rechargeAmount;
            NoOfChannels = noOfChannels;
        }
    }
}