using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class SysFun
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        public int FunID { get; set; }
        public string FunName { get; set; }
        public int ParentID { get; set; }
        public string ControlName { get; set; }
        public string ActionName { get; set; }
        public string Parameter { get; set; }
    }
}