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
using AphelionTrigger.Library.Security;

public partial class Includes_Navigation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // bind house link buttons with correct CSS classes
        HouseTab.DataBind();
        HouseLink.DataBind();

        HouseTab.Click += new EventHandler( House_Click );
        HouseLink.Click += new EventHandler( House_Click );
    }

    void House_Click( object sender, EventArgs e )
    {
        ((AphelionTriggerPage)Page).HOUSE = new HouseStub( ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID );
        Response.Redirect( "~/House/Profile.aspx", true );
    }

    protected string IsAuthenticated()
    {
        return IsAuthenticated( false );
    }

    protected string IsAuthenticated( bool requireHouse)
    {
        if (requireHouse) 
            return (Csla.ApplicationContext.User.Identity.IsAuthenticated && ((AphelionTriggerPage)Page).User.HasHouse ? string.Empty : "none");
        else
            return (Csla.ApplicationContext.User.Identity.IsAuthenticated ? string.Empty : "none");
    }

    protected string IsAdministrator()
    {
        return (Csla.ApplicationContext.User.IsInRole( "Administrator" ) ? string.Empty : "none");
    }

    protected string IsLinkSelectedClass( string directory )
    {
        return (Request.Url.ToString().ToLower().Contains( directory.ToLower() ) ? "selected" : string.Empty);
    }

    protected string IsTabLinkVisible( string directory )
    {
        return (Request.Url.ToString().ToLower().Contains( directory.ToLower() ) ? string.Empty : "none");
    }

    protected bool HasGuild()
    {
        return ((ATIdentity)Csla.ApplicationContext.User.Identity).GuildID > 0;
    }
}
