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

public partial class Warfare_Attack : AphelionTriggerPage
{
    public enum AttackStep
    {
        Target = 0,
        Review = 1,
        Resolution = 2,
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();

        TargetButton.Click += new EventHandler( TargetButton_Click );
        AttackButton.Click += new EventHandler( AttackButton_Click );
        NewAttack.Click += new EventHandler( NewAttack_Click );
        ContinueAttack.Click += new EventHandler( AttackButton_Click );

        // Ensure that the attacker has 1 or more turns to perform the attack
        if (UserHouse.Turns < 1)
        {
            NoTurnsPanel.Visible = true;
            AttackUpdatePanel.Visible = false;
            return;
        }

        if (!Page.IsPostBack)
        {
            BindTarget();
            RefreshPanelDisplay();

            TargetHouseName.Focus();
        }

        AttackError.Visible = false;
    }

    #region Business Methods
    public AttackStep CurrentStep
    {
        get
        {
            object step = ViewState["_AttackStep"];
            if (step == null || !(step is AttackStep))
            {
                ViewState.Add( "_AttackStep", AttackStep.Target );
                step = AttackStep.Target;
            }

            return (AttackStep)step;
        }

        set { ViewState.Add( "_AttackStep", value ); }
    }

    public int AttackerHouseID
    {
        get
        {
            object step = ViewState["_AttackerHouseID"];
            if (step == null || !(step is int))
            {
                ViewState.Add( "_AttackerHouseID", 0 );
                step = 0;
            }

            return (int)step;
        }

        set { ViewState.Add( "_AttackerHouseID", value ); }
    }

    public int DefenderHouseID
    {
        get
        {
            object step = ViewState["_DefenderHouseID"];
            if (step == null || !(step is int))
            {
                ViewState.Add( "_DefenderHouseID", 0 );
                step = 0;
            }

            return (int)step;
        }

        set { ViewState.Add( "_DefenderHouseID", value ); }
    }
    #endregion

    #region Targeting
    private void BindTarget()
    {
        // attempt to populate target house name textbox with last stored attacked house
        TargetHouseName.Text = (Session["_LASTATTACKEDHOUSENAME"] as string);
        
        if (!((AphelionTriggerPage)Page).HOUSE.Initialized) return;

        TargetHouseName.Text = ((AphelionTriggerPage)Page).HOUSE.HouseName;

        // automatically advance to next step
        TargetButton_Click( TargetButton, new EventArgs() );
    }

    void TargetButton_Click( object sender, EventArgs e )
    {
        // initiate error message
        System.Text.StringBuilder text = new System.Text.StringBuilder();
        text.AppendFormat( "{0}<br/><br />", "Could not target house:" );

        int targetHouseId = 0;

        try
        {
            // use entered house name to retrieve ID from cached house list
            bool hasTarget = false;

            foreach (House h in Houses)
            {
                if (h.Name.ToLower() == TargetHouseName.Text.ToLower())
                {
                    hasTarget = true;
                    targetHouseId = h.ID;
                    break;
                }
            }

            // validate recipient before continuing the attack
            if (!hasTarget)
            {
                text.AppendFormat( "* {0}: {1}<br/>", "House Name", "House not Found" );
                CurrentStep = AttackStep.Target;
                RefreshPanelDisplay();
                throw new Csla.Validation.ValidationException();
            }
        }
        catch (Csla.Validation.ValidationException ex)
        {
            AttackError.Text = text.ToString();
            AttackError.Visible = true;
            return;
        }

        CurrentStep = AttackStep.Review;
        RefreshPanelDisplay();

        // place house IDs in viewstate for lookup in future steps
        AttackerHouseID = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;
        DefenderHouseID = targetHouseId;

        BindHouses( ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID, targetHouseId );

        ((AphelionTriggerPage)Page).HOUSE = new HouseStub( false );
    }
    #endregion

    #region Attacking
    void AttackButton_Click( object sender, EventArgs e )
    {
        int attackId = 0;

        AttackCommand.Attack( AttackerHouseID, DefenderHouseID, ref attackId );

        CurrentStep = AttackStep.Resolution;
        RefreshPanelDisplay();

        #region Add Attack Report
        ATIdentity identity = ((ATIdentity)Csla.ApplicationContext.User.Identity);

        string targetName = string.Empty;
        int targetID = DefenderHouseID;
        foreach (House h in Houses)
        {
            if (h.ID == DefenderHouseID)
            {
                targetName = h.Name;
                break;
            }
        }

        AphelionTrigger.Library.Report report = Report.NewReport();
        report.FactionID = identity.FactionID;
        report.GuildID = identity.GuildID;
        report.HouseID = identity.HouseID;
        report.Message = "House " + identity.House + " attacked House " + targetName + ".";
        report.ReportLevelID = 2 + House.GetSecrecyBonus( identity.Intelligence );
        report.Save();
        #endregion
        
        Results.Text = Attack.GetAttack( attackId ).Description;
        
        // pause for X seconds, to prevent the user from easily attacking too quickly
        System.Threading.Thread.Sleep( ATConfiguration.Instance.AttackDelay * 1000 );

        // house stats probably changed so emtpy House cache and refresh HUD
        OnRefreshHUD( new EventArgs() );
        RefreshUserHouse();
        
        // store the attacked house's name to default populate the target textbox on next visit
        Session["_LASTATTACKEDHOUSENAME"] = targetName;
    }

    void NewAttack_Click( object sender, EventArgs e )
    {
        CurrentStep = AttackStep.Target;
        RefreshPanelDisplay();
    }
    #endregion

    private void BindHouses( int attackerHouseId, int defenderHouseId )
    {
        House attacker = House.GetHouse( attackerHouseId );
        House defender = House.GetHouse( defenderHouseId );

        AttackerNameLabel.Text = attacker.Name;
        AttackerPowerLabel.Text = attacker.Power.ToString();
        AttackerProtectionLabel.Text = attacker.Protection.ToString();
        AttackerAttackLabel.Text = attacker.Attack.ToString();
        AttackerDefenseLabel.Text = attacker.Defense.ToString();
        AttackerCaptureLabel.Text = attacker.Capture.ToString();
        AttackerPlunderLabel.Text = attacker.Plunder.ToString();
        AttackerStunLabel.Text = attacker.Stun.ToString();
        AttackerForcesLabel.Text = attacker.ForcesCount.ToString();
        AttackerAmbitionLabel.Text = attacker.Ambition.ToString();
        AttackerCreditsLabel.Text = attacker.Credits.ToString();

        DefenderNameLabel.Text = defender.Name;
        DefenderAttackLabel.Text = defender.Attack.ToString();
        DefenderDefenseLabel.Text = defender.Defense.ToString();
        DefenderCaptureLabel.Text = defender.Capture.ToString();
        DefenderPlunderLabel.Text = defender.Plunder.ToString();
        DefenderStunLabel.Text = defender.Stun.ToString();
        DefenderForcesLabel.Text = defender.ForcesCount.ToString();
        DefenderCreditsLabel.Text = defender.Credits.ToString();

        // ensure that the attacker has the requisite secrecy level to view the defender's details
        if ( House.GetSecrecyBonus( defender.Intelligence ) + 2 < House.GetSecreyLevel( attacker.Intelligence ) )
        {
            DefenderAmbitionLabel.Text = defender.Ambition.ToString();
            DefenderPowerLabel.Text = defender.Power.ToString();
            DefenderProtectionLabel.Text = defender.Protection.ToString();
        }
    }

    private void RefreshPanelDisplay()
    {
        switch (CurrentStep)
        {
            case AttackStep.Target:
                TargetPanel.Visible = true;
                ReviewPanel.Visible = false;
                ResultsPanel.Visible = false;
                break;
            case AttackStep.Review:
                TargetPanel.Visible = true;
                ReviewPanel.Visible = true;
                ResultsPanel.Visible = false;
                break;
            case AttackStep.Resolution:
                TargetPanel.Visible = false;
                ReviewPanel.Visible = false;
                ResultsPanel.Visible = true;
                break;
        }
    }
}
