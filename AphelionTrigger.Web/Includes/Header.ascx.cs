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
using AphelionTrigger.Library;

public partial class Includes_Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ATConfiguration config = ATConfiguration.Instance;
        VersionLabel.Text = "v" + config.Version.ToString();

        Age age = Application["_CURRENTAGE"] as Age;
        if (age == null)
        {
            AgeList ages = AgeList.GetAgeList();
            foreach (Age a in ages)
            {
                if (a.IsCurrent)
                {
                    age = a;
                    break;
                }
            }

            Application["_CURRENTAGE"] = age;
        }

        AgeLabel.Text = "The " + age.ID.ToString() + GetAgeSuffix(age.ID) + " Age | " + age.Name;
    }

    private string GetAgeSuffix( int ageId )
    {
        switch (ageId.ToString())
        {
            case "1":
                return "st";
                break;
            case "2":
                return "nd";
                break;
            case "3":
                return "rd";
                break;
            default:
                return "th";
                break;
        }
    }

    public void RefreshHUD()
    {
        ATHUD.RefreshHUD( true );
    }
}
