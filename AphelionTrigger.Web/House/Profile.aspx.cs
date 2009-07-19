using System;
using System.Text;
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

public partial class House_Profile : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        if (!IsPostBack) Session["CurrentObject"] = null;

        GuildLink.Click += new EventHandler( GuildLink_Click );
        AttackGrid.RowCommand += new GridViewCommandEventHandler( AttackGrid_RowCommand );

        if (!Page.IsPostBack)
        {
            BindHouse();
        }
    }

    void AttackGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        switch (e.CommandName)
        {
            case "Attack":
                string[] attackArgs = e.CommandArgument.ToString().Split( '|' );
                int targetHouseId = Int32.Parse( attackArgs[0] );
                string targetHouse = attackArgs[1];
                ((AphelionTriggerPage)Page).HOUSE = new HouseStub( targetHouseId, targetHouse );
                Response.Redirect( "~/Warfare/Attack.aspx", true );
                break;
        }
    }

    void GuildLink_Click( object sender, EventArgs e )
    {
        ((AphelionTriggerPage)Page).GUILD = new GuildStub( Convert.ToInt32( ((LinkButton)sender).CommandArgument ) );
        Response.Redirect( "~/Guild/Profile.aspx", true );
    }

    #region House
    private void BindHouse()
    {
        House house = GetHouseDataSource();

        ATConfiguration config = ATConfiguration.Instance;

        // if the user is staff-only (i.e. has no house) we don't want them landing here so redirect
        if (!house.Exists) Response.Redirect( "~/System/Configuration.aspx" );

        HouseLabel.Text = house.Name;

        FactionLabel.Text = house.Faction;
        RankLabel.Text = house.Rank.ToString();
        PointsLabel.Text = house.Points.ToString();
        LevelLabel.Text = house.Level.Rank.ToString();
        ExperienceLabel.Text = house.Experience.ToString();
        AmbitionLabel.Text = house.Ambition.ToString() + "%";
        CreditsLabel.Text = house.Credits.ToString();

        // if viewing an enemy's house, ensure that the viewer has the requisite secrecy level to view an opponent's profile details
        if (UserHouse.ID != house.ID)
        {
            if ( House.GetSecreyLevel( UserHouse.Intelligence ) < 2 + House.GetSecrecyBonus( house.Intelligence ) )
            {
                AmbitionRow.Visible = false;
                PowerRow.Visible = false;
                ProtectionRow.Visible = false;
                AffluenceRow.Visible = false;
                SpeedRow.Visible = false;
                ContingencyRow.Visible = false;
                StatsRow.Visible = false;
            }
            if (House.GetSecreyLevel( UserHouse.Intelligence ) < 3 + House.GetSecrecyBonus( house.Intelligence ))
            {
                IntelligenceRow.Visible = false;
            }
        }

        // if this house is a faction leader, display the row indicating their status
        string factionLeaderBonusLabel = string.Empty;
        if ( house.FactionLeaderHouseID == house.ID )
        {
            FactionLeaderRow.Visible = true;
            if ( UserHouse.ID == house.ID )
            {
                FactionLeaderLabel.Text = "You are the leader of the " + UserHouse.Faction + ", the chosen of many, the lord of lords, the eternal owner of soverignity, the Olympion, the supreme inheritor, the watchful, the powerful, the guardian, the most strong.";
            }
            else if ( UserHouse.FactionID == house.FactionID )
            {
                FactionLeaderLabel.Text = house.Name + " is the leader of your Faction, the " + house.Faction + ", and the key to power and collective ascendency over Terra Nova.Is this incombency misplaced, are will they lead their allies together to a victorious future?";
            }
            else
            {
                FactionLeaderLabel.Text = house.Name + " is the leader of the " + house.Faction + ", renowned among them and chosen from their ranks as the flagbearer of their contemptable stabs at conquest.";
            }

            // only show faction bonus to leaders
            if ( UserHouse.ID == house.ID ) factionLeaderBonusLabel = "<span style=\"color:rgb(237,218,16);margin-left:10px;\">(+" + ( config.FactionLeaderBonus * 100 ).ToString() + "%)</span>";
        }

        // vary the ambition label's color from red for 0 to green for 100
        double ambitionGreen = ( ( house.Ambition / 100 ) * 255 ) / 2;
        int ambitionRed = ( ( 255 - (int)ambitionGreen ) / 2 );
        AmbitionLabel.Attributes.Add( "style", "color:rgb(" + ambitionRed.ToString() + "," + ambitionGreen.ToString() + ",0);" );

        IntelligenceLabel.Text = house.Intelligence.ToString();
        PowerLabel.Text = house.Power.ToString();
        ProtectionLabel.Text = house.Protection.ToString();
        AffluenceLabel.Text = house.Affluence.ToString();
        SpeedLabel.Text = house.Speed.ToString();
        ContingencyLabel.Text = house.Contingency.ToString();

        AttackLabel.Text = house.Attack.ToString() + factionLeaderBonusLabel;
        DefenseLabel.Text = house.Defense.ToString() + factionLeaderBonusLabel;
        CaptureLabel.Text = house.Capture.ToString() + factionLeaderBonusLabel;
        PlunderLabel.Text = house.Plunder.ToString() + factionLeaderBonusLabel;
        StunLabel.Text = house.Stun.ToString() + factionLeaderBonusLabel;

        // the information below is private, so exit from stat display now if viewing enemy house
        if ( UserHouse.ID != house.ID ) return;

        GuildLabel.Visible = house.GuildID == 0;
        GuildLink.Visible = house.GuildID > 0;
        GuildLink.Text = house.Guild;
        GuildLink.CommandArgument = house.GuildID.ToString();

        // TODO: modify display for when level advancement has been maxed out.

        // Populate experience progress bar and associated labels
        double nextLevel = house.NextLevel.Experience - house.Level.Experience;
        double experienceGained = nextLevel - (house.NextLevel.Experience - house.Experience);

        double percentLeveled = (experienceGained / nextLevel) * 100.00;

        LevelProgressBar.Style["width"] = percentLeveled.ToString() + "%";
        
        LevelExperienceLabel.Text = Convert.ToInt32( house.Level.Experience + 1 ).ToString();
        NextLevelExperienceLabel.Text = house.NextLevel.Experience.ToString();

        if (percentLeveled < 50)
            OuterProgressPercentLabel.Text = "&nbsp;" + percentLeveled.ToString( "N2" ) + "%";
        else
            InnerProgressPercentLabel.Text = "&nbsp;" + percentLeveled.ToString( "N2" ) + "%";

        // Populate unit cap progress bar and associated labels
        double percentUnitCap = (((double)house.UnitCapForcesCount) / ((double)house.Level.UnitCapacity)) * 100;

        UnitCapBar.Style["width"] = percentUnitCap.ToString() + "%";

        UnitCountLabel.Text = house.UnitCapForcesCount.ToString();
        UnitCapLabel.Text = house.Level.UnitCapacity.ToString();

        if (percentUnitCap < 50)
            OuterUnitCountPercentLabel.Text = "&nbsp;" + percentUnitCap.ToString( "N2" ) + "%";
        else
            InnerUnitCountPercentLabel.Text = "&nbsp;" + percentUnitCap.ToString( "N2" ) + "%";

        // ((AphelionTriggerPage)Page).HOUSE = new HouseStub( false );
    }
    #endregion

    #region Business Methods
    protected string GetUnitDetails( string description, string attack, string defense, string capture, string plunder, string repopulationRate, string depopulationRate )
    {
        StringBuilder details = new StringBuilder();
        details.AppendLine( description + "<br /><br />" );
        details.AppendLine( "<p>Attack: <strong>" + attack + "</strong></p>" );
        details.AppendLine( "<p>Defense: <strong>" + defense + "</strong></p>" );
        details.AppendLine( "<p>Capture: <strong>" + capture + "</strong></p>" );
        details.AppendLine( "<p>Plunder: <strong>" + plunder + "</strong></p>" );
        details.AppendLine( "<p>Repopulation: <strong>" + repopulationRate + "</strong></p>" );
        details.AppendLine( "<p>Depopulation: <strong>" + depopulationRate + "</strong></p>" );

        return details.ToString();
    }

    private int HouseID
    {
        get
        {
            if (HOUSE.Initialized)
            {
                return HOUSE.HouseID;
            }
            else
            {
                return User.HouseID;
            }
        }
    }

    public bool GetIsAttacker( int attackerHouseId )
    {
        return attackerHouseId == User.HouseID;
    }
    #endregion

    #region HouseDataSource
    private House GetHouseDataSource()
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is House))
        {
            businessObject = House.GetHouse( HouseID );
            Session["CurrentObject"] = businessObject;
        }
        return (House)businessObject;
    }
    #endregion

    #region TechnologyListDataSource
    protected void TechnologyListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        // If viewing another player's house, ensure that the viewer has the requisite secrecy level to view an opponent's technology
        House house = GetHouseDataSource();
        if (UserHouse.ID != house.ID)
        {
            if (House.GetSecreyLevel( UserHouse.Intelligence ) < 3 + House.GetSecrecyBonus( house.Intelligence ))
            {
                TechnologyLabel.Visible = false;
                TechnologyBoxPanel.Visible = false;
                return;
            }
        }

        e.BusinessObject = GetTechnologyList();
    }

    private TechnologyList GetTechnologyList()
    {

        return TechnologyList.GetResearchedTechnologies( HouseID );
    }
    #endregion

    #region UnitListDataSource
    protected void UnitListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        // If viewing another player's house, ensure that the viewer has the requisite secrecy level to view an opponent's forces
        House house = GetHouseDataSource();
        if (UserHouse.ID != house.ID)
        {
            if (House.GetSecreyLevel( UserHouse.Intelligence ) < 4 + House.GetSecrecyBonus( house.Intelligence ))
            {
                ForcesGrid.Visible = false;
                UnitCapRow.Visible = false;
                AttackSeparatorPanel.Visible = false;
                return;
            }
        }

        e.BusinessObject = GetUnitList();
    }

    private UnitList GetUnitList()
    {
        return UnitList.GetForces( HouseID );
    }
    #endregion

    #region AdvancementListDataSource
    protected void AdvancementListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        // If viewing another player's house, ensure that the viewer has the requisite secrecy level to view an opponent's advancements
        House house = GetHouseDataSource();
        if (UserHouse.ID != house.ID)
        {
            if (House.GetSecreyLevel( UserHouse.Intelligence ) < 3 + House.GetSecrecyBonus( house.Intelligence ))
            {
                AdvancementLabel.Visible = false;
                AdvancementBoxPanel.Visible = false;
                return;
            }
        }

        AdvancementList adv = GetAdvancementList();
        AdvancementsRow.Visible = adv.Count > 0;

        e.BusinessObject = adv;
    }

    private AdvancementList GetAdvancementList()
    {        
        return AdvancementList.GetAdvancementList( HouseID );
    }
    #endregion

    #region AttackListDataSource
    protected void AttackListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        // If viewing another player's house, ensure that the viewer has the requisite secrecy level to view an opponent's recent enemies
        House house = GetHouseDataSource();
        if (UserHouse.ID != house.ID)
        {
            if (House.GetSecreyLevel( UserHouse.Intelligence ) < 2 + House.GetSecrecyBonus( house.Intelligence ))
            {
                EnemiesLabel.Visible = false;
                EnemiesBoxPanel.Visible = false;
                return;
            }
        }

        e.BusinessObject = GetAttackList();
    }

    private AttackList GetAttackList()
    {
        return AttackList.GetEnemyAttackList( HouseID );
    }
    #endregion
}
