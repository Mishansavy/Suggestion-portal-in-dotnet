<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Project5thSem.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Suggestion Page</title>
    <link rel="stylesheet" href="Content/MainPage.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="sidebar">
                <a class="sidebar-option" href="Profile.aspx">Profile</a>
                <a class="sidebar-option" href="Logout.aspx" >Logout</a>
            </div>

            <div class="main-content">
                <div class="post-box">
                    <h2>What's on your mind?</h2>
                    <asp:TextBox ID="txtPostContent" runat="server" TextMode="MultiLine" placeholder="Share something..." CssClass="textarea"></asp:TextBox>
                   
                    <br />
                    <asp:Button ID="btnPost" runat="server" Text="Post"  CssClass="post-button" OnClick="btnPost_Click"/>
                </div>
                
               <div class="post-content" id="postContentDiv" runat="server"></div>
            </div>
        </div>
    </form>
</body>
</html>
