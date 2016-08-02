using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMember.Models
{
    public class ItemList
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public int ItemParent { get; set; }
        public int ItemOrder { get; set; }
    }
}