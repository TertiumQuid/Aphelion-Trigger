using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
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

public partial class System_Units : AphelionTriggerPage
{
    private enum Views
    {
        MainView = 0,
        DetailsView = 1,
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAdministration();
        if (!IsPostBack) Session["CurrentObject"] = null;

        EnableTinyMCE();

        MainGrid.SelectedIndexChanged += new EventHandler( MainGrid_SelectedIndexChanged );
        MainDetails.ModeChanging += new DetailsViewModeEventHandler( MainDetails_ModeChanging );

        MainDetails.ItemInserted += new DetailsViewInsertedEventHandler( MainDetails_ItemInserted );

        InsertLink.Click += new EventHandler( InsertLink_Click );

        UnitError.Visible = false;
        InsertLink.Visible = MainMultiView.ActiveViewIndex == 0;
    }

    #region Events
    void MainDetails_ItemInserted( object sender, DetailsViewInsertedEventArgs e )
    {
        // keep in insert mode if there was an error
        e.KeepInInsertMode = UnitError.Visible;
    }

    protected void MainDetails_ModeChanging( object sender, DetailsViewModeEventArgs e )
    {
        // canceling, so switch back to the main grid view
        if (e.CancelingEdit) ShowGrid();
    }

    void InsertLink_Click( object sender, EventArgs e )
    {
        MainMultiView.ActiveViewIndex = (int)Views.DetailsView;
        MainDetails.ChangeMode( DetailsViewMode.Insert );
        InsertLink.Visible = false;
    }

    void MainGrid_SelectedIndexChanged( object sender, EventArgs e )
    {
        // a unit record was selected, so switch to edit view
        MainMultiView.ActiveViewIndex = (int)Views.DetailsView;
        MainDetails.ChangeMode( DetailsViewMode.Edit );
        MainDetails.DataBind();
        InsertLink.Visible = false;
    }
    #endregion

    #region Business Methods
    private void ShowGrid()
    {
        MainMultiView.ActiveViewIndex = (int)Views.MainView;
        MainGrid.SelectedIndex = -1;
        MainGrid.DataBind();
        InsertLink.Visible = true;
    }
    #endregion

    #region UnitDataSource
    protected void UnitDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        if (MainGrid.SelectedIndex < 0) return;
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        e.BusinessObject = GetUnit( id );

        MainDetails.ChangeMode( DetailsViewMode.Edit );
    }

    protected void UnitDataSource_UpdateObject( object sender, Csla.Web.UpdateObjectArgs e )
    {
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        AphelionTrigger.Library.Unit obj = GetUnit( id );
        Csla.Data.DataMapper.Map( e.Values, obj );

        e.RowsAffected = SaveDetails( obj );

        // if there were no errors, then update was successful so switch views
        if (e.RowsAffected > 0) ShowGrid();
    }

    protected void UnitDataSource_InsertObject( object sender, Csla.Web.InsertObjectArgs e )
    {
        AphelionTrigger.Library.Unit obj = AphelionTrigger.Library.Unit.NewUnit();
        Csla.Data.DataMapper.Map( e.Values, obj, new string[] { "ID" } );

        e.RowsAffected = SaveDetails( obj );

        if (e.RowsAffected > 0) ShowGrid();
    }

    private int SaveDetails( AphelionTrigger.Library.Unit item )
    {
        int rowsAffected = 0;
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

            UnitError.Visible = true;
            UnitError.Text = message.ToString();
            rowsAffected = 0;
        }
        catch (Csla.DataPortalException ex)
        {
            UnitError.Visible = true;
            UnitError.Text = ex.BusinessException.Message;
            rowsAffected = 0;
        }
        catch (Exception ex)
        {
            UnitError.Visible = true;
            UnitError.Text = ex.Message;
            rowsAffected = 0;
        }
        return rowsAffected;
    }

    private AphelionTrigger.Library.Unit GetUnit( int id )
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is NewsPost))
        {
            businessObject = AphelionTrigger.Library.Unit.GetUnit( id );
            Session["CurrentObject"] = businessObject;
        }
        return (AphelionTrigger.Library.Unit)businessObject;
    }
    #endregion

    #region UnitListDataSource
    protected void UnitListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetUnitList();
    }

    private UnitList GetUnitList()
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is UnitList))
        {
            businessObject = UnitList.GetUnits();
            Session["CurrentObject"] = businessObject;
        }
        return (UnitList)businessObject;
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

    #region FactionListDataSource
    protected void FactionListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        // we have to violate the strong typing by returning a ListItemCollection because the 
        // ReadOnlyBase type UnitClass prevents us from inserting "empty" records in the list
        e.BusinessObject = GetFactionList();
    }

    private ListItemCollection GetFactionList()
    {
        FactionList list = FactionList.GetFactionList();

        // prepare a collection containing an empty value signifying that no unit class is selected
        ListItemCollection collection = new ListItemCollection();
        foreach (Faction item in list)
            collection.Add( new ListItem( item.Name, item.ID.ToString() ) );

        collection.Insert( 0, new ListItem( "UNSELECTED", "0" ) );

        return collection;
    }
    #endregion
}
