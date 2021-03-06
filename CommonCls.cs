﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using log4net;
namespace MvcMember
{
    public static class CommonCls
    {
        public static string Key = "aspnetm5";
        public static void WriteXML_(string msg)
        {
            Page pg = new Page();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(pg.Server.MapPath("log.xml"));
                XmlNode xmldocSelect = xmlDoc.SelectSingleNode("message");
                XmlNodeList xmlnl = xmldocSelect.ChildNodes;
                if (xmlnl != null && xmlnl.Count > 50)
                {
                    int xmlcount = xmlnl.Count;
                    for (int j = 0; j < xmlcount - 50; j++)
                    {
                        xmldocSelect.RemoveChild(xmlnl[j]);
                    }
                }
                XmlElement el = xmlDoc.CreateElement("msg");
                el.SetAttribute("Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                el.SetAttribute("Description", msg); //添加person节点的属性 "sex"
                xmldocSelect.AppendChild(el);
                xmlDoc.Save(pg.Server.MapPath("log.xml"));
            }
            catch { }
        }
        public static string MD5Encrypt(string pToEncrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(Key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
       public static string MD5Decrypt(string pToDecrypt)
       {
           DESCryptoServiceProvider des = new DESCryptoServiceProvider();
           byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
           for (int x = 0; x < pToDecrypt.Length / 2; x++)
           {
               int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
               inputByteArray[x] = (byte)i;
           }
           des.Key = ASCIIEncoding.ASCII.GetBytes(Key);
           des.IV = ASCIIEncoding.ASCII.GetBytes(Key);
           MemoryStream ms = new MemoryStream();
           CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
           cs.Write(inputByteArray, 0, inputByteArray.Length);
           cs.FlushFinalBlock(); StringBuilder ret = new StringBuilder(); return System.Text.Encoding.UTF8.GetString(ms.ToArray());
       }
       public static void WriteErrLog(string appName,Exception exp, string errMsg)
       {
           log4net.Config.XmlConfigurator.Configure();
           ILog log = LogManager.GetLogger(appName);
           log.Error(errMsg,exp);
       }
       public static void WriteMsgLog(string appName, string msg)
       {
           log4net.Config.XmlConfigurator.Configure();
           ILog log = LogManager.GetLogger(appName);
           log.Info(msg);
       }
       public static void WriteSysLog(string msg,string action,string username)
       {
           WriteMsgLog("CommonCls", msg + "。" + action + "。" + username);
       }
    }
}