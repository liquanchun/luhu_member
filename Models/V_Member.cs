using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class V_Member
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        [Required(ErrorMessage="客户姓名不能为空")]
        public string Name { get; set; }
        public string ClientType { get; set; }
        public string Sex { get; set; }
        public string IDCard { get; set; }
        public string Birthday { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DmsID { get; set; }
        public string District { get; set; }
        public string Hobby { get; set; }
        public string Postcode { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Address { get; set; }
        public int ComeTimes { get; set; }
        public DateTime LastComeDate { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }
        [Required(ErrorMessage="车架号不能为空")]
        [StringLength(17,ErrorMessage="车架号必须17位")]
        [MinLength(17, ErrorMessage = "车架号必须17位")]
        [Remote("CheckVinNOExists", "Member", ErrorMessage = "车架号已存在")]
        public string VinNO { get; set; }
        [Required(ErrorMessage = "车牌号不能为空")]
        [Remote("CheckCarNOExists", "Member", ErrorMessage = "车牌号已存在")]
        public string CarNO { get; set; }
        public string CarType { get; set; }
        [DefaultValue(0)]
        public int RunKM { get; set; }
        [DefaultValue(0)]
        public int NextMaintainKM { get; set; }
        public  DateTime?  NextMaintainDate { get; set; }
        public DateTime? LicenseExpire { get; set; }
        public string InsureCompany { get; set; }
        public string InsureType { get; set; }
        public DateTime? InsureExpire { get; set; }
        public DateTime? YearCheckDate { get; set; }
        public string CarOwner { get; set; }
        public string CardNo { get; set; }
        public string DmsName { get; set; }
    }
}