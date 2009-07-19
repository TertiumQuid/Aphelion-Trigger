<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Invite.aspx.cs" Inherits="Guild_Invite" Title="Aphelion Trigger - Invite House to Guild" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Send Guild Invitation</h1>  
        <div class="content-unit">
            <aphelion:ErrorLabel ID="MessageError" runat="server" Legend="Message Error" Visible="false" />
            <table class="form">
                <tr>
                    <td><asp:Label ID="RecipientLabel" runat="server" AssociatedControlID="Recipient" CssClass="label" Text="To:" /></td>
                    <td>
                        <asp:TextBox ID="Recipient" runat="server" CssClass="textbox input5" autocomplete="off" />
                    
                        <ajax:AutoCompleteExtender 
                            runat="server" 
                            ID="HouseAutoCompleteExtender" 
                            TargetControlID="Recipient"
                            ServicePath="~/WebServices/Houses.asmx" 
                            ServiceMethod="GetNames"
                            MinimumPrefixLength="2" 
                            CompletionInterval="1000"
                            EnableCaching="true"
                            CompletionSetCount="12" />
                    </td>
                </tr>             
                <tr>
                    <td><asp:Label ID="SubjectLabel" runat="server" AssociatedControlID="Subject" CssClass="label" Text="Subject:" /></td>
                    <td><asp:Label ID="Subject" runat="server" CssClass="textbox input5" /></td>
                </tr>          
                <tr>
                    <td><asp:Label ID="BodyLabel" runat="server" AssociatedControlID="Body" CssClass="label" Text="Body:" /></td>
                    <td>
                        <asp:TextBox ID="Body" runat="server" CssClass="textbox input5" TextMode="multiLine" Rows="12" />
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
	                            theme_advanced_buttons1 : "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,fontsizeselect",
	                            theme_advanced_buttons2 : "bullist,numlist,indent,outdent,hr,link,unlink,code,|formatselect,|,fontselect,",
	                            theme_advanced_buttons3 : "",
	                            elements : '<%=Body.ClientID %>',
	                            width : "400"
                                }
                            );
                        </script>
                    </td>
                </tr>          
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:LinkButton ID="SendMessage" runat="server" CssClass="button-inline" Text="Send" style="width:75px;" />
                        <asp:HyperLink ID="Cancel" runat="server" CssClass="button-inline" Text="Back to Profile" NavigateUrl="~/Guild/Profile.aspx" style="width:125px;" />
                    </td>
                </tr>      
            </table>
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

