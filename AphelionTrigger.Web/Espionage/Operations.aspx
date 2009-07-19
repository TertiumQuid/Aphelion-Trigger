<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Operations.aspx.cs" Inherits="Espionage_Operations" Title="Aphelion Trigger - Espionage Operations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">

<asp:ScriptManagerProxy runat="server" id="scriptManagerProxy" />

<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Espionage Operations</h1>
        <p><asp:Label ID="CreditsLabel" runat="server" /></p>    
        <div class="content-unit">
            <asp:Panel ID="NoTurnsPanel" runat="server" Visible="false"><p>You have no turns. You must have at least one turn to perform espionage operations.</p></asp:Panel>
            <asp:UpdatePanel ID="EspionageUpdatePanel" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <aphelion:ErrorLabel ID="BusinessError" runat="server" Legend="Operations Error" Text="" Visible="false" />
                    <asp:Panel ID="TargetPanel" runat="server">
                        <table class="form input100">
                            <tr><th colspan="2">Target the Enemy</th></tr>
                            <tr>
                                <td><asp:Label ID="HouseNameLabel" runat="server" AssociatedControlID="TargetHouseName" CssClass="label" Text="House Name:" /></td>
                                <td>
                                    <asp:TextBox ID="TargetHouseName" runat="server" CssClass="textbox input4"  />
                                        
                                    <ajax:AutoCompleteExtender 
                                        runat="server" 
                                        ID="HouseAutoCompleteExtender" 
                                        TargetControlID="TargetHouseName"
                                        ServicePath="~/WebServices/Houses.asmx" 
                                        ServiceMethod="GetNames"
                                        MinimumPrefixLength="2" 
                                        CompletionInterval="1000"
                                        EnableCaching="true"
                                        CompletionSetCount="12" />
                                </td>
                            </tr>                    
                        </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="OperationPanel" runat="server">
                        <table class="form input100">
                            <tr><th colspan="2">Select Operation</th></tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="Operations" runat="server" 
                                        DataSourceID="EspionageOperationListDataSource" 
                                        DataKeyNames="ID"  
                                        AllowSorting="true"
                                        AutoGenerateColumns="false" 
                                        AllowPaging="false" 
                                        GridLines="none" 
                                        CellPadding="0" 
                                        CellSpacing="0" 
                                        CssClass="grid input100"> 
                                        <SelectedRowStyle BackColor="yellow" ForeColor="black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate> 
                                                    <asp:Label ID="OperationStatus" runat="server" 
                                                        Text='<%# GetOperationStatus( (int)Eval("ID"), (int)Eval("Cost"), (int)Eval("Turns") ) %>' 
                                                        Visible='<%# !CanPerformOperation( (int)Eval("ID"), (int)Eval("Cost"), (int)Eval("Turns") ) %>' 
                                                        style="color:rgb(237,218,16);" />
                                                    <asp:LinkButton ID="Select" runat="server" Visible='<%# CanPerformOperation( (int)Eval("ID"), (int)Eval("Cost"), (int)Eval("Turns") ) %>' CommandName="Select" CommandArgument="Select" Text='Select' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:Label ID="Operation" runat="server" Text='<%# Eval( "Name" ) %>' /><br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Description" HeaderText="Details" />
                                            <asp:BoundField DataField="Cost" HeaderText="Cost" />
                                            <asp:BoundField DataField="Turns" HeaderText="Turns" />
                                            <asp:BoundField DataField="Experience" HeaderText="Base Experience" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>                       
                        </table>
                        <table class="form input100">
                            <tr>
                                <td colspan="2"><asp:LinkButton ID="Commit" runat="server" CssClass="button" Text="Commit Espionage" style="width:100px;" /></td>
                            </tr>                   
                        </table>
                    </asp:Panel>    
                    
                    <asp:Panel ID="ResultsPanel" runat="server" Visible="false">
                        <table class="form input100">
                            <tr><th colspan="2">Operation Results</th></tr>
                            <tr>
                                <td><asp:Label ID="Results" runat="server" /></td>
                            </tr>                   
                        </table>
                        <table class="form input100">
                            <tr>
                                <td colspan="2">
                                    <asp:LinkButton ID="NewOperation" runat="server" CssClass="button-inline" Text="New Operation" style="width:125px;" />
                                    <asp:LinkButton ID="ContinueOperation" runat="server" CssClass="button-inline" Text="Continue Operation" style="width:125px;" /><br />
                                </td>
                            </tr>                   
                        </table>
                    </asp:Panel>                         
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <csla:CslaDataSource ID="EspionageOperationListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.EspionageOperationList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="EspionageOperationListDataSource_SelectObject" />   
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>    
</asp:Content>

