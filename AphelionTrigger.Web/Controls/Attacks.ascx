<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Attacks.ascx.cs" Inherits="Controls_Attacks" %>

<asp:UpdatePanel ID="AttacksUpdatePanel" runat="server" UpdateMode="conditional">
    <ContentTemplate>
        <div class="sidebar-blue">
            <div class="round-border-topleft"></div><div class="round-border-topright"></div>
            <h1 class="blue">You have been attacked!</h1>
            <asp:GridView ID="Attacks" runat="server" AutoGenerateColumns="false" GridLines="horizontal" ShowHeader="false" CssClass="attacks">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2"><%# "House " + Eval( "AttackerHouseName" )%></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="color:rgb(125,125,125);"><%# ((Csla.SmartDate)Eval( "AttackDate" )).Date.ToString( "MMM dd, hh:mm tt" )%></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>  
        <asp:Timer ID="AttackTimer" runat="server" OnTick="AttackTimer_Tick" />
    </ContentTemplate>
</asp:UpdatePanel>