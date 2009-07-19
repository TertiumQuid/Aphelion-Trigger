<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GuildLeaderboard.ascx.cs" Inherits="Includes_GuildLeaderboard" %>

<div class="sidebar-blue">
    <div class="round-border-topleft"></div><div class="round-border-topright"></div>
    <h1 class="blue">Guild Leaderboard</h1>
    <div class="sidebar-box">
        <asp:GridView ID="Rankings" runat="server" 
            AutoGenerateColumns="false" 
            GridLines="none"  
            CssClass="leaderboard"  
            HeaderStyle-CssClass="leaderboard-header" 
            RowStyle-CssClass="leaderboard-row" 
            CellPadding=2
            EmptyDataText="From the shadows, houses assemble into allied guilds, but for now, none choose to show their public face. ">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="#" />
                <asp:TemplateField HeaderText="Guild">
                    <ItemTemplate>
                        <asp:Panel CssClass="popup-menu" ID="PopupMenu" runat="server" Visible='<%# IsAuthenticated && (int)Eval( "ID") != GuildID %>' style="display:none;">
                            <div style="border:1px outset white;padding:4px;">
                                <div>&nbsp;<asp:LinkButton ID="MembersLink" runat="server" CommandName="Members" Text="Members" OnClientClick="return void(0);" /></div>
                                <div>&nbsp;<asp:LinkButton ID="ProfileLink" runat="server" CommandName="Profile" CommandArgument='<%# Eval( "ID") %>' Text="Profile" /></div>
                            </div>
                        </asp:Panel>
                                                    
                        <asp:Panel ID="NamePanel" runat="server">
                            <table width="100%">
                                <tr>
                                    <td><%# FormatName( (int)Eval( "ID" ), Eval( "Name" ).ToString() )%></td>
                                </tr>
                            </table>
                        </asp:Panel> 
                        
                        <ajax:HoverMenuExtender ID="HouseHoverMenu" runat="Server" 
                            Enabled='<%# IsAuthenticated && false && (int)Eval( "ID") != GuildID  %>'
                            HoverCssClass="popup-hover"
                            PopupControlID="PopupMenu"
                            PopupPosition="Left"
                            TargetControlID="NamePanel"
                            PopDelay="20" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>