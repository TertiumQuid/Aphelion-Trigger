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
using System.Web.Caching;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class Includes_Reports : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) RefreshReports( false );

        // set the refresh timer interval in seconds
        ATConfiguration config = ATConfiguration.Instance;
        ReportsTimer.Interval = config.ReportsRefreshRate * 1000;
    }

    protected void ReportsTimer_Tick( object sender, EventArgs e )
    {
        RefreshReports( false );
    }

    public void RefreshReports( bool forceRefresh )
    {
        ReportList reports;

        ATIdentity identity = ((ATIdentity)Csla.ApplicationContext.User.Identity);

        if (Cache["Reports"] == null || forceRefresh)
        {
            reports = ReportList.GetReportList();

            HttpContext.Current.Cache.Insert( "Reports", reports, null, DateTime.Now.AddMinutes( 0.25 ), Cache.NoSlidingExpiration, CacheItemPriority.BelowNormal, null );
        }
        else
        {
            reports = (ReportList)Cache["Reports"];
        }

        reports = ReportList.GetReportList( reports, identity.Intelligence, identity.FactionID, identity.GuildID, identity.HouseID );

        ReportsRepeater.DataSource = reports;
        ReportsRepeater.DataBind();
    }
}
