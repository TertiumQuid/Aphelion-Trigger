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
using AjaxControlToolkit;

public partial class Communications_Send : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();

        EnableTinyMCE();

        if (((AphelionTriggerPage)Page).MESSAGE.Initialized)
        {
            Recipient.Text = ((AphelionTriggerPage)Page).MESSAGE.RecipientHouse;
            Subject.Text = ((AphelionTriggerPage)Page).MESSAGE.Subject;

            // clear message stub from cache now that the message has been loaded
            ((AphelionTriggerPage)Page).MESSAGE = new MessageStub( false );
        }

        SendMessage.Click += new EventHandler( SendMessage_Click );
    }

    #region Message
    protected void SendMessage_Click( object sender, EventArgs e )
    {
        Message message = Message.NewMessage();
        message.MessageTypeID = 1;
        message.Subject = Subject.Text;
        message.Body = Body.Text;
        message.SenderHouseID = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;

        // if redirected here from a previous page, if in response
        // to a message, then see if there's a thread to continue
        message.ThreadID = ((AphelionTriggerPage)Page).MESSAGE.Initialized ? ((AphelionTriggerPage)Page).MESSAGE.ThreadID : 0;

        int recipientHouseId = 0;

        // initiate error message
        System.Text.StringBuilder text = new System.Text.StringBuilder();
        text.AppendFormat( "{0}<br/><br />", "Could not send message:" );

        try
        {
            // use entered house name to retrieve ID from cached house list
            bool hasRecipient = false;
            foreach (House h in Houses)
            {
                if (h.Name.ToLower() == Recipient.Text.ToLower())
                {
                    hasRecipient = true;
                    recipientHouseId = h.ID;
                    break;
                }
            }

            // validate recipient before saving message
            if (!hasRecipient)
            {
                text.AppendFormat( "* {0}: {1}<br/>", "To", "Recipient not found" );
                throw new Csla.Validation.ValidationException();
            }

            // cannot send a message to yourself
            if (recipientHouseId == message.SenderHouseID)
            {
                text.AppendFormat( "* {0}: {1}<br/>", "To", "Cannot message yourself" );
                throw new Csla.Validation.ValidationException();
            }

            message.Save();
            Message.AddRecipient( message.ID, recipientHouseId, false );
        }
        catch (Csla.Validation.ValidationException ex)
        {

            if (message.BrokenRulesCollection.Count == 1)
            {
                text.AppendFormat( "* {0}: {1}", message.BrokenRulesCollection[0].Property, message.BrokenRulesCollection[0].Description );
            }
            else
            {
                foreach (Csla.Validation.BrokenRule rule in message.BrokenRulesCollection)
                    text.AppendFormat( "* {0}: {1}<br/>", rule.Property, rule.Description );
            }

            MessageError.Text = text.ToString();
            MessageError.Visible = true;
            return;
        }

        Recipient.Text = string.Empty;
        Subject.Text = string.Empty;
        Body.Text = string.Empty;

        MessageError.Visible = false;

        Response.Redirect( "Records.aspx", true );
    }
    #endregion
}