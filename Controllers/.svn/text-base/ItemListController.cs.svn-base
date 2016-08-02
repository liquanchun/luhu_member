using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class ItemListController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        //
        // GET: /ItemList/
        [CheckLogin]
        public ActionResult Index()
        {
            var itemlist = DBContext.ItemListDb.ToList();
            return View(itemlist);
        }
    }
}
