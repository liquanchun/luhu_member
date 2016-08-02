using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class CardServiceItem
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        public int MemberID { get; set; }
        public string CardNo { get; set; }
        public string ServiceItemID { get; set; }
        public int Times { get; set; }
        public int SubTimes { get; set; }
        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }
        public int Fee { get; set; }
        public string PayType { get; set; }
        public string Remark { get; set; }
        public string ServiceType { get; set; }
    }
};