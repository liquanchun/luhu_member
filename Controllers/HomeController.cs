using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class HomeController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        ClsSys clssys = new ClsSys();
        public ActionResult Index()
        {
            if (Request.Url.Segments.Count() == 5)
            {
                string uid = Request.Url.Segments[4].ToString();
                if (uid != "loading.gif")
                {
                    var users = DBContext.AdminUserDb.First(f => f.UserID == uid);
                    if (users != null)
                    {
                        Session["DmsID"] = users.DmsID;
                        Session["RoleName"] = users.RoleName;
                        Session["UserName"] = users.UserName;
                        Session["UserID"] = users.UserID;
                    }
                }
                ViewBag.Message = "欢迎你使用汽车会员管理系统!";
                CommonCls.WriteSysLog("用户登录成功！", Request.RawUrl, this.Session["UserName"].ToString());
                return View();
            }
            else
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.Message = "欢迎你使用汽车会员管理系统!";
                    return View();
                }
            }
        }
        public ActionResult PartialJsCache()
        {
            return PartialView();
        }
        public ActionResult PartialLogOnCache()
        {
            var dmsid = Session["DmsID"].ToString();
            var dmsname = DBContext.BranchDb.Where(w => w.DmsID == dmsid).FirstOrDefault().Name;
            ViewBag.dmsname = dmsname;
            return PartialView();
        }
        public ActionResult PartialMenuCache()
        {
            return PartialView();
        }
        [CheckLogin]
        public ActionResult About()
        {
            return View();
        }
    }
}
