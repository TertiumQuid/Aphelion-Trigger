<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HouseLeaderboard.ascx.cs" Inherits="Includes_HouseLeaderboard" %>

<div class="sidebar-blue">
    <div class="round-border-topleft"></div><div class="round-border-topright"></div>
    <h1 class="blue">House Leaderboard</h1>
    <div class="sidebar-box">
        <asp:UpdatePanel ID="RankingsUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="Rankings" runat="server" 
                    AutoGenerateColumns="false" 
                    AllowPaging="true" 
                    PageSize="25" 
                    GridLines="none"  
                    PagerSettings-Mode="NumericFirstLast" 
                    PagerSettings-PageButtonCount="12"
                    CssClass="leaderboard"  
                    HeaderStyle-CssClass="leaderboard-header" 
                    RowStyle-CssClass="leaderboard-row" 
                    PagerStyle-CssClass="leaderboard-pager"
                    CellPadding="2"
                    EmptyDataText="Forces are marshalled, charters written, plans drafted, and fortress-estates rise where the greatest scientists and statesmen may gather beneath the tense banner of enlightened self-interest and a will to power.">
                    <Columns>
                        <asp:BoundField DataField="Rank" HeaderText="#" />
                        <asp:TemplateField HeaderText="House" ItemStyle-Font-Size="105%">
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
                                    <asp:LinkButton ID="EspionageLink" runat="server" CommandName="Espionage" CommandArgument='<%# Eval( "ID") + "|" + Eval( "Name") %>' Text="Espionage" />
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
                        <asp:TemplateField HeaderText="Credits" HeaderStyle-Width="40">
                            <ItemTemplate>   
                                <%# (int)Eval( "Credits") / 1000 %>k
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ForcesCount" HeaderText="Forces" HeaderStyle-Width="40" />
                        <asp:TemplateField HeaderText="" ItemStyle-Width="16">
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