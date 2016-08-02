using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class Recharge
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [Required(ErrorMessage="请确认会员信息")]
        public int MemberID { get; set; }
        public int Balance { get; set; }
        [Required(ErrorMessage = "请输入充值金额")]
        public int InMoney { get; set; }
        [DefaultValue(0)]
        public int? GiveMoney { get; set; }
        [DefaultValue(0)]
        public int? GiveInte { get; set; }
        public string GiveServiceID { get; set; }
        public string InType { get; set; }
        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }
        public string Remark { get; set; }
        public string DmsID { get; set; }
    }
}