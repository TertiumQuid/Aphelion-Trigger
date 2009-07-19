<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Faction_Profile" Title="Aphelion Trigger - Faction Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle"><asp:Label ID="FactionLabel" runat="server" /></h1>  
        <div class="column1-unit">
            <p><asp:Label ID="DescriptionLabel" runat="server" /></p>
            
            <h5>Faction Leader: <asp:LinkButton ID="FactionLeaderLink" runat="server" /></h5>  
            
            <div class="terra-form">
                <p>You can vote for your chosen faction leader, effectively electing to instate a house (including yours) as your faction's representative and awarding them special powers and privileges.</p>
                <p>
                    <asp:Label ID="FactionLeaderLabel" runat="server" AssociatedControlID="FactionLeader" CssClass="left" Text="Faction Leader House:" />
                    <asp:DropDownList ID="FactionLeader" runat="server" CssClass="field" />&nbsp;&nbsp;<asp:LinkButton ID="Vote" runat="server" Text="[save vote]" />
                </p>    
            </div>         
        </div>
        
    </div>            
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>  
</asp:Content>

