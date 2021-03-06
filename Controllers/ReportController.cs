﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using MvcMember.Models;
using Webdiyer.WebControls.Mvc;
namespace MvcMember.Controllers
{
    public class ReportController : Controller
    {
        SqlDbHelper sqldb = new SqlDbHelper();
        ClsSys clssys = new ClsSys();
        //
        // GET: /Report/
        [CheckLogin]
        public ActionResult Index()
        {
            return View();
        }
        //会员积分储值汇总表
        [CheckLogin]
        public ActionResult CardInteStat(int? id)
        {
            int p = id ?? 1;
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            searchConditions.Add("Date1", Request["Date1"]);
            searchConditions.Add("Date2", Request["Date2"]);
            this.TempData["SearchConditions"] = searchConditions;
            PagedList<DataRow> paged = GetData(Request["Date1"], Request["Date2"], "R_CardInteStat", p);
            ViewBag.sum1 = paged.Sum(s => (int)s[4]);
            ViewBag.sum2 = paged.Sum(s => (int)s[5]);
            ViewBag.sum3 = paged.Sum(s => (int)s[6]);
            ViewBag.sum4 = paged.Sum(s => (int)s[7]);
            ViewBag.sum5 = paged.Sum(s => (int)s[8]);

            ViewBag.sum6 = paged.Sum(s => (int)s[9]);
            ViewBag.sum7 = paged.Sum(s => (int)s[10]);
            ViewBag.sum8 = paged.Sum(s => (int)s[11]);
            ViewBag.sum9 = paged.Sum(s => (int)s[12]);
            ViewBag.sum10 = paged.Sum(s => (int)s[13]);
            ViewBag.sum11 = paged.Sum(s => (int)s[14]);
            return View(paged);
        }
        //消费结算明细一览表
        [CheckLogin]
        public ActionResult ConsumeList(int? id)
        {
            int p = id ?? 1;
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            searchConditions.Add("Date1", Request["Date1"]);
            searchConditions.Add("Date2", Request["Date2"]);
            searchConditions.Add("VinNo", Request["VinNo"]);
            searchConditions.Add("CarNo", Request["CarNo"]);
            searchConditions.Add("CardNo", Request["CardNo"]);
            this.TempData["SearchConditions"] = searchConditions;
            PagedList<DataRow> paged = GetData2(Request["Date1"], Request["Date2"],Request["CardNo"],Request["CarNo"], Request["VinNo"],"R_ConsumeList", p);
            ViewBag.sum1 = paged.Sum(s => (int)s[5]);
            ViewBag.sum2 = paged.Sum(s => (int)s[6]);
            ViewBag.sum3 = paged.Sum(s => (int)s[7]);
            ViewBag.sum4 = paged.Sum(s => (int)s[8]);
            ViewBag.sum5 = paged.Sum(s => (int)s[9]);
            ViewBag.sum6 = paged.Sum(s => (int)s[10]);

            ViewBag.sum7 = paged.Sum(s => (int)s[11]);
            ViewBag.sum8 = paged.Sum(s => (int)s[12]);
            ViewBag.sum9 = paged.Sum(s => (int)s[13]);
            ViewBag.sum10 = paged.Sum(s => (int)s[14]);
            ViewBag.sum11= paged.Sum(s => (int)s[15]);
            ViewBag.sum12 = paged.Sum(s => (int)s[16]);
            ViewBag.sum13 = paged.Sum(s => (int)s[17]);
            return View(paged);
        }
        //储值明细表
        [CheckLogin]
        public ActionResult RechargeList(int? id)
        {
            int p = id ?? 1;
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            searchConditions.Add("Date1", Request["Date1"]);
            searchConditions.Add("Date2", Request["Date2"]);
            this.TempData["SearchConditions"] = searchConditions;
            PagedList<DataRow> paged = GetData(Request["Date1"], Request["Date2"], "R_RechargeList", p);
            ViewBag.sum1 = paged.Sum(s => (int)s[5]);
            return View(paged);
        }

        //储值消费明细表
        [CheckLogin]
        public ActionResult RechargeConsumeList(int? id)
        {
            int p = id ?? 1;
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            searchConditions.Add("Date1", Request["Date1"]);
            searchConditions.Add("Date2", Request["Date2"]);
            this.TempData["SearchConditions"] = searchConditions;
            PagedList<DataRow> paged = GetData(Request["Date1"], Request["Date2"], "R_RechargeConsumeList", p);
            ViewBag.sum1 = paged.Sum(s => (int)s[5]);
            ViewBag.sum2 = paged.Sum(s => (int)s[6]);
            return View(paged);
        }
        //调整积分明细表
        [CheckLogin]
        public ActionResult AdjustInteList(int? id)
        {
            int p = id ?? 1;
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            searchConditions.Add("Date1", Request["Date1"]);
            searchConditions.Add("Date2", Request["Date2"]);
            this.TempData["SearchConditions"] = searchConditions;
            PagedList<DataRow> paged = GetData(Request["Date1"], Request["Date2"], "R_AdjustInteList", p);
            ViewBag.sum1 = paged.Sum(s => (int)s[5]);
            return View(paged);
        }
        //会员卡积分与储值余额统计报表
        [CheckLogin]
        public ActionResult CardInteRechargeStat(int? id)
        {
            int p = id ?? 1;
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            searchConditions.Add("Date1", Request["Date1"]);
            searchConditions.Add("Date2", Request["Date2"]);
            this.TempData["SearchConditions"] = searchConditions;
            PagedList<DataRow> paged = GetData(Request["Date1"], Request["Date2"], "R_CardInteRechargeStat", p);
            ViewBag.sum1 = paged.Sum(s => (int)s[4]);
            ViewBag.sum2 = paged.Sum(s => (int)s[5]);
            ViewBag.sum3 = paged.Sum(s => (int)s[6]);
            ViewBag.sum4 = paged.Sum(s => (int)s[7]);
            return View(paged);
        }
        private PagedList<DataRow> GetData2(string date1, string date2,string cardno,string carno,string vinno, string spName, int id)
        {
            int defaultPageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());

            DateTime dt1 = string.IsNullOrEmpty(date1) ? DateTime.Today : DateTime.Parse(date1);
            DateTime dt2 = string.IsNullOrEmpty(date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(date2).AddDays(1);

            string sqlstring = string.Empty;
            string dmsid = Session["DmsID"].ToString();
            sqlstring = " exec " + spName + " '" + dt1 + "','" + dt2 + "','" + cardno + "','" + carno + "','" + vinno + "','" + dmsid + "'";
            DataTable Dt = sqldb.ExecuteDataTable(sqlstring);
            IList<DataRow> list = new List<DataRow>(Dt.Select());
            PagedList<DataRow> paged = new PagedList<DataRow>(list, id, defaultPageSize);
            return paged;
        }
        private PagedList<DataRow> GetData(string date1, string date2, string spName, int id)
        {
            int defaultPageSize =int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());

            DateTime dt1 = string.IsNullOrEmpty(date1) ? DateTime.Today : DateTime.Parse(date1);
            DateTime dt2 = string.IsNullOrEmpty(date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(date2).AddDays(1);

            string sqlstring = string.Empty;
            string dmsid = Session["DmsID"].ToString();
            sqlstring = " exec " + spName + " '" + dt1 + "','" + dt2 + "','" + dmsid + "'";
            DataTable Dt = sqldb.ExecuteDataTable(sqlstring);
            IList<DataRow> list = new List<DataRow>(Dt.Select());
            PagedList<DataRow> paged = new PagedList<DataRow>(list, id, defaultPageSize);
            return paged;
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
