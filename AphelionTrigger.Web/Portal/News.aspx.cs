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
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;
using AjaxControlToolkit;

public partial class Portal_News : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        ATConfiguration config = ATConfiguration.Instance;
        config.InvalidateCache();
    }

    #region NewsPostListDataSource
    protected void NewsPostListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetNewsPostList();
    }

    private NewsPostList GetNewsPostList()
    {
        NewsPostList news;

        if (Cache["NEWS"] == null)
        {
            news = NewsPostList.GetNewsPostList();

            HttpContext.Current.Cache.Insert( "NEWS", news, null, DateTime.Now.AddMinutes( 5 ), Cache.NoSlidingExpiration, CacheItemPriority.BelowNormal, null );
        }
        else
        {
            news = (NewsPostList)Cache["NEWS"];
        }

        return news;
    }
    #endregion
}
