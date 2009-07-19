<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Advancement.aspx.cs" Inherits="House_Advancement" Title="Aphelion Trigger - House Advancement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Advancement</h1>
        <div class="dark-box">    
            <table>
                <tr>
                    <td style="padding-right:7px;color:rgb(165,165,165);">Current Level:</td>
                    <td><asp:Label ID="LevelLabel" runat="server" /></td>
                </tr>
                <tr>
                    <td style="padding-right:7px;color:rgb(165,165,165);">Current Experience:</td>
                    <td><asp:Label ID="CurrentExperienceLabel" runat="server" /></td>
                </tr>
                <tr style="vertical-align:top;">
                    <td style="padding-right:7px;color:rgb(165,165,165);">Progress:</td>
                    <td>
                        <asp:Label ID="LevelExperienceLabel" runat="server" style="float:left;" />
                        <asp:Panel ID="LevelProgressBarContainer" runat="server" CssClass="progressbar_container-dark">
                            <asp:Panel ID="LevelProgressBar" runat="server" CssClass="progressbar_bar">
                                <asp:Label ID="InnerProgressPercentLabel" runat="server" style="float:left;" />
                            </asp:Panel>
                            <asp:Label ID="OuterProgressPercentLabel" runat="server" style="float:left;" />
                         </asp:Panel>
                        <asp:Label ID="NextLevelExperienceLabel" runat="server" style="float:left;" /> 
                        <p>
                        <asp:Label ID="LevelProgressLabel" runat="server" />                    
                    </td>
                    <td><asp:Label ID="ExperienceLabel" runat="server" CssClass="field" /></td>
                </tr>   
             </table>             
                       
            
            <hr style="background-color:rgb(150,150,150);" /><br /> 
            <asp:Panel ID="FreePanel" runat="server">
                <h5>Free Point Placement</h5>
                <asp:Table ID="FreeTable" runat="server" CssClass="grid input100" style="margin-bottom:3px;">
                    <asp:TableRow>
                        <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Intelligence</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Power</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Protection</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Affluence</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Speed</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Contingency</asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Current</asp:TableCell>
                        <asp:TableCell><asp:Label ID="Intelligence" runat="server" /></asp:TableCell>
                        <asp:TableCell><asp:Label ID="Power" runat="server" /></asp:TableCell>
                        <asp:TableCell><asp:Label ID="Protection" runat="server" /></asp:TableCell>
                        <asp:TableCell><asp:Label ID="Affluence" runat="server" /></asp:TableCell>
                        <asp:TableCell><asp:Label ID="Speed" runat="server" /></asp:TableCell>
                        <asp:TableCell><asp:Label ID="Contingency" runat="server" /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:LinkButton ID="SaveFreeLink" runat="server" Text="Save Placement(s)" />
            </asp:Panel>
            <br /><br />
            <h5>Level Advancements</h5>
            <asp:GridView ID="Advancements" runat="server"  
                DataSourceID="AdvancementListDataSource" 
                ShowHeader="true"
                AutoGenerateColumns="false" 
                AllowPaging="false"  
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0" 
                EmptyDataText="<p>Pathetic! Still at first level? Why, it's a wonder you're not dead! Naturally, you've not yet gained any statistics advancements for your house.</p>" 
                CssClass="grid input100"> 
                    <Columns>
                        <asp:BoundField HeaderText="Level" DataField="Rank" />
                        <asp:BoundField HeaderText="Experience" DataField="Experience" />
                        <asp:BoundField HeaderText="Intelligence" DataField="Intelligence" />
                        <asp:BoundField HeaderText="Affluence" DataField="Affluence" />
                        <asp:BoundField HeaderText="Power" DataField="Power" />
                        <asp:BoundField HeaderText="Protection" DataField="Protection" />
                        <asp:BoundField HeaderText="Speed" DataField="Speed" />
                        <asp:TemplateField HeaderText="Free">
                            <ItemTemplate><%# Eval( "FreePlaced" ) + "/" + Eval( "Free" ) %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
            </asp:GridView>            
        </div>
    </div>
    <csla:CslaDataSource ID="AdvancementListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.AdvancementList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="AdvancementListDataSource_SelectObject" />
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div>    
</div>         
</asp:Content>

