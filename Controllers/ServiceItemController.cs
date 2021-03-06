﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMember.Models;
namespace MvcMember.Controllers
{
    public class ServiceItemController : Controller
    {
        WebMemberDBContext DBContext = new WebMemberDBContext();
        ClsSys clssys = new ClsSys();
        //
        // GET: /Employee/
        [CheckLogin]
        public ActionResult Index()
        {
            var dmsid = Session["DmsID"].ToString();
            var serviceitem = DBContext.ServiceItemDb.Where(w =>w.DmsID == dmsid).ToList();

            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "服务项目类型").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();

            ViewBag.dmsid = dmsid;
            ViewBag.userid = this.Session["UserName"].ToString();
            CommonCls.WriteSysLog("查看服务项目", Request.RawUrl, this.Session["UserName"].ToString());
            return View(serviceitem);
        }

        //
        // GET: /Employee/Details/5
        [CheckLogin]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Employee/Create
        [CheckLogin]
        public ActionResult Create()
        {
             var dmsid = Session["DmsID"].ToString();
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "服务项目类型").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            ViewBag.SerLst = DBContext.ServiceItemDb.Where(s => s.ItemType == "单项服务" && s.DmsID == dmsid).ToList();
            var serviceitem = new ServiceItem();
            return View(serviceitem);
        } 

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(ServiceItem serviceitem)
        {
            try
            {
                // TODO: Add insert logic here
                serviceitem.Creator = this.Session["UserName"].ToString();
                serviceitem.CreateDate = DateTime.Now;
                serviceitem.DmsID = Session["DmsID"].ToString();
                string taocanstr = Request.Form.Get("taocanstr");
                if (ModelState.IsValid)
                {
                    DBContext.ServiceItemDb.Add(serviceitem);
                    DBContext.SaveChanges();
                    //保存套餐项目
                    //if (taocanstr != string.Empty)
                    //{
                    //    if (taocanstr.IndexOf("|") > -1)
                    //    {
                    //        string[] servicetype = taocanstr.Split('|');
                    //        for (int i = 0; i < servicetype.Length; i++)
                    //        {
                    //            SaveTaocanItem(servicetype[i], serviceitem.ItemID);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        SaveTaocanItem(taocanstr, serviceitem.ItemID);
                    //    }
                    //}
                    CommonCls.WriteSysLog("新增服务项目：" + serviceitem.ItemName, Request.RawUrl, this.Session["UserName"].ToString());
                    return RedirectToAction("Index");
                }
                return View(serviceitem);
            }
            catch(Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.Message);
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "服务项目类型").First().ID;
                ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
                return View();
            }
        }
        private int SaveTaocanItem(string strval, string itemid)
        {
            string sqlstr = string.Empty;
            int RetVal = 0;
            string[] fileds = strval.Split(',');
            try
            {
                ServiceItem2 si2 = new ServiceItem2();
                si2.ItemID = itemid;
                si2.ChildItemID = fileds[0];
                si2.Times = Convert.ToInt32(fileds[2]);
                DBContext.ServiceItem2Db.Add(si2);
                DBContext.SaveChanges();
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.Message);
            }
            return RetVal;
        }
        //
        // GET: /Employee/Edit/5
        [CheckLogin]
        public ActionResult Edit(int id)
        {
            int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "服务项目类型").First().ID;
            ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
            var serviceitem = DBContext.ServiceItemDb.First(e => e.ID == id);
            //if (serviceitem.ItemType == "套餐服务")
            //{
            //    ViewBag.taocan = DBContext.ServiceItem2Db.Where(s => s.ItemID == serviceitem.ItemID).ToList();
            //    ViewBag.SerLst = DBContext.ServiceItemDb.Where(s => s.ItemType == "单项服务").ToList();
            //}
            return View(serviceitem);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var serviceitem = DBContext.ServiceItemDb.First(e => e.ID == id);
                serviceitem.CreateDate = DateTime.Now;
                serviceitem.Creator = this.Session["UserName"].ToString();
                if (TryUpdateModel(serviceitem))
                {
                    DBContext.SaveChanges();
                    CommonCls.WriteSysLog("修改服务项目：" + serviceitem.ItemName, Request.RawUrl, this.Session["UserName"].ToString());
                    return RedirectToAction("Index");
                }
                int parentid = DBContext.ItemListDb.Where(w => w.ItemName == "服务项目类型").First().ID;
                ViewBag.ItemList = DBContext.ItemListDb.Where(w => w.ItemParent == parentid).ToList();
                return View(serviceitem);
            }
            catch(Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.Message);
                return View();
            }
        }
        [HttpGet]
        public ActionResult CheckItemNameExists(string itemname)
        {
            bool exists = DBContext.ServiceItemDb.Where(w => w.ItemName == itemname).Count() > 0 ? true : false;
            exists = HttpContext.Request.UrlReferrer.AbsoluteUri.Contains("Edit") == true ? false : exists;
            return Json(!exists, JsonRequestBehavior.AllowGet);
        }
    }
}
