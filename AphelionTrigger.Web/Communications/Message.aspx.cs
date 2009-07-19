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
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class Communications_Message : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();

        Reply.Click += new EventHandler( Reply_Click );
        RefuseGuild.Click += new EventHandler( RefuseGuild_Click );
        AcceptGuild.Click += new EventHandler( AcceptGuild_Click );

        if (!Page.IsPostBack)
        {
            BindMessage();
        }
    }


    void Reply_Click( object sender, EventArgs e )
    {
        ((AphelionTriggerPage)Page).MESSAGE = new MessageStub( Msg.ThreadID, Msg.SenderHouseID, Msg.SenderHouse, (Msg.Subject.StartsWith( "RE:" ) ? Msg.Subject : "RE: " + Msg.Subject) );
        Response.Redirect( "Send.aspx", true );
    }

    private void CheckHasRead( Message message )
    {
        // If user hasn't read the message, update it to be read/viewed
        if (!message.HasRead)
        {
            int houseId = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;
            Message.UpdateMessageHasRead( message.ID, houseId );
        }
    }

    #region Guilds
    void AcceptGuild_Click( object sender, EventArgs e )
    {
        int houseId = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;
        House senderHouse = House.GetHouse( Msg.SenderHouseID );

        Guild.AddGuildMember( senderHouse.GuildID, houseId );

        // update identity to reflect guild membership
        ATIdentity identity = (ATIdentity)Csla.ApplicationContext.User.Identity;
        ATPrincipal.Login( identity.Name, identity.Password );
        HttpContext.Current.Session["CslaPrincipal"] = Csla.ApplicationContext.User;

        Message.UpdateMessageGuildInvitationStatus( Msg.ID, houseId, 2 );

        // add guild report.
        AphelionTrigger.Library.Report report = Report.NewReport();
        report.FactionID = identity.FactionID;
        report.GuildID = identity.GuildID;
        report.HouseID = identity.HouseID;
        report.Message = "House " + identity.House + " joined the guild: " + identity.Guild + ".";
        report.ReportLevelID = 2 + House.GetSecrecyBonus( identity.Intelligence );
        report.Save();

        HousesClear();
        Response.Redirect( "~/Guild/Profile.aspx", true );
    }

    void RefuseGuild_Click( object sender, EventArgs e )
    {
        int houseId = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;
        Message.UpdateMessageGuildInvitationStatus( Msg.ID, houseId, 3 );
        Response.Redirect( "Records.aspx", true );
    }
    #endregion

    #region Message
    private void BindMessage()
    {
        // Redirect if no message was initialized for viewing
        if (!((AphelionTriggerPage)Page).MESSAGE.Initialized) Response.Redirect( "Records.aspx", true );

        int messageId = ((AphelionTriggerPage)Page).MESSAGE.MessageID;
        int houseId = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;

        // if no message, null data error is thrown, and user gets redirected
        try
        {
            Message message = Message.GetMessage( messageId, houseId );

            MessageList thread = MessageList.NewMessageList();

            if (message.ThreadCount > 1)
            {
                thread = MessageList.GetMessageList( houseId, message.ThreadID );
            }
            else
            {
                thread.Add( message );
            }

            Thread.DataSource = thread;
            Thread.DataBind();

            ViewState.Add( "_Msg", message );

            if (message.SenderHouseID == ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID)
            {
                Reply.Visible = false;
            }
            // if this is a guild message, display appropriate buttons
            else if (message.IsGuildMessage)
            {
                if (((ATIdentity)Csla.ApplicationContext.User.Identity).HasGuild)
                {
                    HasGuildLabel.Text = "<em>You are a member of the guild <strong>" + ((ATIdentity)Csla.ApplicationContext.User.Identity).Guild + "</strong></em>";
                    HasGuildLabel.Visible = true;
                }
                else
                {
                    AcceptGuild.Visible = true;
                    RefuseGuild.Visible = true;
                }
                Reply.Visible = false;
            }
            else
            {
                // guild messages only get checked on refusal or acceptance
                CheckHasRead( message );
            }

        }
        catch { Response.Redirect( "Records.aspx", true ); }
    }

    public Message Msg
    {
        get
        {
            object msg = ViewState["_Msg"];
            return (Message)msg;
        }
        set { ViewState.Add( "_Msg", value ); }
    }
    #endregion
}
