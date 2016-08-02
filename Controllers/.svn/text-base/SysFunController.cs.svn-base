using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class SysFunController : Controller
    {
        WebMemberDBContext DbContext = new WebMemberDBContext();
        //
        // GET: /SysFun/
        [CheckLogin]
        public ActionResult Index(FormCollection collection)
        {
            string reqname = Request["RoleName"];
            string rolename=string.Empty;
            if(Session["RoleName"]!=null) rolename = Session["RoleName"].ToString();
            if (reqname == null && Session["RoleName"] != null)
            {
                rolename = Session["RoleName"].ToString();
            }
            else
            {
                rolename = reqname;
            }
            var sysfun =(from v in DbContext.V_SysFunRole where v.RoleName == rolename select v).ToList();
            int parentid = DbContext.ItemListDb.Where(w => w.ItemName == "用户角色").First().ID;
            ViewBag.ItemList = DbContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            ViewData.Model = sysfun;
            ViewBag.rolename = rolename;
            return View();
        }
        //
        // GET: /SysFun/Create
        [CheckLogin]
        public ActionResult FunList()
        {
            var sysfun = DbContext.SysFunDb.OrderBy(s => s.FunID).ToList();
            return View(sysfun);
        }
        [CheckLogin]
        public ActionResult Create()
        {
            var sysfun = new SysFun();
            return View(sysfun);
        } 

        //
        // POST: /SysFun/Create

        [HttpPost]
        public ActionResult Create(SysFun sysfun)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbContext.SysFunDb.Add(sysfun);
                    DbContext.SaveChanges();
                    return RedirectToAction("FunList");
                }
                return View(sysfun);
            }
            catch
            {
                return View();
            }
        }
        //
        // GET: /SysFun/Delete/5
        [CheckLogin]
        public ActionResult Delete(int id)
        {
            var sysfun = DbContext.SysFunDb.First(f => f.FunID == id);
            return View(sysfun);
        }

        //
        // POST: /SysFun/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var sysfun = DbContext.SysFunDb.First(f => f.FunID == id);
            try
            {
                // TODO: Add delete logic here
                DbContext.SysFunDb.Remove(sysfun);
                DbContext.SaveChanges();
                return RedirectToAction("FunList");
            }
            catch
            {
                return View(sysfun);
            }
        }
    }
}
