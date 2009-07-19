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

public partial class Communications_Records : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();

        RecieveMessages.RowCommand += new GridViewCommandEventHandler( Messages_RowCommand );
        SentMessages.RowCommand += new GridViewCommandEventHandler( Messages_RowCommand );
        RecieveMessages.RowCreated += new GridViewRowEventHandler( Messages_RowCreated );
        SentMessages.RowCreated += new GridViewRowEventHandler( Messages_RowCreated );

        if (!Page.IsPostBack) BinMessages();
    }

    #region Business Methods
    public string GetMessageImage( bool hasRead, int messageTypeId )
    {
        if (messageTypeId == 2 && !hasRead)
            return "~/Images/Messages/message-guild.gif";
        else if (hasRead)
            return "~/Images/Messages/message-read.gif";
        else if (!hasRead)
            return "~/Images/Messages/message-new.gif";
        else
            return string.Empty;
    }
    #endregion

    #region Messages

    void Messages_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        switch (e.CommandName)
        {
            case "View":
                int messageId = Convert.ToInt32(e.CommandArgument);
                ((AphelionTriggerPage)Page).MESSAGE = new MessageStub( messageId );

                Response.Redirect( "~/Communications/Message.aspx", true );
                break;
        }
    }

    void Messages_RowCreated( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add( "OnMouseOver", "this.style.backgroundColor = 'rgb(25,25,25)';" );
            e.Row.Attributes.Add( "OnMouseOut", "this.style.backgroundColor = 'Transparent';" );
        }
    }

    protected void BinMessages()
    {
        MessageList messages = MessageList.GetMessageList( User.HouseID );
        MessageList recieveMessages = MessageList.NewMessageList();
        MessageList sentMessages = MessageList.NewMessageList();
        foreach (Message m in messages)
        {

            if (m.SenderHouseID != User.HouseID)
                recieveMessages.Add( m );
            else
                sentMessages.Add( m );
        }

        RecieveMessages.DataKeyNames = new string[1] { "ID" };
        RecieveMessages.DataSource = recieveMessages;
        RecieveMessages.DataBind();

        SentMessages.DataKeyNames = new string[1] { "ID" };
        SentMessages.DataSource = sentMessages;
        SentMessages.DataBind();
    }

    #endregion
}
