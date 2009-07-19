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

public partial class Guild_Create : AphelionTriggerPage
{
    private const int _COST = 500000;

    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();

        // prevent multiple guild memberships
        if (HasGuild()) Response.Redirect( "Profile.aspx", true );

        CreateGuild.Click += new EventHandler( CreateGuild_Click );

        CreditsLabel.DataBind();

        EnableTinyMCE();
    }

    #region Business Methods
    public int Cost
    {
        get { return _COST; }
    }

    protected bool HasGuild()
    {
        return ((ATIdentity)Csla.ApplicationContext.User.Identity).HasGuild;
    }
    #endregion

    #region Guild
    void CreateGuild_Click( object sender, EventArgs e )
    {
        Guild guild = Guild.NewGuild();
        guild.Name = Name.Text;
        guild.Description = Description.Text;
        guild.LeaderHouseID = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;
        guild.Cost = Cost;

        // initiate error message
        System.Text.StringBuilder text = new System.Text.StringBuilder();
        text.AppendFormat( "{0}<br/><br />", "Could not create guild:" );

        try
        {
            if (UserHouse.Credits < Cost)
            {
                text.AppendFormat( "* {0}: {1}<br/>", "Cost", "You do not have enough credits" );
                throw new Csla.Validation.ValidationException();
            }

            guild.Save();

            ATIdentity identity = (ATIdentity)Csla.ApplicationContext.User.Identity;

            // update identity to reflect guild membership
            ATPrincipal.Login( identity.Name, identity.Password );
            HttpContext.Current.Session["CslaPrincipal"] = Csla.ApplicationContext.User;

            // refresh HUD to reflect new credits, etc.
            int userId = ((ATIdentity)Csla.ApplicationContext.User.Identity).ID;
            Cache.Remove( "HUD" + userId.ToString() );

            // add guild report.
            AphelionTrigger.Library.Report report = Report.NewReport();
            report.FactionID = identity.FactionID;
            report.GuildID = identity.GuildID;
            report.HouseID = identity.HouseID;
            report.Message = "House " + identity.House + " formed a guild: " + guild.Name + ".";
            report.ReportLevelID = 1 + House.GetSecrecyBonus( identity.Intelligence );
            report.Save();

            Response.Redirect( "Profile.aspx", true );
        }
        catch (Csla.Validation.ValidationException ex)
        {
            if (guild.BrokenRulesCollection.Count == 1)
            {
                text.AppendFormat( "* {0}: {1}", guild.BrokenRulesCollection[0].Property, guild.BrokenRulesCollection[0].Description );
            }
            else
            {
                foreach (Csla.Validation.BrokenRule rule in guild.BrokenRulesCollection)
                    text.AppendFormat( "* {0}: {1}<br/>", rule.Property, rule.Description );
            }

            GuildError.Text = text.ToString();
            GuildError.Visible = true;
            return;
        }
    }
    #endregion
}
