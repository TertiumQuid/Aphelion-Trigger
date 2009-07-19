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

public partial class Guild_Invite : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();

        // Prevent non-guild members from accessing the page
        if (!HasGuild()) Response.Redirect( "Create.aspx", true );

        SendMessage.Click += new EventHandler( SendMessage_Click );

        // Initialize form text
        ATIdentity identity = (ATIdentity)Csla.ApplicationContext.User.Identity;
        Subject.Text = identity.Guild + " Invitation";
        Body.Text = "House " + identity.House + " invites you to join " + identity.Guild + ". ";
        Body.Text += "Accept, and you will become powerful. Refuse, and you will be crushed with our enemies.";

        EnableTinyMCE();
    }

    #region Business Methods
    protected bool HasGuild()
    {
        return ((ATIdentity)Csla.ApplicationContext.User.Identity).GuildID > 0;
    }
    #endregion

    #region Message

    protected void SendMessage_Click( object sender, EventArgs e )
    {
        Message message = Message.NewMessage();
        message.MessageTypeID = 2;
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
            Message.AddRecipient( message.ID, recipientHouseId, true );
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

        Response.Redirect( "Profile.aspx", true );
    }
    #endregion
}
