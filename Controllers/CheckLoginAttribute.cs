using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class CheckLoginAttribute : ActionFilterAttribute,IAuthorizationFilter
    {
        public CheckLoginAttribute()
        {
            
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 处理方法
            if (filterContext.HttpContext.Session["UserID"] == null)
            {
                ErrorRedirect(filterContext);
            }
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] == null)
            {
                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Account", action = "Login" }), false);
                return;
            }
            if (filterContext.HttpContext.Session["UserID"].ToString() == "admin" || filterContext.HttpContext.Session["RoleName"].ToString().IndexOf("管理员") > -1)
            {
                return;
            }
            else
            {
                string acname = filterContext.ActionDescriptor.ActionName;
                string corname = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string userid = filterContext.HttpContext.Session["UserID"].ToString();
                string rolename = filterContext.HttpContext.Session["RoleName"].ToString();
                if (filterContext.HttpContext.Session["authlist"] == null)
                {
                    WebMemberDBContext DBContext = new WebMemberDBContext();
                    var FunList = (from d in DBContext.V_SysFunRole where d.RoleName == rolename select d).ToList();
                    //保存用户权限到seesion
                    filterContext.HttpContext.Session["authlist"] = FunList;
                }
                var funlist = filterContext.HttpContext.Session["authlist"] as IList<V_SysFunRole>;
                if ((from c in funlist where c.ActionName == acname && c.ControlName == corname && c.IsUse == 0 select c).ToList().Count > 0)
                {   //没有设置权限，或者无权限，跳转页面
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "Author",
                        ViewData = filterContext.Controller.ViewData,
                    };
                }
                else
                {
                    string actionnamestr = "Index";  //把需要控制权限的页面Action保存到配置文件中
                    if (ConfigurationManager.AppSettings["ActionName"].ToString() != string.Empty)
                    {
                        actionnamestr = ConfigurationManager.AppSettings["ActionName"].ToString();
                    }
                    if (actionnamestr.IndexOf(acname) > -1)
                    {
                        var fl = (from c in funlist where c.ActionName == acname && c.ControlName == corname select c).ToList();
                        if (fl.Count > 0)
                        {
                            var funparentid = fl.First().FunID;  //保存请求页按钮、连接权限
                            filterContext.Controller.TempData["qx"] = (from a in funlist where a.ParentID == funparentid select a).ToList();
                        }
                    }
                }
            }
        }
        // 错误处理方法
        private void ErrorRedirect(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Account", action = "Login" }), false);
        }
    }
}