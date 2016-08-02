using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class V_CardInfo
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string DmsID { get; set; }
        public string CarNO { get; set; }
        [MaxLength(6, ErrorMessage = "卡号必须是6位")]
        [MinLength(6, ErrorMessage = "卡号必须是6位")]
        [Remote("CheckCardNoExists", "CardInfo", ErrorMessage = "卡号已存在")]
        public string CardNo { get; set; }
        public DateTime StartDate { get; set; }
        public int SumConsumeIntegral { get; set; }
        public int SumGiveIntegral { get; set; }
        public int BalanceIntegral { get; set; }
        public string CardLevel { get; set; }
        public string State { get; set; }
        public string ComeType { get; set; }
        public DateTime EndDate { get; set; }
        public int InitialIntegral { get; set; }
        public int SumConsumeMoney { get; set; }
        public int SumRecharge { get; set; }
        public int SumGiveMoney { get; set; }
        public string SaleMan { get; set; }

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string CardSort { get; set; }
        public int CardYears { get; set; }
        public decimal HourDiscount { get; set; }
        public decimal PartDiscount { get; set; }
        public int SumExchange { get; set; }
        public int Balance { get; set; }
        public string VinNO { get; set; }
        public string TypeID { get; set; }
        public string ClientType { get; set; }
        public DateTime LastComeDate { get; set; }
        public string DmsName { get; set; }
    }
}