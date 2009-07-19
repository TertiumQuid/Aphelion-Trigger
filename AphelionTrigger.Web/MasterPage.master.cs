using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AphelionTrigger;
using AphelionTrigger.Library.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page is AphelionTriggerPage) ((AphelionTriggerPage)Page).RefreshHUD += new EventHandler( MasterPage_RefreshHUD );

        RegisterScripts();
    }

    void RegisterScripts()
    {
        bool isAuthenticated = Csla.ApplicationContext.User.Identity.IsAuthenticated;

        // capture keypress events for designated hotkeys
        StringBuilder hotkeyScript = new StringBuilder();
        hotkeyScript.AppendLine( "document.onkeypress=keyevent;" );
        hotkeyScript.AppendLine( "function keyevent(e){" );
        hotkeyScript.AppendLine( "var c;" );
        hotkeyScript.AppendLine( "var target;" );
        hotkeyScript.AppendLine( "var altKey;" );
        hotkeyScript.AppendLine( "var ctrlKey;" );
        hotkeyScript.AppendLine( "if (window.event != null) {" );
        hotkeyScript.AppendLine( "c=String.fromCharCode(window.event.keyCode).toUpperCase(); " );
        hotkeyScript.AppendLine( "altKey=window.event.altKey;" );
        hotkeyScript.AppendLine( "ctrlKey=window.event.ctrlKey;" );
        hotkeyScript.AppendLine( "}else{" );
        hotkeyScript.AppendLine( "c=String.fromCharCode(e.charCode).toUpperCase();" );
        hotkeyScript.AppendLine( "altKey=e.altKey;" );
        hotkeyScript.AppendLine( "ctrlKey=e.ctrlKey;" );
        hotkeyScript.AppendLine( "}" );
        hotkeyScript.AppendLine( "if (window.event != null)" );
        hotkeyScript.AppendLine( "target=window.event.srcElement;" );
        hotkeyScript.AppendLine( "else" );
        hotkeyScript.AppendLine( "target=e.originalTarget;" );
        hotkeyScript.AppendLine( "if (target.nodeName.toUpperCase()=='INPUT' || target.nodeName.toUpperCase()=='TEXTAREA' || altKey || ctrlKey){" );
        hotkeyScript.AppendLine( "}else{" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'A') { window.location='../Warfare/Attack.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'F') { window.location='../Warfare/Forces.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'Y') { window.location='../Warfare/History.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'S') { window.location='../Espionage/Spies.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'E') { window.location='../Espionage/Operations.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'R') { window.location='../Communications/Records.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'M') { window.location='../Communications/Send.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'H') { window.location='../House/Profile.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'V') { window.location='../House/Advancement.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'C') { window.location='../House/Census.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'G') { window.location='../Guild/Profile.aspx'; return false; }" );
        if ( isAuthenticated ) hotkeyScript.AppendLine( "if (c == 'F') { window.location='../Faction/Profile.aspx'; return false; }" );
        hotkeyScript.AppendLine( "if (c == 'B') { window.location='../Portal/ForumBoards.aspx'; return false; }" );
        hotkeyScript.AppendLine( "if (c == 'X') { window.location='../Codex/Index.aspx'; return false; }" );
        hotkeyScript.AppendLine( "}" );
        hotkeyScript.AppendLine( "}" );

        ScriptManager.RegisterClientScriptBlock( Page, this.GetType(), "HotkeyScript", hotkeyScript.ToString(), true );
    }

    void MasterPage_RefreshHUD( object sender, EventArgs e )
    {
        TNHeader.RefreshHUD();
    }
}
