<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="Controls_Login" %>

<%@ Register Src="~/Controls/ErrorLabel.ascx" TagName="ErrorLabel" TagPrefix="aphelion" %>

<asp:Panel ID="LoginPanel" runat="server">
<div class="sidebar-blue">
    <div class="round-border-topleft"></div><div class="round-border-topright"></div>
    <h1 class="blue">Login</h1>
    <div class="sidebar-box">
        <fieldset>
            <aphelion:ErrorLabel ID="LoginError" runat="server" Legend="Login Error" Text="You entered an invalid username or password." Visible="false" />
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="Username" CssClass="label" Text="Username:" />
                <br />
                <asp:TextBox ID="Username" runat="server" CssClass="textbox input5" />
            </p>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="label" Text="Password:" />
                <br />
                <asp:TextBox ID="Password" runat="server" TextMode="password" CssClass="textbox input5" />
            </p>  
            <p>
                <asp:CheckBox ID="RememberMe" runat="server" Checked="true" Text="Remember Me" /> 
            </p>  
            <p><asp:LinkButton ID="SubmitLogin" runat="server" CssClass="button" Text="Submit" style="width:50px;" /></p>
        </fieldset>    
    </div>
</div> 
</asp:Panel>