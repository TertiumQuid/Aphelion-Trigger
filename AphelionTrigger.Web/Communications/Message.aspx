<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Communications_Message" Title="Aphelion Trigger - View Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Read Message</h1>  
        <div class="light-box">
            <asp:Repeater ID="Thread" runat="server">
                <ItemTemplate>
                    <p class="header"><%# Eval( "Subject" ) %></p>
                    <p><asp:Label ID="Body" runat="server" Text='<%# Eval( "Body" ) %>' /></p>   
                    <p><asp:Label ID="From" runat="server" Text='<%#  "- House " + Eval( "SenderHouse" ) + " | @ " + ((Csla.SmartDate)Eval( "WriteDate" )).Date.ToString( "MMM dd, hh:mm tt" ) %>' style="font-style:italic;font-size:90%;color:rgb(90,90,90);" /></p>           
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <p>
            <asp:HyperLink ID="Cancel" runat="server" CssClass="button-inline" Text="Back to Messages" NavigateUrl="~/Communications/Records.aspx" style="width:125px;" /><asp:LinkButton ID="Reply" runat="server" CssClass="button-inline" Text="Reply" />
            <asp:Label ID="HasGuildLabel" runat="server" Visible="false" /><asp:LinkButton ID="AcceptGuild" runat="server" CssClass="button-inline" Text="Accept" Visible="false" /><asp:LinkButton ID="RefuseGuild" runat="server" CssClass="button-inline" Text="Refuse" Visible="false" />
            
            <ajax:ConfirmButtonExtender ID="AcceptGuildConfirmButtonExtender" runat="server" 
            ConfirmText="Are you sure you want to join? Once you join a guild, you must fulfill your contract to the guild and may not leave for a period of indentured membership." 
            TargetControlID="AcceptGuild" />
        </p>
    </div>            
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>    
</asp:Content>

