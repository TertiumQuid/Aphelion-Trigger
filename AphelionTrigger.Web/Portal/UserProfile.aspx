<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" ValidateRequest="false" Inherits="Portal_UserProfile" Title="Aphelion Trigger - User Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle"><asp:Label ID="TitleLabel" runat="server" /></h1>
        <aphelion:ErrorLabel ID="BusinessError" runat="server" Legend="Profile Error" Visible="false" />
        <div class="content-unit input100">
            <div class="form">
                <asp:DetailsView ID="MainDetails" runat="server"  
                    AutoGenerateEditButton="false" 
                    GridLines="none"
                    CommandRowStyle-CssClass="button-row"
                    AutoGenerateRows="False" 
                    DataSourceID="UserDataSource" 
                    DataKeyNames="ID">    
                    <Fields>
                        <asp:TemplateField HeaderText="Email">
                            <EditItemTemplate>
                                <asp:TextBox ID="Email" runat="server" CssClass="textbox input6" Text='<%# Bind("Email") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Password">
                            <EditItemTemplate>
                                <asp:TextBox ID="Password" runat="server" CssClass="textbox input6" Text='<%# Bind("Password") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <EditItemTemplate>
                                <asp:TextBox ID="Location" runat="server" CssClass="textbox input6" Text='<%# Bind("Location") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Signature">
                            <EditItemTemplate>
                                <asp:TextBox ID="Signature" runat="server" CssClass="textbox input6" Text='<%# Bind("Signature") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Website URL">
                            <EditItemTemplate>
                                <asp:TextBox ID="WebsiteURL" runat="server" CssClass="textbox input6" Text='<%# Bind("WebsiteURL") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Personal Text">
                            <EditItemTemplate>
                                <asp:TextBox ID="PersonalText" runat="server" CssClass="textbox input6" Text='<%# Bind("PersonalText") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ICQ">
                            <EditItemTemplate>
                                <asp:TextBox ID="ICQ" runat="server" CssClass="textbox input6" Text='<%# Bind("ICQ") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AIM">
                            <EditItemTemplate>
                                <asp:TextBox ID="AIM" runat="server" CssClass="textbox input6" Text='<%# Bind("AIM") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MSN">
                            <EditItemTemplate>
                                <asp:TextBox ID="MSN" runat="server" CssClass="textbox input6" Text='<%# Bind("MSN") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="YIM">
                            <EditItemTemplate>
                                <asp:TextBox ID="YIM" runat="server" CssClass="textbox input6" Text='<%# Bind("YIM") %>' />                                    
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" CssClass="button" style="width:50px;margin-left:0;" Text="Update" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Fields>
                </asp:DetailsView>
            </div>
        </div>
        <p class="vertical" />
    </div>
        
    <csla:CslaDataSource ID="UserDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.User" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnUpdateObject="UserDataSource_UpdateObject"
        OnSelectObject="UserDataSource_SelectObject" />

    <div class="sidebar">
        <aphelion:Login ID="ATLogin" runat="server" />

        <aphelion:Messages ID="ATMessages" runat="server" />

        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>

