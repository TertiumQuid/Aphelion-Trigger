<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ForumBoards.aspx.cs" Inherits="Portal_Forum_Boards" Title="Aphelion Trigger - Forum Boards" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main"> 
    <div class="main-content main-full">
        <h1 class="pagetitle">Forums</h1>
        <div class="column1-unit">
            <asp:Repeater ID="CategoryRepeater" runat="server" DataSourceID="ForumCategoryListDataSource">
                <HeaderTemplate>
                    <table cellspacing="0" class="boards">
                        <tr class="boards-top">
                            <th colspan="2">Forum</th>
                            <th>Topics</th>
                            <th>Posts</th>
                            <th>Last Post</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <th colspan="5" class="board-title">
                             <%# Eval( "Name" ) %>
                        </th>
                    </tr>
                    <asp:Repeater ID="BoardRepeater" runat="server" DataSourceID="ForumBoardListDataSource">
                        <ItemTemplate>
                            <tr>
                                <td rowspan="2"><asp:Image ID="BoardImage" runat="server" ImageUrl="~/Images/Forum/topic.gif" /></td>
                                <td><asp:HyperLink ID="BoardLink" runat="server" Text='<%# Eval( "Name" ) %>' NavigateUrl='<%# "ForumBoard.aspx?ID=" + Eval( "ID" ) %>' /></td>
                                <td><%# Eval( "TopicCount" ) %></td>
                                <td><%# Eval( "PostCount" ) %></td>
                                <td><%# (int)Eval( "PostCount" ) > 0 ? ( (Csla.SmartDate)Eval( "LastPostDate" ) ).Date.ToString( "f" ) + ( Eval( "LastTopicID" ).ToString() != "0" ? "<br /> by <em>" + Eval( "LastPostUser" ) + "</em> in <strong><a href='ForumTopic.aspx?ID=" + Eval( "LastTopicID" ) + "'>" + Eval( "LastPostSubject" ) + "</strong>" : "" ) : "Dead air..." %></td>
                            </tr>
                            <tr>
                                <td colspan="4"><%# Eval( "Description" ) %></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="board-alt">
                                <td rowspan="2"><asp:Image ID="BoardImage" runat="server" ImageUrl="~/Images/Forum/topic.gif" /></td>
                                <td><asp:HyperLink ID="BoardLink" runat="server" Text='<%# Eval( "Name" ) %>' NavigateUrl='<%# "ForumBoard.aspx?ID=" + Eval( "ID" ) %>' /></td>
                                <td><%# Eval( "TopicCount" ) %></td>
                                <td><%# Eval( "PostCount" ) %></td>
                                <td><%# (int)Eval( "PostCount" ) > 0 ? ( (Csla.SmartDate)Eval( "LastPostDate" ) ).Date.ToString( "f" ) + ( Eval( "LastTopicID" ).ToString() != "0" ? "<br /> by <em>" + Eval( "LastPostUser" ) + "</em> in <strong><a href='ForumTopic.aspx?ID=" + Eval( "LastTopicID" ) + "'>" + Eval( "LastPostSubject" ) + "</strong>" : "" ) : "Dead air..." %></td>
                            </tr>
                            <tr class="board-alt">
                                <td colspan="4"><%# Eval( "Description" ) %></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <csla:CslaDataSource ID="ForumCategoryListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.ForumCategoryList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="ForumCategoryListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="ForumBoardListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.ForumBoardList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="ForumBoardListDataSource_SelectObject" />
</div> 
</asp:Content>

