<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SystemLogs.aspx.cs" Inherits="System_SystemLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-full">
        <h1 class="pagetitle">System Logs</h1>
        <aphelion:ErrorLabel ID="NewsPostError" runat="server" Legend="News Post Error" Visible="false" />
        <div class="dark-box">
                    <asp:GridView ID="MainGrid" runat="server" 
                        DataSourceID="SystemLogListDataSource" 
                        DataKeyNames="ID" 
                        AutoGenerateColumns="false" 
                        GridLines="none" 
                        AllowPaging="true"  
                        PageSize="12"
                        CellPadding="0" 
                        CellSpacing="0" 
                        CssClass="grid input100"> 
                        <Columns> 
                            <asp:BoundField DataField="SystemType" HeaderText="Type" />
                            <asp:TemplateField HeaderText="Date" ItemStyle-Wrap="false">
                                <ItemTemplate>    
                                    <%# ( (Csla.SmartDate)Eval( "LogDate" ) ).Date.ToString( "%M/dd %h:mm tt" )%>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Details">
                                <ItemTemplate>
                                    <asp:Label ID="Message" runat="server" 
                                        Text='<%# Eval( "Message" ) %>' />
                                    <boxover:BoxOver id="LogBoxOver" runat="server" 
                                        CssBody="boxover-body input50" 
                                        CssClass="boxover" 
                                        CssHeader="boxover-header" 
                                        body='<%# Eval( "Details" ) + "&nbsp;" %>' 
                                        controltovalidate="Message"  
                                        SingleClickStop="true"
                                        header='<%# Eval( "Application" ) + " : " + Eval( "Source" ) %>' />                        
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
        </div>
        <p class="vertical" />
    </div>
    <csla:CslaDataSource ID="SystemLogListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.SystemLogList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="SystemLogListDataSource_SelectObject" /> 
</div>            
</asp:Content>