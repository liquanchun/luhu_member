using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
using Webdiyer.WebControls.Mvc;
namespace MvcMember.Controllers
{
    public class CardServiceItemController : Controller
    {
        //
        // GET: /CardServiceItem/
        WebMemberDBContext DBContext = new WebMemberDBContext();
        ClsSys clssys = new ClsSys();
        [CheckLogin]
        public ActionResult Index(int? id)
        {
            int p = id ?? 1;
            int defaultPageSize = 15;
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            searchConditions.Add("Date1", Request["Date1"]);
            searchConditions.Add("Date2", Request["Date2"]);
            searchConditions.Add("CardNo", Request["CardNo"]);
            this.TempData["SearchConditions"] = searchConditions;

            string Date1 = GetSearchConditionValue(searchConditions, "Date1");
            string Date2 = GetSearchConditionValue(searchConditions, "Date2");
            string CardNo = GetSearchConditionValue(searchConditions, "CardNo");
            DateTime dt1 = string.IsNullOrEmpty(Date1) ? DateTime.Today.AddYears(-10) : DateTime.Parse(Date1);
            DateTime dt2 = string.IsNullOrEmpty(Date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(Date2).AddDays(1);
            if (string.IsNullOrEmpty(CardNo) && string.IsNullOrEmpty(Date1))
            {
                dt1 = DateTime.Today.AddYears(-10);
            }
            var dmsid = Session["DmsID"].ToString();
            PagedList<V_CardServiceItem> cardserviceitem = null;
            if (CardNo == string.Empty || CardNo == null)
            {
                cardserviceitem = DBContext.V_CardServiceItemDb.Where(w => w.CreateDate.CompareTo(dt1) >= 0
                    && w.CreateDate.CompareTo(dt2) < 0 && w.DmsID == dmsid ).OrderByDescending(o => o.CreateDate).ToPagedList(id ?? 1, defaultPageSize);
            }
            else
            {
                cardserviceitem = DBContext.V_CardServiceItemDb.Where(w =>w.CardNo == CardNo && w.DmsID == dmsid).OrderByDescending(o => o.CreateDate).ToPagedList(id ?? 1, defaultPageSize);
            }
            return View(cardserviceitem);
        }
        [CheckLogin]
        public ActionResult Create()
        {
            var dmsid = Session["DmsID"].ToString();
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "付款方式").First().ID;
            ViewBag.RechargeType = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();

            ViewBag.ServiceItem = DBContext.ServiceItemDb.Where(w => w.DmsID == dmsid && w.ItemType == "单项服务").ToList();
            ViewBag.username = this.Session["UserName"].ToString();
            var V_CardServiceItem = new V_CardServiceItem();
            return View(V_CardServiceItem);
        }
        private static string GetSearchConditionValue(IDictionary<string, string> searchConditions, string key)
        {
            string tempValue = string.Empty;
            if (searchConditions != null && searchConditions.Count > 0)
            {
                searchConditions.TryGetValue(key, out tempValue);
            }
            return tempValue;
        }
    }
}
