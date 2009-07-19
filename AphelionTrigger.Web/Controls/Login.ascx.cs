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
using AphelionTrigger.Library.Security;

public partial class Controls_Login : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        SubmitLogin.Click += new EventHandler( SubmitLogin_Click );

        LoginError.Visible = false;
        LoginPanel.Visible = !Csla.ApplicationContext.User.Identity.IsAuthenticated;

        // check if user prefers automatic login
        if ( !IsPostBack && !Csla.ApplicationContext.User.Identity.IsAuthenticated )
        {
            if ( Request.Cookies["AphelionTriggerLogin"] != null )
            {
                HttpCookie cookie = Request.Cookies.Get( "AphelionTriggerLogin" );

                string username = cookie.Values["Username"].ToString();
                string password = cookie.Values["Password"].ToString();

                if ( ATPrincipal.Login( username, password ) )
                {
                    HttpContext.Current.Session["CslaPrincipal"] = Csla.ApplicationContext.User;
                    Response.Redirect( "~/House/Profile.aspx", true );
                }
            }
        }
    }

    void SubmitLogin_Click( object sender, EventArgs e )
    {
        string username = Username.Text;
        string password = Password.Text;

        bool isValid = ATPrincipal.Login( username, password );
        HttpContext.Current.Session["CslaPrincipal"] = Csla.ApplicationContext.User;

        if (!isValid)
        {
            LoginError.Visible = true;
            return;
        }

        // store user preference for automatic login
        if ( RememberMe.Checked )
        {
            HttpCookie cookie = new HttpCookie( "AphelionTriggerLogin" );
            cookie.Expires = DateTime.Now.AddDays( 2 );

            Response.Cookies.Remove( "AphelionTriggerLogin" );
            Response.Cookies.Add( cookie );

            cookie.Values.Add( "Username", username );
            cookie.Values.Add( "Password", password );
        }

        Response.Redirect( "~/House/Profile.aspx", true );
    }
}
