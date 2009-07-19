<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="Warfare_History" Title="Aphelion Trigger - Warfare History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">History</h1>
        <div class="dark-box">
        <asp:UpdatePanel ID="LogUpdatePanel" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <div class="form">
                <asp:RadioButtonList ID="LogFilter" runat="server" RepeatDirection="horizontal" AutoPostBack="true" Width="510" style="padding:0;margin:0">
                    <asp:ListItem Text="Show All" Value="0" Selected="true" />
                    <asp:ListItem Text="Attacks Only" Value="1" />
                    <asp:ListItem Text="Was Attacked" Value="2" />
                </asp:RadioButtonList>
                <asp:RadioButtonList ID="LogView" runat="server" RepeatDirection="horizontal" AutoPostBack="true" Width="510" style="padding:0;margin:0">
                    <asp:ListItem Text="Summary" Value="0" Selected="true" />
                    <asp:ListItem Text="Details" Value="1" />
                </asp:RadioButtonList>
                </div>
                <br />
                <asp:GridView ID="AttackLogs" runat="server" 
                    ShowHeader="true"
                    AutoGenerateColumns="false" 
                    AllowPaging="true"  
                    PageSize="10" 
                    GridLines="none" 
                    CellPadding="0" 
                    CellSpacing="0"
                    CssClass="grid input100"
                    EmptyDataText="<p>No attack history found. Start making more enemies!</p>" > 
                    <Columns>     

                        <asp:TemplateField HeaderText="Opponent">
                            <ItemTemplate>
                                <%# GetAttackText( Convert.ToInt32( Eval( "AttackerHouseID") ), Convert.ToInt32( Eval( "DefenderHouseID") ) ) + " " + (GetIsAttacker( Convert.ToInt32( Eval( "AttackerHouseID" ) ) ) ? Eval("DefenderHouseName") : Eval("AttackerHouseName") ) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%# ((Csla.SmartDate)Eval( "AttackDate" )).Date.ToString( "%M/dd %h:mm tt" )%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Casualties">
                            <ItemTemplate>
                                <%# DisplayCasualties( Convert.ToInt32( Eval( "AttackerHouseID" ) ), Convert.ToInt32( Eval( "AttackerCasualties" ) ), Convert.ToInt32( Eval( "DefenderCasualties" ) ) )%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plunder">
                            <ItemTemplate>
                                <%# DisplayPlunder( Convert.ToInt32( Eval( "AttackerHouseID" ) ), Convert.ToInt32( Eval( "Plundered" ) ) )%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Captured">
                            <ItemTemplate>
                                <%# DisplayCapture( Convert.ToInt32( Eval( "AttackerHouseID" ) ), Convert.ToInt32( Eval( "Captured" ) ) )%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>
                                <%# Server.HtmlDecode( FormatPronouns( Convert.ToInt32( Eval( "AttackerHouseID" ) ), Eval( "Description" ).ToString() ) )%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
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

