<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Reports.ascx.cs" Inherits="Includes_Reports" %>

<div class="report-header">World Reports</div>
<div class="report-box">
    <asp:UpdatePanel ID="ReportsUpdatePanel" runat="server" UpdateMode="conditional">
        <ContentTemplate>
            <asp:Repeater ID="ReportsRepeater" runat="server">
                <ItemTemplate>
                    <div style="padding-bottom:5px;">
                        <%# Eval( "Signifier" ) + " " + Eval( "Message" ) %>
                        <span style="color:rgb(160,160,160);"><%# " @ " + ((Csla.SmartDate)Eval( "ReportDate" )).Date.ToString( "%M/dd %h:mm tt" ) %></span>                    
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Timer ID="ReportsTimer" runat="server" OnTick="ReportsTimer_Tick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>  