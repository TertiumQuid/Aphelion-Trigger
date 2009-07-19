using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Security;
using AphelionTrigger.Library.Security;

public partial class Portal_Welcome : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        EliotPanel.Visible = Request.UrlReferrer != null && Request.UrlReferrer.ToString().EndsWith( "Registration.aspx" );
    }
}
