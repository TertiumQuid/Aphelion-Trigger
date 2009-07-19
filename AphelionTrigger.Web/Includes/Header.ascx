<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Includes_Header" %>
<%@ Register Src="~/Includes/Navigation.ascx" TagName="Navigation" TagPrefix="aphelion" %>
<%@ Register Src="~/Controls/Reports.ascx" TagName="Reports" TagPrefix="aphelion" %>
<div class="header">
    <div class="header-top">
        <div class="title">
            <h1>
                <asp:Label ID="TitleLabel" runat="server">
                    <span style='color:rgb(208,225,239);'>Aphelion</span>
                    <span style='color:rgb(107,141,148);'>Trigger</span>
                </asp:Label>
                <span class="version">&nbsp;<asp:Label ID="VersionLabel" runat="server" /></span>
            </h1>
            <h2>&nbsp;:: <asp:Label ID="AgeLabel" runat="server" /> ::</h2>
        </div>

        <aphelion:HUD ID="ATHUD" runat="server" />
    </div>
  
    <div class="header-middle">   
        <aphelion:Reports ID="ATReports" runat="server" />
    </div>

    <aphelion:Navigation ID="ATNavigation" runat="server" />
</div>