using System;
using System.Text.RegularExpressions;
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

public partial class Warfare_History : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();

        AttackLogs.RowCreated += new GridViewRowEventHandler( AttackLogs_RowCreated );
        AttackLogs.PageIndexChanging += new GridViewPageEventHandler( AttackLogs_PageIndexChanging );
        LogFilter.SelectedIndexChanged += new EventHandler( Filters_SelectedIndexChanged );
        LogView.SelectedIndexChanged += new EventHandler( Filters_SelectedIndexChanged );

        if (!Page.IsPostBack) BindLogs();
    }

    void Filters_SelectedIndexChanged( object sender, EventArgs e )
    {
        BindLogs();
    }

    #region Business Methods
    public string GetAttackText( int attackerHouseId, int defenderHouseId )
    {
        if (attackerHouseId == User.HouseID)
            return "Attacked ";
        else if (defenderHouseId == User.HouseID)
            return "Was attacked by";
        else
            return string.Empty;
    }

    public string DisplayCasualties( int attackerHouseId, int attackerCasualties, int defenderCasualties )
    {
        int killed = (attackerHouseId == User.HouseID) ? defenderCasualties : attackerCasualties;
        int lost = (attackerHouseId == User.HouseID) ? attackerCasualties : defenderCasualties;

        string text = "<span style='color:blue;'>+" + killed.ToString() + "</span>/<span style='color:red;'>-" + lost.ToString() + "</span>";
        return text;
    }

    public string DisplayPlunder( int attackerHouseId, int plundered )
    {
        string text = (attackerHouseId == User.HouseID) ? "<span style='color:blue;'>+" + plundered.ToString() + "</span>" : "<span style='color:red;'>-" + plundered.ToString() + "</span>";
        return text;
    }

    public string DisplayCapture( int attackerHouseId, int capture )
    {
        if (capture == 0) return "--";
        string text = (attackerHouseId == User.HouseID) ? "<span style='color:blue;'>+" + capture.ToString() + "</span>" : "<span style='color:red;'>-" + capture.ToString() + "</span>";
        return text;
    }

    public string FormatPronouns( int attackerHouseId, string description )
    {
        // attack logs are stored from the attacker's point of view, so we only need to format the pronouns if the user was defending
        if (attackerHouseId == User.HouseID) return description;

        string text = description;

        // defender shouldn't see attacker's ambition and experience results
        text = Regex.Replace( text, @"You gained \d* experience\.<br/>", "" );
        text = Regex.Replace( text, @"The ambition around you.*?<br/>", "" );
        text = Regex.Replace( text, @"You advanced to level.*?<br/>", "" );

        // format the description's pronouns
        text = Regex.Replace( text, @"You killed", "dellikuoy" );
        text = Regex.Replace( text, @"You lost", "You killed" );
        text = Regex.Replace( text, @"dellikuoy", "You lost" );
        text = Regex.Replace( text, @"You seized", "You lost" );
        text = Regex.Replace( text, @"You captured", "You lost" );

        return text;
    }

    public bool GetIsAttacker( int attackerHouseId )
    {
        return attackerHouseId == User.HouseID;
    }
    #endregion

    #region Attack Logs

    void AttackLogs_RowCreated( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add( "OnMouseOver", "this.style.backgroundColor = 'rgb(25,25,25)';" );
            e.Row.Attributes.Add( "OnMouseOut", "this.style.backgroundColor = 'Transparent';" );
        }
    }

    void AttackLogs_PageIndexChanging( object sender, GridViewPageEventArgs e )
    {
        AttackLogs.PageIndex = e.NewPageIndex;
        BindLogs();
    }

    protected void BindLogs()
    {
        AttackList attacks = AttackList.GetAttackList( User.HouseID );

        if (LogFilter.SelectedIndex > 0)
        {
            AttackList filteredAttacks = AttackList.NewAttackList();
            foreach (Attack log in attacks)
            {
                if (LogFilter.SelectedValue == "1") // attacks made
                {
                    if (User.HouseID == log.AttackerHouseID) filteredAttacks.Add( log );
                }
                else if (LogFilter.SelectedValue == "2") // was attacked
                {
                    if (User.HouseID == log.DefenderHouseID) filteredAttacks.Add( log );
                }
            }

            AttackLogs.DataSource = filteredAttacks;
        }
        else
        {
            AttackLogs.DataSource = attacks;
        }

        if (LogView.SelectedIndex == 0)
        {
            AttackLogs.Columns[3].Visible = true;
            AttackLogs.Columns[4].Visible = true;
            AttackLogs.Columns[5].Visible = true;
        }
        else if (LogView.SelectedIndex == 1)
        {
            AttackLogs.Columns[3].Visible = false;
            AttackLogs.Columns[4].Visible = false;
            AttackLogs.Columns[5].Visible = false;
        }

        AttackLogs.DataKeyNames = new string[1] { "ID" };
        AttackLogs.DataBind();

        LogUpdatePanel.Update();
    }

    #endregion
}
