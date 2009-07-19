<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Resets.aspx.cs" Inherits="System_Resets" Title="Aphelion Trigger - Admin Game Resets" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-full">
        <h1 class="pagetitle">Game State Resets</h1>
        <div class="content-unit input100">
            <table class="form input100">
                <tr>
                    <td><asp:Label ID="AdvancementLabel" runat="server" AssociatedControlID="Advancement" CssClass="left" Text="Reset Advancement:" /></td>
                    <td><asp:Checkbox ID="Advancement" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="AttacksLabel" runat="server" AssociatedControlID="Attacks" CssClass="left" Text="Reset Attack Logs:" /></td>
                    <td><asp:Checkbox ID="Attacks" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="CreditsLabel" runat="server" AssociatedControlID="Credits" CssClass="left" Text="Reset Credits:" /></td>
                    <td><asp:Checkbox ID="Credits" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="ForcesLabel" runat="server" AssociatedControlID="Forces" CssClass="left" Text="Reset Forces:" /></td>
                    <td><asp:Checkbox ID="Forces" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="MessagesLabel" runat="server" AssociatedControlID="Forces" CssClass="left" Text="Reset Messages:" /></td>
                    <td><asp:Checkbox ID="Messages" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="RankingsLabel" runat="server" AssociatedControlID="Rankings" CssClass="left" Text="Reset Rankings:" /></td>
                    <td><asp:Checkbox ID="Rankings" runat="server" CssClass="field" /></td>
                </tr>  
                <tr>
                    <td><asp:Label ID="ReportsLabel" runat="server" AssociatedControlID="Reports" CssClass="left" Text="Reset Reports:" /></td>
                    <td><asp:Checkbox ID="Reports" runat="server" CssClass="field" /></td>
                </tr>  
                <tr>
                    <td><asp:Label ID="TurnsLabel" runat="server" AssociatedControlID="Turns" CssClass="left" Text="Reset Turns:" /></td>
                    <td><asp:Checkbox ID="Turns" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="AmbitionLabel" runat="server" AssociatedControlID="Ambition" CssClass="left" Text="Reset Ambition:" /></td>
                    <td><asp:Checkbox ID="Ambition" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="StatsReset" runat="server" AssociatedControlID="Stats" CssClass="left" Text="Reset Stats:" /></td>
                    <td><asp:Checkbox ID="Stats" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="TechnologyReset" runat="server" AssociatedControlID="Technology" CssClass="left" Text="Reset Technology:" /></td>
                    <td><asp:Checkbox ID="Technology" runat="server" CssClass="field" /></td>
                </tr> 
                <tr>
                    <td><asp:Label ID="AgesLabel" runat="server" AssociatedControlID="Ages" CssClass="left" Text="Reset Data on all Ages:" /></td>
                    <td><asp:Checkbox ID="Ages" runat="server" CssClass="field" />(leave unchecked for current age only)</td>
                </tr> 
                <tr><td colspan="2"><asp:LinkButton ID="Reset" runat="server" CssClass="button" style="width:100px;" Text="Reset Selected" /></td></tr>  
            </table>
        </div>
    </div>
</div>                
</asp:Content>

