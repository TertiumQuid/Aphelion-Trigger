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
using AjaxControlToolkit;

public partial class House_Census : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        if ( !IsPostBack ) Session["CurrentObject"] = null;

        Houses.RowCommand += new GridViewCommandEventHandler( Houses_RowCommand );
        Houses.PageIndexChanging += new GridViewPageEventHandler( Houses_PageIndexChanging );
        Houses.Sorting += new GridViewSortEventHandler( Houses_Sorting );
    }

    #region Events

    void Houses_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        switch ( e.CommandName )
        {
            case "Message":
                string[] messageArgs = e.CommandArgument.ToString().Split( '|' );
                int recipientHouseId = Int32.Parse( messageArgs[0] );
                string recipientHouse = messageArgs[1];

                ( (AphelionTriggerPage)Page ).MESSAGE = new MessageStub( 0, recipientHouseId, recipientHouse, string.Empty );
                Response.Redirect( "~/Communications/Send.aspx", true );
                break;
            case "Profile":
                ( (AphelionTriggerPage)Page ).HOUSE = new HouseStub( Convert.ToInt32( e.CommandArgument ) );
                Response.Redirect( "~/House/Profile.aspx", true );
                break;
            case "Attack":
                string[] attackArgs = e.CommandArgument.ToString().Split( '|' );
                int targetHouseId = Int32.Parse( attackArgs[0] );
                string targetHouse = attackArgs[1];
                ( (AphelionTriggerPage)Page ).HOUSE = new HouseStub( targetHouseId, targetHouse );
                Response.Redirect( "~/Warfare/Attack.aspx", true );
                break;
        }
    }

    void Houses_PageIndexChanging( object sender, GridViewPageEventArgs e )
    {
        Houses.PageIndex = e.NewPageIndex;
        Houses.DataBind();
    }

    void Houses_Sorting( object sender, GridViewSortEventArgs e )
    {
        Master.SortExpression = e.SortExpression;

        switch ( e.SortDirection.ToString() )
        {
            case "Ascending":
                Master.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
                break;
            case "Descending":
                Master.SortDirection = System.ComponentModel.ListSortDirection.Descending;
                break;
        }
    }

    #endregion

    #region Business Methods
    public string FormatRank( int rank, int lastRank )
    {
        if ( lastRank - rank > 0 ) return " +" + ( lastRank - rank ).ToString();
        else if ( lastRank - rank < 0 ) return " -" + ( lastRank - rank ).ToString();

        return " -- ";
    }

    public string FormatName( int id, string name, int guildId )
    {
        if ( id == HouseID )
        {
            return "<span style=\"color:rgb(0,150,0);font-weight:bold;\">" + name + "</span>";
        }
        else if ( guildId > 0 && guildId == GuildID )
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
            object id = ViewState["_HouseCensusHouseID"];
            if ( id == null || !( id is int ) )
            {
                if ( Csla.ApplicationContext.User.Identity.IsAuthenticated )
                    ViewState.Add( "_HouseCensusHouseID", ( (ATIdentity)Csla.ApplicationContext.User.Identity ).HouseID );
                else
                    ViewState.Add( "_HouseCensusHouseID", 0 );


                id = ViewState["_HouseCensusHouseID"];
            }

            return (int)id;
        }
        set { Session.Add( "_HouseCensusHouseID", value ); }
    }

    public int GuildID
    {
        get
        {
            object id = ViewState["_HouseCensusGuildID"];
            if ( id == null || !( id is int ) )
            {
                if ( Csla.ApplicationContext.User.Identity.IsAuthenticated )
                    ViewState.Add( "_HouseCensusGuildID", ( (ATIdentity)Csla.ApplicationContext.User.Identity ).GuildID );
                else
                    ViewState.Add( "_HouseCensusGuildID", 0 );


                id = ViewState["_HouseCensusGuildID"];
            }

            return (int)id;
        }
        set { Session.Add( "_HouseCensusGuildID", value ); }
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

    #region HouseListDataSource
    protected void HouseListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetHouseList();
    }

    private Csla.SortedBindingList<House> GetHouseList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is HouseList ) )
        {
            businessObject = HouseList.GetHouseList();
            Session["CurrentObject"] = businessObject;
        }

        Csla.SortedBindingList<House> list = new Csla.SortedBindingList<House>( (HouseList)businessObject );
        list.ApplySort( SortExpression, SortDirection );
        return list;
    }
    
    public string SortExpression
    {
        get
        {
            object exp = ViewState["_SortExpression"];
            if ( exp == null || !( exp is string ) )
            {
                ViewState.Add( "_SortExpression", "Rank" );
                exp = ViewState["_SortExpression"];
            }

            return exp.ToString();
        }
        set { ViewState["_SortExpression"] = value; }
    }

    public System.ComponentModel.ListSortDirection SortDirection
    {
        get
        {
            object dir = ViewState["_SortDirection"];
            if ( dir == null || !( dir is System.ComponentModel.ListSortDirection ) )
            {
                ViewState.Add( "_SortDirection", System.ComponentModel.ListSortDirection.Ascending );
                dir = ViewState["_SortDirection"];
            }

            return (System.ComponentModel.ListSortDirection)dir;
        }
        set { ViewState["_SortDirection"] = value; }
    }
    #endregion
}
