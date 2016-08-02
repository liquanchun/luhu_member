using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class ActivityController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        ClsSys clssys = new ClsSys();
        //
        // GET: /Activity/
        [CheckLogin]
        public ActionResult Index()
        {
            var dmsid = Session["DmsID"].ToString();
            var act = DBContext.ActivityDb.Where(w => w.DmsID == dmsid).OrderByDescending(A => A.StartTime).ToList();
            return View(act);
        }
        [CheckLogin]
        public ActionResult Create()
        {
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "活动类别").First().ID;
            ViewBag.EventType = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            Activity act = new Activity();
            return View(act);
        }
        [CheckLogin]
        public ActionResult ComplainAdd()
        {

            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "投诉性质").First().ID;
            ViewBag.EventType = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            ViewBag.DmsID = Session["DmsID"].ToString();
            ViewBag.UserID = Session["UserName"].ToString();
            return View();
        }
        [CheckLogin]
        public ActionResult ComplainEdit()
        {
            string tag = "-1";
            int id = Convert.ToInt32(Request.Url.Segments[3].Replace("/", "")); ;
            if (Request.Url.Segments.Count() == 5)
            {
                tag = Request.Url.Segments[4].ToString();
            }
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "投诉性质").First().ID;
            ViewBag.EventType = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            ViewBag.DmsID = Session["DmsID"].ToString();
            ViewBag.UserID = Session["UserName"].ToString();
            ViewBag.Tag = tag;
            ViewBag.ID = id;
            if (id > 0)
            {
                var result = DBContext.ComplainDb.FirstOrDefault(F => F.ID == id);
                this.ViewData.Model = result;
            }
            else
            {
                this.ViewData.Model = new Complain();
            }
            return View();
        }
        [CheckLogin]
        public ActionResult ComplainList()
        {
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            if (this.Request.Form.AllKeys.Length > 0)
            {
                searchConditions.Add("Date1", Request["Date1"]);
                searchConditions.Add("Date2", Request["Date2"]);
                searchConditions.Add("Name", Request["Name"]);
                searchConditions.Add("Mobile", Request["Mobile"]);
                searchConditions.Add("CarNo", Request["CarNo"]);
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
            string Mobile = GetSearchConditionValue(searchConditions, "Mobile");
            string CarNo = GetSearchConditionValue(searchConditions, "CarNo");
            DateTime dt1 = string.IsNullOrEmpty(Date1) ? DateTime.Today.AddYears(-10) : DateTime.Parse(Date1);
            DateTime dt2 = string.IsNullOrEmpty(Date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(Date2).AddDays(1);
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(CarNo) && string.IsNullOrEmpty(Date1))
            {
                dt1 = DateTime.Today.AddYears(-10);
            }
            var dmsid = Session["DmsID"].ToString();
            var result = (from m in DBContext.ComplainDb
                          where m.DmsID == dmsid && (string.IsNullOrEmpty(Name.Trim()) || m.Customer.StartsWith(Name.Trim()))
                          && (string.IsNullOrEmpty(Mobile.Trim()) || m.Mobile.StartsWith(Mobile.Trim()))
                          && (string.IsNullOrEmpty(CarNo.Trim()) || m.CarNO.Trim().EndsWith(CarNo.Trim()))
                          && m.CreateTime.CompareTo(dt1) >= 0 && m.CreateTime.CompareTo(dt2) < 0
                          select m).ToList().OrderByDescending(o => o.CreateTime);
            this.ViewData.Model = result;
            ViewBag.UserID = Session["UserID"].ToString();
            CommonCls.WriteSysLog("查看客户客户抱怨意见", Request.RawUrl, this.Session["UserName"].ToString());
            return View();
        }
        [CheckLogin]
        public ActionResult CallBack()
        {
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            if (this.Request.Form.AllKeys.Length > 0)
            {
                searchConditions.Add("Date1", Request["Date1"]);
                searchConditions.Add("Date2", Request["Date2"]);
                searchConditions.Add("Rno", Request["Rno"]);
                searchConditions.Add("Mobile", Request["Mobile"]);
                searchConditions.Add("CarNo", Request["CarNo"]);
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
            string Rno = GetSearchConditionValue(searchConditions, "Rno");
            string Mobile = GetSearchConditionValue(searchConditions, "Mobile");
            string CarNo = GetSearchConditionValue(searchConditions, "CarNo");
            DateTime dt1 = string.IsNullOrEmpty(Date1) ? DateTime.Today.AddYears(-10) : DateTime.Parse(Date1);
            DateTime dt2 = string.IsNullOrEmpty(Date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(Date2).AddDays(1);
            if (string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(CarNo) && string.IsNullOrEmpty(Rno) && string.IsNullOrEmpty(Date1))
            {
                dt1 = DateTime.Today.AddYears(-10);
            }
            var dmsid = Session["DmsID"].ToString();
            var result = (from m in DBContext.V_CallBackDb
                          where m.DmsID == dmsid && (string.IsNullOrEmpty(Rno.Trim()) || m.Rno.StartsWith(Rno.Trim()))
                          && (string.IsNullOrEmpty(Mobile.Trim()) || m.mobile.StartsWith(Mobile.Trim()))
                          && (string.IsNullOrEmpty(CarNo.Trim()) || m.carno.Trim().EndsWith(CarNo.Trim()))
                          && m.CreateDate.CompareTo(dt1) >= 0 && m.CreateDate.CompareTo(dt2) < 0
                          select m).ToList().OrderByDescending(o => o.CreateDate);
            this.ViewData.Model = result;
            ViewBag.UserID = Session["UserID"].ToString();
            CommonCls.WriteSysLog("查看客户回访", Request.RawUrl, this.Session["UserName"].ToString());
            return View();
        }
        /// <summary>
        /// 积分兑换礼品
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        public ActionResult Exchange()
        {
            ViewBag.UserID = Session["UserID"].ToString();
            var dmsid = Session["DmsID"].ToString();
            var result = (from m in DBContext.GiftInfoDb
                          where m.DmsID == dmsid 
                          select m).ToList().OrderByDescending(o => o.UpdateTime);
            this.ViewData.Model = result;
            ViewBag.DmsID = dmsid;
            ViewBag.GiftInfo = DBContext.GiftInfoDb.Where(w => w.DmsID == dmsid && w.IsUse == 1).ToList();
            return View();
        }
        [CheckLogin]
        public ActionResult ExchangeList()
        {
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            if (this.Request.Form.AllKeys.Length > 0)
            {
                searchConditions.Add("Date1", Request["Date1"]);
                searchConditions.Add("Date2", Request["Date2"]);
                searchConditions.Add("Name", Request["Name"]);
                searchConditions.Add("CardNo", Request["CardNo"]);
                searchConditions.Add("CarNo", Request["CarNo"]);
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
            string CarNo = GetSearchConditionValue(searchConditions, "CarNo");
            DateTime dt1 = string.IsNullOrEmpty(Date1) ? DateTime.Today.AddYears(-10) : DateTime.Parse(Date1);
            DateTime dt2 = string.IsNullOrEmpty(Date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(Date2).AddDays(1);
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(CardNo) && string.IsNullOrEmpty(CarNo) && string.IsNullOrEmpty(Date1))
            {
                dt1 = DateTime.Today.AddYears(-10);
            }
            var dmsid = Session["DmsID"].ToString();
            var result = (from m in DBContext.V_ExchangePointDb
                          where m.DmsID == dmsid && (string.IsNullOrEmpty(Name.Trim()) || m.Name.StartsWith(Name.Trim()))
                          && (string.IsNullOrEmpty(CardNo.Trim()) || m.CardNo.StartsWith(CardNo.Trim()))
                          && (string.IsNullOrEmpty(CarNo.Trim()) || m.CarNO.Trim().EndsWith(CarNo.Trim()))
                          && m.CreateDate.CompareTo(dt1) >= 0 && m.CreateDate.CompareTo(dt2) < 0
                          select m).ToList().OrderByDescending(o => o.CreateDate);
            this.ViewData.Model = result;
            CommonCls.WriteSysLog("查看积分兑换记录", Request.RawUrl, this.Session["UserName"].ToString());
            return View();
        }
        [HttpPost]
        public ActionResult Create(Activity activity)
        {
            try
            {
                // TODO: Add insert logic here
                activity.CreateDate = DateTime.Now;
                activity.Creator = Session["UserName"].ToString();
                activity.DmsID = Session["DmsID"].ToString();
                if (ModelState.IsValid)
                {
                    DBContext.ActivityDb.Add(activity);
                    DBContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "活动类别").First().ID;
                ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
                return View(activity);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.Message);
                return View();
            }
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
