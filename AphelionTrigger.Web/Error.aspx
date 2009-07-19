<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="MainHead" runat="server">
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="Styles/aphelion.css" />
    <title>Aphelion Trigger Error Page of Woe</title>
</head>
<body>
    <form id="MainForm" runat="server">
        <div class="page-container">
            <div class="header">
                <div class="header-top">
                    <div class="title">
                        <h1>
                            <asp:Label ID="TitleLabel" runat="server">
                                <span style='color:rgb(208,225,239);'>Aphelion</span>
                                <span style='color:rgb(107,141,148);'>Trigger</span>
                            </asp:Label>
                            <span class="version">&nbsp;<asp:Label ID="VersionLabel" runat="server" /></span>
                        </h1>
                        <h2>&nbsp;</h2>
                    </div>
                </div>
              

            </div>
            <div class="main">    
                <div class="main-content main-full">
                    <h1 class="pagetitle">It's an Error! It's an Error! It's an Error!</h1>
                    <p>
                        I was afraid this would happen... Um... Hi, user, I am the error page. I hate to say this, but 
                        something went wrong in Aphelion Trigger so now you’re here. We can talk for awhile, or you can 
                        <asp:HyperLink ID="Home" runat="server" NavigateUrl="~/Default.aspx">go back home immediately</asp:HyperLink> 
                        and see if things are better there.
                    </p>
                    <p>
                        Some errors are interesting and rare. You should share them with the developers 
                        <asp:HyperLink ID="Forum" runat="server" NavigateUrl="~/Portal/ForumBoard.aspx?ID=8">here on the forum</asp:HyperLink>, 
                        because more than anything developers love errors, especially interesting ones.
                    </p>
                    <p>
                        And me, I like the peace and quiet of the server, so I need your help to find errors so they can be repaired and 
                        I won’t need to have this talk with you again. 
                    </p>
                    <p>***END MESSAGE***</p>
                    <p class="vertical" />
                </div>

                <div class="sidebar">
            </div>               
                    
            <aphelion:Footer ID="TNFooter" runat="server" />
        </div>
        </form>
        <!-- Google Analytics tracking script -->    
        <script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
        </script>
        <script type="text/javascript">
        _uacct = "UA-2195710-1";
        urchinTracker();
        </script>
        <!-- End Script-->     
</body>
</html>
