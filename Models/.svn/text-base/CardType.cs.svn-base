using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class CardType
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [Remote("CheckTypeID", "CardType", ErrorMessage = "已经存在")]
        public string TypeID { get; set; }
        public string CardSort { get; set; }
        [Remote("CheckCardLevel", "CardType", ErrorMessage = "已经存在")]
        public string CardLevel { get; set; }
        [Range(1,10,ErrorMessage="年限必须大于1")]
        public int CardYears { get; set; }
        [Range(1.0,10.0,ErrorMessage="折扣范围1.0到10.0")]
        public decimal HourDiscount { get; set; }
        [Range(1.0, 10.0, ErrorMessage = "折扣范围1.0到10.0")]
        public decimal PartDiscount { get; set; }
        public int IniIntegral { get; set; }
        public int UpgradeIntegral { get; set; }
        public string NextTypeID { get; set; }
        public string DmsID { get; set; }
    }
}