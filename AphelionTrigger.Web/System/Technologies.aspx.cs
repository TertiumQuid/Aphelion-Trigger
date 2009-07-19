using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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

public partial class System_Technologies : AphelionTriggerPage
{
    private enum Views
    {
        MainView = 0,
        EditView = 1
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        TechnologyTypeList.InvalidateCache();
        
        RequireAdministration();

        if (!IsPostBack) Session["CurrentObject"] = null;

        EnableTinyMCE();

        MainGrid.SelectedIndexChanged += new EventHandler( MainGrid_SelectedIndexChanged );
        MainDetails.ModeChanging += new DetailsViewModeEventHandler( MainDetails_ModeChanging );

        TechnologyError.Visible = false;
    }

    #region Events
    protected void MainDetails_ModeChanging( object sender, DetailsViewModeEventArgs e )
    {
        // canceling, so switch back to the main grid view
        if (e.CancelingEdit) ShowGrid();
    }

    void MainGrid_SelectedIndexChanged( object sender, EventArgs e )
    {
        // a unit record was selected, so switch to edit view
        MainMultiView.ActiveViewIndex = (int)Views.EditView;
        MainDetails.ChangeMode( DetailsViewMode.Edit );
        MainDetails.DataBind();
    }
    #endregion

    #region Business Methods
    private void ShowGrid()
    {
        MainMultiView.ActiveViewIndex = (int)Views.MainView;
        MainGrid.SelectedIndex = -1;
        MainGrid.DataBind();
    }
    #endregion

    #region TechnologyDataSource
    protected void TechnologyDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        if (MainGrid.SelectedIndex < 0) return;
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        e.BusinessObject = GetTechnology( id );

        MainDetails.ChangeMode( DetailsViewMode.Edit );
    }

    protected void TechnologyDataSource_UpdateObject( object sender, Csla.Web.UpdateObjectArgs e )
    {
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        Technology obj = GetTechnology( id );
        Csla.Data.DataMapper.Map( e.Values, obj );

        e.RowsAffected = SaveDetails( obj );

        // if there were no errors, then update was successful so switch views
        if (e.RowsAffected > 0) ShowGrid();
    }

    private int SaveDetails( Technology item )
    {
        int rowsAffected;
        try
        {
            Session["CurrentObject"] = item.Save();
            rowsAffected = 1;
        }
        catch (Csla.Validation.ValidationException ex)
        {
            System.Text.StringBuilder message = new System.Text.StringBuilder();
            message.AppendFormat( "{0}<br/>", ex.Message );
            if (item.BrokenRulesCollection.Count == 1)
                message.AppendFormat( "* {0}: {1}", item.BrokenRulesCollection[0].Property, item.BrokenRulesCollection[0].Description );
            else
                foreach (Csla.Validation.BrokenRule rule in item.BrokenRulesCollection)
                    message.AppendFormat( "* {0}: {1}<br/>", rule.Property, rule.Description );

            TechnologyError.Visible = true;
            TechnologyError.Text = message.ToString();
            rowsAffected = 0;
        }
        catch (Csla.DataPortalException ex)
        {
            TechnologyError.Visible = true;
            TechnologyError.Text = ex.BusinessException.Message;
            rowsAffected = 0;
        }
        catch (Exception ex)
        {
            TechnologyError.Visible = true;
            TechnologyError.Text = ex.Message;
            rowsAffected = 0;
        }
        return rowsAffected;
    }

    private Technology GetTechnology( int id )
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is Technology))
        {
            businessObject = Technology.GetTechnology( id );
            Session["CurrentObject"] = businessObject;
        }
        return (Technology)businessObject;
    }
    #endregion

    #region TechnologyListDataSource
    protected void TechnologyListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetTechnologyList();
    }

    private TechnologyList GetTechnologyList()
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is TechnologyList))
        {
            businessObject = TechnologyList.GetTechnologies( 0 );
            Session["CurrentObject"] = businessObject;
        }
        return (TechnologyList)businessObject;
    }
    #endregion

    #region TechnologyTypeListDataSource
    protected void TechnologyTypeListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = TechnologyTypeList.GetTechnologyTypeList();
    }
    #endregion

    #region UnitClassListDataSource
    protected void UnitClassListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        // we have to violate the strong typing by returning a ListItemCollection because the 
        // ReadOnlyBase type UnitClass prevents us from inserting "empty" records in the list
        e.BusinessObject = GetUnitClassList();
    }

    private ListItemCollection GetUnitClassList()
    {
        UnitClassList list = UnitClassList.GetUnitClassList();
        
        // prepare a collection containing an empty value signifying that no unit class is selected
        ListItemCollection collection = new ListItemCollection();
        foreach (UnitClass item in list)
            collection.Add( new ListItem( item.Name, item.ID.ToString() ) );

        collection.Insert( 0, new ListItem( "DOES NOT APPLY", "0" ) );

        return collection;
    }
    #endregion

    #region UnitListDataSource
    protected void UnitListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetUnitList();
    }

    private ListItemCollection GetUnitList()
    {
        UnitList list = UnitList.GetUnits();

        // prepare a collection containing an empty value signifying that no unit class is selected
        ListItemCollection collection = new ListItemCollection();
        foreach (AphelionTrigger.Library.Unit unit in list)
            collection.Add( new ListItem( unit.Name, unit.ID.ToString() ) );

        collection.Insert( 0, new ListItem( "DOES NOT APPLY", "0" ) );

        return collection;
    }
    #endregion
}
