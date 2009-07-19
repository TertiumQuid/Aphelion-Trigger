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

public partial class System_Resets : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAdministration();

        Reset.Click += new EventHandler( Reset_Click );
    }

    void Reset_Click( object sender, EventArgs e )
    {
        if (Messages.Checked) Message.ResetMessages( Ages.Checked );
        if (Forces.Checked) AphelionTrigger.Library.Unit.ResetForces( Ages.Checked );
        if (Turns.Checked) House.ResetTurns( Ages.Checked );
        if (Credits.Checked) House.ResetCredits( Ages.Checked );
        if (Ambition.Checked) House.ResetAmbition( Ages.Checked );
        if (Technology.Checked) AphelionTrigger.Library.Technology.ResetTechnology( Ages.Checked );
        if (Advancement.Checked) AphelionTrigger.Library.Advancement.ResetAdvancement( Ages.Checked );
        if (Attacks.Checked) Attack.ResetAttacks( Ages.Checked );
        if (Reports.Checked) Report.ResetReports( Ages.Checked );
        if (Stats.Checked) House.ResetStats( Ages.Checked );
    }
}
