using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace MvcMember.Controllers.Filters
{
    public class LoggerFilter : FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["ExecutingLogger"] = "正要添加公告，已以写入日志！时间：" + DateTime.Now; 
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["ExecutedLogger"] = "公告添加完成，已以写入日志！时间：" + DateTime.Now;
        }

    }
}