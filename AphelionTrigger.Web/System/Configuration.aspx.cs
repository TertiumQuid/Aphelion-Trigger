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
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;
using AjaxControlToolkit;

public partial class System_Configuration : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        ATConfiguration config = ATConfiguration.Instance;
        config.InvalidateCache();
        RequireAdministration();

        EnableTinyMCE();

        UpdateSettings.Click += new EventHandler( UpdateSettings_Click );

        if (!Page.IsPostBack) BindSettings();
        ConfigurationError.Visible = false;
    }

    #region Configuration 
    void UpdateSettings_Click( object sender, EventArgs e )
    {
        if (!ValidateConfiguration()) return;

        ATConfiguration.UpdateConfiguration( "RegistrationActive", Registration.Checked.ToString() );
        ATConfiguration.UpdateConfiguration( "RegistrationActiveText", RegistrationActiveText.Text );
        ATConfiguration.UpdateConfiguration( "RegistrationInactiveText", RegistrationInactiveText.Text );
        ATConfiguration.UpdateConfiguration( "PlayerProtocols", PlayerProtocols.Text );
        ATConfiguration.UpdateConfiguration( "WelcomeText", WelcomeText.Text );
        ATConfiguration.UpdateConfiguration( "Version", Version.Text );
        ATConfiguration.UpdateConfiguration( "AttackDelay", AttackDelay.Text );
        ATConfiguration.UpdateConfiguration( "StartingTurns", StartingTurns.Text );
        ATConfiguration.UpdateConfiguration( "TurnUnitStep", TurnUnitStep.Text );
        ATConfiguration.UpdateConfiguration( "TurnUnitCap", TurnUnitCap.Text );
        ATConfiguration.UpdateConfiguration( "LevelCap", LevelCap.Text );
        ATConfiguration.UpdateConfiguration( "TurnFactor", TurnFactor.Text );
        ATConfiguration.UpdateConfiguration( "StartingCredits", StartingCredits.Text );
        ATConfiguration.UpdateConfiguration( "CreditsFactor", CreditsFactor.Text );
        ATConfiguration.UpdateConfiguration( "CaptureCap", CaptureCap.Text );
        ATConfiguration.UpdateConfiguration( "CaptureFactor", CaptureFactor.Text );
        ATConfiguration.UpdateConfiguration( "CaptureDivisor", CaptureDivisor.Text );
        ATConfiguration.UpdateConfiguration( "ReportsRefreshRate", ReportsRefreshRate.Text );
        ATConfiguration.UpdateConfiguration( "MessagesRefreshRate", MessagesRefreshRate.Text );
        ATConfiguration.UpdateConfiguration( "AttacksRefreshRate", AttacksRefreshRate.Text );
        ATConfiguration.UpdateConfiguration( "CasualtyFactor", CasualtyFactor.Text );
        ATConfiguration.UpdateConfiguration( "FactionLeaderBonus", FactionLeaderBonus.Text );
        ATConfiguration.UpdateConfiguration( "ContingencyFactor", ContingencyFactor.Text );

        ATConfiguration config = ATConfiguration.Instance;
        config.InvalidateCache();
    }

    bool ValidateConfiguration()
    {
        bool isValid = true;

        List<string> errors = new List<string>();

        try { double.Parse( Version.Text ); }
        catch { errors.Add( "You must enter a number for codebase version." ); }

        try { double.Parse( LevelCap.Text ); }
        catch { errors.Add( "You must enter a number of four digits or less for level cap." ); }

        try 
        { 
            int.Parse( AttackDelay.Text );
            if (Convert.ToInt32( AttackDelay.Text ) > 99) errors.Add( "You must enter a number less than 100 for attack delay." );
        }
        catch { errors.Add( "You must enter a number of two digits (in seconds) or less for attack delay." ); }

        try { int.Parse( TurnUnitStep.Text ); }
        catch { errors.Add( "You must enter a number for turn unit step." ); }

        try { int.Parse( TurnUnitCap.Text ); }
        catch { errors.Add( "You must enter a number for turn unit cap." ); }

        try { int.Parse( TurnFactor.Text ); }
        catch { errors.Add( "You must enter a number for turn factor." ); }

        try { int.Parse( CreditsFactor.Text ); }
        catch { errors.Add( "You must enter a number for credits factor." ); }

        try { int.Parse( StartingTurns.Text ); }
        catch { errors.Add( "You must enter a number for starting turns." ); }

        try { int.Parse( StartingCredits.Text ); }
        catch { errors.Add( "You must enter a number for starting credits." ); }

        if (errors.Count > 0)
        {
            ConfigurationError.Text = "&nbsp;Configuration setting update <u>failed</u> for the following reasons:<br /><br />";
            ConfigurationError.Text += "<ul>";
            foreach (string error in errors)
            {
                ConfigurationError.Text += "<li>" + error + "</li>";
            }
            ConfigurationError.Text += "</ul>";
            isValid = false;

            ConfigurationError.Visible = true;
        }

        return isValid;
    }

    private void BindSettings()
    {
        ATConfiguration config = ATConfiguration.Instance;
        config.InvalidateCache();
        config = ATConfiguration.Instance;

        Registration.Checked = config.RegistrationActive;

        CurrentAge.Items.Add( new ListItem( "[SUSPENDED]", "0" ) );
        AgeList ages = AgeList.GetAgeList();
        foreach (Age age in ages)
        {
            ListItem item = new ListItem( age.Name + " (" + age.ID + ")", age.ID.ToString() );
            item.Selected = age.IsCurrent;

            CurrentAge.Items.Add( item );
        }
        
        Version.Text = config.Version.ToString();

        WelcomeText.Text = config.WelcomeText;
        RegistrationActiveText.Text = config.RegistrationActiveText;
        RegistrationInactiveText.Text = config.RegistrationInactiveText;
        PlayerProtocols.Text = config.PlayerProtocols;

        LevelCap.Text = config.LevelCap.ToString();

        FactionLeaderBonus.Text = config.FactionLeaderBonus.ToString();

        AttackDelay.Text = config.AttackDelay.ToString();
        CaptureCap.Text = config.CaptureCap.ToString();
        CaptureFactor.Text = config.CaptureFactor.ToString();
        CaptureDivisor.Text = config.CaptureDivisor.ToString();

        StartingTurns.Text = config.StartingTurns.ToString();
        TurnUnitStep.Text = config.TurnUnitStep.ToString();
        TurnUnitCap.Text = config.TurnUnitCap.ToString();
        TurnFactor.Text = config.TurnFactor.ToString();

        CreditsFactor.Text = config.CreditsFactor.ToString();
        StartingCredits.Text = config.StartingCredits.ToString();

        ReportsRefreshRate.Text = config.ReportsRefreshRate.ToString();
        MessagesRefreshRate.Text = config.MessagesRefreshRate.ToString();
        AttacksRefreshRate.Text = config.AttacksRefreshRate.ToString();
        CasualtyFactor.Text = config.CasualtyFactor.ToString();
        ContingencyFactor.Text = config.ContingencyFactor.ToString();

    }
    #endregion
}
