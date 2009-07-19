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

public partial class Controls_ErrorLabel : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
    }

    public string Legend
    {
        get { return ErrorLegend.Text; }
        set { ErrorLegend.Text = value; }
    }

    public string Text
    {
        get { return ErrorText.Text; }
        set { ErrorText.Text = value; }
    }
}
