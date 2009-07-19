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

public partial class System_Codex : AphelionTriggerPage
{
    private enum Views
    {
        MainView = 0,
        DetailsView = 1,
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAdministration();
        if ( !IsPostBack ) 
        { 
            Session["CurrentObject"] = null; CodexRecordList.InvalidateCache(); 
        }

        EnableTinyMCE();

        MainGrid.SelectedIndexChanged += new EventHandler( MainGrid_SelectedIndexChanged );
        MainDetails.ModeChanging += new DetailsViewModeEventHandler( MainDetails_ModeChanging );

        MainDetails.ItemInserted += new DetailsViewInsertedEventHandler( MainDetails_ItemInserted );

        InsertLink.Click += new EventHandler( InsertLink_Click );

        CodexRecordError.Visible = false;
        InsertLink.Visible = MainMultiView.ActiveViewIndex == 0;
    }

    #region Events
    void MainDetails_ItemInserted( object sender, DetailsViewInsertedEventArgs e )
    {
        // keep in insert mode if there was an error
        e.KeepInInsertMode = CodexRecordError.Visible;
    }

    protected void MainDetails_ModeChanging( object sender, DetailsViewModeEventArgs e )
    {
        // canceling, so switch back to the main grid view
        if ( e.CancelingEdit ) ShowGrid();
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

    #region CodexRecordListDataSource
    protected void CodexRecordListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetCodexRecordList();
    }

    private CodexRecordList GetCodexRecordList()
    {
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is CodexRecordList ) )
        {
            businessObject = CodexRecordList.GetCodexRecordList();
            Session["CurrentObject"] = businessObject;
        }
        return (CodexRecordList)businessObject;
    }
    #endregion

    #region ParentListDataSource
    protected void ParentListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetParentList();
    }

    private ListItemCollection GetParentList()
    {
        CodexRecordList list;
        object businessObject = Session["CurrentObject"];
        if ( businessObject == null || !( businessObject is CodexRecordList ) )
        {
            businessObject = CodexRecordList.GetCodexRecordList();
            Session["CurrentObject"] = businessObject;
        }
        list = (CodexRecordList)businessObject;

        // prepare a collection containing an empty value signifying that no unit class is selected
        ListItemCollection collection = new ListItemCollection();
        foreach ( CodexRecord item in list )
            collection.Add( new ListItem( item.Title, item.ID.ToString() ) );

        collection.Insert( 0, new ListItem( "***ROOT***", "0" ) );

        return collection;
    }
    #endregion

    #region CodexRecordDataSource
    protected void CodexRecordDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        if ( MainGrid.SelectedIndex < 0 ) return;
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        e.BusinessObject = GetCodexRecord( id );

        MainDetails.ChangeMode( DetailsViewMode.Edit );
    }

    protected void CodexRecordDataSource_UpdateObject( object sender, Csla.Web.UpdateObjectArgs e )
    {
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        CodexRecord obj = GetCodexRecord( id );
        Csla.Data.DataMapper.Map( e.Values, obj );

        e.RowsAffected = SaveDetails( obj );

        // if there were no errors, then update was successful so switch views
        if ( e.RowsAffected > 0 )
        {
            CodexRecordList.InvalidateCache();
            ShowGrid();
        }
    }

    protected void CodexRecordDataSource_InsertObject( object sender, Csla.Web.InsertObjectArgs e )
    {
        CodexRecord obj = CodexRecord.NewCodexRecord();
        Csla.Data.DataMapper.Map( e.Values, obj, new string[] { "ID" } );

        e.RowsAffected = SaveDetails( obj );

        if ( e.RowsAffected > 0 )
        {
            ShowGrid();
            CodexRecordList.InvalidateCache();
        }
    }

    private int SaveDetails( CodexRecord item )
    {
        int rowsAffected;
        try
        {
            Session["CurrentObject"] = item.Save();
            rowsAffected = 1;
        }
        catch ( Csla.Validation.ValidationException ex )
        {
            System.Text.StringBuilder message = new System.Text.StringBuilder();
            message.AppendFormat( "{0}<br/>", ex.Message );
            if ( item.BrokenRulesCollection.Count == 1 )
                message.AppendFormat( "* {0}: {1}", item.BrokenRulesCollection[0].Property, item.BrokenRulesCollection[0].Description );
            else
                foreach ( Csla.Validation.BrokenRule rule in item.BrokenRulesCollection )
                    message.AppendFormat( "* {0}: {1}<br/>", rule.Property, rule.Description );

            CodexRecordError.Visible = true;
            CodexRecordError.Text = message.ToString();
            rowsAffected = 0;
        }
        catch ( Csla.DataPortalException ex )
        {
            CodexRecordError.Visible = true;
            CodexRecordError.Text = ex.BusinessException.Message;
            rowsAffected = 0;
        }
        catch ( Exception ex )
        {
            CodexRecordError.Visible = true;
            CodexRecordError.Text = ex.Message;
            rowsAffected = 0;
        }
        return rowsAffected;
    }

    private CodexRecord GetCodexRecord( int id )
    {
        return CodexRecord.GetCodexRecord( id );
    }
    #endregion
}
