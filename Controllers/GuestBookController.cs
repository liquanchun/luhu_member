using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
using MvcMember.Controllers.Filters;
using Webdiyer.WebControls.Mvc;
namespace MvcMember.Controllers
{
    public class GuestBookController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        //
        // GET: /GuestBook/
        [LoggerFilter()]
        [CheckLogin]
        public ActionResult Index(int? id)
        {
            //var guestbk = DBContext.GuestBookDb.OrderByDescending(G => G.LTime).ToList();
            //ViewBag.guestbk = guestbk;

            PagedList<GuestBook> m = (from c in DBContext.GuestBookDb
                                         select c).OrderByDescending(o => o.LTime).ToPagedList(id ?? 1, 7);
            ViewBag.username = Session["UserName"].ToString();
            return View(m);
        }
        //
        // GET: /GuestBook/Create
        [CheckLogin]
        public ActionResult Create()
        {
            var guestbk = new GuestBook();
            return View(guestbk);
        } 

        //
        // POST: /GuestBook/Create

        [HttpPost]
        public ActionResult Create(GuestBook gesbk)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    gesbk.LTime = DateTime.Now;
                    if(Session["UserID"] != null)
                    gesbk.LUser = Session["UserID"].ToString();
                    DBContext.GuestBookDb.Add(gesbk);
                    DBContext.SaveChanges();
                    return RedirectToAction("Index");
                    // TODO: Add insert logic here
                }
                return View();
               
            }
            catch
            {
                return View();
            }
        }
    }
}
