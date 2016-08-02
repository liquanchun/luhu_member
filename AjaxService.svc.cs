﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MvcMember.Controllers;
using MvcMember.Models;
namespace MvcMember
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxService
    {
        ClsSys clssys = new ClsSys();
        // 要使用 HTTP GET，请添加 [WebGet] 特性。(默认 ResponseFormat 为 WebMessageFormat.Json)
        // 要创建返回 XML 的操作，
        //     请添加 [WebGet(ResponseFormat=WebMessageFormat.Xml)]，
        //     并在操作正文中包括以下行:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
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
        public string CheckLogin(string userid, string pass)
        {
            // 在此处添加操作实现
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            // 在此处添加操作实现
            string sql = "Select * from AdminUser Where UserID='" + userid + "' and Password='" + pass + "'";
            sql = sql + ";Update AdminUser set LastLogin = '"+ DateTime.Now +"' , LoginTimes = isnull(LoginTimes,0) + 1 where UserID='" + userid + "'";
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    RetVal = CommonCls.MD5Encrypt(pass);
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, sql);
            }
            return RetVal;
        }
        [OperationContract]
        public string DoWork(int id,string itemname)
        {
            // 在此处添加操作实现
            return "ID:" + id.ToString() + ",ItemName" + itemname;
        }

        // 添加词典项目
        [OperationContract]
        public string AddItemList(int parentid, string itemname, int itemorder)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Insert into ItemList(ItemName,ItemParent,ItemOrder) values ('" + itemname + "'," + parentid + "," + itemorder + ")";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, sql);
            }
            return RetVal.ToString();
        }
        // 删除词典项目
        [OperationContract]
        public string DelItemList(int itemID)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from ItemList Where ID="+ itemID;
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, sql);
            }
            return RetVal.ToString();
        }
        // 删除分店资料
        [OperationContract]
        public string DelBranch(string branchid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from Branch Where BranchID='" + branchid + "'";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, sql);
            }
            return RetVal.ToString();
        }

        // 删除员工资料
        [OperationContract]
        public string DelEmployee(int id)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from Employee Where ID="+ id;
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, sql);
            }
            return RetVal.ToString();
        }

        // 删除储值赠送方案
        [OperationContract]
        public string DelRechargeGive(int id)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from RechargeGive Where ID=" + id;
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, sql);
            }
            return RetVal.ToString();
        }
        // 删除用户
        [OperationContract]
        public string DelUser(string userid,string opuserid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from AdminUser Where UserID='" + userid + "'";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteMsgLog(this.GetType().ToString(), "删除用户：" + userid + ",操作员：" +opuserid);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
                CommonCls.WriteErrLog(this.GetType().ToString(), Err, sql);
            }
            return RetVal.ToString();
        }
        // 删除客户资料
        [OperationContract]
        public string DelMember(int memberid,string userid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            if (int.Parse(sqlDb.ExecuteScalar("select count(1) from cardinfo where memberid=" + memberid).ToString()) > 0)
            {
                return "2";
            }
            // 在此处添加操作实现
            string sql = "Delete from Member Where MemberID=" + memberid;
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                sql = "Delete from Member2 Where MemberID=" + memberid;
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteMsgLog(this.GetType().ToString(), "删除客户资料,客户编号:" + memberid + ",操作员：" + userid);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        // 删除会员资料
        [OperationContract]
        public string DelCardInfo(string cardno,int memberid, string userid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            if (int.Parse(sqlDb.ExecuteScalar("select count(1) from cardinfo where Balance>0 and cardno='" + cardno + "'").ToString()) > 0)
            {
                return "2";
            }
            // 在此处添加操作实现
            string sql = "Delete from CardInfo Where CardNo='"+ cardno +"'";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteMsgLog(this.GetType().ToString(), "删除会员资料,会员卡号:" + cardno + ",客户编号:" + memberid + ",操作员：" + userid);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        // 删除卡类型
        [OperationContract]
        public string DelCardType(int typeid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from CardType Where TypeID=" + typeid + " And TypeID not in (Select TypeID from CardInfo)";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        [OperationContract]
        public string SelectMember(string mobile,string carno)
        {
            string RetVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = "Select A.MemberID,Name,Mobile,CarNo,VinNO,C.CardNo from Member A inner join Member2 B on A.MemberID=B.MemberID left join CardInfo C on A.MemberID=C.MemberID";
            if (mobile != string.Empty)
            {
                sql += " Where Mobile ='" + mobile + "'";
            }
            else if (carno != string.Empty)
            {
                sql += " Where CarNo ='" + carno + "'";
            }
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    string cardno = Dt.Rows[0]["CardNo"]== null ? string.Empty : Dt.Rows[0]["CardNo"].ToString();
                    RetVal = Dt.Rows[0]["MemberID"].ToString() + "," + Dt.Rows[0]["Name"].ToString() + "," + Dt.Rows[0]["Mobile"].ToString() + "," + Dt.Rows[0]["CarNo"].ToString() + "," + Dt.Rows[0]["VinNO"].ToString() + "," + cardno;
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public string SelectMemberByCardNo(string cardno)
        {
            string RetVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = "Select MemberID,Mobile,CarNo,Name,ClientType,BalanceIntegral,Balance,CardLevel,PartDiscount,HourDiscount,State,VinNO,SumConsumeMoney,LastComeDate,CardNo,SumGiveMoney from V_CardInfo Where CardNo='" + cardno + "'";
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    RetVal = Dt.Rows[0]["MemberID"].ToString() + "," + Dt.Rows[0]["Mobile"].ToString() + "," + Dt.Rows[0]["CarNo"].ToString() + "," + Dt.Rows[0]["Name"].ToString() + "," + Dt.Rows[0]["ClientType"].ToString();
                    RetVal += "," + Dt.Rows[0]["BalanceIntegral"].ToString() + "," + Dt.Rows[0]["Balance"].ToString() + "," + Dt.Rows[0]["CardLevel"].ToString();
                    RetVal += "," + Dt.Rows[0]["PartDiscount"].ToString() + "," + Dt.Rows[0]["HourDiscount"].ToString() + "," + Dt.Rows[0]["State"].ToString();
                    RetVal += "," + Dt.Rows[0]["VinNO"].ToString() + "," + Dt.Rows[0]["SumConsumeMoney"].ToString() + "," + Dt.Rows[0]["LastComeDate"].ToString() + "," + Dt.Rows[0]["CardNo"].ToString() + "," + Dt.Rows[0]["SumGiveMoney"].ToString();
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public string SelectQueryMember(string memberid)
        {
            string RetVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = @"select InitialIntegral,SumGiveIntegral,SumExchange,SumRecharge,StartDate,
                            CarOwner,ComeType,Sex,Address,Birthday,NextMaintainKM,NextMaintainDate,
                            LicenseExpire,InsureExpire,YearCheckDate,CarType,SumConsumeIntegral,LastComeDate,CardNo from 
                            member A inner join member2 B on A.memberid = B.memberid inner join cardinfo C on A.memberid = C.memberid
                            where A.memberid=" + memberid;
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    RetVal = Dt.Rows[0][0].ToString() + "," + Dt.Rows[0][1].ToString() + "," + Dt.Rows[0][2].ToString() + "," + Dt.Rows[0][3].ToString() + "," + Convert.ToDateTime(Dt.Rows[0][4].ToString()).ToShortDateString() + "," ;
                    RetVal += Dt.Rows[0][5].ToString() + "," + Dt.Rows[0][6].ToString() + "," + Dt.Rows[0][7].ToString() + "," + Dt.Rows[0][8].ToString() + "," + Dt.Rows[0][9].ToString() + ",";
                    RetVal += Dt.Rows[0][10].ToString() + "," + Convert.ToDateTime(Dt.Rows[0][11].ToString()).ToShortDateString() + "," + Convert.ToDateTime(Dt.Rows[0][12].ToString()).ToShortDateString() + "," + Convert.ToDateTime(Dt.Rows[0][13].ToString()).ToShortDateString() + ",";
                    RetVal += Convert.ToDateTime(Dt.Rows[0][14].ToString()).ToShortDateString() + "," + Dt.Rows[0][15].ToString() + "," + Dt.Rows[0][16].ToString() + "," + Dt.Rows[0][17].ToString() + "," + Dt.Rows[0][18].ToString();
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public string SelectConsumeInfo(string cardno)
        {
            string RetVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = "select top 1 * from consume where cardno='" + cardno + "' order by createdate desc";
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    RetVal = Dt.Rows[0]["integral"].ToString() + "," + Dt.Rows[0]["deductinte"].ToString();
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public string SelectMemberByCarNo(string mobile, string carno)
        {
            string RetVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = "Select MemberID,Mobile,CarNo,Name,ClientType,BalanceIntegral,Balance,CardLevel,PartDiscount,HourDiscount,State,VinNO,SumConsumeMoney,LastComeDate,CardNo,SumGiveMoney from V_CardInfo ";
            try
            {
                if (mobile != string.Empty)
                {
                    sql += " Where Mobile ='" + mobile + "'";
                }
                else if (carno != string.Empty)
                {
                    sql += " Where CarNo ='" + carno + "'";
                }
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    RetVal = Dt.Rows[0]["MemberID"].ToString() + "," + Dt.Rows[0]["Mobile"].ToString() + "," + Dt.Rows[0]["CarNo"].ToString() + "," + Dt.Rows[0]["Name"].ToString() + "," + Dt.Rows[0]["ClientType"].ToString();
                    RetVal += "," + Dt.Rows[0]["BalanceIntegral"].ToString() + "," + Dt.Rows[0]["Balance"].ToString() + "," + Dt.Rows[0]["CardLevel"].ToString();
                    RetVal += "," + Dt.Rows[0]["PartDiscount"].ToString() + "," + Dt.Rows[0]["HourDiscount"].ToString() + "," + Dt.Rows[0]["State"].ToString();
                    RetVal += "," + Dt.Rows[0]["VinNO"].ToString() + "," + Dt.Rows[0]["SumConsumeMoney"].ToString() + "," + Dt.Rows[0]["LastComeDate"].ToString() + "," + Dt.Rows[0]["CardNo"].ToString() + "," + Dt.Rows[0]["SumGiveMoney"].ToString();
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        [OperationContract]
        public string SelectCardInfo(string cardno)
        {
            string RetVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = "Select BalanceIntegral,State,EndDate,CardLevel from CardInfo A Inner join CardType B on A.TypeID=B.TypeID Where CardNo='" + cardno + "'";
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    RetVal = Dt.Rows[0]["BalanceIntegral"].ToString() + "," + Dt.Rows[0]["State"].ToString() + "," + Dt.Rows[0]["EndDate"].ToString();
                    RetVal += "," + Dt.Rows[0]["CardLevel"].ToString();
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        // 修改用户密码
        [OperationContract]
        public string UpdatePwd(string userid, string oldpwd, string newpwd)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Select count(*) from adminuser where UserID='"+ userid +"' and Password='"+ oldpwd +"'";
            try
            {
                RetVal = int.Parse(sqlDb.ExecuteScalar(sql).ToString());
                if (RetVal > 0)
                {
                    sql = "Update adminuser Set password='" + newpwd + "' Where UserID='" + userid + "'";
                    RetVal = int.Parse(sqlDb.ExecuteNonQuery(sql).ToString());
                }
                else
                {
                    RetVal = 0; 
                }
                CommonCls.WriteMsgLog(this.GetType().ToString(), sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        //保存系统设置
        [OperationContract]
        public string SaveSetting(string intetomoneyrate, string intetomoneymax)
        {
            int RetVal = 0;
            try
            {
                ConfigurationManager.AppSettings["InteToMoneyRate"] = intetomoneyrate;
                ConfigurationManager.AppSettings["InteToMoneyMax"] = intetomoneymax;
                RetVal = 1;
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
                //保存系统设置
        [OperationContract]
        public string SelectSysSet(string keyword1, string keyword2, string keyword3,string keyword4)
        {
            string RetVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string sql = "Select * from SysSetting Where keyword in('" + keyword1 + "','" + keyword2 + "','" + keyword3 + "','" + keyword4 + "')";
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        RetVal += "," + Dt.Rows[i]["Val1"].ToString() + "," + Dt.Rows[i]["Val2"].ToString();
                    }
                }
                if (RetVal.Length > 1) RetVal = RetVal.Substring(1);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }

        // 更改积分
        [OperationContract]
        public string UpdateIntegral(int memberid, string cardno, string userid,int blanceintegral, int integarl,string reason)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Update CardInfo Set sumconsumeintegral = sumconsumeintegral +  " + integarl + ",BalanceIntegral = BalanceIntegral + " + integarl + " Where CardNo='" + cardno + "'";
            sql += ";Insert into AdjustInte(memberid,cardno,blanceintegral,Integral,reason,createdate,creator) values (" + memberid + ",'" + cardno + "'," + blanceintegral + "," + integarl + ",'" + reason + "','" + DateTime.Now + "','" + userid + "')";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteMsgLog(this.GetType().ToString(), userid + "更改了卡号" + cardno + "会员积分" + integarl + ",客户编号:" + memberid);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        // 挂失
        [OperationContract]
        public string SetCardLoss(string cardno, string userid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Update CardInfo Set State = '挂失' Where CardNo='" + cardno + "'";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteSysLog("挂失了卡号" + cardno, string.Empty, userid);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        // 补卡
        [OperationContract]
        public string ReplaceCard(string cardno, string newcardno, string userid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Exec Sp_ReplaceCard '" + cardno + "','" + newcardno + "'";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteSysLog("补卡，旧卡号" + cardno + "，新卡号" + newcardno, string.Empty, userid);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        // 续会
        [OperationContract]
        public string AddEndDate(string cardno, string newdate, string userid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Update CardInfo Set EndDate = '" + newdate + "' Where CardNo='" + cardno + "'";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteSysLog(cardno + "卡号续会到" + newdate, string.Empty, userid);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        //改变级别
        [OperationContract]
        public string ChangeCardLevel(string cardno, string oldlevel, string newlevel ,int newlevelid,string userid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Update CardInfo Set TypeID = " + newlevelid + " Where CardNo='" + cardno + "'";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteSysLog(cardno + "卡号调整级别,从" + oldlevel + "调整到" + newlevel, string.Empty, userid);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        [OperationContract]
        public string MemberDistrict(int years)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal1 = string.Empty;
            string RetVal2 = string.Empty;
            string sqlstr = "select district,count(*) as cnt from member where district is not null group by district";
            DataTable Dt = sqlDb.ExecuteDataTable(sqlstr);
            if (Dt != null && Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    RetVal1 = RetVal1 + ", '" + Dt.Rows[i]["district"].ToString() + "'";
                    RetVal2 = RetVal2 + ", " + Dt.Rows[i]["cnt"].ToString();
                }
            }
            if (RetVal1.Length > 1) RetVal1 = RetVal1.Substring(1).Trim();
            if (RetVal2.Length > 1) RetVal2 = RetVal2.Substring(1).Trim();
            if (RetVal1 != string.Empty && RetVal2 != string.Empty)
            {
                return "[" + RetVal1 + "]|[" + RetVal2 + "]";
            }
            else
            {
                return string.Empty;
            }
        }
        [OperationContract]
        public string MemberType(int years)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal1 = string.Empty;
            string RetVal2 = string.Empty;
            string sqlstr = @"select substring(convert(nvarchar(50),createdate,20),6,2)  as mon,ClientType,count(*) as cnt from member
                                group by substring(convert(nvarchar(50),createdate,20),6,2),ClientType
                                order by Clienttype,mon";
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sqlstr);
                int[] val1 = new int[13];
                int[] val2 = new int[13];
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        if (Dt.Rows[i]["ClientType"].ToString().IndexOf("个人") > -1)
                        {
                            val1[int.Parse(Dt.Rows[i]["mon"].ToString())] = int.Parse(Dt.Rows[i]["cnt"].ToString());
                        }
                        else
                        {
                            val2[int.Parse(Dt.Rows[i]["mon"].ToString())] = int.Parse(Dt.Rows[i]["cnt"].ToString());
                        }
                    }
                }
                for (int i = 1; i < 13; i++)
                {
                    RetVal1 = RetVal1 + "," + val1[i].ToString();
                    RetVal2 = RetVal2 + "," + val2[i].ToString();
                }
                RetVal1 = RetVal1.Substring(1).Trim();
                RetVal2 = RetVal2.Substring(1).Trim();
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return "[" + RetVal1 + "]|[" + RetVal2 + "]";
        }
        [OperationContract]
        public string CardStat(int year, int month)
        {
            string RetVal = string.Empty;
            SqlDbHelper sqlDb = new SqlDbHelper();
            string[,] str2s = new string[33,5];
            string sqlstr = string.Format(@"select cardlevel,cast(substring(convert(nvarchar(50),createdate,20),9,2) as int) as days,count(*) as cnt 
                                from cardinfo A inner join CardType B on A.TypeID=B.TypeID
                                where year(createdate)={0} and month(createdate)={1}
                                group by cardlevel,cast(substring(convert(nvarchar(50),createdate,20),9,2) as int)", year, month);
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sqlstr);
                WebMemberDBContext DbContext = new WebMemberDBContext();
                IList<CardType> cardTypeLst = DbContext.CardTypeDb.ToList();
                if (cardTypeLst != null && Dt != null && Dt.Rows.Count>0)
                {
                    for (int i = 1; i < 33; i++)
                    {
                        for (int j = 0; j < cardTypeLst.Count && j < 5; j++)
                        {
                            str2s[0, j] = cardTypeLst[j].CardLevel;
                            DataRow[] drs = Dt.Select("cardlevel='" + cardTypeLst[j].CardLevel + "' and days=" + i);
                            if (drs != null && drs.Length > 0)
                            {
                                str2s[i, j] = drs[0]["cnt"].ToString();
                            }
                            else
                            {
                                str2s[i, j] = "0";
                            }
                        }
                    }
                }

                for (int i = 0; i < 33; i++)
                {
                    if (str2s[i, 0] != string.Empty && str2s[i, 0] != "")
                    {
                        for (int j = 0; j < cardTypeLst.Count && j < 5; j++)
                        {
                            if (RetVal.EndsWith("|"))
                            {
                                RetVal += str2s[i, j];
                            }
                            else
                            {
                                RetVal += "," + str2s[i, j];
                            }
                        }
                        RetVal += "|";
                    }
                }
                if (RetVal != string.Empty)
                {
                    RetVal = RetVal.Substring(1);
                    RetVal = RetVal.Substring(0, RetVal.Length - 1);
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        // 获取新卡号
        [OperationContract]
        public string NewCardNo()
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            // 在此处添加操作实现
            string sql = "execute NewCardNo";
            try
            {
                DataTable dt = sqlDb.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    RetVal = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //保存业务类型积分
        [OperationContract]
        public string SaveServiceType(string cardtype, string clienttype, string servicetype, string hoursrate, string partsrate, string otherrate, int usemaxrate)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            // 在此处添加操作实现
            string sql = @"Insert into ServiceInte(CardType,ClientType,ServiceType,HoursRate,PartsRate,OtherRate,UseMaxRate) values('" + cardtype + "','" + clienttype + "','" + servicetype + "','" + hoursrate + "','" + partsrate + "','" + otherrate + "'," + usemaxrate + ")";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql).ToString();
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //删除业务类型积分
        [OperationContract]
        public string DelServiceType(int id)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            // 在此处添加操作实现
            string sql = "Delete from ServiceInte Where ID=" + id;
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql).ToString();
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //查询业务分类
        [OperationContract]
        public string SelectServiceInte(string clienttype, string servicetype,string cardtype)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            // 在此处添加操作实现
            string sql = "select * from ServiceInte where clientType ='" + clienttype + "' and ServiceType ='" + servicetype + "' and CardType ='" + cardtype + "'";
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    RetVal = Dt.Rows[0]["HoursRate"].ToString() + "," + Dt.Rows[0]["PartsRate"].ToString() + "," + Dt.Rows[0]["OtherRate"].ToString() + "," + Dt.Rows[0]["UseMaxRate"].ToString();
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }

        // 删除活动
        [OperationContract]
        public string DelActivity(int id)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from Activity Where ID=" + id;
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
                CommonCls.WriteSysLog("删除活动", string.Empty, string.Empty);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        // 获取积分单号
        [OperationContract]
        public string GetBillNo()
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            // 在此处添加操作实现
            try
            {
                string bill = ConfigurationManager.AppSettings["FirstBillNo"];
                DateTime dt1 = DateTime.Today;
                DateTime dt2 = DateTime.Today.AddDays(1);
                WebMemberDBContext DBContext = new WebMemberDBContext();
                List<Consume> lstCon = (from c in DBContext.ConsumeDb where c.CreateDate > dt1 && c.CreateDate < dt2 select c).ToList();
                int cnt = lstCon.Count + 1;
                RetVal = bill + DateTime.Today.ToString("yyyyMMdd") + cnt.ToString().PadLeft(3, '0');
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //保存结算积分信息
        [OperationContract]
        public string SaveConsume(string billinfo, string serviceitem, string sklist)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            try
            {
                ArrayList sqlstrList = new ArrayList();
                //保存积分单信息
                string sql1 = @"Insert into Consume(DmsID,BillNo,MemberID,CardNo,CurBIntegral,Integral,BalanceMoney,
                                                    SumFee,CreateDate,Creator,InteToMoney,DeductInte) values";
                
                string[] billinfos = billinfo.Split(',');
                //单号
                string billno = billinfos[1];
                if (billinfos[1] == string.Empty || billinfos[1] == "0" || billinfos[1].Length <3)
                {
                    billno = GetBillNo();
                }
                sql1 += "('" + billinfos[0] + "','" + billno + "','" + billinfos[2] + "'," + PareToInt(billinfos[3]) + "," + PareToInt(billinfos[4]) + "," + PareToInt(billinfos[5]) + "";
                sql1 += "," + PareToInt(billinfos[6]) + "," + PareToInt(billinfos[7]) + ",'" + DateTime.Now.ToString() + "','" + billinfos[9] + "'," + PareToInt(billinfos[10]) + "," + PareToInt(billinfos[11]) + ")";
                sqlstrList.Add(sql1);
                //RetVal = sqlDb.ExecuteNonQuery(sql1);
                CommonCls.WriteSysLog("保存结算积分信息,SQL:" + sql1, string.Empty, string.Empty);

                if (serviceitem.IndexOf("|") > -1)
                {
                    string[] servicetype = serviceitem.Split('|');
                    for (int i = 0; i < servicetype.Length; i++)
                    {
                        sqlstrList.Add(SaveService(servicetype[i], billno));
                    }
                }
                else
                {
                    sqlstrList.Add(SaveService(serviceitem, billno));
                }
                if (sklist.IndexOf("|") > -1)
                {
                    string[] sklists = sklist.Split('|');
                    for (int i = 0; i < sklists.Length; i++)
                    {
                        sqlstrList.Add(SavePayList(sklists[i], billno));
                    }
                }
                else
                {
                    sqlstrList.Add(SavePayList(sklist, billno));
                }
                RetVal = sqlDb.ExecuteSqlTranArrayList(sqlstrList);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        private string SavePayList(string strval, string billno)
        {
            string sqlstr = string.Empty;
            string[] fileds = strval.Split(',');
            try
            {
                string patytime = fileds[3];
                if (patytime == string.Empty || patytime.Length < 3)
                {
                    patytime = DateTime.Now.ToString();
                }
                sqlstr = @"Insert into PayList(BillNo,PayWay,PayMoney,PayTime) values";
                sqlstr += "('" + billno + "','" + fileds[1] + "'," + PareToInt(fileds[2]) + ",'" + patytime + "')";
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return sqlstr;
        }
        private string SaveService(string strval, string billno)
        {
            string sqlstr = string.Empty;
            string[] fileds = strval.Split(',');
            try
            {
                sqlstr = @"Insert into ConsumeItem(BillNo,ServiceType,HoursFee,PartsFee,OtherFee,
                                                    SumFee,ToFee,UseInte,FactFee,Integral) values";
                sqlstr += "('" + billno + "','" + fileds[1] + "'," + PareToInt(fileds[2]) + "," + PareToInt(fileds[3]) + "," + PareToInt(fileds[4]);
                sqlstr += "," + PareToInt(fileds[5]) + "," + PareToInt(fileds[6]) + "," + PareToInt(fileds[7]) + "," + PareToInt(fileds[8]) + "," + PareToInt(fileds[9]) + ")";
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return sqlstr;
        }
        [OperationContract]
        public string SaveGuestBook(int id, string username, string message)
        {
            string sqlstr = string.Empty;
            int RetVal = 0;
            SqlDbHelper sqlDb = new SqlDbHelper();
            try
            {
                sqlstr = "Update GuestBook Set Reply='" + message + "',ReplyMan='" + username + "',ReplyTime='"+ DateTime.Now +"' where ID="+ id;
                RetVal = sqlDb.ExecuteNonQuery(sqlstr);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        [OperationContract]
        public string DelGuestBook(int id)
        {
            string sqlstr = string.Empty;
            int RetVal = 0;
            SqlDbHelper sqlDb = new SqlDbHelper();
            try
            {
                sqlstr = "Delete from GuestBook where ID=" + id;
                RetVal = sqlDb.ExecuteNonQuery(sqlstr);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        //审核客户资料
        [OperationContract]
        public string AudiMember(int id, string username)
        {
            int RetVal1 = 0;
            int RetVal2 = 0;

            SqlDbHelper sqlDb = new SqlDbHelper();
            WebMemberDBContext DbContext = new WebMemberDBContext();
            MemberUpdate memberlst = DbContext.MemberUpdateDb.Find(id);
            Member member1 = DbContext.MemberDb.First(f => f.MemberID == memberlst.MemberID);
            Member2 member2 = DbContext.Member2Db.First(f => f.MemberID == memberlst.MemberID);
            string sqlstr1 = "Update Member Set ";
            if (memberlst.Address != member1.Address) { sqlstr1 += " Address='" + memberlst.Address + "',"; };
            if (memberlst.Birthday != member1.Birthday) { sqlstr1 += " Birthday='" + memberlst.Birthday + "',"; };
            if (memberlst.ClientType != member1.ClientType) { sqlstr1 += " ClientType='" + memberlst.ClientType + "',"; };
            if (memberlst.District != member1.District) { sqlstr1 += " District='" + memberlst.District + "',"; };
            if (memberlst.Email != member1.Email) { sqlstr1 += " Email='" + memberlst.Email + "',"; };
            if (memberlst.Hobby != member1.Hobby) { sqlstr1 += " Hobby='" + memberlst.Hobby + "',"; };
            if (memberlst.IDCard != member1.IDCard) { sqlstr1 += " IDCard='" + memberlst.IDCard + "',"; };
            if (memberlst.Mobile != member1.Mobile) { sqlstr1 += " Mobile='" + memberlst.Mobile + "',"; };
            if (memberlst.Name != member1.Name) { sqlstr1 += " Name='" + memberlst.Name + "',"; };
            if (memberlst.Postcode != member1.Postcode) { sqlstr1 += " Postcode='" + memberlst.Postcode + "',"; };
            if (memberlst.Remark != member1.Remark) { sqlstr1 += " Remark='" + memberlst.Remark + "',"; };
            if (memberlst.Sex != member1.Sex) { sqlstr1 += " Sex='" + memberlst.Sex + "'"; };

            if(sqlstr1.EndsWith(","))
            {
                sqlstr1 = sqlstr1.Substring(0, sqlstr1.Length - 1);
                sqlstr1 += " Where MemberID=" + memberlst.MemberID;
                try
                {
                    RetVal1 = sqlDb.ExecuteNonQuery(sqlstr1);
                }
                catch(Exception Err)
                {
                    CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.InnerException.Message + "," + sqlstr1);
                    return "0";
                }
            }

            string sqlstr2 = "Update Member2 Set ";
            if (memberlst.CarNO != member2.CarNO) { sqlstr2 += " CarNO='" + memberlst.CarNO + "',"; };
            if (memberlst.VinNO != member2.VinNO) { sqlstr1 += " VinNO='" + memberlst.VinNO + "',"; };
            if (memberlst.CarOwner != member2.CarOwner) { sqlstr2 += " CarOwner='" + memberlst.CarOwner + "',"; };
            if (memberlst.CarType != member2.CarType) { sqlstr2 += " CarType='" + memberlst.CarType + "',"; };
            if (memberlst.RunKM != member2.RunKM) { sqlstr2 += " RunKM=" + memberlst.RunKM + ","; };
            if (memberlst.InsureExpire != member2.InsureExpire) { sqlstr2 += " InsureExpire='" + memberlst.InsureExpire + "',"; };
            if (memberlst.LicenseExpire != member2.LicenseExpire) { sqlstr2 += " LicenseExpire='" + memberlst.LicenseExpire + "',"; };
            if (memberlst.NextMaintainDate != member2.NextMaintainDate) { sqlstr2 += " NextMaintainDate='" + memberlst.NextMaintainDate + "',"; };
            if (memberlst.NextMaintainKM != member2.NextMaintainKM) { sqlstr2 += " NextMaintainKM=" + memberlst.NextMaintainKM + ","; };
            if (memberlst.YearCheckDate != member2.YearCheckDate) { sqlstr2 += " YearCheckDate='" + memberlst.YearCheckDate + "',"; };

            if (sqlstr2.EndsWith(","))
            {
                sqlstr2 = sqlstr2.Substring(0, sqlstr2.Length - 1);
                sqlstr2 += " Where MemberID=" + memberlst.MemberID;
                try
                {
                    RetVal2 = sqlDb.ExecuteNonQuery(sqlstr2);
                }
                catch (Exception Err)
                {
                    CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.InnerException.Message + "," + sqlstr2);
                    return "0";
                }
            }
            if (RetVal1 > 0 || RetVal2 > 0)
            {
                string sqlstr3 = "Update memberupdate Set AudiTime='" + DateTime.Now + "',Auditor='" + username + "' Where ID=" + id;
                sqlDb.ExecuteNonQuery(sqlstr3);
            }
            CommonCls.WriteSysLog("审核客户资料,审核人:" + username + "，客户编号:" + id, string.Empty, string.Empty);
            return RetVal1 > 0 || RetVal2 >0 ? "1" : "0";
        }
        //设置权限
        [OperationContract]
        public string UpdateAuth(string rolename, int funid, int isuse)
        {
            string sqlstr = string.Empty;
            int RetVal = 0;
            SqlDbHelper sqlDb = new SqlDbHelper();
            try
            {
                sqlstr = "Update Userauth Set IsUse=" + isuse + " Where RoleName='"+ rolename +"' And FunID=" + funid;
                RetVal = sqlDb.ExecuteNonQuery(sqlstr);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        //更改用户角色
        [OperationContract]
        public string UpdateRole(string userid,string username, string rolename)
        {
            string sqlstr = string.Empty;
            int RetVal = 0;
            SqlDbHelper sqlDb = new SqlDbHelper();
            try
            {
                sqlstr = "Update AdminUser Set RoleName='" + rolename + "',UserName='"+ username +"' Where UserID='" + userid + "'";
                RetVal = sqlDb.ExecuteNonQuery(sqlstr);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        // 删除服务项目
        [OperationContract]
        public int DelServiceItem(string itemid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            WebMemberDBContext DBContext = new WebMemberDBContext();
            if (DBContext.CardServiceItemDb.Count() > 0 && DBContext.CardServiceItemDb.Where(f => f.ServiceItemID == itemid && f.SubTimes>0).Select(f => f).Count() > 0)
            {
                return 2;
            }
            if (DBContext.ServiceItem2Db.Count() > 0 && DBContext.ServiceItem2Db.Where(f => f.ChildItemID == itemid).Select(f => f).Count() > 0)
            {
                return 2;
            }
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from ServiceItem2 where ChildItemID ='" + itemid + "';Delete from ServiceItem2 where ItemID='" + itemid + "';Delete from ServiceItem Where ItemID='" + itemid + "'";
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        // 删除会员服务项目
        [OperationContract]
        public string DelCardServiceItem(int id)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            int RetVal = 0;
            // 在此处添加操作实现
            string sql = "Delete from CardServiceItem where ID =" + id;
            try
            {
                RetVal = sqlDb.ExecuteNonQuery(sql);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        //保存套餐服务项目
        [OperationContract]
        public string SaveTaocan(int itemid, string serviceitem)
        {
            string sqlstring = string.Empty;
            int RetVal = 0;
            try
            {
                if (serviceitem.IndexOf("|") > -1)
                {
                    string[] servicetype = serviceitem.Split('|');
                    for (int i = 0; i < servicetype.Length; i++)
                    {
                        RetVal = SaveTaocanItem(servicetype[i], itemid);
                    }
                }
                else
                {
                    RetVal = SaveTaocanItem(serviceitem, itemid);
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        private int SaveTaocanItem(string strval, int itemid)
        {
            string sqlstr = string.Empty;
            int RetVal = 0;
            string[] fileds = strval.Split(',');
            SqlDbHelper sqlDb = new SqlDbHelper();
            try
            {
                sqlstr = "Insert into ServiceItem2(ItemID,ChildItemID,Times,BalanceTimes) values";
                sqlstr += "(" + itemid + "," + fileds[0] + "," + PareToInt(fileds[2]) + "," + PareToInt(fileds[2]) +")";
                RetVal = sqlDb.ExecuteNonQuery(sqlstr);
                
                CommonCls.WriteSysLog("保存套餐项目信息,SQL:" + sqlstr, string.Empty, string.Empty);
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal;
        }
        //查询调整积分记录
        [OperationContract]
        public string SelectAdjustInte(int memberid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            // 在此处添加操作实现
            string sql = "Select Createdate,CarNo,CardNo,Integral,Reason,Creator from V_AdjustInte where MemberID =" + memberid;
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        RetVal += "|" + Dt.Rows[i][0].ToString() + "," + Dt.Rows[i][1].ToString() + "," + Dt.Rows[i][2].ToString() + "," + Dt.Rows[i][3].ToString();
                        RetVal += "," + Dt.Rows[i][4].ToString() + "," + Dt.Rows[i][5].ToString();
                    }
                }
                if (RetVal.Length > 1)
                {
                    return RetVal.Substring(1);
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
        //查询储值赠送方案
        [OperationContract]
        public string SelectRechargeGive(string dmsid)
        {
            SqlDbHelper sqlDb = new SqlDbHelper();
            string RetVal = string.Empty;
            // 在此处添加操作实现
            string sql = "select ID,StartDate,EndDate,Amount1,Amount2,GiveMoney,GiveRate,GivePoints,GiveServiceItem,GiveItemTimes,CreateTime,Creator from rechargegive where dmsid='"+ dmsid +"' order by CreateTime desc";
            try
            {
                DataTable Dt = sqlDb.ExecuteDataTable(sql);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        string givestring = string.Empty;
                        RetVal += "|" + Dt.Rows[i]["ID"].ToString() + "," + Convert.ToDateTime(Dt.Rows[i]["StartDate"]).ToShortDateString() + "," + Convert.ToDateTime(Dt.Rows[i]["EndDate"]).ToShortDateString() + "," + Dt.Rows[i]["Amount1"].ToString() + "," + Dt.Rows[i]["Amount2"].ToString();
                        if (Convert.ToInt32(Dt.Rows[i]["GiveMoney"]) > 0)
                        {
                            givestring = "赠送金额" + Dt.Rows[i]["GiveMoney"].ToString() + "元";
                        }
                        if (Convert.ToDecimal(Dt.Rows[i]["GiveRate"]) > 0)
                        {
                            givestring = "赠送储值金额的" + Dt.Rows[i]["GiveRate"].ToString() + "%";
                        }
                        if (Convert.ToInt32(Dt.Rows[i]["GivePoints"]) > 0)
                        {
                            givestring = "赠送积分" + Dt.Rows[i]["GivePoints"].ToString() + "分";
                        }
                        if (Convert.ToInt32(Dt.Rows[i]["GiveServiceItem"]) > 0)
                        {
                            givestring = "赠送服务项目" + Dt.Rows[i]["GiveServiceItem"].ToString() + Dt.Rows[i]["GiveItemTimes"].ToString() + "次";
                        }
                        RetVal += "," + givestring + "," + Convert.ToDateTime(Dt.Rows[i]["CreateTime"]).ToShortDateString() + "," + Dt.Rows[i]["Creator"].ToString();
                    }
                }
                if (RetVal.Length > 1)
                {
                    return RetVal.Substring(1);
                }
            }
            catch (Exception Err)
            {
                CommonCls.WriteErrLog(this.GetType().ToString(), Err,Err.StackTrace + Err.InnerException.Message);
            }
            return RetVal.ToString();
        }
    }
}
