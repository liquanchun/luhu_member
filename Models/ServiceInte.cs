using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class ServiceInte
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string ClientType { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string ServiceType { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string HoursRate { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string PartsRate { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string OtherRate { get; set; }
        public string DmsID { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string CardType { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public int UseMaxRate { get; set; }
    }
}