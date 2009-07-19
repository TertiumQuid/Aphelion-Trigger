<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Messages.ascx.cs" Inherits="Controls_Messages" %>

<asp:UpdatePanel ID="MessagesUpdatePanel" runat="server" UpdateMode="conditional">
    <ContentTemplate>
        <asp:Panel ID="DivWrapper" runat="server">
        <div class="sidebar-blue">
            <div class="round-border-topleft"></div><div class="round-border-topright"></div>
            <h1 class="blue">You have messages!</h1>
            <asp:GridView ID="Messages" runat="server" AutoGenerateColumns="false" GridLines="horizontal" ShowHeader="false" CssClass="messages">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td>Subject:<asp:LinkButton ID="ViewLink" runat="server" Text='<%# Eval( "Subject" ) %>' CommandName="View" CommandArgument='<%# Eval( "ID" ) %>' /> </td>
                                    <td style="text-align:right"><asp:LinkButton ID="DismissLink" runat="server" Text="dismiss" CommandName="Dismiss" CommandArgument='<%# Eval( "ID" ) %>' /> </td>
                                </tr>
                                <tr>
                                    <td colspan="2">Sender: <%# "House " + Eval( "SenderHouse" ) %></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="color:rgb(125,125,125);"><%# GetMessageDate( (Csla.SmartDate)Eval( "WriteDate" ) ) %></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>  
        </asp:Panel>
        <asp:Timer ID="MessageTimer" runat="server" OnTick="MessageTimer_Tick" />
    </ContentTemplate>        
</asp:UpdatePanel>