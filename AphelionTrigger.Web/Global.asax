<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
    }

    void Application_End( object sender, EventArgs e ) 
    {
    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        HttpContext context = HttpContext.Current;

        Exception exception = context.Server.GetLastError();

        string details =
           "Offending URL: " + context.Request.Url.ToString() + "<br /><br />" +
           "<br />Stack Trace: " + exception.StackTrace + "<br /><br />Inner Stack Trace: " + ( exception.InnerException != null ? exception.InnerException.Message + "<br /><br />" + exception.InnerException.StackTrace : string.Empty );

        AphelionTrigger.Library.Logs.SystemLogCommand.Log(
            AphelionTrigger.Library.Logs.SystemLogType.Error,
            AphelionTrigger.Library.Logs.SystemLogDestination.Database,
            "Aphelion Trigger Web",
            exception.Source,
            exception.Message,
            details );

        //AphelionTrigger.Library.Logs.SystemLogCommand.Log(
        //    AphelionTrigger.Library.Logs.SystemLogType.Error,
        //    AphelionTrigger.Library.Logs.SystemLogDestination.Email,
        //    "Aphelion Trigger Web",
        //    exception.Source,
        //    exception.Message,
        //    details );

        context.ClearError();
        Response.Redirect( "~/Error.aspx" );
    }

    void Session_Start(object sender, EventArgs e)
    {
    }

    void Session_End(object sender, EventArgs e) 
    {
    }

    protected void Application_PreRequestHandlerExecute( Object sender, EventArgs e )
    {
        InsertUserOnlineCacheItem( string.Empty );
    }

    protected void Application_AcquireRequestState( object sender, EventArgs e )
    {
        if (Csla.ApplicationContext.AuthenticationType == "Windows") return;

        AphelionTrigger.Library.Security.ATPrincipal principal;
        try
        {
            principal = (AphelionTrigger.Library.Security.ATPrincipal)HttpContext.Current.Session["CslaPrincipal"];
        }
        catch
        {
            principal = null;
        }

        if (principal == null)
        {
            // didn't get a principal from Session, so
            // set it to an unauthenticted LPrincipal
            AphelionTrigger.Library.Security.ATPrincipal.Logout();
        }
        else
        {
            // use the principal from Session
            Csla.ApplicationContext.User = principal;
        }
    }

    public void InsertUserOnlineCacheItem( string id )
    {
        // if no user id is supplied, try to get the user's id from the Identity object
        if (id.Length == 0) id = ((AphelionTrigger.Library.Security.ATIdentity)Csla.ApplicationContext.User.Identity).ID.ToString();
        
        if (this.GetUserOnlineCacheItem( id ) != "") { return; }
        CacheItemRemovedCallback callback = new CacheItemRemovedCallback( Session_Ended );
        HttpContext.Current.Cache.Insert( "IsOnline" + id, id, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes( 3 ), CacheItemPriority.High, callback );
    }

    public string GetUserOnlineCacheItem( string id )
    {
        string result = "";

        try
        {
            result = (string)HttpContext.Current.Cache["IsOnline" + id];
            if (result == null) { result = ""; }
        }
        catch (Exception) { }
        return result;
    }

    void Session_Ended( string key, object val, CacheItemRemovedReason reason )
    {
        // TODO: log session deaths
    }
       
</script>
