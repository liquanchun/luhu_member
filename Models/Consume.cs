using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class Consume
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        public string BillNo { get; set; }
        [Required(ErrorMessage = "请确认会员卡号")]
        public int MemberID { get; set; }
        [Required(ErrorMessage="请确认会员卡号")]
        public string CardNo { get; set; }
        public int CurBIntegral { get; set; }

        public int Integral { get; set; }
        public int BalanceMoney { get; set; }
        public int SumFee { get; set; }
        public string SaleMan { get; set; }
        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }

        public bool IsUndo { get; set; }
        public int? InteToMoney { get; set; }
        public int? DeductInte { get; set; }
    }
}