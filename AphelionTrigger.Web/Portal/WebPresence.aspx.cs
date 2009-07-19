using System;
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
using AphelionTrigger.Library;

public partial class Portal_WebPresence : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        // if entering from another site, as is often the case with toplists, redirect the user to the home page.
        if ( Request.UrlReferrer != null && !Request.UrlReferrer.ToString().ToLower().Contains( "apheliontrigger.com" ) ) Response.Redirect( "~" );
    }
}
