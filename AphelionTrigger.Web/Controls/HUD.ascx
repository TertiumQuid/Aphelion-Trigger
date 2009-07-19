<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HUD.ascx.cs" Inherits="Controls_HUD" %>

<asp:UpdatePanel ID="HUDUpdatePanel" runat="server" UpdateMode="conditional">
    <ContentTemplate>
        <div id="AuthenticatedHUD" runat="server" class="hud">
            <ul>
                <li><a>House: <span><asp:Label ID="HouseName" runat="server" /></span></a></li>
            </ul>
            <div style="float:right;">
                <asp:HyperLink ID="UserProfileLink" runat="server" NavigateUrl="~/Portal/UserProfile.aspx" Text="[edit user profile]" style="text-decoration:none;" />
                <asp:LinkButton ID="LogoutLink" runat="server" OnClick="Logout_Click" Text="[logout]" style="text-decoration:none;" />
            </div>
            <br />
            <ul>
                <li><a>Turns: <span><asp:Label ID="Turns" runat="server" /></span></a></li>
                <li><a>|</a></li>
                <li><a>Credits: <span><asp:Label ID="Credits" runat="server" /></span></a></li>
                <li><a>|</a></li>
                <li><a>Forces: <span><asp:Label ID="Forces" runat="server" /></span></a></li>
                <li><a>|</a></li>
                <li><a>Level: <span><asp:Label ID="Level" runat="server" /></span></a></li>
            </ul>
            <ul>
                <li><span></span></li>
            </ul>
            <br />
        </div>   
        <div id="AdminHUD" runat="server" class="hud">
            <ul><li><a>You are logged in as <asp:Label ID="AdminName" runat="server" /></a></li></ul>
        </div>
        <div id="UnauthenticatedHUD" runat="server" class="hud">
            <a href="../Portal/Registration.aspx" style="font-weight:bold;">Click here to register and start playing immediately</a>. Aphelion Trigger is a 100% FREE 
            web-based, 4X strategy game. The only requirements are a browser, a little time, and some competitive ambition.
        </div>
        <asp:Timer ID="HUDTimer" runat="server" Interval="120000" OnTick="HUDTimer_Tick" />
    </ContentTemplate>
</asp:UpdatePanel>