using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class Employee
    {
        [ReadOnly(true)]
        public int ID { get; set; }
        [Required(ErrorMessage = "员工编号必须填写")]
        [Remote("CheckEmployeeIDExists", "Employee", ErrorMessage = "员工编号已存在")]
        public string EmployeeID { get; set; }
        [Required(ErrorMessage = "员工姓名必须填写")]
        [Remote("CheckNameExists", "Employee", ErrorMessage = "员工姓名已存在")]
        public string Name { get; set; }
        public string Sex { get; set; }
        public string CardNo { get; set; }

        public string IDCard { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string DmsID { get; set; }
    }
}