<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="Spies.aspx.cs" Inherits="System_Spies" Title="Aphelion Trigger - Admin Spies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">

<div class="main">    
    <div class="main-content main-full">
        <h1 class="pagetitle">Spys</h1>
        <aphelion:ErrorLabel ID="BusinessError" runat="server" Legend="Spy Error" Visible="false" />
        <div class="dark-box">
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="SpyListView" runat="server">
                    <asp:GridView ID="MainGrid" runat="server" 
                        DataSourceID="SpyListDataSource" 
                        DataKeyNames="ID" 
                        AutoGenerateColumns="false" 
                        GridLines="none" 
                        CellPadding="0" 
                        CellSpacing="0" 
                        CssClass="grid input100"> 
                        <Columns> 
                            <asp:TemplateField HeaderText="Tech" ItemStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="SpyName" runat="server" Text='<%# Eval( "Name" ) %>' CommandName="Select" />
                                    <boxover:BoxOver id="SpyNameBoxOver" runat="server" 
                                        header='<%# Eval( "Name" ) %>'
                                        body='<%# Eval( "Summary" ) %>' 
                                        CssBody="boxover-body input10"
                                        CssHeader="boxover-header"  
                                        CssClass="boxover" 
                                        controltovalidate="SpyName"  />                        
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Faction" HeaderText="Faction" />
                        </Columns>
                    </asp:GridView>
                </asp:View>
                <asp:View ID="SpyEditView" runat="server"> 
                    <div class="form">
                        <asp:DetailsView ID="MainDetails" runat="server"
                            AutoGenerateEditButton="true"
                            AutoGenerateInsertButton="true" 
                            CommandRowStyle-CssClass="button-row"
                            GridLines="none"
                            AutoGenerateRows="False" 
                            DataSourceID="SpyDataSource" 
                            DataKeyNames="ID">    
                            <Fields>
                                <asp:TemplateField HeaderText="Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Name" runat="server" CssClass="textbox input6"  Text='<%# Bind( "Name" ) %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Faction">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="Faction" runat="server"
                                            SelectedValue='<%# Bind("FactionID") %>' 
                                            DataSourceID="FactionListDataSource"
                                            DataTextField="Text"
                                            DataValueField="Value" 
                                            CssClass="textbox input8"  />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Cost" runat="server" CssClass="textbox input6" Text='<%# Bind("Cost") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
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
                                        <asp:TextBox ID="Description" TextMode="MultiLine" Rows="15" runat="server" Text='<%# Bind("Description") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>                           
                            </Fields>
                        </asp:DetailsView>
                    </div>                                    
                </asp:View>
            </asp:MultiView>
            <asp:LinkButton ID="InsertLink" runat="server" CssClass="button" style="width:100px;margin-top:4px;" Text="Create a Spy" Visible="true" />
        </div>
        <p class="vertical" />
    </div>
    <csla:CslaDataSource ID="SpyListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.SpyList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="SpyListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="SpyDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.Spy" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnInsertObject="SpyDataSource_InsertObject" 
        OnUpdateObject="SpyDataSource_UpdateObject"
        OnSelectObject="SpyDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="FactionListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.FactionList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="FactionListDataSource_SelectObject" />
            
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" />
    </div> 
</div>           
</asp:Content>
