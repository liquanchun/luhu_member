﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using MvcMember.Models;
using Webdiyer.WebControls.Mvc;
namespace MvcMember.Controllers
{
    public class MemberController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        ClsSys clssys = new ClsSys();
        //
        // GET: /Member/
        [CheckLogin]
        public ActionResult Index()
        {
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            if (this.Request.Form.AllKeys.Length > 0)
            {
                searchConditions.Add("Date1", Request["Date1"]);
                searchConditions.Add("Date2", Request["Date2"]);
                searchConditions.Add("Name", Request["Name"]);
                searchConditions.Add("Mobile", Request["Mobile"]);
                searchConditions.Add("CarNo", Request["CarNo"]);
                searchConditions.Add("VinNo", Request["VinNo"]);
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
            string Name = GetSearchConditionValue(searchConditions, "Name");
            string Mobile = GetSearchConditionValue(searchConditions, "Mobile");
            string CarNo = GetSearchConditionValue(searchConditions, "CarNo");
            string VinNo = GetSearchConditionValue(searchConditions, "VinNo");
            DateTime dt1 = string.IsNullOrEmpty(Date1) ? DateTime.Today.AddYears(-10) : DateTime.Parse(Date1);
            DateTime dt2 = string.IsNullOrEmpty(Date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(Date2).AddDays(1);
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(CarNo) && string.IsNullOrEmpty(VinNo) && string.IsNullOrEmpty(Date1))
            {
                dt1 = DateTime.Today.AddYears(-10);
            }
            var dmsid = Session["DmsID"].ToString();
            var result = (from m in DBContext.V_MemberDb
                          where m.DmsID == dmsid && (string.IsNullOrEmpty(Name.Trim()) || m.Name.StartsWith(Name.Trim()))
                          && (string.IsNullOrEmpty(Mobile.Trim()) || m.Mobile.StartsWith(Mobile.Trim()))
                          && (string.IsNullOrEmpty(CarNo.Trim()) || m.CarNO.Trim().EndsWith(CarNo.Trim()))
                          && (string.IsNullOrEmpty(VinNo.Trim()) || m.VinNO.StartsWith(VinNo.Trim()))
                          && m.CreateDate.CompareTo(dt1) >= 0 && m.CreateDate.CompareTo(dt2) < 0
                          select m).ToList().OrderByDescending(o => o.CreateDate);
            this.ViewData.Model = result;
            ViewBag.UserID = Session["UserID"].ToString();
            //等待审核
            ViewBag.AudiCount = DBContext.MemberUpdateDb.Where(S => S.Auditor == null).Count();
            CommonCls.WriteSysLog("查看客户资料", Request.RawUrl, this.Session["UserName"].ToString());
            return View();
        }
        private static string GetSearchConditionValue(IDictionary<string, string> searchConditions, string key)
        {
            string tempValue = string.Empty;
            if (searchConditions != null && searchConditions.Count>0)
            {
                searchConditions.TryGetValue(key, out tempValue);
            }
            return tempValue;
        }
        //
        // GET: /Member/Details/5
        [CheckLogin]
        public ActionResult AudiMember(int? id)
        {
            int defaultPageSize = 15;
            //var result = (from m in DBContext.MemberUpdateDb
            //              where m.Auditor == null
            //              select m).ToList().OrderByDescending(o => o.CreateDate);
            PagedList<MemberUpdate> memberlst = (from m in DBContext.MemberUpdateDb select m).OrderByDescending(o => o.CreateDate).ToPagedList(id ?? 1, defaultPageSize);
            //for(int i=0;i<memberlst.Count;i++)
            //{
            //    int memberid = memberlst[i].MemberID;
            //    var update = DBContext.V_MemberUpdateDb.First(f => f.MemberID == memberid);
            //    if (memberlst[i].Address == update.Address) memberlst[i].Address = "";
            //    if (memberlst[i].Birthday == update.Birthday) memberlst[i].Birthday = "";
            //    if (memberlst[i].CarNO == update.CarNO) memberlst[i].CarNO = "";
            //    if (memberlst[i].CarOwner == update.CarOwner) memberlst[i].CarOwner = "";
            //    if (memberlst[i].CarType == update.CarType) memberlst[i].CarType = "";
            //    if (memberlst[i].ClientType == update.ClientType) memberlst[i].ClientType = "";
            //    if (memberlst[i].District == update.District) memberlst[i].District = "";
            //    if (memberlst[i].Email == update.Email) memberlst[i].Email = "";
            //    if (memberlst[i].Hobby == update.Hobby) memberlst[i].Hobby = "";
            //    if (memberlst[i].IDCard == update.IDCard) memberlst[i].IDCard = "";
            //    if (memberlst[i].InsureExpire == update.InsureExpire) memberlst[i].InsureExpire = null;
            //    if (memberlst[i].LicenseExpire == update.LicenseExpire) memberlst[i].LicenseExpire = null;
            //    if (memberlst[i].Mobile == update.Mobile) memberlst[i].Mobile = "";
            //    if (memberlst[i].Name == update.Name) memberlst[i].Name = "";
            //    if (memberlst[i].NextMaintainDate == update.NextMaintainDate) memberlst[i].NextMaintainDate = null;
            //    if (memberlst[i].NextMaintainKM == update.NextMaintainKM) memberlst[i].NextMaintainKM = 0;
            //    if (memberlst[i].Postcode == update.Postcode) memberlst[i].Postcode = "";
            //    if (memberlst[i].Remark == update.Remark) memberlst[i].Remark = "";
            //    if (memberlst[i].RunKM == update.RunKM) memberlst[i].RunKM = 0;
            //    if (memberlst[i].Sex == update.Sex) memberlst[i].Sex = "";

            //    if (memberlst[i].VinNO == update.Name) memberlst[i].Name = "";
            //    if (memberlst[i].YearCheckDate == update.YearCheckDate) memberlst[i].Name = "";

            //}
            ViewBag.UserName = Session["UserName"].ToString();
            this.ViewData.Model = memberlst;
            return View();
        }
        //
        // GET: /Member/Details/5
        [CheckLogin]
        public ActionResult Details(int id)
        {
            var v_mb = DBContext.V_MemberDb.First(f => f.MemberID == id);
            return View(v_mb);
        }
        [CheckLogin]
        public ActionResult AllDetail(int? id)
        {
            var v_mb =new V_Member();
            if (id.HasValue)
            {
                var cardinfo = DBContext.CardInfoDb.FirstOrDefault(f => f.MemberID == id);
                ViewBag.cardno =cardinfo != null ? cardinfo.CardNo : "";
            }
            else
            {
                ViewBag.cardno = "";
            }
            return View(v_mb);
        }
        
        //
        // GET: /Member/Create
        [CheckLogin]
        public ActionResult Create()
        {
            SetValue();
            var v_members = new V_Member();
            return View(v_members);
        }
        [CheckLogin]
        public ActionResult Remind()
        {
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            if (this.Request.Form.AllKeys.Length > 0)
            {
                searchConditions.Add("Name", Request["Name"]);
                searchConditions.Add("Mobile", Request["Mobile"]);
                searchConditions.Add("CarNo", Request["CarNo"]);
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
            string Name = GetSearchConditionValue(searchConditions, "Name");
            string Mobile = GetSearchConditionValue(searchConditions, "Mobile");
            string CarNo = GetSearchConditionValue(searchConditions, "CarNo");
           
            var dmsid = Session["DmsID"].ToString();
            var result = (from m in DBContext.V_MemberDb
                          where m.DmsID == dmsid && (string.IsNullOrEmpty(Name.Trim()) || m.Name.StartsWith(Name.Trim()))
                          && (string.IsNullOrEmpty(Mobile.Trim()) || m.Mobile.StartsWith(Mobile.Trim()))
                          && (string.IsNullOrEmpty(CarNo.Trim()) || m.CarNO.Trim().EndsWith(CarNo.Trim()))
                          select m).ToList().OrderByDescending(o => o.CreateDate);
            this.ViewData.Model = result;
            return View();
        }

        [CheckLogin]
        public ActionResult Smssend()
        {
            IDictionary<string, string> searchConditions = new Dictionary<string, string>();
            if (this.Request.Form.AllKeys.Length > 0)
            {
                searchConditions.Add("Date1", Request["Date1"]);
                searchConditions.Add("Date2", Request["Date2"]);
                searchConditions.Add("Name", Request["Name"]);
                searchConditions.Add("Mobile", Request["Mobile"]);
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
            string Name = GetSearchConditionValue(searchConditions, "Name");
            string Mobile = GetSearchConditionValue(searchConditions, "Mobile");
            DateTime dt1 = string.IsNullOrEmpty(Date1) ? DateTime.Today.AddYears(-10) : DateTime.Parse(Date1);
            DateTime dt2 = string.IsNullOrEmpty(Date2) ? DateTime.Today.AddDays(1) : DateTime.Parse(Date2).AddDays(1);
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(Date1))
            {
                dt1 = DateTime.Today.AddYears(-10);
            }
            var dmsid = Session["DmsID"].ToString();
            var result = (from m in DBContext.SmssendDb
                          where (string.IsNullOrEmpty(Name.Trim()) || m.Receiver.StartsWith(Name.Trim()))
                          && (string.IsNullOrEmpty(Mobile.Trim()) || m.Mobile.StartsWith(Mobile.Trim()))
                          && m.CreateTime.CompareTo(dt1) >= 0 && m.CreateTime.CompareTo(dt2) < 0
                          select m).ToList().OrderByDescending(o => o.CreateTime);
            this.ViewData.Model = result;
            ViewBag.UserID = Session["UserID"].ToString();
            return View();
        }
        //
        // POST: /Member/Create

        [HttpPost]
        public ActionResult Create(V_Member v_member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (v_member.Remark != null && v_member.Remark.IndexOf("入会") > -1)
                    {
                        v_member.Remark = v_member.Remark.Replace("入会", "");
                    }
                    DBContext.MemberDb.Add(GetMembers(v_member));
                    DBContext.SaveChanges();

                    DBContext.Member2Db.Add(GetMembers2(v_member, true));
                    DBContext.SaveChanges();
                }
                CommonCls.WriteSysLog("新增客户资料，车牌号码:"+ v_member.CarNO +",客户姓名:"+ v_member.Name, Request.RawUrl, this.Session["UserName"].ToString());
                if (v_member.Remark != null && v_member.Remark.IndexOf("入会") > -1)
                {
                    var maxid = DBContext.MemberDb.Max(m => m.MemberID);
                    return Redirect("../CardInfo/Create/" + maxid);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch(Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.Message);
                SetValue();
                var v_members = new V_Member();
                return View(v_members);
            }
        }
        private Member GetMembers(V_Member v_member)
        {
            Member mbs = new Member();
            mbs.MemberID = v_member.MemberID;
            mbs.Address = v_member.Address;
            mbs.Birthday = v_member.Birthday;
            mbs.DmsID = Session["DmsID"].ToString();
            mbs.ClientType = v_member.ClientType;
            if (Session["UserName"] != null)
            {
                mbs.Creator = Session["UserName"].ToString();
            }
            mbs.DmsID = Session["DmsID"].ToString();
            mbs.CreateDate = DateTime.Now;
            mbs.District = v_member.District;
            mbs.Email = v_member.Email;
            mbs.Hobby = v_member.Hobby;
            mbs.IDCard = v_member.IDCard;
            mbs.Mobile = v_member.Mobile;
            mbs.Name = v_member.Name;
            mbs.Postcode = v_member.Postcode;
            mbs.Remark = v_member.Remark;
            mbs.Sex = v_member.Sex;
            mbs.LastComeDate = DateTime.Parse("1900-01-01");
            return mbs;
        }
        private Member2 GetMembers2(V_Member v_member,bool isnew)
        {
            var memberid = v_member.MemberID;
            if (isnew)
            {
                memberid = DBContext.MemberDb.Select(s => s.MemberID).Max();
            }
            Member2 mbs2 = new Member2();
            mbs2.MemberID = memberid;
            mbs2.CarNO = v_member.CarNO;
            mbs2.CarOwner = v_member.CarOwner;
            mbs2.CarType = v_member.CarType;
            mbs2.InsureExpire = v_member.InsureExpire;
            mbs2.LicenseExpire = v_member.LicenseExpire;
            mbs2.NextMaintainDate = v_member.NextMaintainDate;
            mbs2.NextMaintainKM = v_member.NextMaintainKM;
            mbs2.RunKM = v_member.RunKM;
            mbs2.VinNO = v_member.VinNO;
            mbs2.YearCheckDate = v_member.YearCheckDate;
            return mbs2;
        }
        private void SetValue()
        {
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "客户类型").First().ID;
            ViewBag.khlxList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();

            parentid = DBContext.ItemListDb.Where(w => w.ItemName == "车型").First().ID;
            ViewBag.cxList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();

            ViewBag.DmsID = Session["DmsID"].ToString();
            ViewBag.UserID = Session["UserName"].ToString();
        }
        //
        // GET: /Member/Edit/5
        [CheckLogin]
        public ActionResult Edit(int id)
        {
            var v_member = DBContext.V_MemberDb.First(s => s.MemberID == id);
            SetValue();
            return View(v_member);
        }

        //
        // POST: /Member/Edit/5

        [HttpPost]
        public ActionResult Edit(int id,V_Member v_member)
        {
            try
            {
                var mbs = DBContext.MemberDb.First(f => f.MemberID == id);
                //TryUpdateModel(mbs);
                //DBContext.SaveChanges();

                //var mb2 = DBContext.Member2Db.First(f => f.MemberID == id);
                //TryUpdateModel(mb2);
                //DBContext.SaveChanges();

                MemberUpdate memberupdate = new MemberUpdate();
                memberupdate.Address = v_member.Address;
                memberupdate.Birthday = v_member.Birthday;
                memberupdate.ClientType = v_member.ClientType;
                memberupdate.District = v_member.District;
                memberupdate.Email = v_member.Email;
                memberupdate.Hobby = v_member.Hobby;
                memberupdate.IDCard = v_member.IDCard;

                memberupdate.Mobile = v_member.Mobile;
                memberupdate.Name = v_member.Name;
                memberupdate.Postcode = v_member.Postcode;
                memberupdate.Remark = v_member.Remark;
                memberupdate.Sex = v_member.Sex;
                memberupdate.MemberID = mbs.MemberID;

                memberupdate.CreateDate = DateTime.Now;
                memberupdate.Creator = Session["UserID"].ToString();
                memberupdate.DmsID =  Session["DmsID"].ToString();
                memberupdate.VinNO = v_member.VinNO;
                memberupdate.CarNO = v_member.CarNO;
                memberupdate.CarOwner = v_member.CarOwner;
                memberupdate.CarType = v_member.CarType;
                memberupdate.RunKM = v_member.RunKM;
                memberupdate.NextMaintainKM = v_member.NextMaintainKM;
                memberupdate.NextMaintainDate = v_member.NextMaintainDate;
                memberupdate.LicenseExpire = v_member.LicenseExpire;
                memberupdate.InsureExpire = v_member.InsureExpire;
                memberupdate.YearCheckDate = v_member.YearCheckDate;

                DBContext.MemberUpdateDb.Add(memberupdate);
                DBContext.SaveChanges();
                CommonCls.WriteSysLog("修改客户资料，车牌号码：" + v_member.CarNO + ",客户姓名:" + v_member.Name, Request.RawUrl, this.Session["UserName"].ToString());
                return RedirectToAction("Index");
            }
            catch(Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.Message);
                SetValue();
                var v_members = new V_Member();
                return View(v_members);
            }
        }

        public ActionResult MemType()
        {
            return View();
        }
        public ActionResult R_MemberStat()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CheckMobileExists(string mobile)
        {
            bool exists = DBContext.MemberDb.Where(w => w.Mobile == mobile).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckNameExists(string name)
        {
            bool exists = DBContext.MemberDb.Where(w => w.Name == name).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckVinNOExists(string vinno)
        {
            bool exists = DBContext.Member2Db.Where(w => w.VinNO == vinno).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CheckCarNOExists(string carno)
        {
            bool exists = DBContext.Member2Db.Where(w => w.CarNO == carno).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
    }
}
