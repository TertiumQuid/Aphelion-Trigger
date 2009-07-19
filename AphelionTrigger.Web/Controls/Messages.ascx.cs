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
using Csla;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class Controls_Messages : System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !( (AphelionTriggerPage)Page ).User.IsAuthenticated )
        {
            this.Visible = false;
            return;
        }

        Messages.RowCommand += new GridViewCommandEventHandler( Messages_RowCommand );

        if (!Page.IsPostBack) RefreshMessages();

        // set the refresh timer interval in seconds
        ATConfiguration config = ATConfiguration.Instance;
        MessageTimer.Interval = config.MessagesRefreshRate * 1000;
    }

    void Messages_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        int messageId = 0;
        switch (e.CommandName)
        {
            case "View":
                messageId = Int32.Parse( e.CommandArgument.ToString() );

                ((AphelionTriggerPage)Page).MESSAGE = new MessageStub( messageId );
                Response.Redirect( "~/Communications/Message.aspx", true );
                break;
            case "Dismiss":
                messageId = Int32.Parse( e.CommandArgument.ToString() );
                int houseId = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;
                int userId = ((ATIdentity)Csla.ApplicationContext.User.Identity).ID;

                Message.UpdateMessageHasViewed( messageId, houseId );
                HttpContext.Current.Cache.Remove( "Messages" + userId.ToString() );
                RefreshMessages();
                break;
        }
    }

    protected void MessageTimer_Tick( object sender, EventArgs e )
    {
        RefreshMessages();
    }
    
    private void RefreshMessages()
    {
        if (!Csla.ApplicationContext.User.Identity.IsAuthenticated) return;

        MessageList messages;

        int userId = ((ATIdentity)Csla.ApplicationContext.User.Identity).ID;

        if (Cache["Messages" + userId.ToString()] == null)
        {
            int id = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;
            messages = MessageList.GetMessageList( id, false );

            HttpContext.Current.Cache.Insert( "Messages" + userId.ToString(), messages, null, DateTime.Now.AddMinutes( 0.25 ), Cache.NoSlidingExpiration, CacheItemPriority.BelowNormal, null );
        }
        else
        {
            messages = (MessageList)Cache["Messages" + userId.ToString()];
        }

        this.Visible = (messages.Count > 0);

        if (messages.Count == 0) return;

        Messages.DataSource = messages;
        Messages.DataBind();
    }

    protected string GetMessageDate( SmartDate date )
    {
        string messageDate = string.Empty;

        if ( date.Date.ToShortDateString() == DateTime.Today.ToShortDateString() )
            messageDate = "Today, " + date.Date.ToShortTimeString();
        else if ( date.Date.ToShortDateString() == DateTime.Today.AddDays( -1 ).ToShortDateString() )
            messageDate = "Yesterday, " + date.Date.ToShortTimeString();
        else
            messageDate = date.Date.ToString( "MMM dd, hh:mm tt" );

        return messageDate;
    }
}
