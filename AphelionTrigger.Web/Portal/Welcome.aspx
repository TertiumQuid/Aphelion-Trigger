<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Portal_Welcome" Title="Aphelion Trigger - Welcome to the Game" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ATContent" Runat="Server">
<div class="main">
    <div class="main-navigation">
    </div>

    <div class="main-content main-partial">
        <h1 class="pagetitle">Getting Started</h1>
        <p>(For full gameplay details, view the <asp:HyperLink ID="Codex" runat="server" NavigateUrl="~/Codex/Index.aspx" Text="Codex" />)</p>
        <p>
            When you start playing Terra Nova, your activity will revolve around two main actions: (1) <a href="../Warfare/Forces.aspx">recruiting forces</a> 
            and (2) <a href="../Warfare/Attack.aspx">attacking</a> other houses. By attacking houses, you will steal that house’s credits and capture its troops, 
            as well as gaining experience that will enable you to improve your house’s abilities.
        </p>        
        <p>Your ability to act in the game is dependent on the number of turns that you have. You will gain more turns over time at a rate determined by your speed. You will also gain additional credits, at a rate determined by your affluence.</p>
        <p>Once you have built up your forces and sufficiently increased your wealth, you will want to begin researching available <a href="../Technology/Research.aspx"> technology</a>. Specific technologies will improve the quality of your forces in different ways, but are expensive and take time to research so you'll have to schedule your research carefully.</p>
        <asp:Panel ID="EliotPanel" runat="server">
            <p>
                Pay close attention while you play. *84F2E00206DFDCA7008C0306<br />
                00010104A80600A2F0C4EBA182E4A80584F2E00BC876700FF46202EFE4*3A<br />
                EF2E4A2F0C2F074EBA1DC672FFB280504F67FF00079d4202EFE4E6000079*E700<br />
                12D40FE52722AB28FF66610202EFE5253AEFE5219EFE401B4880<br />
                ^^<br />
                &lt;logical reset @95FC119&gt;
            </p>
            <p>*Transfer 44000-p delayed</p>
            <p>*Transfer 44000-p delayed</p>
            <p>*Transfer 44000-p delayed</p>
            <p>&lt;answering unsigned,4,1,0,8000,0,0,101&gt;</p>
            <p> &lt;NO CARRIER&gt;</p>
            <p>GAME MANUAL 251.2.9 HELP7800FEF0...</p>
            <p>                
                The endless cycle of idea and action,<br />
                Endless invention, endless experiment,<br />
                Brings knowledge of motion, but not of stillness;<br />
                Knowledge of speech, but not of silence;<br />
                Knowledge of words, and ignorance of the Word.<br />
                All our knowledge brings us nearer to our ignorance,<br />
                All our ignorance brings us nearer to death,<br />
                But nearness to death no nearer to GOD.<br />
                Where is the Life we have lost in living?<br />
                Where is the wisdom we have lost in knowledge?<br />
                Where is the knowledge we have lost in information?<br />
                The cycles of Heaven in twenty centuries<br />
                Bring us farther from GOD and nearer to the Dust.<br />
            </p>
            <p style="text-align:center;">***END OF MESSAGE***</p>
        </asp:Panel>
    </div>

    <div class="sidebar">
        <aphelion:HouseLeaderboard ID="ATHouseLeaderboard" runat="server" />
        <aphelion:GuildLeaderboard ID="ATGuildLeaderboard" runat="server" />
    </div> 
</div>
</asp:Content>