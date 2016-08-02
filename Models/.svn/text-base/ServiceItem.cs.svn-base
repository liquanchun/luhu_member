using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class ServiceItem
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [Required(ErrorMessage = "必须填写")]
        [Remote("CheckItemIDExists", "ServiceItem", ErrorMessage = "编号已存在")]
        public string ItemID { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string ItemType { get; set; }
        [Required(ErrorMessage = "必须填写")]
        [Remote("CheckItemNameExists", "ServiceItem", ErrorMessage = "名称姓名已存在")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public int Price { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public int Integral { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public int Number { get; set; }
        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }
        public string Remark { get; set; }
        public string DmsID { get; set; }
    }
}