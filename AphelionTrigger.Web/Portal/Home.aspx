<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Home" Title="Aphelion Trigger - Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">    
    <div class="main-content main-partial">
        <div style="width:97%;background-color:rgb(230,230,230);border:1px solid rgb(255,20,20);padding:5px;margin-bottom:10px;">
            <strong>NOTICE:</strong> Aphelion Trigger is currently in the early stages of development and not yet balanced, bug free or feature-complete. 
            Please be patient during the game's infancy; we're updating the site every day. If you'd like to help, don't hesitate to 
            <a href="mailto:development@apheliontrigger.com">contact us</a>.
        </div>
        <h1 class="pagetitle">Welcome to Terra Nova</h1>
        <p>. . . a rich but hostile planet that was to become <strong>humanity’s new homeworld after catastrophe deformed the earth</strong>. It was here mankind would perfect itself, consummating new extremes of brilliance and cruelty, evolving our race to its natural conclusion.</p>
        <p>On Terra Nova, the battle lines are etched down the equator, with mankind organized into two chief geographic factions: <strong>The Allied Northern Principalities</strong> and the <strong>Southern Emirate</strong>. Each with its own culture and history of <strong>espionage and warfare</strong>, these two flagship superpowers are anything but unified, and furious power struggles rage as much inside their borders as out. </p>
        <p><strong>You play the leader of a house</strong>, an institution both part city-state and part corporation. Serving under one of the two factions, your duty is to <strong>advance the power of your faction <em>and</em> your house</strong> by committing unrestrained assault, sabotage, alliance, and betrayal against the houses of other players. If you emerge as the most powerful statesman on Terra Nova, you be rewarded and reviled. But you must hurry. . .</p>
        <p>Terra Nova is a world of extremes: rich in resources and biodiversity, but highly unstable. With a sharp axial tilt and five moons, <strong>the planet undergoes constant transformation and upheaval</strong>. Terranovans have learned to survive, but the results are always devastating. <strong>Every so often, the planet’s poles shift – this is called an age. When a pole shift happens, the age is at an end and the most powerful houses win</strong>. The devastation is so complete, however, that you must rebuild your house or start a new one, and <strong>thus the cycle continues</strong>.</p>
        <p><a href="Registration.aspx" style="font-size:125%;">Register and start playing immediately. . .</a></p>    
        <p><a href="../Codex/Index.aspx" style="font-size:125%;">Study the Codex game guide and learn its secrets. . .</a></p>
    </div>

    <div class="sidebar">
        <aphelion:Login ID="ATLogin" runat="server" />

        <aphelion:Messages ID="ATMessages" runat="server" />      

        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>



