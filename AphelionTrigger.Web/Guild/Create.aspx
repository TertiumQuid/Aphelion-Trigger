<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Create.aspx.cs" Inherits="Guild_Create" Title="Aphelion Trigger - Create Guild" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Create a Guild</h1>  
        <div class="content-unit">
            <aphelion:ErrorLabel ID="GuildError" runat="server" Legend="Guild Error" Visible="false" />
            <table class="form input100">
                <tr>
                    <td><asp:Label ID="NameLabel" runat="server" AssociatedControlID="Name" CssClass="label" Text="Name:" /></td>
                    <td><asp:TextBox ID="Name" runat="server" CssClass="textbox input6" /></td>
                </tr>         
                <tr>
                    <td><asp:Label ID="DescriptionLabel" runat="server" AssociatedControlID="Description" CssClass="label" Text="Description:" /></td>
                    <td>
                        <asp:TextBox ID="Description" runat="server" CssClass="textbox input6" TextMode="multiLine" Rows="12" />
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
	                            elements : '<%=Description.ClientID %>',
	                            width : "400"
                                }
                            );
                        </script>
                    </td>
                </tr>              
                <tr>
                    <td><asp:Label ID="CostLabel" runat="server" AssociatedControlID="CreditsLabel" CssClass="label" Text="Cost:" /></td>
                    <td><asp:Label ID="CreditsLabel" runat="server" CssClass="label" Text='<%# Cost.ToString() + " credits" %>' /></td>
                </tr> 
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:LinkButton ID="CreateGuild" runat="server" CssClass="button" Text="Create" style="width:75px;" />
                    </td>
                </tr> 
            </table>
        </div>
        <p class="vertical" />
    </div>

    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>

