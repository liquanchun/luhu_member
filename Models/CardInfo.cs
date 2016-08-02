using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class CardInfo
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        public string CardNo { get; set; }
        public int MemberID { get; set; }
        public string TypeID { get; set; }
        public string State { get; set; }

        public string ComeType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int InitialIntegral { get; set; }
        public int SumConsumeIntegral { get; set; }

        public int SumGiveIntegral { get; set; }
        public int BalanceIntegral { get; set; }
        public int SumConsumeMoney { get; set; }
        public int SumRecharge { get; set; }
        public int SumExchange { get; set; }

        public int Balance { get; set; }
        public string SaleMan { get; set; }
        public string Creator { get; set; }
        public DateTime  CreateDate { get; set; }

    }
}