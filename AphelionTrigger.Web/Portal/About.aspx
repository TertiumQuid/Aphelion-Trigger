<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="Portal_About" Title="Aphelion Trigger - About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <h1 class="pagetitle">About Aphelion Trigger</h1>
        <h2>Can You Immanentize the Eschaton?</h2>
        <p>Aphelion Trigger is a <strong>Persistent, Browser-Based Text Game</strong> combining elements from strategy, simulation, and role playing games in order to tell a transhumanist story about the conflicts and journeys of humanity.</p>
        <p>You play the leader of one of many Houses on the planet Terra Nova, competing with other players in a massive multiplayer online gameworld where you will use a varied arsenal of tactics to become the most successful statesman and figurehead of your people.</p>
        <p>Play is free and occurs entirely in your browser, so because there isn't any software to download, you can play anytime, anywhere. Aphelion Trigger can be treated as a "coffee-break" game or a meticulous tactical obsession – the game is designed to balance online and offline play, allowing both strategies to prove sucessful.</p>
        <p>Your progress in Aphelion Trigger is persistant. You gain and loose miltary, resources, technology, wealth, etc. but you also improve the abilities of your house, gain experience, and collectively contribute to the development of your associated guild and faction.</p>
        <p>Aphelion Trigger is also a free, open-source project that you can download for use with your own game or simply for instructional purposes. All the software needed to develop for and run the game is available freely from Microsoft, including basic hosting if you want to put your version of Aphelion Trigger online.</p>
        <p class="vertical" />
    </div>

    <div class="sidebar">
        <aphelion:Login ID="ATLogin" runat="server" />

        <aphelion:Messages ID="ATMessages" runat="server" />

        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>

