<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ForumTopic.aspx.cs" Inherits="Portal_ForumTopic" ValidateRequest="false" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main"> 
    <div class="main-content main-full">
        <h1 class="pagetitle"><asp:Label ID="TopicTitle" runat="server" /></h1>
        <div class="column1-unit">
            <aphelion:ErrorLabel ID="PostError" runat="server" Legend="Post Error" Visible="false" />
            
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="UnitListView" runat="server">
                    <div class="dark-box" style="color:rgb(0,0,0);">
                        <asp:Panel ID="TopicPanel" runat="server" Visible="false" style="color:rgb(255,255,255);margin:5px 0 20px 3px;">
                            <div style="font-weight:bold;margin-bottom:3px;">For Administrators:</div>
                            <asp:DetailsView ID="TopicDetails" runat="server"   
                                AutoGenerateEditButton="true"
                                AutoGenerateInsertButton="False"
                                CommandRowStyle-CssClass="button-row"
                                GridLines="none"
                                AutoGenerateRows="False"  
                                DataSourceID="ForumTopicDataSource"
                                DataKeyNames="ID">  
                                <Fields>
                                    <asp:TemplateField HeaderText="Title:&nbsp;&nbsp;">
                                        <ItemTemplate><%# Eval( "Title" ) %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Title" runat="server" Text='<%# Bind( "Title" ) %>' CssClass="textbox input6" />                                   
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Locked:&nbsp;&nbsp;">
                                        <ItemTemplate><%# Eval( "Locked" ) %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="Locked" runat="server" Checked='<%# Bind( "Locked" ) %>' />                                   
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sticky:&nbsp;&nbsp;">
                                        <ItemTemplate><%# Eval( "Sticky" ) %></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="Sticky" runat="server" Checked='<%# Bind( "Sticky" ) %>' />                                   
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField><ItemTemplate>&nbsp;</ItemTemplate></asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                        </asp:Panel>
                    
                        <asp:GridView ID="MainGrid" runat="server" 
                            DataSourceID="ForumPostListDataSource" 
                            DataKeyNames="ID" 
                            AutoGenerateColumns="false" 
                            ShowHeader="false"
                            GridLines="none" 
                            CellPadding="0"  
                            CellSpacing="0" 
                            CssClass="input100"> 
                            <Columns> 
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table class="input100">
                                            <tr>
                                                <td style="padding:3px;width:100px;color:rgb(255,255,255);font-size:105%;border:1px solid rgb(75,75,75)" rowspan="3"><%# Eval( "Username" ) %></td>
                                                <td class="board-title"><%# "<strong>" + ( Eval( "Subject" ).ToString().Length > 0 ? Eval( "Subject" ) : "RE: " + Eval( "Topic" ) ) + "</strong><br /><span style=\"font-weight:normal;font-size:95%;\">" + GetPostDate( (Csla.SmartDate)Eval( "PostDate" ) ) + "</span>" %></td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:rgb(255,255,255);padding:5px;font-size:95%;"><%# Eval( "Body" ) %></td>
                                            </tr>
                                            <tr>
                                                <td style="background-color:rgb(255,255,255);"><asp:LinkButton ID="EditLink" runat="server" CssClass="button" style="width:50px;border-color:rgb(0,0,0);" Text="Edit" CommandArgument='<%# Eval( "ID" ) %>' Visible='<%# CanEdit( Convert.ToInt32( Eval("UserID") ) ) %>' /></td>
                                            </tr>
                                        </table>                      
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="PostEditView" runat="server">
                    <div class="form">
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
                        <asp:DetailsView ID="MainDetails" runat="server"   
                            AutoGenerateEditButton="true"
                            AutoGenerateInsertButton="true"
                            CommandRowStyle-CssClass="button-row"
                            GridLines="none"
                            AutoGenerateRows="False" 
                            DataSourceID="ForumPostDataSource" 
                            DataKeyNames="ID">
                            <Fields>
                                <asp:TemplateField HeaderText="Subject">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Subject" runat="server" Text='<%# Bind( "Subject" ) %>' CssClass="textbox input6" />                                   
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Body">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Body" runat="server" TextMode="MultiLine" Width="95%" Rows="10" Text='<%# Bind( "Body" ) %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                    </div>
                </asp:View>
            </asp:MultiView> 
            <asp:Panel ID="LinkPanel" runat="server">
                <asp:LinkButton ID="InsertLink" runat="server" CssClass="button-inline" style="width:100px;border-color:rgb(0,0,0);" Text="Post Reply" /> <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="~/Portal/ForumBoards.aspx" CssClass="button-inline" style="width:175px;border-color:rgb(0,0,0);" Text="Return to Boards" /> <asp:HyperLink ID="BoardsLink" runat="server" NavigateUrl="~/Portal/ForumBoards.aspx" CssClass="button-inline" style="width:125px;border-color:rgb(0,0,0);" Text="Return to Boards" />
            </asp:Panel>
        </div>
    </div>
    <csla:CslaDataSource ID="ForumPostListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.ForumPostList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="ForumPostListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="ForumTopicDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.ForumTopic" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnUpdateObject="ForumTopicDataSource_UpdateObject"
        OnSelectObject="ForumTopicDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="ForumPostDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.ForumPost" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnInsertObject="ForumPostDataSource_InsertObject" 
        OnUpdateObject="ForumPostDataSource_UpdateObject"
        OnSelectObject="ForumPostDataSource_SelectObject" />        
</div>         
</asp:Content>

