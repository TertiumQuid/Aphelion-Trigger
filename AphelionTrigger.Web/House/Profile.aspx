<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="House_Profile" Title="Aphelion Trigger - House Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle"><asp:Label ID="HouseLabel" runat="server" /></h1>  
        <h5>Profile:</h5>
        <div class="dark-box input100">
            <asp:Table ID="ProfileTable" runat="server" GridLines="none" CssClass="grid input100">
                <asp:TableRow ID="FactionRow" runat="server">
                    <asp:TableHeaderCell Width="150">Faction:</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2"><asp:Label ID="FactionLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="FactionLeaderRow" runat="server" Visible="false">
                    <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2" style="color:rgb(237,218,16);"><asp:Label ID="FactionLeaderLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="RankRow" runat="server">
                    <asp:TableHeaderCell>Rank:</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2"><asp:Label ID="RankLabel" runat="server" /> (<asp:Label ID="PointsLabel" runat="server" /> pts.)</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="AmbitionRow" runat="server">
                    <asp:TableHeaderCell>Ambition:</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2"><asp:Label ID="AmbitionLabel" runat="server" Font-Bold="true" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="CreditsRow" runat="server">
                    <asp:TableHeaderCell>Credits:</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2"><asp:Label ID="CreditsLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="StatsRow" runat="server">
                    <asp:TableHeaderCell>Stats:</asp:TableHeaderCell>
                    <asp:TableCell>&nbsp;</asp:TableCell>
                    <asp:TableCell>&nbsp;</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="IntelligenceRow" runat="server">
                    <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                    <asp:TableCell>Intelligence:</asp:TableCell>
                    <asp:TableCell Width="375"><asp:Label ID="IntelligenceLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="PowerRow" runat="server">
                    <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                    <asp:TableCell>Power:</asp:TableCell>
                    <asp:TableCell><asp:Label ID="PowerLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="ProtectionRow" runat="server">
                    <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                    <asp:TableCell>Protection:</asp:TableCell>
                    <asp:TableCell><asp:Label ID="ProtectionLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="AffluenceRow" runat="server">
                    <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                    <asp:TableCell>Affluence:</asp:TableCell>
                    <asp:TableCell><asp:Label ID="AffluenceLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="SpeedRow" runat="server">
                    <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                    <asp:TableCell>Speed:</asp:TableCell>
                    <asp:TableCell><asp:Label ID="SpeedLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="ContingencyRow" runat="server">
                    <asp:TableHeaderCell>&nbsp;</asp:TableHeaderCell>
                    <asp:TableCell>Contingency:</asp:TableCell>
                    <asp:TableCell><asp:Label ID="ContingencyLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="GuildRow" runat="server">
                    <asp:TableHeaderCell>Guild:</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2"><asp:Label ID="GuildLabel" runat="server" Text="No guild affiliation" /><asp:LinkButton ID="GuildLink" runat="server" /></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <asp:Literal ID="AdvancementLabel" runat="server" Text="<h5>Advancement:</h5>" />
        <asp:Panel ID="AdvancementBoxPanel" runat="server" CssClass="dark-box input100">
            <asp:Table ID="ExperienceTable" runat="server" GridLines="none" CssClass="grid input100">
                <asp:TableRow ID="LevelRow" runat="server">
                    <asp:TableHeaderCell>Level:</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2"><asp:Label ID="LevelLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="PointsRow" runat="server">
                    <asp:TableHeaderCell>Experience:</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2"><asp:Label ID="ExperienceLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="NextLevelRow" runat="server" VerticalAlign="top">
                    <asp:TableHeaderCell>Next Level:</asp:TableHeaderCell>
                    <asp:TableCell ColumnSpan="2">
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
                        </p>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="AdvancementsRow" runat="server">
                    <asp:TableCell ColumnSpan="3" >
                        <asp:GridView ID="Advancements" runat="server"  
                            DataSourceID="AdvancementListDataSource" 
                            ShowHeader="true"
                            AutoGenerateColumns="false" 
                            AllowPaging="false"  
                            GridLines="none" 
                            CellPadding="0" 
                            CellSpacing="0" 
                            CssClass="input100"> 
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
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
        <asp:Literal ID="TechnologyLabel" runat="server" Text="<h5>Technology:</h5>" />
        <asp:Panel ID="TechnologyBoxPanel" runat="server" CssClass="dark-box input100">
            <asp:GridView ID="TechnologyGrid" runat="server" 
                DataSourceID="TechnologyListDataSource" 
                DataKeyNames="ID"  
                EmptyDataText="No technology researched; obsolescence is not far behind."
                AutoGenerateColumns="false" 
                ShowHeader="false"
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0" 
                CssClass="grid input100"> 
                <Columns> 
                    <asp:TemplateField HeaderText="Tech">
                        <ItemTemplate>
                            <asp:Label ID="TechnologyName" runat="server" Text='<%# Eval( "Name" ) %>' style="cursor:help;" />
                            <boxover:BoxOver id="NameBoxOver" runat="server" 
                                CssBody="boxover-body input10" 
                                CssClass="boxover"  
                                CssHeader="boxover-header" 
                                body='<%# Eval( "Summary" ) + " " %>' 
                                controltovalidate="TechnologyName" 
                                header='<%# Eval( "Name" ) %>' 
                                SingleClickStop="true" />      
                        </ItemTemplate>
                        <FooterTemplate></ol></FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView> 
        </asp:Panel>
        <br />
        <asp:Literal ID="ForcesLabel" runat="server" Text="<h5>Forces:</h5>" /> 
        <asp:Panel ID="ForcesBoxPanel" runat="server" CssClass="dark-box input100">
            <asp:GridView ID="ForcesGrid" runat="server" 
                DataSourceID="UnitListDataSource" 
                DataKeyNames="ID"  
                EmptyDataText="Without an army, you can't even be master of youself."
                AutoGenerateColumns="false" 
                ShowHeader="false"
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0" 
                CssClass="grid input100"> 
                <Columns> 
                    <asp:TemplateField HeaderText="Tech">
                        <ItemTemplate>
                            <asp:Label ID="UnitName" runat="server" Text='<%# Eval( "Count" ) + " " + Pluralize( Eval( "Name" ).ToString() ) %>' style="cursor:help;" />
                            <boxover:BoxOver id="UnitNameBoxOver" runat="server" 
                                CssBody="boxover-body input10" 
                                CssClass="boxover" 
                                CssHeader="boxover-header" 
                                header='<%# Eval( "Name" ) %>' 
                                body='<%# Eval( "Summary" ) %>' 
                                controltovalidate="UnitName" 
                                SingleClickStop="true" /> 
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Panel ID="AttackSeparatorPanel" runat="server"><br /><hr style="background-color:rgb(150,150,150);" /><br /> </asp:Panel>
            <asp:Table ID="WarfareTable" runat="server" GridLines="none" CssClass="grid input100" Width="520">
                <asp:TableRow ID="AttackRow" runat="server">
                    <asp:TableHeaderCell Width="150">Total Attack:</asp:TableHeaderCell>
                    <asp:TableCell><asp:Label ID="AttackLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="DefenseRow" runat="server">
                    <asp:TableHeaderCell>Total Defense:</asp:TableHeaderCell>
                    <asp:TableCell><asp:Label ID="DefenseLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="CaptureRow" runat="server">
                    <asp:TableHeaderCell>Total Capture:</asp:TableHeaderCell>
                    <asp:TableCell><asp:Label ID="CaptureLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="PlunderRow" runat="server">
                    <asp:TableHeaderCell>Total Plunder:</asp:TableHeaderCell>
                    <asp:TableCell><asp:Label ID="PlunderLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="StunRow" runat="server">
                    <asp:TableHeaderCell>Total Stun:</asp:TableHeaderCell>
                    <asp:TableCell><asp:Label ID="StunLabel" runat="server" /></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="UnitCapRow" runat="server">
                    <asp:TableHeaderCell>Unit Cap:</asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:Label ID="UnitCountLabel" runat="server" style="float:left;" />
                        <asp:Panel ID="UnitCapBarContainer" runat="server" CssClass="progressbar_container-dark">
                            <asp:Panel ID="UnitCapBar" runat="server" CssClass="progressbar_bar">
                                <asp:Label ID="InnerUnitCountPercentLabel" runat="server" style="float:left;" />
                            </asp:Panel>
                            <asp:Label ID="OuterUnitCountPercentLabel" runat="server" style="float:left;" />
                         </asp:Panel>
                        <asp:Label ID="UnitCapLabel" runat="server" style="float:left;" /> 
                        <p>
                        <asp:Label ID="UnitCapProgressLabel" runat="server" />
                        </p>
                    </asp:TableCell>
                </asp:TableRow>
        </asp:Table> 
        </asp:Panel>
        <asp:Literal ID="EnemiesLabel" runat="server" Text="<h5>Recent Enemies:</h5>" /> 
        <asp:Panel ID="EnemiesBoxPanel" runat="server" CssClass="dark-box input100">
            <asp:GridView ID="AttackGrid" runat="server" 
                DataSourceID="AttackListDataSource" 
                DataKeyNames="ID"  
                EmptyDataText="No ememies made; dormancy and fear abound."
                AutoGenerateColumns="false" 
                ShowHeader="false"
                GridLines="none" 
                RowStyle-Font-Size="85%"
                CellPadding="0" 
                CellSpacing="0" 
                CssClass="input100"> 
                <Columns>    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <p style="margin:0;padding:3px;">The ignoble House <asp:LinkButton ID="EnemyLink" runat="server" CommandName="Attack" CommandArgument='<%# (GetIsAttacker( Convert.ToInt32( Eval( "AttackerHouseID" ) ) ) ? Eval("DefenderHouseID") : Eval("AttackerHouseID")) + "|" + (GetIsAttacker( Convert.ToInt32( Eval( "AttackerHouseID" ) ) ) ? Eval("DefenderHouseName") : Eval("AttackerHouseName"))  %>' Text='<%# GetIsAttacker( Convert.ToInt32( Eval( "AttackerHouseID" ) ) ) ? Eval("DefenderHouseName") : Eval("AttackerHouseName") %>' /></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>  
        </asp:Panel>
        <csla:CslaDataSource ID="TechnologyListDataSource" runat="server" 
            TypeName="AphelionTrigger.Library.TechnologyList" 
            TypeAssemblyName="AphelionTrigger.Library"
            OnSelectObject="TechnologyListDataSource_SelectObject" />
        <csla:CslaDataSource ID="UnitListDataSource" runat="server" 
            TypeName="AphelionTrigger.Library.UnitList" 
            TypeAssemblyName="AphelionTrigger.Library"
            OnSelectObject="UnitListDataSource_SelectObject" />
        <csla:CslaDataSource ID="AdvancementListDataSource" runat="server" 
            TypeName="AphelionTrigger.Library.AdvancementList" 
            TypeAssemblyName="AphelionTrigger.Library"
            OnSelectObject="AdvancementListDataSource_SelectObject" />
        <csla:CslaDataSource ID="AttackListDataSource" runat="server" 
            TypeName="AphelionTrigger.Library.AttackList" 
            TypeAssemblyName="AphelionTrigger.Library"
            OnSelectObject="AttackListDataSource_SelectObject" />
    </div>            

    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>  
</asp:Content>

