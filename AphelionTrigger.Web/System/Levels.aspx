<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Levels.aspx.cs" Inherits="System_Levels" Title="Aphelion Trigger - Admin Levels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-full">
        <h1 class="pagetitle">Levels</h1>
        <aphelion:ErrorLabel ID="LevelError" runat="server" Legend="Level Error" Visible="false" />
        <div class="dark-box">
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="LevelListView" runat="server">
                    <asp:GridView ID="MainGrid" runat="server" 
                        DataSourceID="LevelListDataSource" 
                        DataKeyNames="ID" 
                        AutoGenerateColumns="false"  
                        GridLines="none" 
                        CellPadding="0"
                        CellSpacing="0"
                        AllowPaging="true" 
                        PageSize="100" 
                        CssClass="grid input100"> 
                        <Columns> 
                            <asp:TemplateField HeaderText="Level" ItemStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LevelLink" runat="server" Text='<%# Eval( "Faction" ) + " (" + Eval( "Rank" ) + ")" %>' CommandName="Select" CommandArgument='<%# Eval( "ID" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Experience" HeaderText="Experience" />
                            <asp:BoundField DataField="UnitCapacity" HeaderText="Unit Cap" />
                            <asp:BoundField DataField="Intelligence" HeaderText="Intelligence" />
                            <asp:BoundField DataField="Affluence" HeaderText="Affluence" />
                            <asp:BoundField DataField="Power" HeaderText="Power" />
                            <asp:BoundField DataField="Protection" HeaderText="Protection" />
                            <asp:BoundField DataField="Speed" HeaderText="Speed" />
                            <asp:BoundField DataField="Contingency" HeaderText="Contingency" />
                            <asp:BoundField DataField="Free" HeaderText="Free Points" />
                        </Columns>
                    </asp:GridView>                       
                </asp:View>
                <asp:View ID="LevelEditView" runat="server">
                    <div class="form">
                        <asp:DetailsView ID="MainDetails" runat="server" 
                            DataKeyNames="ID" 
                            AutoGenerateEditButton="true"
                            AutoGenerateRows="False" 
                            CommandRowStyle-CssClass="button-row"
                            DataSourceID="LevelDataSource">  
                                <Fields>
                                    <asp:TemplateField HeaderText="Faction" InsertVisible="false">
                                        <EditItemTemplate>
                                            <%# Eval("Faction") %>                                 
                                        </EditItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Rank" InsertVisible="false">
                                        <EditItemTemplate>
                                            <%# Eval("Rank") %>                                 
                                        </EditItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Experience">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Experience" runat="server" CssClass="textbox input6" Text='<%# Bind("Experience") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Unit Capacity">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="UnitCapacity" runat="server" CssClass="textbox input6" Text='<%# Bind("UnitCapacity") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Intelligence">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Intelligence" runat="server" CssClass="textbox input6" Text='<%# Bind("Intelligence") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Affluence">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Affluence" runat="server" CssClass="textbox input6" Text='<%# Bind("Affluence") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Power">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Power" runat="server" CssClass="textbox input6" Text='<%# Bind("Power") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Protection">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Protection" runat="server" CssClass="textbox input6" Text='<%# Bind("Protection") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Speed">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Speed" runat="server" CssClass="textbox input6" Text='<%# Bind("Speed") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Contingency">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Contingency" runat="server" CssClass="textbox input6" Text='<%# Bind("Contingency") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Free">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Free" runat="server" CssClass="textbox input6" Text='<%# Bind("Free") %>' />                                    
                                        </EditItemTemplate>
                                    </asp:TemplateField> 
                                </Fields>
                        </asp:DetailsView>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <csla:CslaDataSource ID="LevelListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.LevelList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="LevelListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="LevelDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.Level" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnUpdateObject="LevelDataSource_UpdateObject"
        OnSelectObject="LevelDataSource_SelectObject" />
</div>          
</asp:Content>

