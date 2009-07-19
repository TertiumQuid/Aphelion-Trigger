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

public partial class Warfare_Forces : AphelionTriggerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RequireAuthentication();
        RequireHouse();

        StandingForces.RowCreated += new GridViewRowEventHandler( StandingForces_RowCreated );
        StandingForces.RowCommand += new GridViewCommandEventHandler( StandingForces_RowCommand );

        if (!Page.IsPostBack)
        {
            RefreshUserHouse();
            Session["CurrentObject"] = null;
        }

        BindLabels();
    }

    #region Business Methods
    protected int MaxTroopRecruitment( int cost, int unitClassId )
    {
        int maxByCredits = UserHouse.Credits / cost;
        int maxByCap = ( unitClassId == 3 ) ? maxByCredits : UserHouse.Level.UnitCapacity - UserHouse.UnitCapForcesCount;

        int maxTroops = ( cost != 0 ? ( maxByCap < maxByCredits ? maxByCap : maxByCredits ) : 0 );

        // don't let a player recruit more than 300 units at a time - larger numbers could produce slider display issues
        maxTroops = maxTroops > 300 ? 300 : maxTroops;

        return maxTroops;
    }

    protected string GetRecruitmentStatus( int cost, int unitClassId )
    {
        string status = string.Empty;

        int availableByCap = ( unitClassId == 3 ? 1 : UserHouse.Level.UnitCapacity - UserHouse.UnitCapForcesCount );
        double availableByCredits = (double)UserHouse.Credits / (double)cost;

        if ( availableByCap < 1 ) status = "Unit Cap reached!";
        else if ( availableByCredits < 1 ) status = "Insufficient funds!";

        return status;
    }

    protected bool CanRecruitForces( int cost, int unitClassId )
    {
        bool canRecruit = ( ( UserHouse.Level.UnitCapacity - UserHouse.UnitCapForcesCount > 0 || unitClassId == 3 ) && ( UserHouse.Credits / cost ) > 0 );
        return canRecruit;
    }
    #endregion

    #region StandingForces
    void StandingForces_RowCreated( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.Browser.Browser != "IE")
            {
                e.Row.Attributes.Add( "OnMouseOver", "this.style.backgroundColor = 'rgb(25,25,25)';" );
                e.Row.Attributes.Add( "OnMouseOut", "this.style.backgroundColor = 'Transparent';" );
            }
        }
    }

    void StandingForces_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        switch (e.CommandName)
        {
            case "Recruit":
                string[] args = e.CommandArgument.ToString().Split( '|' );
                RecruitForces( Convert.ToInt32( args[0] ), Convert.ToInt32( args[1] ), args[2] );
                break;
        }
    }

    void RecruitForces( int unitId, int cost, string name )
    {
        GridViewRow row;
        
        ATIdentity identity = ((ATIdentity)Csla.ApplicationContext.User.Identity);

        int houseId = identity.HouseID;
        int count = 0;

        // find the row (needed for processing) using the Unit ID data key
        for (int i = 0; i < StandingForces.Rows.Count; i++)
        {
            if (unitId == Convert.ToInt32( StandingForces.DataKeys[i]["ID"] ))
            {
                row = StandingForces.Rows[i];
                TextBox cnt = (TextBox)row.Cells[7].FindControl( "RecruitmentCount" );
                count = Convert.ToInt32( cnt.Text );
                break;
            }
        }

        if (count == 0) return;

        int userId = ((ATIdentity)Csla.ApplicationContext.User.Identity).ID;

        AphelionTrigger.Library.Unit.AddForces( houseId, unitId, cost, count );
        
        RefreshUserHouse();
        
        // add basic recruitment report.
        AphelionTrigger.Library.Report report1 = Report.NewReport();
        report1.FactionID = identity.FactionID;
        report1.GuildID = identity.GuildID;
        report1.HouseID = identity.HouseID;
        report1.Message = identity.House + " recurited forces.";
        report1.ReportLevelID = 2 + House.GetSecrecyBonus( identity.Intelligence );
        report1.Save();

        // add detailed recruitment report.
        AphelionTrigger.Library.Report report2 = Report.NewReport();
        report2.FactionID = identity.FactionID;
        report2.GuildID = identity.GuildID;
        report2.HouseID = identity.HouseID;
        report2.Message = identity.House + " recurited " + count.ToString() + " " + (count > 0 ? Pluralize( name ) : name) + ".";
        report2.ReportLevelID = 3 + House.GetSecrecyBonus( identity.Intelligence );
        report2.Save();

        Session["CurrentObject"] = null;
        StandingForces.DataBind();

        BindLabels();

        OnRefreshHUD( new EventArgs() );
    }
    #endregion

    #region UnitListDataSource
    protected void UnitListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        UnitList units = GetUnitList();

        // bind summary labels
        TotalAttack.Text = units.TotalAttack.ToString();
        TotalDefense.Text = units.TotalDefense.ToString();
        TotalPlunder.Text = units.TotalPlunder.ToString();
        TotalCapture.Text = units.TotalCapture.ToString();
        
        MilitiaCount.Text = units.MilitiaCount.ToString();
        MilitaryCount.Text = units.MilitaryCount.ToString();
        MercenaryCount.Text = units.MercenaryCount.ToString();
        ForcesCount.Text = units.ForcesCount.ToString();

        e.BusinessObject = units;
    }

    private UnitList GetUnitList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is UnitList ) )
        {
            int houseId = ( (ATIdentity)Csla.ApplicationContext.User.Identity ).HouseID;

            businessObject = UnitList.GetRecruitmentSchema( houseId );
            Session["CurrentObject"] = businessObject;
        }
        return (UnitList)businessObject;
    }
    #endregion

    void BindLabels()
    {
        int maxByCap = UserHouse.Level.UnitCapacity - (UserHouse.MilitiaCount + UserHouse.MilitaryCount);

        if ((UserHouse.Level.UnitCapacity - (UserHouse.MilitiaCount + UserHouse.MilitaryCount)) > 0)
            CreditsLabel.Text = "You have <span style=\"font-weight:bold;color:rgb(0,175,0);\">" + UserHouse.Credits.ToString() + " credits</span> to recruit up to <span style=\"font-weight:bold;color:rgb(0,0,175);\">" + maxByCap.ToString() + "</span> more units with.";
        else
            CreditsLabel.Text = "<span style=\"font-weight:bold;color:rgb(175,0,0);\">You have reached your unit cap (" 
                + UserHouse.Level.UnitCapacity + ") and cannot recruit additional MILITIA or MILITARY forces.</span> " 
                + "Level advancements will raise your unit cap, however, and MERCENARIES don't count against your unit cap at all, "
                + "but too large a force will reduce your speed.";
    }

}
