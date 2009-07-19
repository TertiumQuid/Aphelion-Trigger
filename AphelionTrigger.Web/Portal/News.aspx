<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="Portal_News" Title="Aphelion Trigger - News" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main"> 
    <div class="main-content main-partial">
        <h1 class="pagetitle">News</h1>
        <asp:Repeater ID="NewsRepeater" runat="server" DataSourceID="NewsPostListDataSource">
            <ItemTemplate>
                  <h2><%# Eval( "Title" ) %></h2>                            
                  <h4><%# ((DateTime)Eval( "NewsDate" )).Date.ToString( "%M/dd %h:mm tt" ) %>, by <%# Eval( "PostedByUser" ) %></h4>
                  <p><%# Eval( "Body" ) %></p>
                  <hr />
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <csla:CslaDataSource ID="NewsPostListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.NewsPostList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="NewsPostListDataSource_SelectObject" />
        
    <div class="sidebar">
        <aphelion:Login ID="ATLogin" runat="server" />
        
        <aphelion:Messages ID="ATMessages" runat="server" />      

        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>    
</asp:Content>

