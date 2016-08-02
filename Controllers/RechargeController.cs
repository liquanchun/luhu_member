using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;

namespace MvcMember.Controllers
{
    public class RechargeController : Controller
    {
        //
        // GET: /Recharge/
        WebMemberDBContext DBContext = new WebMemberDBContext();
        ClsSys clssys = new ClsSys();
        [CheckLogin]
        public ActionResult Index()
        {
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            if (this.Request.Form.AllKeys.Length > 0)
            {
                searchConditions.Add("Date1", Request["Date1"]);
                searchConditions.Add("Date2", Request["Date2"]);
                searchConditions.Add("Name", Request["Name"]);
                searchConditions.Add("CardNo", Request["CardNo"]);
            }
            else
            {
                object values = null;
                if (this.TempData.TryGetValue("SearchConditions", out values))
                {
                    searchConditions = values as Dictionary<string, string>;
                }
            }
            this.TempData["SearchConditions"] = searchConditions;
            string Date1 = GetSearchConditionValue(searchConditions, "Date1");
            string Date2 = GetSearchConditionValue(searchConditions, "Date2");
            string Name = GetSearchConditionValue(searchConditions, "Name");
            string CardNo = GetSearchConditionValue(searchConditions, "CardNo");
            DateTime dt1 = string.IsNullOrEmpty(Date1) ? DateTime.Today.AddYears(-10) : DateTime.Parse(Date1);
            DateTime dt2 = string.IsNullOrEmpty(Date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(Date2).AddDays(1);
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(CardNo) && string.IsNullOrEmpty(Date1))
            {
                dt1 = DateTime.Today.AddYears(-10);
            }
            var dmsid = Session["DmsID"].ToString();
            var result = (from c in DBContext.V_RechargeDb
                          where c.DmsID == dmsid && (string.IsNullOrEmpty(Name.Trim()) || c.Name.StartsWith(Name.Trim()))
                          && (string.IsNullOrEmpty(CardNo.Trim()) || c.CardNo.StartsWith(CardNo.Trim()))
                          && c.CreateDate.CompareTo(dt1) >= 0 && c.CreateDate.CompareTo(dt2) < 0
                          select c).ToList().OrderByDescending(o => o.CreateDate);
            this.ViewData.Model = result;
            CommonCls.WriteSysLog("查看充值记录", Request.RawUrl, this.Session["UserName"].ToString());
            return View(result);
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
        //
        // GET: /Recharge/Details/5
        [CheckLogin]
        public ActionResult Details(int id)
        {
            var v_recharge = DBContext.V_RechargeDb.FirstOrDefault(f => f.ID == id);
            return View(v_recharge);
        }
        [CheckLogin]
        public ActionResult RechargeGive()
        {
             var dmsid = Session["DmsID"].ToString();
             ViewBag.userid = this.Session["UserName"].ToString();
             ViewBag.dmsid = dmsid;
            return View();
        }
        //
        // GET: /Recharge/Create
        [CheckLogin]
        public ActionResult Create()
        {
            var dmsid = Session["DmsID"].ToString();
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "付款方式").First().ID;
            var list = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            ViewBag.RechargeType = list;
            ViewBag.ServiceItem = DBContext.ServiceItemDb.Where(w => w.DmsID == dmsid && w.ItemType == "单项服务").ToList();
            ViewBag.username = this.Session["UserName"].ToString();
            ViewBag.DmsID = dmsid;
            var v_recharge = new V_Recharge();
            return View(v_recharge);
        } 

        //
        // POST: /Recharge/Create

        [HttpPost]
        public ActionResult Create(V_Recharge v_recharge)
        {
            try
            {
                // TODO: Add insert logic here
                Recharge recharge = new Recharge();
                recharge.Balance = v_recharge.Balance;
                recharge.GiveInte = v_recharge.GiveInte;
                recharge.GiveMoney = v_recharge.GiveMoney;
                recharge.InMoney = v_recharge.InMoney;
                recharge.InType = v_recharge.InType;
                recharge.MemberID = v_recharge.MemberID;
                recharge.Remark = v_recharge.Remark;
                recharge.CreateDate = DateTime.Now;
                recharge.Creator = Session["UserName"].ToString();
                string ServiceItemID = Request.Form.Get("selitemid");
                if (ServiceItemID != string.Empty && ServiceItemID != "-1")
                {
                    recharge.GiveServiceID = ServiceItemID;
                }
                DBContext.RechargeDb.Add(recharge);
                DBContext.SaveChanges();
                if (ServiceItemID != string.Empty && ServiceItemID != "-1")
                {
                    //有赠送服务
                    string times = Request.Form.Get("times");
                    //单项服务
                    CardServiceItem cardserviceitem = new CardServiceItem();
                    cardserviceitem.CardNo = v_recharge.CardNo;
                    cardserviceitem.MemberID = v_recharge.MemberID;
                    cardserviceitem.Creator = Session["UserName"].ToString();
                    cardserviceitem.CreateDate = DateTime.Now;
                    cardserviceitem.ServiceItemID = ServiceItemID;
                    cardserviceitem.Times = int.Parse(times);
                    cardserviceitem.SubTimes = int.Parse(times);
                    cardserviceitem.Remark = "充值赠送";
                    cardserviceitem.ServiceType = "单项服务";
                    DBContext.CardServiceItemDb.Add(cardserviceitem);
                    DBContext.SaveChanges();
                }

                CommonCls.WriteSysLog("新增会员充值，会员卡号:" + v_recharge.CardNo + ",当前余额:" + v_recharge.Balance + ",充值金额:" + v_recharge.InMoney, Request.RawUrl, this.Session["UserName"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "付款方式").First().ID;
                ViewBag.RechargeType = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
                ViewBag.ServiceItem = DBContext.ServiceItemDb.ToList();
                return View(v_recharge);
            }
        }
    }
}
