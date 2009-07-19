<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Guild_Profile" Title="Aphelion Trigger - Guild Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle"><asp:Label ID="GuildLabel" runat="server" /></h1>  
        <p><asp:Label ID="DescriptionLabel" runat="server" /></p>
        <div class="dark-box">
            <h5>Members:</h5>
            <asp:GridView ID="Members" runat="server"  
                ShowHeader="true"
                AutoGenerateColumns="false" 
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0"  
                CssClass="grid"> 
                <Columns> 
                    <asp:BoundField DataField="Rank" HeaderText="#" />
                    <asp:TemplateField HeaderText="House">
                        <ItemTemplate>
                            <%# Eval( "Name" ) %>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:BoundField DataField="FactionDisplay" HeaderText="Faction" /> 
                </Columns>
            </asp:GridView>   
            <br /><br />
            <asp:Label ID="Invitations" runat="server"><h5>Invitations:</h5></asp:Label>
            <asp:GridView ID="InvitationMessages" runat="server"  
                ShowHeader="true"
                AutoGenerateColumns="false" 
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0"  
                CssClass="grid" 
                EmptyDataText="No invitations sent yet. <a href='Invite.aspx'>Click here</a> to invite a house to join your guild. "> 
                <Columns>     
                    <asp:TemplateField HeaderText="To">
                        <ItemTemplate>
                            <%# Eval( "Recipients" ) %>
                        </ItemTemplate>  
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Status" ItemStyle-Font-Bold="true">
                        <ItemTemplate>
                            <%# Eval( "GuildInvitationStatusType" ) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From">
                        <ItemTemplate>
                            <%# Eval( "SenderHouse" ) %>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Sent"> 
                        <ItemTemplate>
                            <%# ((Csla.SmartDate)Eval( "WriteDate" )).Date.ToString( "MMM dd, hh:mm tt" )%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>          
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

