using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class GuestBook
    {
        public int ID { get; set; }
        [Required(ErrorMessage="必须填写")]
        public string Title { get; set; }
        public DateTime LTime { get; set; }
        public string LUser { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string LContent { get; set; }
        [Required(ErrorMessage = "必须填写")]
        public string ContactWay { get; set; }
        public string Reply { get; set; }
        public string ReplyMan { get; set; }
        public DateTime? ReplyTime { get; set; }
    }
}