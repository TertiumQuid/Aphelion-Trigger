<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Records.aspx.cs" Inherits="Communications_Records" Title="Aphelion Trigger - Message Records" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">Message Records</h1>  
        <h5>Recieved Messages</h5>
        <div class="dark-box">
            <asp:GridView ID="RecieveMessages" runat="server"  
                ShowHeader="true"
                AutoGenerateColumns="false" 
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0" 
                EmptyDataText="<p>No messages recieved. Good communication is vital to a successful empire.</p>"                 
                CssClass="grid input100"> 
                <Columns>     
                    <asp:TemplateField ItemStyle-Width="20">
                        <ItemTemplate>
                            <asp:Image ID="HasRead" runat="server" ImageUrl='<%# GetMessageImage( Convert.ToBoolean( Eval( "HasRead") ), Convert.ToInt32( Eval( "MessageTypeID" ) ) ) %>' style="margin:0" />
                        </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="Subject" ItemStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:LinkButton ID="ViewLink" runat="server" Text='<%# Eval( "Subject" ) %>' CommandName="View" CommandArgument='<%# Eval( "ID" ) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From">
                        <ItemTemplate>
                            <%# Eval( "SenderHouse" ) %>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Sent"> 
                        <ItemTemplate>
                            <%# ((Csla.SmartDate)Eval( "WriteDate" )).Date.ToString( "MMM dd, hh:mm tt" )%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>  
        </div>
        <h5>Sent Messages</h5>
        <div class="dark-box">
            <asp:GridView ID="SentMessages" runat="server"  
                ShowHeader="true"
                AutoGenerateColumns="false" 
                GridLines="none" 
                CellPadding="0" 
                CellSpacing="0" 
                EmptyDataText="<p>No messages sent. Isolationism could be your downfall.</p>"                 
                CssClass="grid input100"> 
                <Columns>      
                    <asp:TemplateField HeaderText="Subject" ItemStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:LinkButton ID="ViewLink" runat="server" Text='<%# Eval( "Subject" ) %>' CommandName="View" CommandArgument='<%# Eval( "ID" ) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Recipients">
                        <ItemTemplate>
                            <%# Eval( "Recipients" ) %>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Sent"> 
                        <ItemTemplate>
                            <%# ((Csla.SmartDate)Eval( "WriteDate" )).Date.ToString( "MMM dd, hh:mm tt" )%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>  
        </div>
        <p>
            <asp:HyperLink ID="SendMessage" runat="server" CssClass="button" Text="Send New Message" NavigateUrl="~/Communications/Send.aspx" style="width:125px;border-color:rgb(0,0,0);" />
        </p>
    </div>            
    
    <div class="sidebar">
        <aphelion:Attacks ID="ATAttacks" runat="server" /> 
        <aphelion:Messages ID="ATMessages" runat="server" /> 
        
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div> 
</asp:Content>

