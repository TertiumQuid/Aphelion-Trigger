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
using AjaxControlToolkit;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class House_Advancement : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();
        if (!IsPostBack) Session["CurrentObject"] = null;

        Advancements.RowCreated += new GridViewRowEventHandler( Advancements_RowCreated );
        SaveFreeLink.Click += new EventHandler( SaveFreeLink_Click );

        if (!Page.IsPostBack) BindLevelDetails();
        BindFree();
    }

    #region Business Methods
    protected int GetExperiencePercentage()
    {
        return (UserHouse.Experience / UserHouse.NextLevel.Experience);
    }
    #endregion

    #region Advancements

    void Advancements_RowCreated( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add( "OnMouseOver", "this.style.backgroundColor = 'rgb(35,35,35)';" );
            e.Row.Attributes.Add( "OnMouseOut", "this.style.backgroundColor = 'Transparent';" );
        }
    }

    private void BindLevelDetails()
    {
        double nextLevel = UserHouse.NextLevel.Experience - UserHouse.Level.Experience;
        double experienceGained = nextLevel - ( UserHouse.NextLevel.Experience - UserHouse.Experience );

        double percentLeveled = ( experienceGained / nextLevel ) * 100.00;

        LevelProgressBar.Style["width"] = percentLeveled.ToString() + "%";

        LevelExperienceLabel.Text = "From " + Convert.ToInt32( UserHouse.Level.Experience + 1 ).ToString();
        NextLevelExperienceLabel.Text = "To " + UserHouse.NextLevel.Experience.ToString();

        if ( percentLeveled < 50 )
            OuterProgressPercentLabel.Text = "&nbsp;" + percentLeveled.ToString( "N2" ) + "%";
        else
            InnerProgressPercentLabel.Text = "&nbsp;" + percentLeveled.ToString( "N2" ) + "%";

        LevelLabel.Text = UserHouse.Level.Rank.ToString();
        CurrentExperienceLabel.Text = UserHouse.Experience.ToString();
    }

    #endregion

    #region AdvancementListDataSource
    protected void AdvancementListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetAdvancementList();
    }

    private AdvancementList GetAdvancementList()
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is AdvancementList))
        {
            businessObject = AdvancementList.GetAdvancementList( User.HouseID );
            Session["CurrentObject"] = businessObject;
        }
        return (AdvancementList)businessObject;
    }
    #endregion

    #region Free Placements

    void SaveFreeLink_Click( object sender, EventArgs e )
    {
        // check each row except header
        for (int i = 1; i < FreeTable.Rows.Count; i++)
        {
            // check each cell except point index display
            for (int j = 1; j < FreeTable.Rows[i].Cells.Count; j++)
            {
                foreach (Control ctrl in FreeTable.Rows[i].Cells[j].Controls)
                {
                    // if user checked the cell, update the coresponding house stat
                    if (ctrl is CheckBox && ((CheckBox)ctrl).Checked)
                    {
                        switch (j)
                        {
                            case 1:
                                UpdateFreePlaced( "Intelligence" );
                                break;
                            case 2:
                                UpdateFreePlaced( "Power" );
                                break;
                            case 3:
                                UpdateFreePlaced( "Protection" );
                                break;
                            case 4:
                                UpdateFreePlaced( "Affluence" );
                                break;
                            case 5:
                                UpdateFreePlaced( "Speed" );
                                break;
                        }
                        break;
                    }
                }
            }
        }

        // clear system caches and rebind to display changes
        Session["CurrentObject"] = null;
        RefreshUserHouse();

        Advancements.DataBind();
        BindFree();
    }

    private void UpdateFreePlaced( string statFieldName )
    {
        AdvancementList list = GetAdvancementList();
        foreach (Advancement adv in list)
        {
            if (adv.FreePlaced < adv.Free)
            {
                Advancement.UpdateFreePlaced( User.HouseID, adv.ID, 1, statFieldName );
                adv.FreePlaced++;
                break;
            }
        }
    }

    private void BindFree()
    {
        AdvancementList list = GetAdvancementList();

        // determine how many, if any, free points the user has left.
        int free = 0;
        foreach (Advancement adv in list)
            free += (adv.Free - adv.FreePlaced);

        FreePanel.Visible = (free > 0);

        // do nothing if the user has no free points
        if (free == 0) return;

        Intelligence.Text = UserHouse.Intelligence.ToString();
        Power.Text = UserHouse.Power.ToString();
        Protection.Text = UserHouse.Protection.ToString();
        Affluence.Text = UserHouse.Affluence.ToString();
        Speed.Text = UserHouse.Speed.ToString();
        Contingency.Text = UserHouse.Contingency.ToString();

        // clear any free point rows in order to rebind with new rows - the first two rows are header rows and should be retained
        if ( Page.IsPostBack && FreeTable.Rows.Count > 2 )
        {
            for ( int row = FreeTable.Rows.Count -1; row >= 2; row-- )
                FreeTable.Rows.RemoveAt( row );
        }

        // for each free point, create a table row for managing the point placement
        for (int i = 0; i < free; i++)
        {
            TableRow row = new TableRow();
            TableCell pointCell = new TableCell();
            TableCell intelligenceCell = new TableCell();
            TableCell powerCell = new TableCell();
            TableCell protectionCell = new TableCell();
            TableCell affluenceCell = new TableCell();
            TableCell speedCell = new TableCell();
            TableCell contingencyCell = new TableCell();

            int pointIndex = i + 1;
            pointCell.Text = pointIndex.ToString();

            CheckBox intCheck = new CheckBox();
            intCheck.ID = "IntelligenceCheckBox" + i.ToString();
            AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender intExtend = new MutuallyExclusiveCheckBoxExtender();
            intExtend.ID = "IntelligenceCheckBoxExtender" + i.ToString();
            intExtend.TargetControlID = intCheck.ID;
            intExtend.Key = "Row" + i.ToString() + "CheckBoxes";
            intelligenceCell.Controls.Add( intCheck );
            intelligenceCell.Controls.Add( intExtend );

            CheckBox powCheck = new CheckBox();
            powCheck.ID = "PowerCheckBox" + i.ToString();
            AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender powExtend = new MutuallyExclusiveCheckBoxExtender();
            powExtend.ID = "PowerCheckBoxExtender" + i.ToString();
            powExtend.TargetControlID = powCheck.ID;
            powExtend.Key = "Row" + i.ToString() + "CheckBoxes";
            powerCell.Controls.Add( powCheck );
            powerCell.Controls.Add( powExtend );

            CheckBox proCheck = new CheckBox();
            proCheck.ID = "ProtectionCheckBox" + i.ToString();
            AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender proExtend = new MutuallyExclusiveCheckBoxExtender();
            proExtend.ID = "ProtectionCheckBoxExtender" + i.ToString();
            proExtend.TargetControlID = proCheck.ID;
            proExtend.Key = "Row" + i.ToString() + "CheckBoxes";
            protectionCell.Controls.Add( proCheck );
            protectionCell.Controls.Add( proExtend );

            CheckBox affCheck = new CheckBox();
            affCheck.ID = "AffluenceCheckBox" + i.ToString();
            AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender affExtend = new MutuallyExclusiveCheckBoxExtender();
            affExtend.ID = "AffluenceCheckBoxExtender" + i.ToString();
            affExtend.TargetControlID = affCheck.ID;
            affExtend.Key = "Row" + i.ToString() + "CheckBoxes";
            affluenceCell.Controls.Add( affCheck );
            affluenceCell.Controls.Add( affExtend );

            CheckBox spdCheck = new CheckBox();
            spdCheck.ID = "SpeedCheckBox" + i.ToString();
            AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender spdExtend = new MutuallyExclusiveCheckBoxExtender();
            spdExtend.ID = "SpeedCheckBoxExtender" + i.ToString();
            spdExtend.TargetControlID = spdCheck.ID;
            spdExtend.Key = "Row" + i.ToString() + "CheckBoxes";
            speedCell.Controls.Add( spdCheck );
            speedCell.Controls.Add( spdExtend );

            CheckBox conCheck = new CheckBox();
            conCheck.ID = "ContingencyCheckBox" + i.ToString();
            AjaxControlToolkit.MutuallyExclusiveCheckBoxExtender conExtend = new MutuallyExclusiveCheckBoxExtender();
            conExtend.ID = "ContingencyCheckBoxExtender" + i.ToString();
            conExtend.TargetControlID = conCheck.ID;
            conExtend.Key = "Row" + i.ToString() + "CheckBoxes";
            contingencyCell.Controls.Add( conCheck );
            contingencyCell.Controls.Add( conExtend );

            row.Cells.Add( pointCell );
            row.Cells.Add( intelligenceCell );
            row.Cells.Add( powerCell );
            row.Cells.Add( protectionCell );
            row.Cells.Add( affluenceCell );
            row.Cells.Add( speedCell );
            row.Cells.Add( contingencyCell );

            FreeTable.Rows.Add( row );
        }
    }
    #endregion
}
