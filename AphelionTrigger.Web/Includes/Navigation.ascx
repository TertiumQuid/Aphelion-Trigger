<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Navigation.ascx.cs" Inherits="Includes_Navigation" %>

<div class="header-bottom">
    <div class="navigation">
        <ul><li><a href="../Default.aspx" class='<%Response.Write( IsLinkSelectedClass("/Portal")); %>'>Portal</a></li></ul>
        <ul style='display:<%Response.Write( IsAuthenticated( true ) ); %>'><li><a href="../Warfare/Attack.aspx" class='<%Response.Write( IsLinkSelectedClass("/Warfare")); %>'>Warfare</a></li></ul>
        <ul style='display:<%Response.Write( IsAuthenticated( true ) ); %>'><li><a href="../Espionage/Spies.aspx" class='<%Response.Write( IsLinkSelectedClass("/Espionage")); %>'>Espionage</a></li></ul>
        <ul style='display:<%Response.Write( IsAuthenticated( true ) ); %>'><li><a href="../Technology/Research.aspx" class='<%Response.Write( IsLinkSelectedClass("/Technology")); %>'>Tech</a></li></ul>
        <ul style='display:<%Response.Write( IsAuthenticated( true ) ); %>'><li><a href="../Communications/Records.aspx" class='<%Response.Write( IsLinkSelectedClass("/Communications")); %>'>Comm</a></li></ul>
        <ul style='display:<%Response.Write( IsAuthenticated( true ) ); %>'><li><asp:LinkButton ID="HouseTab" runat="server" Text="House" CssClass='<%# IsLinkSelectedClass("/House")  %>' /></li></ul>
        <ul style='display:<%Response.Write( IsAuthenticated( true ) ); %>'><li><a href='<%Response.Write( HasGuild() ? "../Guild/Profile.aspx" : "../Guild/Create.aspx" ); %>' class='<%Response.Write( IsLinkSelectedClass("/Guild")); %>'>Guild</a></li></ul>
        <ul style='display:<%Response.Write( IsAuthenticated( true ) ); %>'><li><a href="../Faction/Profile.aspx" class='<%Response.Write( IsLinkSelectedClass("/Faction")); %>'>Faction</a></li></ul>
        <ul><li><a href="../Codex/Index.aspx" class='<%Response.Write( IsLinkSelectedClass("/Codex/")); %>'>Codex</a></li></ul>
        <ul style='display:<%Response.Write( IsAdministrator() ); %>'><li><a href="../System/Configuration.aspx" class='<%Response.Write( IsLinkSelectedClass("/System")); %>'>System</a></li></ul>
    </div>
</div>

<div class="sub-navigation" style='display:<%Response.Write( IsTabLinkVisible("/Portal")); %>'>
<ul>
  <li><a href="Home.aspx" class='<%Response.Write( IsLinkSelectedClass("Home.aspx")); %>'>Home</a></li> 
  <li><a href="About.aspx" class='<%Response.Write( IsLinkSelectedClass("About.aspx")); %>'>About</a></li>
  <li><a href="ForumBoards.aspx" class='<%Response.Write( IsLinkSelectedClass("Forum")); %>'>Forums</a></li>
  <li><a href="News.aspx" class='<%Response.Write( IsLinkSelectedClass("News.aspx")); %>'>News</a></li>
  <li><a href="Registration.aspx" class='<%Response.Write( IsLinkSelectedClass("Registration.aspx")); %>'>Registration</a></li>  
  <li><a href="Code.aspx" class='<%Response.Write( IsLinkSelectedClass("Code.aspx")); %>'>Code</a></li>
  <li><a href="WebPresence.aspx" class='<%Response.Write( IsLinkSelectedClass("WebPresence.aspx")); %>'>Web Presence</a></li>
</ul>
</div>

<div class="sub-navigation" style='display:<%Response.Write( IsTabLinkVisible("/Warfare")); %>'>
    <ul>
        <li><a href="Attack.aspx" class='<%Response.Write( IsLinkSelectedClass("Attack.aspx")); %>'>Attack</a></li>
        <li><a href="Forces.aspx" class='<%Response.Write( IsLinkSelectedClass("Forces.aspx")); %>'>Standing Forces</a></li>
        <li><a href="History.aspx" class='<%Response.Write( IsLinkSelectedClass("History.aspx")); %>'>History</a></li>
    </ul>
</div>

<div class="sub-navigation" style='display:<%Response.Write( IsTabLinkVisible("/Espionage")); %>'>
    <ul>
        <li><a href="Spies.aspx" class='<%Response.Write( IsLinkSelectedClass("Spies.aspx")); %>'>Spies</a></li>
        <li><a href="Operations.aspx" class='<%Response.Write( IsLinkSelectedClass("Operations.aspx")); %>'>Operations</a></li>
    </ul>
</div>

<div class="sub-navigation" style='display:<%Response.Write( IsTabLinkVisible("/Technology")); %>'>
    <ul>
        <li><a href="Research.aspx" class='<%Response.Write( IsLinkSelectedClass("Research.aspx")); %>'>Research</a></li>
    </ul>
</div>

<div class="sub-navigation" style='display:<%Response.Write( IsTabLinkVisible("/Communications")); %>'>
<ul>
    <li><a href="Records.aspx" class='<%Response.Write( IsLinkSelectedClass("Records.aspx")); %>'>Records</a></li>
    <li><a href="Send.aspx" class='<%Response.Write( IsLinkSelectedClass("Send.aspx")); %>'>Send Message</a></li>
</ul>
</div>

<div class="sub-navigation" style='display:<%Response.Write( IsTabLinkVisible("/House")); %>'>
<ul>
    <li><asp:LinkButton ID="HouseLink" runat="server" Text="Profile" CssClass='<%# IsLinkSelectedClass("Profile.aspx") %>' /></li>
    <li><a href="Advancement.aspx" class='<%Response.Write( IsLinkSelectedClass("Advancement.aspx")); %>'>Advancement</a></li>
    <li><a href="Census.aspx" class='<%Response.Write( IsLinkSelectedClass("Census.aspx")); %>'>Census</a></li>
</ul>
</div>

<div class="sub-navigation" style='display: <%Response.Write( IsTabLinkVisible("/Guild")); %>'>
<ul>
  <% if (HasGuild()) { %><li><a href="Profile.aspx" class='<%Response.Write( IsLinkSelectedClass("Profile.aspx")); %>'>Profile</a>&nbsp;</li><% } %>
  <% if (!HasGuild()) { %><li><a href="Create.aspx" class='<%Response.Write( IsLinkSelectedClass("Create.aspx")); %>'>Create</a></li><% } %>
  <% if (HasGuild()) { %><li><a href="Invite.aspx" class='<%Response.Write( IsLinkSelectedClass("Invite.aspx")); %>'>Invite</a></li><% } %>
</ul>
</div>

<div class="sub-navigation" style='display:<%Response.Write( IsTabLinkVisible("/Faction")); %>'>
<ul>
  <li><a href="Profile.aspx" class='<%Response.Write( IsLinkSelectedClass("Profile.aspx")); %>'>Profile</a></li>
</ul>
</div>

<div class="sub-navigation" style='display:<%Response.Write( IsTabLinkVisible("/System")); %>'>
<ul>
  <li><a href="Configuration.aspx" class='<%Response.Write( IsLinkSelectedClass("Configuration.aspx")); %>'>Configuration</a></li>
  <li><a href="Units.aspx" class='<%Response.Write( IsLinkSelectedClass("Units.aspx")); %>'>Units</a></li>
  <li><a href="Spies.aspx" class='<%Response.Write( IsLinkSelectedClass("Spies.aspx")); %>'>Spies</a></li>
  <li><a href="Technologies.aspx" class='<%Response.Write( IsLinkSelectedClass("Technologies.aspx")); %>'>Technologies</a></li>
  <li><a href="Resets.aspx" class='<%Response.Write( IsLinkSelectedClass("Resets.aspx")); %>'>Resets</a></li>
  <li><a href="Levels.aspx" class='<%Response.Write( IsLinkSelectedClass("Levels.aspx")); %>'>Levels</a></li>
  <li><a href="News.aspx" class='<%Response.Write( IsLinkSelectedClass("News.aspx")); %>'>News</a></li>
  <li><a href="SystemLogs.aspx" class='<%Response.Write( IsLinkSelectedClass("aspx.aspx")); %>'>System Logs</a></li>
  <li><a href="Codex.aspx" class='<%Response.Write( IsLinkSelectedClass("Codex.aspx")); %>'>Codex</a></li>
</ul>
</div>