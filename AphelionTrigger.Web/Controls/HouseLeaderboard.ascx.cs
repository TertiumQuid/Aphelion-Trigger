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

public partial class Includes_HouseLeaderboard : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Rankings.RowCommand += new GridViewCommandEventHandler( Rankings_RowCommand );
        Rankings.PageIndexChanging += new GridViewPageEventHandler( Rankings_PageIndexChanging );
        Rankings.Sorting += new GridViewSortEventHandler( Rankings_Sorting );

        if (!Page.IsPostBack) RefreshLeaderboard();
    }

    #region Rankings
    void Rankings_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        switch (e.CommandName)
        {
            case "Message":
                string[] messageArgs = e.CommandArgument.ToString().Split( '|' );
                int recipientHouseId = Int32.Parse( messageArgs[0] );
                string recipientHouse = messageArgs[1];

                ((AphelionTriggerPage)Page).MESSAGE = new MessageStub( 0, recipientHouseId, recipientHouse, string.Empty );
                Response.Redirect( "~/Communications/Send.aspx", true );
                break;
            case "Profile":
                ((AphelionTriggerPage)Page).HOUSE = new HouseStub( Convert.ToInt32( e.CommandArgument ) );
                Response.Redirect( "~/House/Profile.aspx", true );
                break;
            case "Attack":
                string[] attackArgs = e.CommandArgument.ToString().Split( '|' );
                int targetHouseId = Int32.Parse( attackArgs[0] );
                string targetHouse = attackArgs[1];
                ((AphelionTriggerPage)Page).HOUSE = new HouseStub( targetHouseId, targetHouse );
                Response.Redirect( "~/Warfare/Attack.aspx", true );
                break;
            case "Espionage":
                string[] espionageArgs = e.CommandArgument.ToString().Split( '|' );
                targetHouseId = Int32.Parse( espionageArgs[0] );
                targetHouse = espionageArgs[1];
                ( (AphelionTriggerPage)Page ).HOUSE = new HouseStub( targetHouseId, targetHouse );
                Response.Redirect( "~/Espionage/Operations.aspx", true );
                break;
        }
    }

    void Rankings_PageIndexChanging( object sender, GridViewPageEventArgs e )
    {
        Rankings.PageIndex = e.NewPageIndex;
        RefreshLeaderboard();
    }

    void Rankings_Sorting( object sender, GridViewSortEventArgs e )
    {
    }

    private void RefreshLeaderboard()
    {
        HouseList rankings;

        if (Cache["HOUSELIST"] == null)
        {
            rankings = HouseList.GetHouseList();

            HttpContext.Current.Cache.Insert( "HOUSELIST", rankings, null, DateTime.Now.AddMinutes( 1 ), Cache.NoSlidingExpiration, CacheItemPriority.BelowNormal, null );
        }
        else
        {
            rankings = (HouseList)Cache["HOUSELIST"];
        }

        Rankings.DataSource = rankings;
        Rankings.DataBind();
    }
    #endregion

    #region Business Methods
    public string FormatName( int id, string name, int guildId )
    {
        if (id == HouseID)
        {
            return "<span style=\"color:rgb(0,150,0);font-weight:bold;\">" + name + "</span>";
        }
        else if (guildId > 0 && guildId == GuildID)
        {
            return "<span style=\"color:rgb(30,140,205);font-weight:bold;\">" + name + "</span>";
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

    public bool IsOnline( string id )
    {
        return (string)HttpContext.Current.Cache["IsOnline" + id] != null;
    }
    #endregion
}
