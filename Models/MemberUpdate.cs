using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class MemberUpdate
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string ClientType { get; set; }
        public string Sex { get; set; }
        public string IDCard { get; set; }
        public string Birthday { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string District { get; set; }
        public string Hobby { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }

        public string VinNO { get; set; }
        public string CarNO { get; set; }
        public string CarOwner { get; set; }
        public string CarType { get; set; }
        public int RunKM { get; set; }
        public int NextMaintainKM { get; set; }
        public DateTime? NextMaintainDate { get; set; }
        public DateTime? LicenseExpire { get; set; }
        public DateTime? InsureExpire { get; set; }
        public DateTime? YearCheckDate { get; set; } 
        public DateTime? CreateDate { get; set; }
        public string Creator { get; set; }
        public DateTime? AudiTime { get; set; }
        public string Auditor { get; set; }
        public string DmsID { get; set; }
    }
}