<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Attack.aspx.cs" Inherits="Warfare_Attack" EnableEventValidation="false" Title="Aphelion Trigger - Attack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">

<asp:ScriptManagerProxy runat="server" id="scriptManagerProxy" />

<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Attack</h1>    
        <div class="content-unit">
            <asp:Panel ID="NoTurnsPanel" runat="server" Visible="false"><p>You have no turns. You must have at least one turn to perform an attack.</p></asp:Panel>
            <asp:UpdatePanel ID="AttackUpdatePanel" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <aphelion:ErrorLabel ID="AttackError" runat="server" Legend="Attack Error" Text="" Visible="false" />
                    <asp:Panel ID="TargetPanel" runat="server">
                        <table class="form input100">
                            <tr><th colspan="2">1. Target the Enemy</th></tr>
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
                            <tr>
                                <td colspan="2"><asp:LinkButton ID="TargetButton" runat="server" CssClass="button" Text="Target" style="width:100px;" /></td>
                            </tr>                        
                        </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="ReviewPanel" runat="server">
                        <table class="form input100">
                            <tr><th colspan="4">2. Review Engagement</th></tr>
                            <tr style="font-weight:bold;">
                                <td colspan="2"><asp:Label ID="AttackerNameLabel" runat="server" /></td>
                                <td colspan="2"><asp:Label ID="DefenderNameLabel" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Power:</td><td><asp:Label ID="AttackerPowerLabel" runat="server" /></td>
                                <td>Power:</td><td><asp:Label ID="DefenderPowerLabel" runat="server" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Protection:</td><td><asp:Label ID="AttackerProtectionLabel" runat="server" /></td>
                                <td>Protection:</td><td><asp:Label ID="DefenderProtectionLabel" runat="server" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Attack:</td><td><asp:Label ID="AttackerAttackLabel" runat="server" /></td>
                                <td>Attack:</td><td><asp:Label ID="DefenderAttackLabel" runat="server" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Defense:</td><td><asp:Label ID="AttackerDefenseLabel" runat="server" /></td>
                                <td>Defense:</td><td><asp:Label ID="DefenderDefenseLabel" runat="server" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Capture:</td><td><asp:Label ID="AttackerCaptureLabel" runat="server" /></td>
                                <td>Capture:</td><td><asp:Label ID="DefenderCaptureLabel" runat="server" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Plunder:</td><td><asp:Label ID="AttackerPlunderLabel" runat="server" /></td>
                                <td>Plunder:</td><td><asp:Label ID="DefenderPlunderLabel" runat="server" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Stun:</td><td><asp:Label ID="AttackerStunLabel" runat="server" /></td>
                                <td>Stun:</td><td><asp:Label ID="DefenderStunLabel" runat="server" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Forces:</td><td><asp:Label ID="AttackerForcesLabel" runat="server" /></td>
                                <td>Forces:</td><td><asp:Label ID="DefenderForcesLabel" runat="server" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Credits:</td><td><asp:Label ID="AttackerCreditsLabel" runat="server" CssClass="field" /></td>
                                <td>Credits:</td><td><asp:Label ID="DefenderCreditsLabel" runat="server" CssClass="field" Text="???" /></td>
                            </tr>
                            <tr>
                                <td>Ambition:</td><td><asp:Label ID="AttackerAmbitionLabel" runat="server" /></td>
                                <td>Ambition:</td><td><asp:Label ID="DefenderAmbitionLabel" Text="???" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:LinkButton ID="AttackButton" runat="server" CssClass="button" Text="Attack" style="width:100px;" /></td>
                            </tr>  
                        </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="ResultsPanel" runat="server">
                        <table class="form input100">
                            <tr><th colspan="4">3. Attack Results</th></tr>
                            <tr>
                                <td colspan="2"><asp:Label ID="Results" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:LinkButton ID="NewAttack" runat="server" CssClass="button-inline" Text="New Attack" style="width:125px;" /><asp:LinkButton ID="ContinueAttack" runat="server" CssClass="button-inline" Text="Continue Attack" style="width:125px;" /><br /></td>
                            </tr> 
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                        </table>                 
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>   
            <span style="float:left;font-weight:bold;font-size:15px;color:rgb(255,0,0);">
            <asp:UpdateProgress runat="server" ID="AttackProgress" DisplayAfter="500" AssociatedUpdatePanelID="AttackUpdatePanel">
                <ProgressTemplate>
                    <div style="padding:2px;"><asp:Image ID="ProgressUpdateImage" runat="server" ImageUrl="~/Images/ajax-loader.gif" /> Performing Attack...</div>
                </ProgressTemplate>
                </asp:UpdateProgress>
            </span>
        </div>
    </div>
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div>    
</div> 
</asp:Content>

