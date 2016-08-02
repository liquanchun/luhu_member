using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class CardTypeController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        ClsSys clssys = new ClsSys();
        //
        // GET: /CardType/
        [CheckLogin]
        public ActionResult Index()
        {
            var dmsid = Session["DmsID"].ToString();
            ViewBag.dmsid = dmsid;
            var cardtype = DBContext.CardTypeDb.Where(w =>w.DmsID == dmsid).ToList();
            CommonCls.WriteSysLog("查看卡级别定义", Request.RawUrl, this.Session["UserName"].ToString());
            return View(cardtype);
        }
        //
        // GET: /CardType/Create
        [CheckLogin]
        public ActionResult Create()
        {
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "卡类型").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            ViewBag.TypeList = DBContext.CardTypeDb.ToList();
            var cardtype = new CardType();
            return View(cardtype);
        } 

        //
        // POST: /CardType/Create

        [HttpPost]
        public ActionResult Create(CardType cardtype)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    cardtype.DmsID = Session["DmsID"].ToString();
                    DBContext.CardTypeDb.Add(cardtype);
                    DBContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "卡类型").First().ID;
                ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
                ViewBag.TypeList = DBContext.CardTypeDb.ToList();
                CommonCls.WriteSysLog("新增卡级别：" + cardtype.CardLevel, Request.RawUrl, this.Session["UserName"].ToString());
                return View(cardtype);
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /CardType/Edit/5
        [CheckLogin]
        public ActionResult Edit(int id)
        {
            string typeid = Request.Url.Segments[3];
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "卡类型").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            ViewBag.TypeList = DBContext.CardTypeDb.ToList();
            var cardtype = DBContext.CardTypeDb.FirstOrDefault(f => f.TypeID == typeid);
            return View(cardtype);
        }

        //
        // POST: /CardType/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var cardtype = DBContext.CardTypeDb.First(f => f.TypeID == id);
                TryUpdateModel(cardtype);
                DBContext.SaveChanges();
                CommonCls.WriteSysLog("修改卡级别：" + cardtype.CardLevel, Request.RawUrl, this.Session["UserName"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                int typeid = int.Parse(Request.Url.Segments[3]);
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "卡类型").First().ID;
                ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
                ViewBag.TypeList = DBContext.CardTypeDb.ToList();
                return View();
            }
        }
        [HttpGet]
        public ActionResult CheckTypeID(string typeid)
        {
            bool exists = DBContext.CardTypeDb.Where(w => w.TypeID == typeid).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckCardLevel(string cardlevel)
        {
            bool exists = DBContext.CardTypeDb.Where(w => w.CardLevel == cardlevel).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
    }
}
