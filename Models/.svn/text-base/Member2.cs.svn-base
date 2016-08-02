using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class Member2
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        public string VinNO { get; set; }
        public string CarNO { get; set; }
        public int MemberID { get; set; }
        [DefaultValue("")]
        public string CarOwner { get; set; }
        public string CarType { get; set; }
        [DefaultValue(0)]
        public int RunKM { get; set; }
        [DefaultValue(0)]
        public int NextMaintainKM { get; set; }
        public DateTime? NextMaintainDate { get; set; }
        public DateTime? LicenseExpire { get; set; }
        public string InsureCompany { get; set; }

        public string InsureType { get; set; }
        public DateTime? InsureExpire { get; set; }
        public DateTime? YearCheckDate { get; set; }
    }
}