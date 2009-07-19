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
using AphelionTrigger.Library.Logs;
using AjaxControlToolkit;

public partial class System_SystemLogs : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAdministration();

        if ( !IsPostBack ) Session["CurrentObject"] = null;
    }

    #region SystemLogListDataSource
    protected void SystemLogListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetSystemLogList();
    }

    private SystemLogList GetSystemLogList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is SystemLogList ) )
        {
            businessObject = SystemLogList.GetSystemLogList();
            Session["CurrentObject"] = businessObject;
        }
        return (SystemLogList)businessObject;
    }
    #endregion
}
