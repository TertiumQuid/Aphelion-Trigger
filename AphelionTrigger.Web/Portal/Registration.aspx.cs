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
using AphelionTrigger.Security;
using AphelionTrigger.Library.Security;

public partial class Portal_Registration : AphelionTriggerPage
{
    private Random random = new Random();

    protected void Page_Load( object sender, EventArgs e )
    {
        ATConfiguration config = ATConfiguration.Instance;
        RegistrationLabel.Text = config.RegistrationActiveText;
        PlayerProtocols.Text = config.PlayerProtocols;

        // if registration is closed, hide form and don't initialize the page
        if (!config.RegistrationActive)
        {
            WrapperPanel.Visible = false;
            RegistrationLabel.Text = config.RegistrationInactiveText;
            return;
        }
        FactionList.InvalidateCache();

        SubmitRegistration.Click += new EventHandler(SubmitRegistration_Click);

        if (Session["CaptchaImageText"] == null)
        {
            Session["CaptchaImageText"] = GenerateRandomCode();
        }
    }

    void SubmitRegistration_Click(object sender, EventArgs e)
    {
        if (!ValidateRegistration()) return;

        int factionId = Convert.ToInt32(Factions.DataKeys[Factions.SelectedIndex].Value);

        string name = HouseName.Text;
        string username = Username.Text;
        string email = Email.Text;
        string password = Password.Text;

        int ageId = AgeList.CurrentAgeID();

        ATPrincipal.Register(ageId, factionId, name, username, email, password);

        ATPrincipal.Login(username, password);
        HttpContext.Current.Session["CslaPrincipal"] = Csla.ApplicationContext.User;

        // add registration report.
        ATIdentity identity = ((ATIdentity)Csla.ApplicationContext.User.Identity);
        AphelionTrigger.Library.Report report = Report.NewReport();
        report.FactionID = identity.FactionID;
        report.GuildID = identity.GuildID;
        report.HouseID = identity.HouseID;
        report.Message = "House " + name + " was formed by " + username + ".";
        report.ReportLevelID = 1 + House.GetSecrecyBonus(identity.Intelligence);
        report.Save();

        Response.Redirect("~/Portal/Welcome.aspx");
    }

    bool ValidateRegistration()
    {
        bool isValid = true;

        List<string> errors = new List<string>();

        if (Factions.SelectedIndex < 0) errors.Add("You must select a Faction to serve.");

        if (HouseName.Text.Length == 0) errors.Add("You must enter your House's Name.");
        else if (HouseName.Text.Length > 300) errors.Add("Your House's Name must be 300 characters or less.");

        if (Username.Text.Length == 0) errors.Add("You must enter a Username.");
        else if (Username.Text.Length > 50) errors.Add("Username must be 50 characters or less.");

        if (Password.Text.Length == 0) errors.Add("You must enter a Password.");
        else if (Password.Text.Length > 15) errors.Add("Your Password must be 15 characters or less.");

        if (!PlayerProtocolsAgreement.Checked) errors.Add("You must read and agree to the Player Protocols.");

        if (errors.Count > 0)
        {
            SaveError.Text = "&nbsp;Sorry, your registration <u>failed</u> for the following reasons:<br /><br />";
            SaveError.Text += "<ul>";
            foreach (string error in errors)
            {
                SaveError.Text += "<li>" + error + "</li>";
            }
            SaveError.Text += "</ul>";
            isValid = false;

            SaveError.Visible = true;
        }

        return isValid;
    }

    private string GenerateRandomCode()
    {
        string s = "";
        for (int i = 0; i < 6; i++)
            s = String.Concat( s, this.random.Next( 10 ).ToString() );
        return s;
    }

    #region FactionListDataSource
    protected void FactionListDataSource_SelectObject(object sender, Csla.Web.SelectObjectArgs e)
    {
        e.BusinessObject = GetFactionList();
    }

    private FactionList GetFactionList()
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is FactionList))
        {
            businessObject = FactionList.GetFactionList();
            Session["CurrentObject"] = businessObject;
        }
        return (FactionList)businessObject;
    }
    #endregion
}
