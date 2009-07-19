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

public partial class Technology_Research : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();
        if (!IsPostBack) Session["CurrentObject"] = null;

        Technologies.RowCreated += new GridViewRowEventHandler( Technologies_RowCreated );
        Technologies.RowCommand += new GridViewCommandEventHandler( Technologies_RowCommand );
    }

    #region Business Methods
    protected string DisplayResearchLink( int researchProgress )
    {
        return researchProgress >= 0 ? "none" : "";
    }

    protected string DisplayResearch( int researchProgress )
    {
        return researchProgress >= 0 ? "" : "none";
    }

    protected bool CanResearch( int cost )
    {
        return UserHouse.Credits - cost >= 0;
    }

    protected bool IsResearchInProgress( int researchStateId )
    {
        return ( researchStateId > 0 && researchStateId < 4 );
    }

    protected bool IsResearchComplete( int researchStateId )
    {
        return ( researchStateId == 4 );
    }
    #endregion

    #region Technologies
    void Technologies_RowCreated( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add( "OnMouseOver", "this.style.backgroundColor = 'rgb(25,25,25)';" );
            e.Row.Attributes.Add( "OnMouseOut", "this.style.backgroundColor = 'Transparent';" );
        }
    }

    void Technologies_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        int houseId = ( (ATIdentity)Csla.ApplicationContext.User.Identity ).HouseID;
        string[] args = e.CommandArgument.ToString().Split( '|' );

        int technologyId = 0;
        string name = string.Empty;

        switch (e.CommandName)
        {
            case "Research":
                technologyId = Convert.ToInt32( args[0] );
                name = args[1];

                Research( houseId, technologyId, name, "commenced" );
                break;
            case "Resume":
                technologyId = Convert.ToInt32( args[0] );
                name = args[1];

                Research( houseId, technologyId, name, "resumed" );
                break;
            case "Suspend":
                technologyId = Convert.ToInt32( args[0] );
                name = args[1];

                Suspend( houseId, technologyId, name );
                break;
            case "Abort":
                technologyId = Convert.ToInt32( args[0] );
                name = args[1];

                Abort( houseId, technologyId, name );
                break;
        }
    }

    void Research( int houseId, int technologyId, string name, string action )
    {
        AphelionTrigger.Library.Technology.AddResearch( houseId, technologyId );

        ATIdentity identity = ((ATIdentity)Csla.ApplicationContext.User.Identity);

        // add research report.
        AphelionTrigger.Library.Report report = Report.NewReport();
        report.FactionID = identity.FactionID;
        report.GuildID = identity.GuildID;
        report.HouseID = identity.HouseID;
        report.Message = "House " + identity.House + " " + action + " research on: " + name + ".";
        report.ReportLevelID = 3 + House.GetSecrecyBonus( identity.Intelligence );
        report.Save();

        // clear cache so that the grid will update with the new research
        Session["CurrentObject"] = null;
        Technologies.DataBind();
    }

    void Suspend( int houseId, int technologyId, string name )
    {
        AphelionTrigger.Library.Technology.SuspendResearch( houseId, technologyId );

        ATIdentity identity = ( (ATIdentity)Csla.ApplicationContext.User.Identity );

        // add research report.
        AphelionTrigger.Library.Report report = Report.NewReport();
        report.FactionID = identity.FactionID;
        report.GuildID = identity.GuildID;
        report.HouseID = identity.HouseID;
        report.Message = "House " + identity.House + " suspended research on: " + name + ".";
        report.ReportLevelID = 3 + House.GetSecrecyBonus( identity.Intelligence );
        report.Save();

        // clear cache so that the grid will update with the new research
        Session["CurrentObject"] = null;
        Technologies.DataBind();
    }

    void Abort( int houseId, int technologyId, string name )
    {
        AphelionTrigger.Library.Technology.AbortResearch( houseId, technologyId );

        ATIdentity identity = ( (ATIdentity)Csla.ApplicationContext.User.Identity );

        // add research report.
        AphelionTrigger.Library.Report report = Report.NewReport();
        report.FactionID = identity.FactionID;
        report.GuildID = identity.GuildID;
        report.HouseID = identity.HouseID;
        report.Message = "House " + identity.House + " aborted research on: " + name + ".";
        report.ReportLevelID = 3 + House.GetSecrecyBonus( identity.Intelligence );
        report.Save();

        // clear cache so that the grid will update with the new research
        Session["CurrentObject"] = null;
        Technologies.DataBind();
    }
    #endregion

    #region TechnologyListDataSource
    protected void TechnologyListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetTechnologyList();
    }

    private TechnologyList GetTechnologyList()
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is TechnologyList))
        {
            businessObject = TechnologyList.GetTechnologies( UserHouse.ID );
            Session["CurrentObject"] = businessObject;
        }
        return (TechnologyList)businessObject;
    }
    #endregion
}
