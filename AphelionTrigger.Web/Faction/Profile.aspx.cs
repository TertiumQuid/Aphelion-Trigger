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

public partial class Faction_Profile : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        RequireHouse();

        FactionLeaderLink.Click += new EventHandler( FactionLeaderLink_Click );
        Vote.Click += new EventHandler( Vote_Click );

        if (!Page.IsPostBack) { BindFaction(); }
    }

    void Vote_Click( object sender, EventArgs e )
    {
        int id = Convert.ToInt32( FactionLeader.SelectedValue );
        House.UpdateFactionLeaderVote( User.HouseID, id );

        // rebind in case the vote tipped the scales
        BindFaction();
    }

    void FactionLeaderLink_Click( object sender, EventArgs e )
    {
        ((AphelionTriggerPage)Page).HOUSE = new HouseStub( Convert.ToInt32( ((LinkButton)sender).CommandArgument ) );
        Response.Redirect( "~/House/Profile.aspx", true );
    }

    #region Faction
    private void BindFaction()
    {
        Faction faction = Faction.GetFaction( User.FactionID );

        FactionLabel.Text = faction.Name;
        DescriptionLabel.Text = faction.Description;
        if (faction.FactionLeaderHouseID > 0)
        {
            FactionLeaderLink.CommandArgument = faction.FactionLeaderHouseID.ToString();
            FactionLeaderLink.Text = faction.FactionLeaderHouse;
        }
        else
        {
            FactionLeaderLink.Text = "Unelected";
            FactionLeaderLink.Enabled = false;
        }

        BindHouses();
    }

    private void BindHouses()
    {
        House house = House.GetHouse( User.HouseID );

        HouseList houses = HouseList.GetHouseList( User.FactionID );
        FactionLeader.ClearSelection();
        FactionLeader.Items.Add( new ListItem( "NO CONTEST", "0" ) );
        foreach (House h in houses)
        {
            ListItem item = new ListItem( h.Name, h.ID.ToString() );
            item.Selected = (h.ID == house.VotedFactionLeaderHouseID);
            FactionLeader.Items.Add( item );
        }
    }
    #endregion
}
