<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Research.aspx.cs" Inherits="Technology_Research" EnableEventValidation="false" Title="Aphelion Trigger - Technological Research" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Research</h1>
        <p>
            Researching technology requires three components: time, turns, and credits. While you are performing research, your R&amp;D team 
            will be constantly depleting your credits and turns to support their tireless work. If you exhaust your turns or credits, your 
            research will be suspended. If you are researching multiple technologies, and can not fund them all, they will all be suspended 
            while your researchers squabble over resources.
        </p>
        <div class="dark-box">
            <asp:GridView ID="Technologies" runat="server" 
                DataSourceID="TechnologyListDataSource" 
                DataKeyNames="ID" 
                AutoGenerateColumns="false" 
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0" 
                CssClass="grid input100"> 
                <Columns>      
                    <asp:TemplateField HeaderText="Technology" ItemStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:Label ID="TechnologyName" runat="server" Text='<%# Eval( "Name" ) %>' style="cursor:help;" />
                            <boxover:BoxOver id="NameBoxOver" runat="server" 
                                CssBody="boxover-body input10" 
                                CssClass="boxover" 
                                CssHeader="boxover-header" 
                                header='<%# Eval( "Name" ) %>' 
                                body='<%# Eval( "Summary" ) + " " %>' 
                                controltovalidate="TechnologyName" 
                                Fade="true" 
                                SingleClickStop="true" />                        
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applies To" >
                        <ItemTemplate>    
                            <%# Eval( "UnitClass" ).ToString() %>
                            <%# Pluralize( Eval( "Unit" ).ToString() ) %>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <img src="../Images/Game/time.gif" alt="Cost in Time" style="padding-right:3px;" /><%# Eval( "ResearchTime" ) %>
                            <img src="../Images/Game/hourglass.gif" alt="Cost in Turns" style="padding-right:3px;" /><%# Eval( "ResearchTurns" ) %>
                            <img src="../Images/Game/money.gif" alt="Cost in Credits" style="padding-right:3px;" /><%# Eval( "ResearchCost" ) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Research" >
                        <ItemTemplate>
                            
                            <asp:Label ID="Completed" runat="server" 
                                Visible='<%# IsResearchComplete( (int)Eval("ResearchStateID") ) %>' 
                                style="padding-left:3px;color:rgb(16,237,118);" 
                                Text="Completed Research" />
                            <asp:Label ID="LeftBarLabel" runat="server" style="float:left;" />
                            <asp:Panel ID="ProgressBarContainer" runat="server" Visible='<%# IsResearchInProgress( (int)Eval("ResearchStateID") ) %>' CssClass="progressbar_container-dark" style="width:110px;"> 
                                <asp:Panel ID="ProgressBar" runat="server" 
                                    CssClass="progressbar_bar" 
                                    style='<%# "width:" + ((Convert.ToDouble(Eval( "TurnsSpent" )) / Convert.ToDouble(Eval( "ResearchTurns" ))) * 100) + "%;"  %>'>
                                    <asp:Label ID="InnerProgressPercentLabel" runat="server" 
                                        style="float:left;" 
                                        Text='<%# ((Convert.ToDouble(Eval( "TurnsSpent" )) / Convert.ToDouble(Eval( "ResearchTurns" ))) * 100).ToString( "N2" ) + "%" %>' 
                                        Visible='<%# (Convert.ToDouble(Eval( "TurnsSpent" )) / Convert.ToDouble(Eval( "ResearchTurns" ))) > 0.5 %>' />
                                </asp:Panel>
                                <asp:Label ID="OuterProgressPercentLabel" runat="server" 
                                    style="float:left;font-size:90%;" 
                                    Text='<%# "&nbsp;" + ((Convert.ToDouble(Eval( "TurnsSpent" )) / Convert.ToDouble(Eval( "ResearchTurns" ))) * 100).ToString( "N2" ) + "%" %>' 
                                    Visible='<%# (Convert.ToDouble(Eval( "TurnsSpent" )) / Convert.ToDouble(Eval( "ResearchTurns" ))) <= 0.5 %>' />
                            </asp:Panel>
                            <asp:Label ID="RightBarLabel" runat="server" style="float:left;" />
                            
                        
                            <asp:LinkButton ID="StartResearch" runat="server" 
                                CommandName='<%#Convert.ToInt32( Eval("ResearchStateID") ) < 1 ? "Research" : "Resume" %>' 
                                CommandArgument='<%# Eval( "ID" ) + "|" + Eval( "Name" ) %>'
                                Text='<%#Convert.ToInt32( Eval("ResearchStateID") ) == 0 ? "Start Research" : "Resume" %>'
                                Visible='<%# Convert.ToInt32( Eval("ResearchStateID") ) != 1 && Convert.ToInt32( Eval("ResearchStateID") ) != 4 %>' />
                            <asp:LinkButton ID="SuspendResearch" runat="server" 
                                CommandName="Suspend" 
                                Text="Suspend" 
                                CommandArgument='<%# Eval( "ID" ) + "|" + Eval( "Name" ) %>' 
                                Visible='<%# Convert.ToInt32( Eval("ResearchStateID") ) == 1 %>' />
                            <asp:LinkButton ID="AbortResearch" runat="server" 
                                CommandName="Abort" 
                                Text="Abort" 
                                CommandArgument='<%# Eval( "ID" ) + "|" + Eval( "Name" ) %>' 
                                Visible='<%# Convert.ToInt32( Eval("ResearchStateID") ) > 0 && Convert.ToInt32( Eval("ResearchStateID") ) < 3 %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>      
        </div>
    </div>
    <csla:CslaDataSource ID="TechnologyListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.TechnologyList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="TechnologyListDataSource_SelectObject" />   
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>

