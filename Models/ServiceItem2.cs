using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class ServiceItem2
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string ItemID { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string ChildItemID { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public int Times { get; set; }
    }
}