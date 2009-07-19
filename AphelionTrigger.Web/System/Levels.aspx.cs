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

public partial class System_Levels : AphelionTriggerPage
{
    private enum Views
    {
        MainView = 0,
        EditView = 1
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAdministration();

        LevelList.InvalidateCache();

        MainGrid.SelectedIndexChanged += new EventHandler( MainGrid_SelectedIndexChanged );
        MainDetails.ModeChanging += new DetailsViewModeEventHandler( MainDetails_ModeChanging );

        LevelError.Visible = false;
    }

    void MainDetails_ModeChanging( object sender, DetailsViewModeEventArgs e )
    {
        MainMultiView.ActiveViewIndex = (int)Views.MainView;
        MainGrid.SelectedIndex = -1;
        MainGrid.DataBind();
    }

    void MainGrid_SelectedIndexChanged( object sender, EventArgs e )
    {
        MainMultiView.ActiveViewIndex = (int)Views.EditView;
        MainDetails.DataBind();
    }

    #region LevelListDataSource
    protected void LevelListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetLevelList();
    }

    private LevelList GetLevelList()
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is LevelList))
        {
            businessObject = LevelList.GetLevelList();
            Session["CurrentObject"] = businessObject;
        }
        return (LevelList)businessObject;
    }
    #endregion

    #region LevelDataSource
    protected void LevelDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        if (MainGrid.SelectedIndex < 0) return;
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        e.BusinessObject = GetLevel( id );

        MainDetails.ChangeMode( DetailsViewMode.Edit );
    }

    protected void LevelDataSource_UpdateObject( object sender, Csla.Web.UpdateObjectArgs e )
    {
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        Level obj = GetLevel( id );
        Csla.Data.DataMapper.Map( e.Values, obj );

        e.RowsAffected = SaveDetails( obj );

        if (e.RowsAffected > 0) MainDetails.ChangeMode( DetailsViewMode.ReadOnly );
    }

    private int SaveDetails( Level item )
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

            LevelError.Visible = true;
            LevelError.Text = message.ToString();
            rowsAffected = 0;
        }
        catch (Csla.DataPortalException ex)
        {
            LevelError.Visible = true;
            LevelError.Text = ex.BusinessException.Message;
            rowsAffected = 0;
        }
        catch (Exception ex)
        {
            LevelError.Visible = true;
            LevelError.Text = ex.Message;
            rowsAffected = 0;
        }
        return rowsAffected;
    }

    private Level GetLevel( int id )
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is Technology))
        {
            businessObject = Level.GetLevel( id );
            Session["CurrentObject"] = businessObject;
        }
        return (Level)businessObject;
    }
    #endregion
}
 