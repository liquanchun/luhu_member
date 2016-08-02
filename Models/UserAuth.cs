using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class UserAuth
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        public int FunID { get; set; }
        public string RoleName { get; set; }
        public int IsUse { get; set; }
    }
}