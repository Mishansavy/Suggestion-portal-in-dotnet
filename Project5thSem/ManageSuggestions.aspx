<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSuggestions.aspx.cs" Inherits="Project5thSem.ManageSuggestions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Suggestions</title>
    <link rel="stylesheet" href="Content/ManageSuggestions.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="sidebar">
                <a class="sidebar-option" href="Profile.aspx">Profile</a>
                <a class="sidebar-option" href="Logout.aspx">Logout</a>
            </div>

            <h2>Manage Suggestions</h2>
            <div class="main-content">
                <div class="post-box">
                    
                    <asp:Repeater ID="rptSuggestions" runat="server">
                        <ItemTemplate>
                            <div class="suggestion-item">
                                <p><strong><%# Eval("username") %></strong> - <%# Eval("created_at", "{0:yyyy-MM-dd}") %></p>
                                <p><%# Eval("suggestion") %></p>

                                <!-- Display Reply if exists -->
                                <%# Eval("reply") != DBNull.Value ? $"<div class='reply-item'><p><strong>Management Reply:</strong> {Eval("reply")}</p><p><em>Replied on: {Eval("reply_at", "{0:yyyy-MM-dd}")}</em></p></div>" : "" %>

                                <br />
                                <!-- Admin can reply -->
                                <asp:TextBox ID="txtReply" runat="server" TextMode="MultiLine" CssClass="textarea" placeholder="Reply to this suggestion..."></asp:TextBox>
                                <!-- Corrected CommandArgument -->
                                <asp:Button ID="btnReply" runat="server" Text="Post Reply" CssClass="post-button" CommandArgument='<%# Eval("suggestion_id") %>' OnCommand="btnReply_Command" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
