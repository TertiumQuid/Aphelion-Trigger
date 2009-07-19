<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Spies.aspx.cs" Inherits="Espionage_Spies" Title="Aphelion Trigger - Spies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Acting Agents</h1>
        <p><asp:Label ID="CreditsLabel" runat="server" /></p>
        <div class="dark-box">
            <asp:GridView ID="ActingAgents" runat="server" 
                DataSourceID="SpyListDataSource" 
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
                    <asp:TemplateField HeaderText="Spy">
                        <ItemTemplate>
                            <asp:Label ID="SpyName" runat="server" Text='<%# Eval( "Name" ) %>' style="cursor:help;" />
                            <boxover:BoxOver id="NameBoxOver" runat="server" 
                                CssBody="boxover-body input10" 
                                CssClass="boxover" 
                                CssHeader="boxover-header" 
                                header='<%# Eval( "Name" ) %>' 
                                body='<%# Eval( "Summary" ) %>' 
                                controltovalidate="SpyName" 
                                Fade="false" 
                                SingleClickStop="true" />    
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lar" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("Larceny") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sur" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("Surveillance") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rec" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("Reconnaissance") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MICE" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("MICE") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amb" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval( "Ambush" )%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sab" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval( "Sabotage" )%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exp" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval( "Expropriation" )%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ins" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval( "Inspection" )%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval( "Subversion" )%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="$">
                        <ItemTemplate>
                            <%# Eval("Cost") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Recruitment" ItemStyle-Font-Size="95%">
                        <ItemTemplate>             
                            <asp:Panel ID="RecruitmentPanel" runat="server" Visible='<%# CanRecruitForces( (int)Eval("Cost") ) %>' style="padding-bottom:1px;">
                                <asp:TextBox ID="RecruitmentCount" runat="server" 
                                    Text='<%# MaxTroopRecruitment( (int)Eval("Cost") ) %>' 
                                    style="display:none;" />
                                <asp:LinkButton ID="RecuritLink" runat="server" 
                                    Text='<%# (MaxTroopRecruitment( (int)Eval("Cost") ) > 0) ? "Recruit" : "Recruit ($)" %>' 
                                    CommandName="Recruit" 
                                    CommandArgument='<%# Eval("ID") + "|" + Eval("Cost") + "|" + Eval("Name") %>' />&nbsp;
                                <asp:Label ID="RecruitmentCount_BoundControl" runat="server" />&nbsp;Spies
                                
                                <ajax:SliderExtender ID="RecruitmentCountExtender" runat="server"  
                                    TargetControlID="RecruitmentCount"
                                    Minimum="0" 
                                    Maximum='<%# MaxTroopRecruitment( (int)Eval("Cost") ) %>'
                                    BoundControlID="RecruitmentCount_BoundControl" 
                                    EnableHandleAnimation="false" 
                                    Steps='<%# MaxTroopRecruitment( (int)Eval("Cost") ) %>'
                                    RailCssClass="ajax__slider_h_rail input7" 
                                    HandleCssClass="ajax__slider_h_handle" />                             
                            </asp:Panel>
                            <asp:Label ID="RecruitmentStatus" runat="server" 
                                Text='<%# GetRecruitmentStatus( (int)Eval("Cost") ) %>' 
                                Visible='<%# GetRecruitmentStatus( (int)Eval("Cost") ).Length > 0 %>' 
                                style="padding-left:3px;color:rgb(237,218,16);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView> 
        </div>
    </div>
    <csla:CslaDataSource ID="SpyListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.SpyList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="SpyListDataSource_SelectObject" />   
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>

