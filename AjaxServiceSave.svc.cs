﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Configuration;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using MvcMember.Controllers;
using MvcMember.Models;
namespace MvcMember
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxServiceSave
    {
        // 要使用 HTTP GET，请添加 [WebGet] 特性。(默认 ResponseFormat 为 WebMessageFormat.Json)
        // 要创建返回 XML 的操作，
        //     请添加 [WebGet(ResponseFormat=WebMessageFormat.Xml)]，
        //     并在操作正文中包括以下行:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        ClsSys clssys = new ClsSys();
        private int PareToInt(string val)
        {
            if (val == string.Empty) return 0;
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return 0;
            }
        }
        [OperationContract]
        public void DoWork()
        {
            // 在此处添加操作实现
            return;
        }
        //保存充值
        [OperationContract]
        public int SaveRecharge(string memberid, string cardno, string balance, string giveinte, string inmoney, string intype, string remark, string selitemid, string times,string givemoney, string userid,string dmsid)
        {
            int RetVal = 0;
            try
            {
                WebMemberDBContext DBContext = new WebMemberDBContext();
                Recharge recharge = new Recharge();
                recharge.Balance = PareToInt(balance);
                recharge.GiveInte = PareToInt(giveinte);
                recharge.GiveMoney = PareToInt(givemoney);
                recharge.InMoney = PareToInt(inmoney);
                recharge.InType = intype;
                recharge.MemberID = PareToInt(memberid);
                recharge.Remark = remark;
                recharge.CreateDate = DateTime.Now;
                recharge.Creator = userid;
                recharge.DmsID = dmsid;
                string ServiceItemID = selitemid;
                if (ServiceItemID != string.Empty && ServiceItemID != "-1")
                {
                    recharge.GiveServiceID = ServiceItemID;
                }
                DBContext.RechargeDb.Add(recharge);
                RetVal = DBContext.SaveChanges();
                if (ServiceItemID != string.Empty && ServiceItemID != "-1" && times != string.Empty && PareToInt(times) > 0)
                {
                    //单项服务
                    CardServiceItem cardserviceitem = new CardServiceItem();
                    cardserviceitem.ID = -1;
                    cardserviceitem.CardNo = cardno;
                    cardserviceitem.MemberID = PareToInt(memberid);
                    cardserviceitem.Creator = userid;
                    cardserviceitem.CreateDate = DateTime.Now;
                    cardserviceitem.ServiceItemID = ServiceItemID;
                    cardserviceitem.Times = PareToInt(times);
                    cardserviceitem.SubTimes = PareToInt(times);
                    cardserviceitem.Fee = 0;
                    cardserviceitem.PayType = "储值赠送";
                    cardserviceitem.Remark = "储值赠送";
                    cardserviceitem.ServiceType = "单项服务";
                    DBContext.CardServiceItemDb.Add(cardserviceitem);
                    RetVal = DBContext.SaveChanges();
                }

            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //保存用户ID
        [OperationContract]
        public int SaveAdminUser(string dmsid, string userid, string username, string password, string rolename)
        {
            int RetVal = 0;
            WebMemberDBContext DBContext = new WebMemberDBContext();
            if (DBContext.AdminUserDb.Count() > 0 && DBContext.AdminUserDb.Where(w => w.UserID == userid && w.UserName == username.Trim()).Count() > 0)
            {
                return 2;
            }
            AdminUser adminuser = new AdminUser();
            adminuser.DmsID = dmsid;
            adminuser.Password = password;
            adminuser.RoleName = rolename;
            adminuser.UserID = userid;
            adminuser.UserName = username;
            try
            {
                DBContext.AdminUserDb.Add(adminuser);
                RetVal = DBContext.SaveChanges();
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public int UpdateMember(string name, string clienttype, string sex, string mobile, string birthday, string address, string carno, string vinno, string cartype, string carowner, string userid, string memberid)
        {
            int RetVal = 0;
            string sqlstring = @"Update Member set name = '" + name + "',clienttype = '" + clienttype + "',sex = '" + sex + "',mobile = '" + mobile + "',birthday = '" + birthday + "',address = '" + address + "' where memberid=" + memberid;
            sqlstring += ";Update Member2 set carno='" + carno + "',vinno='" + vinno + "',carowner='" + carowner + "' Where MemberID="+ memberid;
            SqlDbHelper sqldb = new SqlDbHelper();
            try
            {
                RetVal = sqldb.ExecuteNonQuery(sqlstring);
            }
            catch (Exception Err)
            {

                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public int SaveMember(string name, string clienttype, string sex, string mobile, string birthday, string address, string carno, string vinno, string cartype,string carowner, string userid,string dmsid)
        {
            int RetVal = 0;
            WebMemberDBContext DBContext = new WebMemberDBContext();
            if (DBContext.MemberDb.Count() > 0 && DBContext.MemberDb.Where(w => w.Name == name.Trim()).Count() > 0 || DBContext.Member2Db.Where(w => w.CarNO == carno.Trim() || w.VinNO == vinno.Trim()).Count() > 0)
            {
                return 2;
            }
            Member mbs = new Member();
            mbs.Address = address;
            mbs.Birthday = birthday;
            mbs.DmsID = dmsid;
            mbs.ClientType = clienttype;
            mbs.Creator = userid;

            mbs.CreateDate = DateTime.Now;
            mbs.District = string.Empty;
            mbs.Email = string.Empty;
            mbs.Hobby = string.Empty;
            mbs.IDCard = string.Empty;
            mbs.Mobile = mobile;
            mbs.Name = name;
            mbs.Postcode = string.Empty;
            mbs.Remark = string.Empty;
            mbs.Sex = sex;
            mbs.LastComeDate = DateTime.Parse("1900-01-01");
            try
            {
                DBContext.MemberDb.Add(mbs);
                RetVal = DBContext.SaveChanges();

                int memberid = DBContext.MemberDb.Select(s => s.MemberID).Max();
                Member2 mbs2 = new Member2();
                mbs2.MemberID = memberid;
                mbs2.CarNO = carno;
                mbs2.CarOwner = carowner;
                mbs2.CarType = cartype;
                mbs2.InsureExpire = DateTime.Parse("1900-01-01");
                mbs2.LicenseExpire = DateTime.Parse("1900-01-01");
                mbs2.NextMaintainDate = DateTime.Parse("1900-01-01");
                mbs2.NextMaintainKM = 0;
                mbs2.RunKM = 0;
                mbs2.VinNO = vinno;
                mbs2.YearCheckDate = DateTime.Parse("1900-01-01");

                DBContext.Member2Db.Add(mbs2);
                RetVal = DBContext.SaveChanges();
                RetVal = memberid;
            }
            catch (Exception Err)
            {
                RetVal = 0;
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        // 在此处添加更多操作并使用 [OperationContract] 标记它们
        //保存会员卡信息
        [OperationContract]
        public int SaveCardInfo(string memberid,string cardno,string typeid,string iniintegral,string saleman,string cometype,string startdate,string enddate,string creator)
        {
            int RetVal = 0;
            WebMemberDBContext DBContext = new WebMemberDBContext();
            CardInfo cardinfo = new CardInfo();
            cardinfo.MemberID = PareToInt(memberid) ;
            //检查该客户是否已经发卡
            if (DBContext.CardInfoDb.Count() > 0 && DBContext.CardInfoDb.Where(f => f.MemberID == cardinfo.MemberID || f.CardNo == cardno).Select(f => f).Count() > 0)
            {
                return 2;
            }
            cardinfo.CardNo = cardno;
            cardinfo.InitialIntegral = PareToInt(iniintegral);
            cardinfo.SaleMan = saleman;
            cardinfo.StartDate = DateTime.Parse(startdate);
            cardinfo.TypeID = typeid;
            cardinfo.SumGiveIntegral = PareToInt(iniintegral);
            cardinfo.BalanceIntegral = PareToInt(iniintegral);
            cardinfo.EndDate = DateTime.Parse(enddate);
            cardinfo.ComeType = cometype;
            cardinfo.State = "正常";
            cardinfo.CreateDate = DateTime.Now;
            cardinfo.Creator = creator;
            try
            {
                DBContext.CardInfoDb.Add(cardinfo);
                RetVal = DBContext.SaveChanges();
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //保存服务项目
        [OperationContract]
        public int SaveServiceItem(string op, string dmsid, string itemid, string itemname, string itemtype, string price, string integral, string remark, string creator)
        {
            int RetVal = 0;
            WebMemberDBContext DBContext = new WebMemberDBContext();
            ServiceItem serviceitem = new ServiceItem();
            //检查该客户是否已经发卡
            if (op == "0" && DBContext.ServiceItemDb.Count()>0 && DBContext.ServiceItemDb.Where(f => f.ItemID == itemid || f.ItemName == itemname).Select(f => f).Count() > 0)
            {
                return 2;
            }
            try
            {
                if (op == "0")
                {
                    serviceitem.ID = -1;
                    serviceitem.Creator = creator;
                    serviceitem.CreateDate = DateTime.Now;
                    serviceitem.DmsID = dmsid;
                    serviceitem.ItemType = itemtype;
                    serviceitem.ItemName = itemname;
                    serviceitem.ItemID = itemid;
                    serviceitem.Integral = PareToInt(integral);
                    serviceitem.Price = PareToInt(price);
                    serviceitem.Remark = remark;
                    serviceitem.Number = 0;

                    DBContext.ServiceItemDb.Add(serviceitem);
                    RetVal = DBContext.SaveChanges();
                }
                else
                {
                    string sqlstring = @"Update ServiceItem Set ItemName='" + itemname + "',ItemType='" + itemtype + "',CreateDate='" + DateTime.Now + "',Integral=" + PareToInt(integral) + ",Price=" + PareToInt(price) +
                        ",Remark='" + remark + "',Creator='" + creator + "' Where ItemID = '"+ itemid +"'";
                    SqlDbHelper sqldb = new SqlDbHelper();
                    RetVal = sqldb.ExecuteNonQuery(sqlstring);
                }
                
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }

        //保存购买计次服务项目
        [OperationContract]
        public int SaveCardServiceItem(string memberid, string cardno, string serviceitemid,string servicetype, string times, string fee, string paytype, string remark, string creator)
        {
            int RetVal = 0;
            WebMemberDBContext DBContext = new WebMemberDBContext();
            CardServiceItem cardserviceitem = new CardServiceItem();
            //检查该客户是否已经发卡
            try
            {
                cardserviceitem.ID = -1;
                cardserviceitem.Creator = creator;
                cardserviceitem.CreateDate = DateTime.Now;
                cardserviceitem.PayType = paytype;
                cardserviceitem.MemberID =PareToInt(memberid);
                cardserviceitem.ServiceItemID = serviceitemid;
                cardserviceitem.ServiceType = servicetype;
                cardserviceitem.Times = PareToInt(times);
                cardserviceitem.SubTimes = PareToInt(times);
                cardserviceitem.Fee = PareToInt(fee);
                cardserviceitem.Remark = remark;
                cardserviceitem.CardNo = cardno;

                DBContext.CardServiceItemDb.Add(cardserviceitem);
                RetVal = DBContext.SaveChanges();
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public string GetCardTypeInfo(string cardtype)
        {
            string RetVal = "";
            string sqlString = "Select * from CardType where TypeID='"+ cardtype +"'";
            try
            {
                SqlDbHelper sqldb = new SqlDbHelper();
                DataTable dt = sqldb.ExecuteDataTable(sqlString);
                if (dt.Rows.Count > 0)
                {
                    RetVal = dt.Rows[0]["CardYears"].ToString() + "," + dt.Rows[0]["IniIntegral"].ToString();
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //保存会员卡级别定义
        [OperationContract]
        public int SaveCardType(string op, string dmsid, string typeid, string cardsort, string cardlevel, string cardyears, string hoursdis, string partdis, string initegral, string updateintegral, string nexttypeid)
        {
            int RetVal = 0;
            WebMemberDBContext DBContext = new WebMemberDBContext();
            CardType cardtype = new CardType();
            if (op == "0" && DBContext.CardTypeDb.Count() > 0 && DBContext.CardTypeDb.Where(f => f.TypeID == typeid || f.CardLevel == cardlevel).Select(f => f).Count() > 0)
            {
                return 2;
            }
            //检查该客户是否已经发卡
            try
            {
                string sqlstring = string.Empty;
                if (op == "0")
                {
                    sqlstring = "Insert into CardType(TypeID,CardSort,CardLevel,CardYears,HourDiscount,PartDiscount,IniIntegral,UpgradeIntegral,NextTypeID,DmsID)";
                    sqlstring += " values('" + typeid + "','" + cardsort + "','" + cardlevel + "'," + PareToInt(cardyears) + "," + Convert.ToDecimal(hoursdis) + ","+ Convert.ToDecimal(partdis) ;
                    sqlstring += "," + PareToInt(initegral) + "," + PareToInt(updateintegral) + ",'" + nexttypeid + "','" + dmsid + "')";
                }
                else
                {
                    sqlstring = @"Update CardType Set CardSort='" + cardsort + "',CardLevel='" + cardlevel + "',CardYears=" + PareToInt(cardyears) + ",HourDiscount=" + Convert.ToDecimal(hoursdis) + ",PartDiscount=" + Convert.ToDecimal(partdis) +
                        ",IniIntegral=" + PareToInt(initegral) + ",UpgradeIntegral=" + PareToInt(updateintegral) + ",NextTypeID='" + nexttypeid + "' Where TypeID = '" + typeid + "'";
                }
                SqlDbHelper sqldb = new SqlDbHelper();
                RetVal = sqldb.ExecuteNonQuery(sqlstring);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //获取未发卡的车牌号码,写入XML
        [OperationContract]
        public string GetCarNo()
        {
            string RetVal = string.Empty;
            string sqlstring = "select carno,name,vinno,mobile,memberid,createdate from v_member where cardno is null order by createdate desc";
            SqlDbHelper sqldb = new SqlDbHelper();
            try
            {
                DataTable Dt = sqldb.ExecuteDataTable(sqlstring);
                Dt.TableName = "Member";
                Dt.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "carnolist.xml");
            }
            catch (Exception Err)
            {

                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        [OperationContract]
        public string SetCardNoPublish(string cardno)
        {
            string RetVal = string.Empty;
            string sqlstring = "Update CardNoPublish set IsPublish=1 Where CardNo =  '"+ cardno +"'";
            SqlDbHelper sqldb = new SqlDbHelper();
            return sqldb.ExecuteNonQuery(sqlstring).ToString();
        }
        //获取未发卡的车牌号码,写入XML
        [OperationContract]
        public string GetCarNoAll()
        {
            string RetVal = string.Empty;
            string sqlstring = "select CardNo from CardNoPublish where IsPublish=0 order by CardNo";
            SqlDbHelper sqldb = new SqlDbHelper();
            try
            {
                DataTable Dt = sqldb.ExecuteDataTable(sqlstring);
                Dt.TableName = "Member";
                Dt.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "cardnolist.xml");
            }
            catch (Exception Err)
            {

                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        /// <summary>
        /// 根据memberid 获取购买计次服务
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        [OperationContract]
        public string GetServiceItemTimes(string memberid)
        {
            string RetVal = string.Empty;
            string sqlstring = @"select itemid,itemname,times,subtimes from cardserviceitem a inner join serviceitem b
                                    on a.serviceitemid = b.itemid where itemtype='单项服务' and memberid=" + PareToInt(memberid);
            SqlDbHelper sqldb = new SqlDbHelper();
            try
            {
                DataTable Dt = sqldb.ExecuteDataTable(sqlstring);
                RetVal = DataTableToJson("ServiceItem", Dt);
            }
            catch (Exception Err)
            {

                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        /// <summary>
        /// 扣减计次项目次数
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        [OperationContract]
        public int SubServiceItem(string memberid,string itemid)
        {
            int RetVal = 0;
            string sqlstring = @"Update cardserviceitem set SubTimes = SubTimes -1 
                                        from cardserviceitem A inner join (select top 1 ID from cardserviceitem 
                                        where memberid=" + memberid + " and serviceitemid='"+ itemid +"' and SubTimes>0) B on A.ID = B.ID";
            SqlDbHelper sqldb = new SqlDbHelper();
            try
            {
                RetVal = sqldb.ExecuteNonQuery(sqlstring);
            }
            catch (Exception Err)
            {

                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        /// <summary>
        /// 新建信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        public int NewSms(string userid, string receiver, string mobile, string smscontent, string smstype)
        {
            int RetVal = 0;
            string sqlstring = "Insert into Smssend (Details,State,Receiver,Mobile,SmsType,Creator) values('" + smscontent + "','等待发生','"+ receiver +"','"+ mobile +"','"+ smstype +"','"+ userid +"')";
            SqlDbHelper sqldb = new SqlDbHelper();
            try
            {
                RetVal = sqldb.ExecuteNonQuery(sqlstring);
            }
            catch (Exception Err)
            {

                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //'{"userid":"' + $("#userid").val() + '","dmsid":"' + $("#dmsid").val() + '","startdate":"' + $("#startdate").val() + '","enddate":"' + $("#enddate").val() + '","amount1":"' + $("#amount1").val() + '","amount2":"' + $("#amount2").val() + '","zsje":"' + $("#zsje").val() + '","zsbfb":"' + $("#zsbfb").val() + '","zsjf":"' + $("#zsjf").val() + '"}'
        //保存储值赠送方案
        [OperationContract]
        public int SaveRechargeGive(string userid,string dmsid,string startdate,string enddate,string amount1,string amount2,string zsje,string zsbfb,string zsjf)
        {
            int RetVal = 0;
            SqlDbHelper sqldb = new SqlDbHelper();
            //时间段不能交叉
            //string sqlstring = "Select count(*) from RechargeGive where StartDate>'" + startdate + "' and StartDate<'" + enddate + "' or enddate>'" + startdate + "' and enddate<'" + enddate + "'";
            //if (Convert.ToInt32(sqldb.ExecuteNonQuery(sqlstring)) > 0)
            //{
            //    return 2;
            //}
            string sqlstring = @"Insert into RechargeGive (StartDate,EndDate,Amount1,Amount2,GiveMoney,GiveRate,GivePoints,Creator,DmsID) values('"
                                + startdate + "','" + enddate + "'," + PareToInt(amount1) + "," + PareToInt(amount2) + "," + PareToInt(zsje) + ","
                                + PareToInt(zsbfb) + "," + PareToInt(zsjf) + ",'" + userid + "','" + dmsid + "')";
            
            try
            {
                RetVal = sqldb.ExecuteNonQuery(sqlstring);
            }
            catch (Exception Err)
            {

                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //保存客户投诉
        //'{"customer":"' + $("#Customer").val() + '","carno":"' + $("#CarNO").val() + '","mobile":"' + $("#Mobile").val() + '","cometype":"' + cometype + '","ctime":"' + $("#CTime").val() + '","site":"' + $("#Site").val() + '","person":"' + $("#Person").val() + '","events":"' + $("#Event").val() + '","type":"' + Type + '","eventtype":"' + $("#EventType").val() + '","userid":"' + $("#userid").val() + '","dmsid":"' + $("#dmsid").val() + '"}'
        [OperationContract]
        public int SaveComplain(string customer, string carno, string mobile, string cometype, string ctime, string site, string person, string events, string type, string eventtype, string userid,string dmsid)
        {
            int RetVal = 0;
            SqlDbHelper sqldb = new SqlDbHelper();
            string sqlstring = @"Insert into Complain (Customer,CarNO,Mobile,ComeType,CTime,Site,Person,Event,Type,EventType,Creator,DmsID,Handle,HandleMan,HandleTime) values('"
                                + customer + "','" + carno + "','" + mobile + "','" + cometype + "','" + ctime + "','"
                                + site + "','" + person + "','" + events + "','"+ type +"','"+ eventtype +"','" + userid + "','" + dmsid + "','','','')";
            try
            {
                RetVal = sqldb.ExecuteNonQuery(sqlstring);
            }
            catch (Exception Err)
            {

                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public int UpdateComplain(string id,string customer, string carno, string mobile, string cometype, string ctime, string site, string person, string events, string type, string eventtype,string handle,string handleman, string userid, string dmsid)
        {
            int RetVal = 0;
            SqlDbHelper sqldb = new SqlDbHelper();
            string sqlstring = @"Update Complain Set Customer='" + customer + "',CarNO='" + carno + "',Mobile='" + carno + "',ComeType='"+ cometype +"',CTime='"+ ctime +
                                 "',Site='" + site + "',Person='" + person + "',Event='" + events + "',Type='" + type + "',EventType='" + eventtype + "',Creator='" + userid +
                                 "',DmsID='" + dmsid + "',Handle='" + handle + "',HandleMan='" + handleman + "'";
            if (handle != string.Empty) sqlstring += ",HandleTime='"+ DateTime.Now +"'";
            sqlstring +=" Where ID=" + id ;
            try
            {
                RetVal = sqldb.ExecuteNonQuery(sqlstring);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //获取储值赠送方案
        [OperationContract]
        public string GetRechargeGive()
        {
            string RtVal = string.Empty;
            string sqlstring = "Select * from RechargeGive Where StartDate < '" + DateTime.Now + "' and Enddate > '" + DateTime.Now + "'";
            SqlDbHelper sqldb = new SqlDbHelper();
            DataTable Dt = sqldb.ExecuteDataTable(sqlstring);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                RtVal = DataTableToJson("RechargeGive", Dt);
            }
            return RtVal;
        }
        //保存回访结果
        [OperationContract]
        public string SaveCallBack(string id, string wancheng, string qingk, string neirong, string userid)
        {
            string RtVal = string.Empty;
            string sqlstring = "Update CallBack Set wancheng='"+ wancheng +"',jieguo='"+ qingk +"',neirong='"+ neirong +"',creator='"+ userid +"',CreateDate='"+ DateTime.Now +"' where ID="+ id;
            SqlDbHelper sqldb = new SqlDbHelper();
            RtVal = sqldb.ExecuteNonQuery(sqlstring).ToString();
            return RtVal;
        }
        //保存兑换记录
        [OperationContract]
        public string SaveGiftSend(string memberid, string sendgift, string remark, string userid)
        {
            string RtVal = string.Empty;
            ArrayList sqlstrList = new ArrayList();
            if (sendgift.IndexOf("|") > -1)
            {
                string[] servicetype = sendgift.Split('|');
                for (int i = 0; i < servicetype.Length; i++)
                {
                    sqlstrList.Add(SaveGift(servicetype[i], memberid, remark, userid));
                }
            }
            else
            {
                sqlstrList.Add(SaveGift(sendgift, memberid, remark, userid));
            }

            SqlDbHelper sqlDb = new SqlDbHelper();
            RtVal = sqlDb.ExecuteSqlTranArrayList(sqlstrList).ToString();
            return RtVal;
        }
        //保存车主提醒
        [OperationContract]
        public string SaveRemind(string memberid, string d1, string d2, string d3, string d4, string d5)
        {
            string RtVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = "Update Member2 Set NextMaintainDate='"+ d1 +"',LicenseExpire='"+ d2 +"',InsureExpire='"+ d3 +"',YearCheckDate='"+ d4 +"' Where MemberID="+ memberid;
            sql += ";Update Member Set Birthday='" + d5 + "' Where MemberID=" + memberid;
            RtVal = sqlDb.ExecuteNonQuery(sql).ToString();
            return RtVal;
        }
        //新增礼品
        [OperationContract]
        private string SaveGiftInfo(string op, string dmsid, string giftno, string giftname, string integral, string price, string isuse, string beizhu)
        {
            string RtVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = string.Empty;
            if (op == "0")
            {
                sql = "Insert into GiftInfo (DmsID,GiftNo,GiftName,Integral,Price,IsUse,Remark,UpdateTime)values";
                sql += "('" + dmsid + "','" + giftno + "','" + giftname + "'," + integral + "," + price + "," + isuse + ",'" + beizhu + "','" + DateTime.Now + "')";
            }
            else
            {
                sql = "Update GiftInfo set Integral=" + integral + ",Price=" + price + ",IsUse=" + isuse + ",Remark='" + beizhu + "',UpdateTime='" + DateTime.Now + "' Where GiftNo='" + giftno + "'";
            }
            try
            {
                RtVal = sqlDb.ExecuteNonQuery(sql).ToString();
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RtVal;
        }
        private string SaveGift(string strval, string memberid,string remark,string userid)
        {
            string[] fileds = strval.Split(',');
            string sql = "Insert into GiftSendRecord (MemberID,GiftNo,Integral,SendNum,ChangeIntegral,CBlanceIntegral,Remark,CreateDate,Creator)values";
            sql += "(" + memberid + ",'" + fileds[0] + "'," + fileds[2] + "," + fileds[3] + "," + fileds[4] + "," + fileds[5] + ",'" + remark + "','" + DateTime.Now + "','" + userid + "')";
            return sql;
        }

        //综合查询消费记录
        [OperationContract]
        public string QueryMember(string sqlstring)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            if (sqlstring.ToLower().IndexOf("delete") > -1 || sqlstring.ToLower().IndexOf("drop") > -1 || sqlstring.ToLower().IndexOf("alter") > -1)
            {
                return "";
            }
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sqlstring);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        RetVal += "|";
                        for (int j = 0; j < Dt.Columns.Count; j++)
                        {
                            RetVal += Dt.Rows[i][j] != null ? Dt.Rows[i][j].ToString() : "";
                            if (j != Dt.Columns.Count - 1)
                            {
                                RetVal += ",";
                            }
                        }
                    }
                }
                if (RetVal.Length > 1)
                {
                    return RetVal.Substring(1);
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.InnerException.Message + "," + sqlstring);
            }
            return RetVal.ToString();
        }
        //查询积分对账单
        [OperationContract]
        public string QueryPointList(string sqlstring)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            try
            {
                string sql = "Exec Sp_PointList " + sqlstring;
                DataTable Dt1 = sqlDb.ExecuteDataTable(sql);
                DataView Dw = Dt1.DefaultView;
                Dw.Sort = "createdate Asc";
                DataTable Dt = Dw.ToTable();
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        RetVal += "|";
                        for (int j = 0; j < Dt.Columns.Count; j++)
                        {
                            RetVal += Dt.Rows[i][j] != null ? Dt.Rows[i][j].ToString() : "";
                            if (j != Dt.Columns.Count - 1)
                            {
                                RetVal += ",";
                            }
                        }
                    }
                }
                if (RetVal.Length > 1)
                {
                    return RetVal.Substring(1);
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.InnerException.Message + "," + sqlstring);
            }
            return RetVal.ToString();
        }
        //table 转换为 json
        private string DataTableToJson(string jsonName, System.Data.DataTable dt)
        {
            System.Text.StringBuilder Json = new System.Text.StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        private void TryUpdateModel(ServiceItem serviceitem)
        {
            throw new NotImplementedException();
        }
    }
}
