using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class AdminUserController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        
        ClsSys clssys = new ClsSys();
        //
        // GET: /AdminUser/
        [CheckLogin]
        public ActionResult Index()
        {
            ViewBag.UserID = string.Empty;
            if (Session["UserID"] != null) 
            {
                ViewBag.UserID = Session["UserID"].ToString();
            }
            var dmsid = Session["DmsID"].ToString();
            var adminuser =DBContext.V_AdminUserDb.Where(w => w.ID >0).ToList();
            ViewBag.DmsId = dmsid;
            ViewBag.UserID = Session["UserID"].ToString();
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "用户角色").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();

            ViewBag.branchs = DBContext.BranchDb.ToList();

            CommonCls.WriteSysLog("查看用户管理",Request.RawUrl, this.Session["UserName"].ToString());
            return View(adminuser);
        }
        [CheckLogin]
        public ActionResult Create()
        {
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "用户角色").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            var adminuser = new AdminUser();
            return View(adminuser);
        }
        [CheckLogin]
        public ActionResult UpdatePwd()
        {
            return View();
        }

        //
        // POST: /Branch/Create

        [HttpPost]
        public ActionResult Create(AdminUser adminuser)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    adminuser.DmsID = Session["DmsID"].ToString();
                    DBContext.AdminUserDb.Add(adminuser);
                    DBContext.SaveChanges();
                    CommonCls.WriteSysLog("新增用户ID：" + adminuser.UserID, Request.RawUrl, this.Session["UserName"].ToString());
                    return RedirectToAction("Index");
                }
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "用户角色").First().ID;
                ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();

                return View(adminuser);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, Err.StackTrace + Err.Message);
                return View();
            }
        }
        [HttpGet]
        public ActionResult CheckUserIDExists(string userid)
        {
            bool exists = DBContext.AdminUserDb.Where(w => w.UserID == userid).Count() > 0 ? true : false;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckUserNameExists(string username)
        {
            bool exists = DBContext.AdminUserDb.Where(w => w.UserName == username).Count() > 0 ? true : false;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
    }
}
