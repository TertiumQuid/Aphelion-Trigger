<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="System_News" ValidateRequest="false" Title="Aphelion Trigger - Admin News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-full">
        <h1 class="pagetitle">News Posts</h1>
        <aphelion:ErrorLabel ID="NewsPostError" runat="server" Legend="News Post Error" Visible="false" />
        <div class="dark-box">
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="NewsPostListView" runat="server">
                    <asp:GridView ID="MainGrid" runat="server" 
                        DataSourceID="NewsPostListDataSource" 
                        DataKeyNames="ID" 
                        AutoGenerateColumns="false" 
                        GridLines="none" 
                        CellPadding="0" 
                        CellSpacing="0" 
                        CssClass="grid input100"> 
                        <Columns> 
                            <asp:TemplateField HeaderText="Title" ItemStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="NewsPostTitle" runat="server" 
                                        Text='<%# Eval( "Title" ) %>' 
                                        CommandName="Select" 
                                        CommandArgument='<%# Eval( "ID" ) %>' />
                                    <boxover:BoxOver id="TitleBoxOver" runat="server" 
                                        CssBody="boxover-body input10" 
                                        CssClass="boxover" 
                                        CssHeader="boxover-header" 
                                        body='<%# Eval( "Body" ) + " " %>' 
                                        controltovalidate="NewsPostTitle" 
                                        header='<%# Eval( "Title" ) %>' />                        
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PostedByUser" HeaderText="Posted By" />
                            <asp:TemplateField HeaderText="Date" >
                                <ItemTemplate>    
                                    <%# ((DateTime)Eval( "NewsDate" )).Date.ToString( "%M/dd %h:mm tt" ) %>
                                </ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                    </asp:GridView>
                </asp:View>
                <asp:View ID="NewsPostDetailsView" runat="server">
                    <div class="form">
                        <asp:DetailsView ID="MainDetails" runat="server"  
                            AutoGenerateEditButton="true" 
                            AutoGenerateInsertButton="true"
                            CommandRowStyle-CssClass="button-row"
                            AutoGenerateRows="False" 
                            GridLines="none" 
                            DataSourceID="NewsPostDataSource" 
                            DataKeyNames="ID">    
                            <Fields>
                                <asp:TemplateField HeaderText="Title">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Title" runat="server" CssClass="textbox input8" Text='<%# Bind("Title") %>' />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <EditItemTemplate>
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
                                        <asp:TextBox ID="Body" TextMode="MultiLine" Rows="20" runat="server" Text='<%# Bind("Body") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                    </div>
                </asp:View>
            </asp:MultiView>
            <br />
            <asp:LinkButton ID="InsertLink" runat="server" CssClass="button" style="width:125px;" Text="Create a News Post" />
        </div>
        <p class="vertical" />
    </div>
    <csla:CslaDataSource ID="NewsPostListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.NewsPostList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="NewsPostListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="NewsPostDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.NewsPost" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnUpdateObject="NewsPostDataSource_UpdateObject" 
        OnInsertObject="NewsPostDataSource_InsertObject"
        OnSelectObject="NewsPostDataSource_SelectObject" />
</div>                    
</asp:Content>

