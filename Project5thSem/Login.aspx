<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project5thSem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Login</h1>
            <asp:TextBox ID="txtUsernameOrEmail" runat="server" Placeholder="Enter username or email"></asp:TextBox> <br/> <br/>
            <asp:TextBox ID="txtPassword" runat="server" Placeholder="Enter Password" TextMode="Password"></asp:TextBox><br/> <br/>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/> <br/> <br/>

            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label> <br/> <br/>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Register.aspx">Go to Registration Page.</asp:HyperLink>
        </div>
    </form>
</body>
</html>
