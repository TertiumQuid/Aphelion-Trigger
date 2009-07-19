<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Configuration.aspx.cs" Inherits="System_Configuration" ValidateRequest="false" Title="Aphelion Trigger - Admin System Configuration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-full">
        <h1 class="pagetitle">Configuration Settings</h1>
        <div class="content-unit">
            <aphelion:ErrorLabel ID="ConfigurationError" runat="server" Legend="Configuration Error" Visible="false" />
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
            <table class="form input100">
                <tr>
                    <th colspan="2">Game State</th>
                </tr>
                <tr>
                    <td><asp:Label ID="CurrentAgeLabel" runat="server" AssociatedControlID="CurrentAge" CssClass="left" Text="Current Age:" /></td>
                    <td><asp:DropDownList ID="CurrentAge" runat="server" CssClass="textbox" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="RegistrationLabel" runat="server" AssociatedControlID="Registration" CssClass="left" Text="Registration Active:" /></td>
                    <td><asp:CheckBox ID="Registration" runat="server" CssClass="field" /></td>
                </tr>
                <tr>
                    <th colspan="2">Information</th>
                </tr>
                <tr>
                    <td><asp:Label ID="VersionLabel" runat="server" AssociatedControlID="Version" CssClass="left" Text="Codebase Version:" /></td>
                    <td><asp:TextBox ID="Version" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="WelcomeTextLabel" runat="server" AssociatedControlID="WelcomeText" CssClass="left" Text="Welcome Text:" /></td>
                    <td><asp:TextBox ID="WelcomeText" runat="server" CssClass="textbox" TextMode="multiLine" Rows="10" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="RegistrationActiveTextLabel" runat="server" AssociatedControlID="RegistrationActiveText" CssClass="left" Text="Active Registration Text:" /></td>
                    <td><asp:TextBox ID="RegistrationActiveText" runat="server" CssClass="textbox" TextMode="multiLine" Rows="12" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="RegistrationInactiveTextLabel" runat="server" AssociatedControlID="RegistrationInactiveText" CssClass="left" Text="Inactive Registration Text:" /></td>
                    <td><asp:TextBox ID="RegistrationInactiveText" runat="server" CssClass="textbox" TextMode="multiLine" Rows="12" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="PlayerProtocolsLabel" runat="server" AssociatedControlID="PlayerProtocols" CssClass="left" Text="Player Protocols:" /></td>
                    <td><asp:TextBox ID="PlayerProtocols" runat="server" CssClass="textbox" TextMode="multiLine" Rows="12" /></td>
                </tr>
                <tr>
                    <th colspan="2">Advancement</th>
                </tr>
                <tr>
                    <td><asp:Label ID="LevelCapLabel" runat="server" AssociatedControlID="LevelCap" CssClass="left" Text="Level Cap:" /></td>
                    <td><asp:TextBox ID="LevelCap" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <th colspan="2">Factions</th>
                </tr>
                <tr>
                    <td><asp:Label ID="FactionLeaderBonusLabel" runat="server" AssociatedControlID="FactionLeaderBonus" CssClass="left" Text="Faction Leader Bonus:" /></td>
                    <td><asp:TextBox ID="FactionLeaderBonus" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <th colspan="2">Warfare</th>
                </tr>
                <tr>
                    <td><asp:Label ID="CasualtyFactorLabel" runat="server" AssociatedControlID="CasualtyFactor" CssClass="left" Text="Casualty Factor:" /></td>
                    <td><asp:TextBox ID="CasualtyFactor" runat="server" CssClass="textbox" MaxLength="6" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="AttackDelayLabel" runat="server" AssociatedControlID="AttackDelay" CssClass="left" Text="Attack Delay:" /></td>
                    <td><asp:TextBox ID="AttackDelay" runat="server" CssClass="textbox" MaxLength="2" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="CaptureCapLabel" runat="server" AssociatedControlID="CaptureCap" CssClass="left" Text="Capture Cap:" /></td>
                    <td><asp:TextBox ID="CaptureCap" runat="server" CssClass="textbox" MaxLength="10" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="CaptureFactorLabel" runat="server" AssociatedControlID="CaptureFactor" CssClass="left" Text="Capture Factor:" /></td>
                    <td><asp:TextBox ID="CaptureFactor" runat="server" CssClass="textbox" MaxLength="10" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="CaptureDivisorLabel" runat="server" AssociatedControlID="CaptureDivisor" CssClass="left" Text="Capture Divisor:" /></td>
                    <td><asp:TextBox ID="CaptureDivisor" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="ContingencyFactorLabel" runat="server" AssociatedControlID="ContingencyFactor" CssClass="left" Text="Contingency Factor:" /></td>
                    <td><asp:TextBox ID="ContingencyFactor" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <th colspan="2">Turns</th>
                </tr>
                <tr>
                    <td><asp:Label ID="StartingTurnsLabel" runat="server" AssociatedControlID="StartingTurns" CssClass="left" Text="Starting Turns:" /></td>
                    <td><asp:TextBox ID="StartingTurns" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="TurnUnitStepLabel" runat="server" AssociatedControlID="AttackDelay" CssClass="left" Text="Unit Step:" /></td>
                    <td><asp:TextBox ID="TurnUnitStep" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="TurnUnitCapLabel" runat="server" AssociatedControlID="TurnUnitCap" CssClass="left" Text="Unit Cap:" /></td>
                    <td><asp:TextBox ID="TurnUnitCap" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="TurnFactorLabel" runat="server" AssociatedControlID="TurnFactor" CssClass="left" Text="Turn Factor:" /></td>
                    <td><asp:TextBox ID="TurnFactor" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <th colspan="2">Credits</th>
                </tr>
                <tr>
                    <td><asp:Label ID="StartingCreditsLabel" runat="server" AssociatedControlID="StartingCredits" CssClass="left" Text="Starting Credits:" /></td>
                    <td><asp:TextBox ID="StartingCredits" runat="server" CssClass="textbox" MaxLength="8" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="CreditsFactorLabel" runat="server" AssociatedControlID="CreditsFactor" CssClass="left" Text="Credits Factor:" /></td>
                    <td><asp:TextBox ID="CreditsFactor" runat="server" CssClass="textbox" MaxLength="5" /></td>
                </tr>
                <tr>
                    <th colspan="2">Timers</th>
                </tr>
                <tr>
                    <td><asp:Label ID="ReportsRefreshRateLabel" runat="server" AssociatedControlID="ReportsRefreshRate" CssClass="left" Text="Reports Refresh Rate:" /></td>
                    <td><asp:TextBox ID="ReportsRefreshRate" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="MessagesRefreshRateLabel" runat="server" AssociatedControlID="MessagesRefreshRate" CssClass="left" Text="Messages Refresh Rate:" /></td>
                    <td><asp:TextBox ID="MessagesRefreshRate" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="AttacksRefreshRateLabel" runat="server" AssociatedControlID="AttacksRefreshRate" CssClass="left" Text="Attacks Refresh Rate:" /></td>
                    <td><asp:TextBox ID="AttacksRefreshRate" runat="server" CssClass="textbox" MaxLength="4" /></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:LinkButton ID="UpdateSettings" runat="server" CssClass="button" style="width:100px;" Text="Update Settings" /></td>
                </tr>
            </table>
        </div>
    </div>
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" />
    </div> 
</div>    
</asp:Content>
