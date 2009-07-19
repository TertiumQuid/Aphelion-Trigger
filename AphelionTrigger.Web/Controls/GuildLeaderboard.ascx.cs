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

public partial class Includes_GuildLeaderboard : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Rankings.RowCommand += new GridViewCommandEventHandler( Rankings_RowCommand );

        if (!Page.IsPostBack) RefreshLeaderboard();
    }

    void Rankings_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        switch (e.CommandName)
        {
            case "Profile":
                ((AphelionTriggerPage)Page).GUILD = new GuildStub( Convert.ToInt32( e.CommandArgument ) );
                Response.Redirect( "~/Guild/Profile.aspx", true );
                break;
        }
    }

    private void RefreshLeaderboard()
    {
        GuildList rankings;

        if (Cache["Guilds"] == null)
        {
            rankings = GuildList.GetGuildList();

            HttpContext.Current.Cache.Insert( "Guilds", rankings, null, DateTime.Now.AddMinutes( 30 ), Cache.NoSlidingExpiration, CacheItemPriority.BelowNormal, null );
        }
        else
        {
            rankings = (GuildList)Cache["Guilds"];
        }

        Rankings.DataSource = rankings;
        Rankings.DataBind();
    }

    #region Business Methods
    public string FormatName( int id, string name )
    {
        if (id == GuildID)
        {
            return "<span style=\"color:rgb(0,150,0);font-weight:bold;\">" + name + "</span>";
        }
        else
        {
            return name;
        }
    }

    public int HouseID
    {
        get
        {
            object id = ViewState["_HouseLeaderBoardHouseID"];
            if (id == null || !(id is int))
            {
                if (Csla.ApplicationContext.User.Identity.IsAuthenticated)
                    ViewState.Add( "_HouseLeaderBoardHouseID", ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID );
                else
                    ViewState.Add( "_HouseLeaderBoardHouseID", 0 );


                id = ViewState["_HouseLeaderBoardHouseID"];
            }

            return (int)id;
        }
        set { Session.Add( "_HouseLeaderBoardHouseID", value ); }
    }

    public int GuildID
    {
        get
        {
            object id = ViewState["_HouseLeaderBoardGuildID"];
            if (id == null || !(id is int))
            {
                if (Csla.ApplicationContext.User.Identity.IsAuthenticated)
                    ViewState.Add( "_HouseLeaderBoardGuildID", ((ATIdentity)Csla.ApplicationContext.User.Identity).GuildID );
                else
                    ViewState.Add( "_HouseLeaderBoardGuildID", 0 );


                id = ViewState["_HouseLeaderBoardGuildID"];
            }

            return (int)id;
        }
        set { Session.Add( "_HouseLeaderBoardGuildID", value ); }
    }

    public bool IsAuthenticated
    {
        get
        {
            return Csla.ApplicationContext.User.Identity.IsAuthenticated;
        }
    }
    #endregion
}
