using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class Activity
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [Required(ErrorMessage = "必须填写")]
        [DisplayName("活动编号")]
        public string ActNo { get; set; }
        [Required(ErrorMessage = "必须填写")]
        [DisplayName("活动名称")]
        public string Name { get; set; }
        [DisplayName("活动类别")]
        public string ActType { get; set; }
        [DisplayName("活动积分")]
        public int ActIntegral { get; set; }
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }
        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }
        [DisplayName("创建人")]
        public string Creator { get; set; }
        public string DmsID { get; set; }
    }
}