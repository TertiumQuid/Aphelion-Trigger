<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Units.aspx.cs" Inherits="System_Units" ValidateRequest="false" Title="Aphelion Trigger - Admin Units" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-full">
        <h1 class="pagetitle">Units</h1>
        <aphelion:ErrorLabel ID="UnitError" runat="server" Legend="Unit Error" Visible="false" />
        <div class="dark-box">
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="UnitListView" runat="server">
                    <asp:GridView ID="MainGrid" runat="server" 
                        DataSourceID="UnitListDataSource" 
                        DataKeyNames="ID" 
                        AutoGenerateColumns="false" 
                        GridLines="none" 
                        CellPadding="0" 
                        CellSpacing="0" 
                        CssClass="grid input100"> 
                        <Columns> 
                            <asp:TemplateField HeaderText="Tech" ItemStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="UnitName" runat="server" Text='<%# Eval( "Name" ) %>' CommandName="Select" CommandArgument='<%# Eval( "ID" ) %>' />
                                    <boxover:BoxOver id="UnitNameBoxOver" runat="server" 
                                        CssBody="boxover-body input10" 
                                        CssClass="boxover" CssHeader="boxover-header" 
                                        body='<%# Eval( "Summary" ) %>' 
                                        controltovalidate="UnitName" 
                                        header='<%# Eval( "Name" ) %>' />                        
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Faction" HeaderText="Faction" />
                            <asp:BoundField DataField="UnitClass" HeaderText="Class" />
                        </Columns>
                    </asp:GridView>
                </asp:View>
                <asp:View ID="UnitEditView" runat="server">
                    <div class="form">
                        <asp:DetailsView ID="MainDetails" runat="server"
                            AutoGenerateEditButton="true"
                            AutoGenerateInsertButton="true" 
                            CommandRowStyle-CssClass="button-row"
                            GridLines="none"
                            AutoGenerateRows="False" 
                            DataSourceID="UnitDataSource" 
                            DataKeyNames="ID">    
                            <Fields>
                                <asp:TemplateField HeaderText="Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Name" runat="server" CssClass="textbox input6"  Text='<%# Bind( "Name" ) %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Class">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="UnitClasses" runat="server"
                                            SelectedValue='<%# Bind("UnitClassID") %>' 
                                            DataSourceID="UnitClassListDataSource"
                                            DataTextField="Text"
                                            DataValueField="Value" 
                                            CssClass="textbox input6"  />                                   
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
                                <asp:TemplateField HeaderText="Attack">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Attack" runat="server" CssClass="textbox input6" Text='<%# Bind("Attack") %>' />                                    
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Defense">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Defense" runat="server" CssClass="textbox input6" Text='<%# Bind("Defense") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plunder">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Plunder" runat="server" CssClass="textbox input6" Text='<%# Bind("Plunder") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Capture">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Capture" runat="server" CssClass="textbox input6" Text='<%# Bind("Capture") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stun">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Stun" runat="server" CssClass="textbox input6" Text='<%# Bind("Stun") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Repopulation">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Repopulation" runat="server" CssClass="textbox input6" Text='<%# Bind("RepopulationRate") %>' />                                 
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depopulation">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Depopulation" runat="server" CssClass="textbox input6" Text='<%# Bind("DepopulationRate") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Cost" runat="server" CssClass="textbox input6" Text='<%# Bind("Cost") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Experience">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Experience" runat="server" CssClass="textbox input6" Text='<%# Bind("Experience") %>' />                                   
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
            <asp:LinkButton ID="InsertLink" runat="server" CssClass="button" style="width:100px;margin-top:4px;" Text="Create a Unit" Visible="true" />
        </div>
        <p class="vertical" />
    </div>
    <csla:CslaDataSource ID="UnitListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.UnitList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="UnitListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="UnitDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.Unit" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnInsertObject="UnitDataSource_InsertObject" 
        OnUpdateObject="UnitDataSource_UpdateObject"
        OnSelectObject="UnitDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="UnitClassListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.UnitClassList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="UnitClassListDataSource_SelectObject" />
        
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

