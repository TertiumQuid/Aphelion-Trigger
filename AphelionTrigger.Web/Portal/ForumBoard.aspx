<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ForumBoard.aspx.cs" Inherits="Portal_ForumBoard" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main"> 
    <div class="main-content main-full">
        <h1 class="pagetitle"><asp:Label ID="ForumTitle" runat="server" /></h1>
        <div class="column1-unit">
            <aphelion:ErrorLabel ID="PostError" runat="server" Legend="Post Error" Visible="false" />
            <asp:MultiView ID="MainMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="TopicsView" runat="server">
                    <asp:GridView ID="Topics" runat="server" 
                        DataSourceID="ForumTopicListDataSource" 
                        AutoGenerateColumns="false" 
                        AllowPaging="true" 
                        GridLines="none" 
                        PagerSettings-Mode="NumericFirstLast"
                        PageSize="20" 
                        Width="100%" 
                        CssClass="board"    
                        AlternatingRowStyle-CssClass="board-alt"                        
                        PagerStyle-CssClass="board-pager"
                        EmptyDataText="No Forum topics have been posted yet.">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="TopicImage" runat="server" ImageUrl='<%#  GetTopicImage( Convert.ToBoolean( Eval( "Sticky" ) ), Convert.ToBoolean( Eval( "Locked" ) ) ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
                                    <asp:HyperLink ID="PostLink" runat="server" Text='<%# Eval( "Title" ) %>' NavigateUrl='<%# "ForumTopic.aspx?ID=" + Eval( "ID" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PostCount" HeaderText="Posts" />
                            <asp:BoundField DataField="Views" HeaderText="Views" />
                            <asp:TemplateField HeaderText="Last Post">
                                <ItemTemplate>
                                    <%# GetPostDate( (Csla.SmartDate) Eval( "LastPostDate" ) ) + " " + ( Eval( "LastPostUSer" ).ToString().Length > 0 ? "<br /> by " + Eval( "LastPostUser" ) : "" )%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:View>
                <asp:View ID="NewTopicView" runat="server">
                    <div class="form">
                        <asp:Panel ID="TopicAdminPanel" runat="server">
                            <asp:CheckBox ID="Sticky" runat="server" Text="Sticky" />
                            <br />
                            <asp:CheckBox ID="Locked" runat="server" Text="Locked" />
                        </asp:Panel>
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
                                <asp:TemplateField HeaderText="Title">
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
            <br />
            <asp:Panel ID="LinkPanel" runat="server" style="">
                <asp:LinkButton ID="InsertLink" runat="server" CssClass="button-inline" style="border-color:rgb(0,0,0);width:115px;" Text="Post New Topic" /> <asp:HyperLink ID="BackLink" runat="server" NavigateUrl="~/Portal/ForumBoards.aspx" CssClass="button-inline" style="border-color:rgb(0,0,0);width:115px;" Text="Return to Boards" />
            </asp:Panel>
        </div>
    </div>
    <csla:CslaDataSource ID="ForumTopicListDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.ForumTopicList" 
        TypeAssemblyName="AphelionTrigger.Library"
        OnSelectObject="ForumTopicListDataSource_SelectObject" />
        
    <csla:CslaDataSource ID="ForumPostDataSource" runat="server" 
        TypeName="AphelionTrigger.Library.ForumPost" 
        TypeAssemblyName="AphelionTrigger.Library" 
        OnInsertObject="ForumPostDataSource_InsertObject" 
        OnSelectObject="ForumPostDataSource_SelectObject" />
</div>
</asp:Content>

