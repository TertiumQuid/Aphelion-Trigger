<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Codex.aspx.cs" ValidateRequest="false" Inherits="System_Codex" Title="Aphelion Trigger - Admin Codex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">   
    <div class="main-content main-full"> 
        <h1 class="pagetitle">Codex Records</h1>
        <aphelion:ErrorLabel ID="CodexRecordError" runat="server" Legend="Codex Record Error" Visible="false" />
        <div class="dark-box">
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="CodexRecordListView" runat="server">
                    <asp:GridView ID="MainGrid" runat="server" 
                        DataSourceID="CodexRecordListDataSource" 
                        DataKeyNames="ID,NodeDepth" 
                        AutoGenerateColumns="false"  
                        EmptyDataText="No codex records exist."
                        GridLines="none" 
                        CellPadding="0" 
                        CellSpacing="0"  Width="100%"
                        CssClass="grid"> 
                        <Columns> 
                            <asp:BoundField DataField="NodeDepth" HeaderText="Depth" />
                            <asp:TemplateField HeaderText="Title" ItemStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="CodexRecordTitle" runat="server" 
                                        Text='<%# Eval( "Title" ) %>' 
                                        CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LastUpdatedDate" HeaderText="Last Update" />
                        </Columns>
                    </asp:GridView>
                </asp:View>
                <asp:View ID="CodexRecordDetailsView" runat="server">
                    <div class="form">
                        <asp:DetailsView ID="MainDetails" runat="server"  
                            DataSourceID="CodexRecordDataSource" 
                            AutoGenerateEditButton="true" 
                            AutoGenerateInsertButton="true"
                            CommandRowStyle-CssClass="button-row"
                            AutoGenerateRows="False" 
                            GridLines="none" 
                            DataKeyNames="ID">    
                            <Fields>
                                <asp:TemplateField HeaderText="Title">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Title" runat="server" CssClass="textbox input8" Text='<%# Bind("Title") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parent Record">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="CodexRecords" runat="server"
                                            SelectedValue='<%# Bind("ParentCodexRecordID") %>' 
                                            DataSourceID="ParentListDataSource"
                                            DataTextField="Text"
                                            DataValueField="Value" 
                                            CssClass="textbox input6"  />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Body">
                                    <EditItemTemplate>
                                        <script language="javascript" type="text/javascript">
                                            tinyMCE.init 
                                            ( 
                                                {
                                                mode : "textareas",
                                                theme : "advanced",
                                                theme_advanced_toolbar_location : "top",
                                                theme_advanced_toolbar_align : "left",
                                                theme_advanced_path_location : "bottom",	
                                                theme_advanced_statusbar_location : "none",                    
                                                theme_advanced_buttons1 : "bold,italic,underline,strikethrough,|,bullist,numlist,indent,outdent,hr,link,unlink,|,justifyleft,justifycenter,justifyright,justifyfull,|,code ",
                                                theme_advanced_buttons2 : "formatselect,|,fontselect,fontsizeselect,|,forecolorpicker,backcolorpicker",
                                                theme_advanced_buttons3 : "",
                                                width : "400"
                                                }
                                            );
                                        </script> 
                                        <asp:TextBox ID="Body" TextMode="MultiLine" Rows="40" runat="server" style="width:99%;" Text='<%# Bind("Body") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                    </div>
                </asp:View>
            </asp:MultiView>
            <br />
            <asp:LinkButton ID="InsertLink" runat="server" CssClass="button" style="width:150px;" Text="Create a Codex Record" />
        </div>
    </div>
    <csla:CslaDataSource ID="CodexRecordListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.CodexRecordList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="CodexRecordListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="ParentListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.CodexRecordList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="ParentListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="CodexRecordDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.CodexRecord" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnUpdateObject="CodexRecordDataSource_UpdateObject" 
        OnInsertObject="CodexRecordDataSource_InsertObject"
        OnSelectObject="CodexRecordDataSource_SelectObject" />
</div>
</asp:Content>

