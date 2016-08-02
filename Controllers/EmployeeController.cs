using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class EmployeeController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        ClsSys clssys = new ClsSys();
        //
        // GET: /Employee/
        [CheckLogin]
        public ActionResult Index()
        {
            var dmsid = Session["DmsID"].ToString();
            var employ = DBContext.EmployeeDb.Where(w => w.DmsID == dmsid).ToList();
            CommonCls.WriteSysLog("查看员工资料", Request.RawUrl, this.Session["UserName"].ToString());
            
            return View(employ);
        }

        //
        // GET: /Employee/Details/5
        [CheckLogin]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Employee/Create
        [CheckLogin]
        public ActionResult Create()
        {
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "员工部门").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "男", Value = "1", Selected = true });
            items.Add(new SelectListItem { Text = "女", Value = "0"});
            ViewBag.SexList = items;
            var employee = new Employee();
            return View(employee);
        } 

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(Employee employ)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "男", Value = "1", Selected = true });
            items.Add(new SelectListItem { Text = "女", Value = "0" });
            ViewBag.SexList = items;
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    employ.ID = DBContext.EmployeeDb.Count() > 0 ? DBContext.EmployeeDb.Select(s => s.ID).Max() + 1 :1;
                    employ.DmsID = Session["DmsID"].ToString();
                    DBContext.EmployeeDb.Add(employ);
                    DBContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "员工部门").First().ID;
                ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();

                CommonCls.WriteSysLog("新增员工资料："+ employ.Name, Request.RawUrl, this.Session["UserName"].ToString());
                return View(employ);
            }
            catch(Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.Message);
                return View();
            }
        }
        
        //
        // GET: /Employee/Edit/5
        [CheckLogin]
        public ActionResult Edit(int id)
        {
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "员工部门").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            var employ = DBContext.EmployeeDb.First(e => e.ID == id);
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "男", Value = "1", Selected = true });
            items.Add(new SelectListItem { Text = "女", Value = "0" });
            ViewBag.SexList = items;
            return View(employ);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var employ = DBContext.EmployeeDb.First(e => e.ID == id);
                
                if (TryUpdateModel(employ))
                {
                    DBContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "员工部门").First().ID;
                ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "男", Value = "1", Selected = true });
                items.Add(new SelectListItem { Text = "女", Value = "0" });
                ViewBag.SexList = items;
                CommonCls.WriteSysLog("修改员工资料："+ employ.Name, Request.RawUrl, this.Session["UserName"].ToString());
                return View(employ);
            }
            catch(Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.Message);
                return View();
            }
        }
        [HttpGet]
        public ActionResult CheckEmployeeIDExists(string employeeid)
        {
            bool exists = DBContext.EmployeeDb.Where(w => w.EmployeeID == employeeid).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckNameExists(string name)
        {
            bool exists = DBContext.EmployeeDb.Where(w => w.Name == name).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
    }
}
