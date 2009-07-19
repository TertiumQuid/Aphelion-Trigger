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
using Csla;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class Guild_Profile : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();

        if (!Page.IsPostBack) { BindGuild(); }
    }

    #region Guild
    private void BindGuild()
    {
        if (User.GuildID == 0) Response.Redirect( "Create.aspx", true );

        Guild guild;

        if (GUILD.Initialized)
        {
            guild = Guild.GetGuild( GUILD.GuildID );
        }
        else
        {
            guild = Guild.GetGuild( User.GuildID );
        }

        GuildLabel.Text = guild.Name;
        DescriptionLabel.Text = guild.Description;

        BindMembers( guild.ID );
        if (guild.ID == User.GuildID) BindInvitations( guild.ID );

        ((AphelionTriggerPage)Page).GUILD = new GuildStub( false );
    }

    private void BindMembers( int guildId )
    {
        HouseList houses = Houses;
        FilteredBindingList<House> members = new FilteredBindingList<House>( houses );
        members.ApplyFilter( "GuildID", guildId );

        Members.DataKeyNames = new string[1] { "ID" };
        Members.DataSource = members;
        Members.DataBind();
    }

    private void BindInvitations( int guildId )
    {
        MessageList messages = MessageList.GetGuildMessageList( guildId );

        InvitationMessages.DataKeyNames = new string[1] { "ID" };
        InvitationMessages.DataSource = messages;
        InvitationMessages.DataBind();

        Invitations.Visible = guildId == User.GuildID && messages.Count > 0;
        InvitationMessages.Visible = guildId == User.GuildID && messages.Count > 0;
    }
    #endregion
}
