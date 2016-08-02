using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace MvcMember
{
    public partial class test : System.Web.UI.Page
    {
        protected string cpuInfo = ""; 
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Management.ManagementClass cimobject = new System.Management.ManagementClass("Win32_Processor");
            System.Management.ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            if (!IsPostBack)
            {
                Button2.Visible = false;
                Button4.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AjaxService ms = new AjaxService();
            Label1.Text = ms.DoWork(2,"sa");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Label2.Text = CommonCls.MD5Encrypt(keyword.Text);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Label2.Text = CommonCls.MD5Decrypt(Label2.Text);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (pwd.Text == "mvc3")
            {
                Button2.Visible = true;
                Button4.Visible = true;
            }
        }
    }
}