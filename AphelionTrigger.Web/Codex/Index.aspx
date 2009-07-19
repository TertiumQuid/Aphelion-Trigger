<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Codex_Index" Title="Aphelion Trigger - Codex: Table of Contents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Codex: Game and World Guide</h1>
        <asp:UpdatePanel ID="CodexUpdatePanel" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TreeView ID="CodexMenu" runat="server" 
                                ExpandDepth="0"
                                PopulateNodesFromClient="false"
                                ShowLines="true" 
                                ShowExpandCollapse="true" >
                            </asp:TreeView> 
                        </td>
                        <td> 
                            <asp:DetailsView ID="MainDetails" runat="server"  
                                DataSourceID="CodexRecordDataSource" 
                                DataKeyNames="ID"
                                AutoGenerateEditButton="False" 
                                AutoGenerateInsertButton="False"
                                CommandRowStyle-CssClass="button-row"
                                AutoGenerateRows="False" 
                                GridLines="none" >    
                                <Fields>
                                    <asp:TemplateField HeaderText="">
                                        <EditItemTemplate>
                                            <h2><%# Eval( "Title" ) %></h2>
                                            <div style="font-size:92%;"><%# Eval("Body") %></div>
                                            <p style="font-size:90%;color:rgb(150,150,150);margin-top:10px;"><%# "- Last updated: " + Eval("LastUpdatedDate") %></p>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>                        
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <csla:CslaDataSource ID="CodexRecordDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.CodexRecord" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnSelectObject="CodexRecordDataSource_SelectObject" />

    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" />      

        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>

