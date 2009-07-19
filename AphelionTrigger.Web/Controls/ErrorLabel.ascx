<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ErrorLabel.ascx.cs" Inherits="Controls_ErrorLabel" %>
<div class="error-container">
    <div class="error-legend" class="error-legend"><asp:Label ID="ErrorLegend" runat="server" /></div>
    <asp:Label ID="ErrorText" runat="server" CssClass="error-body" />
</div>