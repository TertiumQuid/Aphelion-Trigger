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
using System.Web.Caching;
using Csla;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;
using AjaxControlToolkit;

public partial class Espionage_Spies : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();

        ActingAgents.RowCreated += new GridViewRowEventHandler( ActingAgents_RowCreated );
        ActingAgents.RowCommand += new GridViewCommandEventHandler( ActingAgents_RowCommand );

        if ( !Page.IsPostBack )
        {
            RefreshUserHouse();
            Session["CurrentObject"] = null;
        }
    }

    #region Business Methods
    protected int MaxTroopRecruitment( int cost )
    {
        int maxByCredits = UserHouse.Credits / cost;
        int maxByCap = Spy.SpyCap( UserHouse.Intelligence );

        int maxTroops = ( cost != 0 ? ( maxByCap < maxByCredits ? maxByCap : maxByCredits ) : 0 );

        // don't let a player recruit more than 300 spies at a time - larger numbers could produce slider display issues
        maxTroops = maxTroops > 300 ? 300 : maxTroops;

        return maxTroops;
    }

    protected string GetRecruitmentStatus( int cost )
    {
        string status = string.Empty;

        int availableByCap = Spy.SpyCap( UserHouse.Intelligence ) - UserHouse.AgentCount;
        double availableByCredits = (double)UserHouse.Credits / (double)cost;

        if ( availableByCap < 1 ) status = "Agent Cap reached!";
        else if ( availableByCredits < 1 ) status = "Insufficient funds!";

        return status;
    }

    protected bool CanRecruitForces( int cost )
    {
        bool canRecruit = ( UserHouse.Credits / cost ) > 0;
        return canRecruit;
    }
    #endregion

    #region Spy Events
    void ActingAgents_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        switch ( e.CommandName )
        {
            case "Recruit":
                string[] args = e.CommandArgument.ToString().Split( '|' );
                RecruitAgents( Convert.ToInt32( args[0] ), Convert.ToInt32( args[1] ), args[2] );
                break;
        }
    }

    void RecruitAgents( int spyId, int cost, string name )
    {
        GridViewRow row;

        ATIdentity identity = ( (ATIdentity)Csla.ApplicationContext.User.Identity );

        int houseId = identity.HouseID;
        int count = 0;

        // find the row (needed for processing) using the spy ID data key
        for ( int i = 0; i < ActingAgents.Rows.Count; i++ )
        {
            if ( spyId == Convert.ToInt32( ActingAgents.DataKeys[i]["ID"] ) )
            {
                row = ActingAgents.Rows[i];
                TextBox cnt = (TextBox)row.Cells[7].FindControl( "RecruitmentCount" );
                count = Convert.ToInt32( cnt.Text );
                break;
            }
        }

        if ( count == 0 ) return;
        
        int userId = User.ID;

        Spy.AddAgents( houseId, spyId, cost, count );

        RefreshUserHouse();

        // add recruitment report.
        AphelionTrigger.Library.Report report = Report.NewReport();
        report.FactionID = identity.FactionID;
        report.GuildID = identity.GuildID;
        report.HouseID = identity.HouseID;
        report.Message = identity.House + " engaged " + count.ToString() + " " + ( count > 0 ? Pluralize( name ) : name ) + ".";
        report.ReportLevelID = 3 + House.GetSecrecyBonus( identity.Intelligence );
        report.Save();

        Session["CurrentObject"] = null;
        ActingAgents.DataBind();

        User.ClearAgentCache();
        OnRefreshHUD( new EventArgs() );
    }
    #endregion

    #region SpyListDataSource
    void ActingAgents_RowCreated( object sender, GridViewRowEventArgs e )
    {
        if ( e.Row.RowType == DataControlRowType.DataRow )
        {
            if ( Request.Browser.Browser != "IE" )
            {
                e.Row.Attributes.Add( "OnMouseOver", "this.style.backgroundColor = 'rgb(25,25,25)';" );
                e.Row.Attributes.Add( "OnMouseOut", "this.style.backgroundColor = 'Transparent';" );
            }
        }
    }

    protected void SpyListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        SpyList spies = GetSpyList();

        e.BusinessObject = spies;
    }

    private SpyList GetSpyList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is SpyList ) )
        {
            int houseId = ( (ATIdentity)Csla.ApplicationContext.User.Identity ).HouseID;

            businessObject = SpyList.GetAgentSchema( houseId, RecordScope.ShowAll );
            Session["CurrentObject"] = businessObject;
        }
        return (SpyList)businessObject;
    }
    #endregion
}
