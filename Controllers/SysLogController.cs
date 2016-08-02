using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class SysLogController : Controller
    {
        //
        // GET: /SysLog/
        WebMemberDBContext DBContext = new WebMemberDBContext();
        [CheckLogin]
        public ActionResult Index()
        {
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            if (this.Request.Form.AllKeys.Length > 0)
            {
                searchConditions.Add("Date1", Request["Date1"]);
                searchConditions.Add("Date2", Request["Date2"]);
                searchConditions.Add("keyword", Request["keyword"]);
                searchConditions.Add("Creator", Request["Creator"]);
            }
            else
            {
                object values = null;
                if (this.TempData.TryGetValue("SearchConditions", out values))
                {
                    searchConditions = values as Dictionary<string, string>;
                }
            }
            this.TempData["SearchConditions"] = searchConditions;
            string Date1 = GetSearchConditionValue(searchConditions, "Date1");
            string Date2 = GetSearchConditionValue(searchConditions, "Date2");
            string keyword = GetSearchConditionValue(searchConditions, "keyword");
            string Creator = GetSearchConditionValue(searchConditions, "keywCreatorord");
            DateTime dt1 = string.IsNullOrEmpty(Date1) ? DateTime.Today.AddYears(-10) : DateTime.Parse(Date1);
            DateTime dt2 = string.IsNullOrEmpty(Date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(Date2).AddDays(1);
            if (string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(Creator) && string.IsNullOrEmpty(Date1))
            {
                dt1 = DateTime.Today.AddYears(-10);
            }
            var dmsid = Session["DmsID"].ToString();
            var result = (from c in DBContext.SysLogDb
                          where c.DmsID == dmsid && (string.IsNullOrEmpty(keyword.Trim()) || c.Message.Contains(keyword.Trim()))
                          && (string.IsNullOrEmpty(Creator.Trim()) || c.Creator.StartsWith(Creator.Trim()))
                          && c.CreateDate.CompareTo(dt1) >= 0 && c.CreateDate.CompareTo(dt2) < 0
                          select c).ToList().OrderByDescending(o => o.CreateDate);
            this.ViewData.Model = result;
            return View(result);
        }
        private static string GetSearchConditionValue(IDictionary<string, string> searchConditions, string key)
        {
            string tempValue = string.Empty;
            if (searchConditions != null && searchConditions.Count > 0)
            {
                searchConditions.TryGetValue(key, out tempValue);
            }
            return tempValue;
        }
    }
}
