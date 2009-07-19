<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Census.aspx.cs" Inherits="House_Census" Title="Aphelion Trigger - House Census Listings" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-full">
        <h1 class="pagetitle">House Census</h1>
        <p>Here before you, the public records of Terranovan Houses, nocuously audited and prepared by bureaucrats, statisticians, and spies.</p>
        <div class="dark-box">
        <asp:UpdatePanel ID="RankingsUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="Houses" runat="server" 
                    DataSourceID="HouseListDataSource" 
                    DataKeyNames="ID"  
                    AllowSorting="true"
                    AutoGenerateColumns="false" 
                    AllowPaging="true" 
                    PageSize="25" 
                    PagerSettings-Mode="NumericFirstLast" 
                    PagerSettings-PageButtonCount="25"
                    GridLines="none" 
                    CellPadding="0" 
                    CellSpacing="0" 
                    CssClass="grid input100"> 
                    <Columns>      
                        <asp:TemplateField HeaderText="Rank" SortExpression="Rank">
                            <ItemTemplate> 
                                <%# Eval( "Rank" ) + " (" + FormatRank( Convert.ToInt32( Eval( "Rank" ) ), Convert.ToInt32( Eval( "LastRank" ) ) ) + ")" %>
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="House" SortExpression="Name" ItemStyle-Font-Size="105%">
                            <ItemTemplate> 
                                <asp:Panel ID="NamePanel" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="cursor:pointer;">
                                                <%# FormatName( (int)Eval( "ID" ), Eval( "Name" ).ToString(), (int)Eval( "GuildID" ) )%>
                                                <asp:Image ID="LeaderIcon" runat="server" ImageUrl="~/Images/Game/sun.gif" Visible='<%# Eval( "ID" ).ToString() == Eval( "FactionLeaderHouseID" ).ToString() %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel> 
                                
                                <asp:Panel CssClass="popup-menu" ID="PopupMenu" runat="server" Visible='<%# IsAuthenticated && (int)Eval( "ID") != HouseID %>' style="display:none;">
                                    <h3><%# Eval( "Name" ) %></h3>
                                    <h4><%# (Eval( "ID" ).ToString() != Eval( "FactionLeaderHouseID" ).ToString() ? "Serving" : "Leading" ) + " the " + Eval( "FactionDisplay" ) %></h4>
                                    <asp:LinkButton ID="AttackLink" runat="server" CommandName="Attack" CommandArgument='<%# Eval( "ID") + "|" + Eval( "Name") %>' Text="Attack" />
                                    <asp:LinkButton ID="SendMessageLink" runat="server" CommandName="Message" CommandArgument='<%# Eval( "ID") + "|" + Eval( "Name") %>' Text="Message" />
                                    <asp:LinkButton ID="ProfileLink" runat="server" CommandName="Profile" CommandArgument='<%# Eval( "ID") %>' Text="Profile" />
                                </asp:Panel>
                                
                                <ajax:HoverMenuExtender ID="HouseHoverMenu" runat="Server" 
                                    Enabled='<%# IsAuthenticated && (int)Eval( "ID") != HouseID  %>'
                                    HoverCssClass="popup-hover"
                                    PopupControlID="PopupMenu"
                                    PopupPosition="Left"
                                    TargetControlID="NamePanel"
                                    PopDelay="10" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Faction" HeaderText="Faction" SortExpression="Faction" />
                        <asp:BoundField DataField="Guild" HeaderText="Guild" SortExpression="Guild" />
                        <asp:BoundField DataField="Credits" HeaderText="Credits" SortExpression="Credits" />
                        <asp:BoundField DataField="ForcesCount" HeaderText="Forces" SortExpression="ForcesCount" />
                        <asp:BoundField DataField="Attack" HeaderText="Attack" SortExpression="Attack" />
                        <asp:BoundField DataField="Defense" HeaderText="Defense" SortExpression="Defense" />
                        <asp:BoundField DataField="Capture" HeaderText="Capture" SortExpression="Capture" />
                        <asp:BoundField DataField="Plunder" HeaderText="Plunder" SortExpression="Plunder" />
                        <asp:BoundField DataField="Stun" HeaderText="Stun" SortExpression="Stun" />
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="16">
                            <ItemTemplate>   
                                <asp:Image ID="StatusIcon" runat="server" ImageUrl='<%# "~/Images/" + (IsOnline( Eval( "UserID" ).ToString() ) ? "status_online.gif" : "status_offline.gif") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>
    <csla:CslaDataSource ID="HouseListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.HouseList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="HouseListDataSource_SelectObject" />   
</div>    
</asp:Content>

