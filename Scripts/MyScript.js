/// <reference path="jquery-1.4.4.min.js" />
function setCookie(Name, value) //cookies设置JS 
{
    var Days = 360;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000); //此 cookie 将被保存 360 天
    document.cookie = Name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}

function getCookie(Name) //cookies读取JS 
{
    var search = Name + "="
    if (document.cookie.length > 0) {
        offset = document.cookie.indexOf(search)
        if (offset != -1) {
            offset += search.length
            end = document.cookie.indexOf(";", offset)
            if (end == -1) end = document.cookie.length
            return unescape(document.cookie.substring(offset, end))
        }
        else return ""
    }
}
function delCookie(name)
{
     var exp = new Date();
     exp.setTime(exp.getTime() - 1);
     var cval=getCookie(name);
     if(cval!=null) document.cookie= name + "="+cval+";expires="+exp.toGMTString();
 }

 function checkNumber(content) {
     var Num = content;
     var i, j;
     var strTemp = "-0123456789";
     if (Num.length == 0)
         return false;
     for (i = 0; i < Num.length; i++) {
         j = strTemp.indexOf(Num.charAt(i));
         if (j == -1) {
             return false;
         }
     }
     return true;
 }
 function checkRate(content) {
     var Num = content;
     var i, j;
     var strTemp = ":0123456789";
     if (Num.length == 0)
         return false;
     for (i = 0; i < Num.length; i++) {
         j = strTemp.indexOf(Num.charAt(i));
         if (j == -1) {
             return false;
         }
     }
     if (content.indexOf(":") == -1) {
         return false;
     }
     return true;
 }

 $.fn.numeral = function () {
     $(this).css("ime-mode", "disabled");
     this.bind("keypress", function (e) {
         var key = window.event ? e.keyCode : e.which;
         if (key == 46) {
             if (this.value.indexOf(".") != -1) {
                 return false;
             }
         } else {
             var rtval = key >= 46 && key <= 57;
             if (key == 20 || key == 8 || key == 23 || key == 0 || key == 45) rtval = true;
             return rtval;
         }
     });
     this.bind("blur", function () {
         if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
             this.value = this.value.substr(0, this.value.length - 1);
         } else if (isNaN(this.value)) {
             this.value = "";
         }
     });
     this.bind("paste", function () {
         var s = clipboardData.getData('text');
         if (!/\D/.test(s));
         value = s.replace(/^0*/, '');
         return false;
     });
     this.bind("dragenter", function () {
         return false;
     });
     this.bind("keyup", function () {
         if (/(^0+)/.test(this.value)) this.value = this.value.replace(/^0*/, '');
     });
 };
 //删除记录,函数名
 function DeleteRow(funname,paranem,paraval) {
     if (confirm("你确定要删除吗？")) {
         $.ajax({
             type: 'post',
             url: '@Url.Content("../AjaxService.svc/"'+ funname + '")',
             contentType: 'application/json',
             dataType: 'text',
             data: '{"' + paranem + '":' + paraval + '}',
             success: function (msg) {
                 var a = eval('(' + msg + ')');
                 if (a.d == "1") {
                     location.reload();
                 }
             }

         });
     }
 }