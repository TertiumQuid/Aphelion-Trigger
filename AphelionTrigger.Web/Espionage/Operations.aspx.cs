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
using AjaxControlToolkit;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class Espionage_Operations : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();

        Commit.Click += new EventHandler( Commit_Click );
        ContinueOperation.Click += new EventHandler( Commit_Click );
        NewOperation.Click += new EventHandler( NewOperation_Click );

        BusinessError.Visible = false;
    }

    void NewOperation_Click( object sender, EventArgs e )
    {
        ResultsPanel.Visible = false;
        TargetPanel.Visible = true;
        OperationPanel.Visible = true;
    }

    void Commit_Click( object sender, EventArgs e )
    {
        System.Text.StringBuilder text = new System.Text.StringBuilder();
        text.AppendFormat( "{0}<br/><br />", "Could not commit espionage:" );

        int targetHouseId = 0;

        try
        {
            // use entered house name to retrieve ID from cached house list
            bool hasTarget = false;

            foreach ( House h in Houses )
            {
                if ( h.Name.ToLower() == TargetHouseName.Text.ToLower() )
                {
                    hasTarget = true;
                    targetHouseId = h.ID;
                    break;
                }
            }

            // validate recipient before continuing the operatoni
            if ( !hasTarget )
            {
                text.AppendFormat( "* {0}: {1}<br/>", "House Name", "House not Found" );
                throw new Csla.Validation.ValidationException();
            }

            // validate recipient before continuing the operatoni
            if ( Operations.SelectedIndex < 0 )
            {
                text.AppendFormat( "* {0}: {1}<br/>", "Operation", "Espionage operation not selected" );
                throw new Csla.Validation.ValidationException();
            }
        }
        catch ( Csla.Validation.ValidationException ex )
        {
            BusinessError.Text = text.ToString();
            BusinessError.Visible = true;
            return;
        }

        int espionageId = 0;

        try
        {
            int operationId = Convert.ToInt32( Operations.DataKeys[Operations.SelectedIndex].Value );

            switch (operationId)
            {
                case 1:            
                    LarcenyCommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                case 2:
                    SurveillanceCommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                case 3:
                    ReconnaissanceCommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                case 4:
                    MICECommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                case 5:
                    AmbushCommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                case 6:
                    SabotageCommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                case 7:
                    ExpropriationCommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                case 8:
                    InspectionCommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                case 9:
                    SubversionCommand.Commit( User.HouseID, targetHouseId, ref espionageId );
                    break;
                default:
                    return;
            }
        }
        catch ( Csla.Validation.ValidationException ex )
        {
            System.Text.StringBuilder message = new System.Text.StringBuilder();
            message.AppendFormat( "{0}<br/>", ex.Message );

            BusinessError.Visible = true;
            BusinessError.Text = message.ToString();
            return;

        }
        
        Results.Text = EspionageLog.GetEspionageLog( espionageId ).Description;

        ResultsPanel.Visible = true;
        TargetPanel.Visible = false;
        OperationPanel.Visible = false;

    }

    #region Business Methods
    protected bool CanPerformOperation( int operationid, int cost, int turns )
    {
        bool canPerformOperation = true;
        switch ( operationid )
        {
            case 1:
                canPerformOperation = User.Agents.Larceny > 0;
                break;
            case 2:
                canPerformOperation = User.Agents.Surveillance > 0;
                break;
            case 3:
                canPerformOperation = User.Agents.Reconnaissance > 0;
                break;
            case 4:
                canPerformOperation = User.Agents.MICE > 0;
                break;
            case 5:
                canPerformOperation = User.Agents.Ambush > 0;
                break;
            case 6:
                canPerformOperation = User.Agents.Sabotage > 0;
                break;
            case 7:
                canPerformOperation = User.Agents.Expropriation > 0;
                break;
            case 8:
                canPerformOperation = User.Agents.Inspection > 0;
                break;
            case 9:
                canPerformOperation = User.Agents.Subversion > 0;
                break;
        }

        if ( canPerformOperation ) canPerformOperation = UserHouse.Credits >= cost;
        if ( canPerformOperation ) canPerformOperation = UserHouse.Turns >= turns;

        return canPerformOperation;
    }

    protected string GetOperationStatus( int operationid, int cost, int turns )
    {
        string status = string.Empty;
        switch ( operationid )
        {
            case 1:
                status = User.Agents.Larceny == 0 ? "Unskilled" : string.Empty;
                break;
            case 2:
                status = User.Agents.Surveillance == 0 ? "Unskilled" : string.Empty;
                break;
            case 3:
                status = User.Agents.Reconnaissance == 0 ? "Unskilled" : string.Empty;
                break;
            case 4:
                status = User.Agents.MICE == 0 ? "Unskilled" : string.Empty;
                break;
            case 5:
                status = User.Agents.Ambush == 0 ? "Unskilled" : string.Empty;
                break;
            case 6:
                status = User.Agents.Sabotage == 0 ? "Unskilled" : string.Empty;
                break;
            case 7:
                status = User.Agents.Expropriation == 0 ? "Unskilled" : string.Empty;
                break;
            case 8:
                status = User.Agents.Inspection == 0 ? "Unskilled" : string.Empty;
                break;
            case 9:
                status = User.Agents.Subversion == 0 ? "Unskilled" : string.Empty;
                break;
        }

        if ( status.Length == 0 )status = UserHouse.Credits < cost ? "Insufficient funds" : status;
        if ( status.Length == 0 )status = UserHouse.Turns < turns ? "Lacking turns" : status;

        return status;
    }

    public int OperatingHouseID
    {
        get
        {
            object step = ViewState["_OperatingHouseID"];
            if ( step == null || !( step is int ) )
            {
                ViewState.Add( "_OperatingHouseID", 0 );
                step = 0;
            }

            return (int)step;
        }

        set { ViewState.Add( "_OperatingHouseID", value ); }
    }

    public int TargetHouseID
    {
        get
        {
            object step = ViewState["_TargetHouseID"];
            if ( step == null || !( step is int ) )
            {
                ViewState.Add( "_TargetHouseID", 0 );
                step = 0;
            }

            return (int)step;
        }

        set { ViewState.Add( "_TargetHouseID", value ); }
    }
    #endregion

    #region EspionageOperationListDataSource
    protected void EspionageOperationListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        EspionageOperationList espionageOperations = GetEspionageOperationList();

        e.BusinessObject = espionageOperations;
    }

    private EspionageOperationList GetEspionageOperationList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is EspionageOperationList ) )
        {
            int houseId = ( (ATIdentity)Csla.ApplicationContext.User.Identity ).HouseID;

            businessObject = EspionageOperationList.GetEspionageOperationList();
            Session["CurrentObject"] = businessObject;
        }
        return (EspionageOperationList)businessObject;
    }
    #endregion
}
