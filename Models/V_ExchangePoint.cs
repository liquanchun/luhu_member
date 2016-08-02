using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMember.Models
{
    public class V_ExchangePoint
    {
        public int ID { get; set; }
        public string DmsID { get; set; }
        public string Name { get; set; }
        public string CarNO { get; set; }
        public string CardNo { get; set; }
        public string GiftName { get; set; }
        public int Integral { get; set; }

        public int SendNum { get; set; }
        public int ChangeIntegral { get; set; }
        public int CBlanceIntegral { get; set; }
        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }
    }
}