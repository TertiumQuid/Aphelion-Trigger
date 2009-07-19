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
using System.Web.Caching;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class Controls_HUD : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        AuthenticatedHUD.Visible = Csla.ApplicationContext.User.Identity.IsAuthenticated && ((AphelionTriggerPage)Page).HasUserHouse();
        AdminHUD.Visible = Csla.ApplicationContext.User.Identity.IsAuthenticated && !((AphelionTriggerPage)Page).HasUserHouse();
        UnauthenticatedHUD.Visible = !AuthenticatedHUD.Visible && !AdminHUD.Visible;

        if (!Page.IsPostBack) RefreshHUD( false );
    }

    protected void Logout_Click( object sender, EventArgs e )
    {
        HttpContext.Current.Cache.Remove( "IsOnline" + ( (AphelionTriggerPage)Page ).User.ID.ToString() );
        Session.Abandon();
        ATPrincipal.Logout();

        Response.Cookies["AphelionTriggerLogin"].Expires = DateTime.Now.AddYears( -30 );
        Response.Redirect( "~/Portal/Home.aspx", true );
    }

    protected void HUDTimer_Tick( object sender, EventArgs e )
    {
        RefreshHUD( false );
    }

    protected string IsAuthenticated( bool auth)
    {
        return (Csla.ApplicationContext.User.Identity.IsAuthenticated && auth ? string.Empty : "none");
    }

    public void RefreshHUD( bool forceRefresh )
    {
        if (!Csla.ApplicationContext.User.Identity.IsAuthenticated) return;

        // if the user is staff-only, do not populate main house display
        if (((AphelionTriggerPage)Page).User.HouseID == 0)
        {
            AdminName.Text = ((AphelionTriggerPage)Page).User.Name;
            return;
        }

        House house = ((AphelionTriggerPage)Page).UserHouse;

        HouseName.Text = house.Name;
        Turns.Text = house.Turns.ToString();
        Credits.Text = house.Credits.ToString();
        Forces.Text = house.ForcesCount.ToString();

        int nextLevel = house.NextLevel.Experience - house.Experience;

        Level.Text = house.Level.Rank.ToString() + " (" + nextLevel.ToString() + " exp. to next level)";
    }
}
