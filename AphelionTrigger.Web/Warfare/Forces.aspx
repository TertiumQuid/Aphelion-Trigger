<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Forces.aspx.cs" Inherits="Warfare_Forces" Title="Aphelion Trigger - Standing Forces" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Standing Forces</h1>
        <p><asp:Label ID="CreditsLabel" runat="server" /></p>
        <div class="dark-box">
            <asp:GridView ID="StandingForces" runat="server" 
                DataSourceID="UnitListDataSource" 
                DataKeyNames="ID" 
                AutoGenerateColumns="false" 
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0" 
                RowStyle-Font-Size="85%"
                CssClass="grid input100"> 
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                        <ItemTemplate>
                            <%# Eval( "Count" ) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="UnitName" runat="server" Text='<%# Eval( "Name" ) %>' style="cursor:help;" />
                            <boxover:BoxOver id="NameBoxOver" runat="server" 
                                CssBody="boxover-body input10" 
                                CssClass="boxover" 
                                CssHeader="boxover-header" 
                                header='<%# "<span style=\"font-size:100%;color:rgb(150,150,150);)\">" + Eval( "UnitClass" ) + ":</span> " + Eval( "Name" ) + ((int)Eval( "Count" ) > 0 ? " (" + Eval( "Count" ) + ")" : string.Empty) %>' 
                                body='<%# Eval( "Summary" ) %>' 
                                controltovalidate="UnitName" 
                                Fade="false" 
                                SingleClickStop="true" />    
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="A" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("Attack") %><%# (Eval( "AttackTech" ).ToString() != "0" ? "<span style='color:rgb(100,100,255);'>+" + Eval( "AttackTech" ) + "</span>" : string.Empty)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("Defense") %><%# (Eval( "DefenseTech" ).ToString() != "0" ? "<span style='color:rgb(100,100,255);'>+" + Eval( "DefenseTech" ) : string.Empty)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="P" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("Plunder") %><%# (Eval( "PlunderTech" ).ToString() != "0" ? "<span style='color:rgb(100,100,255);'>+" + Eval( "PlunderTech" ) : string.Empty)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("Capture") %><%# (Eval( "CaptureTech" ).ToString() != "0" ? "<span style='color:rgb(100,100,255);'>+" + Eval( "CaptureTech" ) : string.Empty)%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="$">
                        <ItemTemplate>
                            <%# Eval("Cost") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Recruitment" ItemStyle-Font-Size="95%">
                        <ItemTemplate>             
                            <asp:Panel ID="RecruitmentPanel" runat="server" Visible='<%# CanRecruitForces( (int)Eval("Cost"), (int)Eval("UnitClassID") ) %>' style="padding-bottom:1px;">
                                <asp:TextBox ID="RecruitmentCount" runat="server" 
                                    Text='<%# MaxTroopRecruitment( (int)Eval("Cost"), (int)Eval("UnitClassID") ) %>' 
                                    style="display:none;" />
                                <asp:LinkButton ID="RecuritLink" runat="server" 
                                    Text='<%# (MaxTroopRecruitment( (int)Eval("Cost"), (int)Eval("UnitClassID") ) > 0) ? "Recruit" : "Recruit ($)" %>' 
                                    CommandName="Recruit" 
                                    CommandArgument='<%# Eval("ID") + "|" + Eval("Cost") + "|" + Eval("Name") %>' />&nbsp;
                                <asp:Label ID="RecruitmentCount_BoundControl" runat="server" />&nbsp;Units
                                
                                <ajax:SliderExtender ID="RecruitmentCountExtender" runat="server"  
                                    TargetControlID="RecruitmentCount"
                                    Minimum="0" 
                                    Maximum='<%# MaxTroopRecruitment( (int)Eval("Cost"), (int)Eval("UnitClassID") ) %>'
                                    BoundControlID="RecruitmentCount_BoundControl" 
                                    EnableHandleAnimation="false" 
                                    Steps='<%# MaxTroopRecruitment( (int)Eval("Cost"), (int)Eval("UnitClassID") ) %>'
                                    RailCssClass="ajax__slider_h_rail input7" 
                                    HandleCssClass="ajax__slider_h_handle" />                             
                            </asp:Panel>
                            <asp:Label ID="RecruitmentStatus" runat="server" 
                                Text='<%# GetRecruitmentStatus( (int)Eval("Cost"), (int)Eval("UnitClassID") ) %>' 
                                Visible='<%# GetRecruitmentStatus( (int)Eval("Cost"), (int)Eval("UnitClassID") ).Length > 0 %>' 
                                style="padding-left:3px;color:rgb(237,218,16);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView> 
        </div>
        <h5>Militaristic Overview:</h5>
        <div class="dark-box">
            <table class="grid input100">
                <tr><th>STATS</th><th>Attack</th><th>Defense</th><th>Plunder</th><th>Capture</th></tr>
                <tr>
                    <th width="100">Total:</th>
                    <td>&nbsp;<asp:Label ID="TotalAttack" runat="server" /></td>
                    <td><asp:Label ID="TotalDefense" runat="server" /></td>
                    <td><asp:Label ID="TotalPlunder" runat="server" /></td>
                    <td><asp:Label ID="TotalCapture" runat="server" /></td>
                </tr>
            </table>
        </div>
        <div class="dark-box">
            <table class="grid input100">
                <tr><th>UNIT CLASS</th><th>Militia</th><th>Military</th><th>Mercenary</th><th>Total</th></tr>
                <tr>
                    <th width="100">Total:</th>
                    <td>&nbsp;<asp:Label ID="MilitiaCount" runat="server" /></td>
                    <td><asp:Label ID="MilitaryCount" runat="server" /></td>
                    <td><asp:Label ID="MercenaryCount" runat="server" /></td>
                    <td><asp:Label ID="ForcesCount" runat="server" /></td>
                </tr>
            </table>
        </div>
    </div>
    <csla:CslaDataSource ID="UnitListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.UnitList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="UnitListDataSource_SelectObject" />   
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>

