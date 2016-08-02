using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MvcMember.Models
{
    public class AdminUser
    {
        public int ID { get; set; }
        [Required(ErrorMessage="用户ID必须填写")]
        [StringLength(8,MinimumLength=3,ErrorMessage="用户ID长度必须大于3小于8")]
        [Remote("CheckUserIDExists", "AdminUser", ErrorMessage = "用户账号已存在")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "用户ID只能包含字母或数字！")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "用户名必须填写")]
        [Remote("CheckUserNameExists", "AdminUser", ErrorMessage = "用户名已存在")]
        public string UserName { get; set; }
        [StringLength(8, MinimumLength = 3, ErrorMessage = "登录密码长度必须大于3小于8")]
        public string Password { get; set; }
        public string RoleName { get; set; }
        [ReadOnly(true)]
        public int LoginTimes { get; set; }
        public DateTime? LastLogin { get; set; }
        public string DmsID { get; set; }
    }
}