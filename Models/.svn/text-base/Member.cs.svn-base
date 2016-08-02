using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        [Required(ErrorMessage = "客户姓名不能为空")]
        public string Name { get; set; }
        public string ClientType { get; set; }
        public string Sex { get; set; }
        [DefaultValue("")]
        public string IDCard { get; set; }
        [DefaultValue("")]
        public string Birthday { get; set; }
        public string Mobile { get; set; }
        [DefaultValue("")]
        public string Email { get; set; }
        public string DmsID { get; set; }
        public string District { get; set; }
        [DefaultValue("")]
        public string Hobby { get; set; }
        [DefaultValue("")]
        public string Postcode { get; set; }
        [DefaultValue("")]
        public string Address { get; set; }
        [ReadOnly(true)]
        public int ComeTimes { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime LastComeDate { get; set; }
        [DefaultValue("")]
        public string Remark { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime CreateDate { get; set; }
        [DefaultValue("")]
        public string Creator { get; set; }
    }
}