<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="MvcMember.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.4.js" type="text/javascript"></script>
    <script type="text/javascript">
        function submit_word() {
            var id = 1;
            var title = "私人";
            $.ajax({
                type: 'post',
                url: '/AjaxService.svc/DoWork',
                contentType: 'application/json',
                dataType:'text',
                data: '{"parentid":' + id + ',"itemname":"' + title + '"}',
                success: function (msg) {
                    var a = eval('(' + msg + ')');
                    if (String(a.d).length > 0) { alert(a.d); }
                    else { alert("服务器超时"); }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1"
            runat="server" Text="调用Wcf" onclick="Button1_Click" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    <p>
    <asp:TextBox ID="pwd" runat="server" TextMode="Password"></asp:TextBox>
    <asp:Button ID="Button3"
            runat="server" Text="认证" onclick="Button3_Click" />
    </p>
    <div style="margin-top:30px;">
        <asp:TextBox ID="keyword" runat="server"></asp:TextBox>
        <asp:Button ID="Button2"
            runat="server" Text="加密" onclick="Button2_Click" />
        <asp:Button ID="Button4"
            runat="server" Text="解密" onclick="Button4_Click" />
        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    </div>
    <div style="margin-top:30px;">
        <label><%=cpuInfo %></label>
    </div>
    </form>
</body>
</html>
