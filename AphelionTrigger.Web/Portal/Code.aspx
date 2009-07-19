<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Code.aspx.cs" Inherits="Portal_Code" Title="Aphelion Trigger - Code Project" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">The Aphelion Trigger Codebase</h1>
        <p>Aphelion Trigger was developed using <a href="http://www.asp.net/" target="_blank">ASP.NET</a>, <a href="http://ajax.asp.net/" target="_blank">ASP.NET AJAX</a> and the <a href="http://ajax.asp.net/ajaxtoolkit/" target="_blank">ASP.NET AJAX Toolit</a>. The business layer for the game is built on Rockford Lhotka's <a href="http://www.lhotka.net/Area.aspx?id=4" target="_blank">Csla.NET</a>. The game runs on a <a href="http://msdn2.microsoft.com/en-us/sql/aa336346.aspx" target="_blank">SQL Server</a> database. The site design is based on a template by <a href="http://www.1-2-3-4.info/" target="_blank">Wolfgang</a></p>
        <p>If you have a questions or contributions, don't hesitate to contact us at <a href="matilto:development@apheliontrigger.com"> development@apheliontrigger.com</a>. We don't have much free time for developer support, but we'll do our best to answer any framework-specific questions and we're trying to keep the code as self-documenting as possible.</p>
        <hr />
        <h2>Downloads | v0.4</h2>
        <p><a href="http://www.lhotka.net/cslanet/download.aspx" target="_blank">CSLA Framework 2.1.4</a> (download from Rockford Lhotka's site)</p>
        <p><a href="~/Portal/Downloads/AphelionTrigger-Library.zip">AphelionTrigger.Library</a>, updated 8/23/2007</p>
        <p><a href="~/Portal/Downloads/AphelionTrigger-Services.zip">AphelionTrigger.Services</a>, updated 8/23/2007</p>
        <p><a href="~/Portal/Downloads/AphelionTrigger-Web.zip">AphelionTrigger.Web</a>, updated 8/23/2007</p>
        <p>The database is coming soon...</p>
        <p>In the future, the codebase will also be hosted through Google Project's SVN repo, located at <a href="http://apheliontrigger.googlecode.com/svn/">apheliontrigger.googlecode.com/svn/</a></p>
        <hr />
        <h2>The Aphelion Trigger License</h2>
        <p>
            AphelionTrigger – http://www.apheliontrigger.com<br />
            Copyright (c) 2007 by Bellwether, Inc.<br />
            Created by Travis Dunn (travis@apheliontrigger.com) and Patrick Walker (patrick@apheliontrigger.com)        
        </p>
        <p>The Aphelion Trigger codebase is licensed under the <a href="http://creativecommons.org/licenses/GPL/2.0/" target="_blank">Creative Commons GNU GPL</a>, as an <a href="http://creativecommons.org/licenses/by-nc-sa/3.0/" target="_blank">Attribution-NonCommercial-ShareAlike</a> project.</p>
        <p>For attribution purposes, the above copyright notice and creative commons links shall be prominently included in all copies or substantial portions of this software.</p>
    </div>

    <div class="sidebar">
        <aphelion:Login ID="ATLogin" runat="server" />

        <aphelion:Messages ID="ATMessages" runat="server" />      

        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>

