using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class SysSettingController : Controller
    {
        //
        // GET: /SysSetting/
        [CheckLogin]
        public ActionResult Index()
        {
            WebMemberDBContext DBContext = new WebMemberDBContext();
            var itemlist = DBContext.ItemListDb.ToList();
            return View(itemlist);
        }
        [CheckLogin]
        public ActionResult Printer()
        {
            return View();
        }
    }
}
