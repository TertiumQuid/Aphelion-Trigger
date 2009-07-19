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
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class Controls_Attacks : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if (!((AphelionTriggerPage)Page).User.IsAuthenticated)
        {
            this.Visible = false;
            return;
        }

        if (!Page.IsPostBack) RefreshAttacks();

        // set the refresh timer interval in seconds
        ATConfiguration config = ATConfiguration.Instance;
        AttackTimer.Interval = config.AttacksRefreshRate * 1000;

    }

    protected void AttackTimer_Tick( object sender, EventArgs e )
    {
        RefreshAttacks();
    }

    private void RefreshAttacks()
    {
        if (!Csla.ApplicationContext.User.Identity.IsAuthenticated) return;

        AttackList attacks;

        int userId = ((AphelionTriggerPage)Page).User.ID;

        if (Cache["Attacks" + userId.ToString()] == null)
        {
            int id = ((AphelionTriggerPage)Page).User.HouseID;
            attacks = AttackList.GetAttackList( id, 3, 2 );

            HttpContext.Current.Cache.Insert( "Attacks" + userId.ToString(), attacks, null, DateTime.Now.AddMinutes( 0.25 ), Cache.NoSlidingExpiration, CacheItemPriority.BelowNormal, null );
        }
        else
        {
            attacks = Cache["Attacks" + userId.ToString()] as AttackList;
        }

        this.Visible = (attacks.Count > 0);

        if (attacks.Count == 0) return;

        Attacks.DataSource = attacks;
        Attacks.DataBind();
    }
}
