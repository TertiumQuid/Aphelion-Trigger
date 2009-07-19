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
using System.Web.Caching;
using Csla;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;
using AjaxControlToolkit;

public partial class Portal_ForumTopic : AphelionTriggerPage
{
    private enum Views
    {
        MainView = 0,
        DetailsView = 1,
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack) Session["CurrentObject"] = null;
        
        BindTopic();

        EnableTinyMCE();

        MainGrid.SelectedIndexChanged += new EventHandler( MainGrid_SelectedIndexChanged );
        MainGrid.RowCommand += new GridViewCommandEventHandler( MainGrid_RowCommand );
        MainDetails.ModeChanging += new DetailsViewModeEventHandler( MainDetails_ModeChanging );

        MainDetails.ItemInserted += new DetailsViewInsertedEventHandler( MainDetails_ItemInserted );

        InsertLink.Click += new EventHandler( InsertLink_Click );

        PostError.Visible = false;
        LinkPanel.Visible = MainMultiView.ActiveViewIndex == 0;
    }

    private void BindTopic()
    {
        int id = 0;
        try { id = Int32.Parse( Request.QueryString["ID"] ); }
        catch { Response.Redirect( "ForumBoards.aspx", true ); }

        ForumTopic topic = ForumTopic.GetForumTopic( id );

        // if an administrative board, check administrator authorization
        if ( topic.BoardTypeID == 3 && !IsAdministrator() ) Response.Redirect( "ForumBoards.aspx", true );

        // hide topic administration from non-administrators
        TopicPanel.Visible = TopicPanel.Enabled = IsAdministrator();

        if (topic.Locked)
        {
            InsertLink.Enabled = false;
            InsertLink.Text = "X LOCKED X";
        }

        TopicTitle.Text = topic.Title;

        Page.Title = "Aphelion Trigger - Forum Topic: " + topic.Title;

        BackLink.NavigateUrl = "~/Portal/ForumBoard.aspx?ID=" + topic.BoardID.ToString();
        BackLink.Text = "Return to " + topic.Board;

        if (!IsPostBack) ForumTopic.UpdateForumTopicViews( id );
    }

    #region Events
    void MainDetails_ItemInserted( object sender, DetailsViewInsertedEventArgs e )
    {
        // keep in insert mode if there was an error
        e.KeepInInsertMode = PostError.Visible;
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
        LinkPanel.Visible = false;
    }

    void MainGrid_SelectedIndexChanged( object sender, EventArgs e )
    {
        // a unit record was selected, so switch to edit view
        MainMultiView.ActiveViewIndex = (int)Views.DetailsView;
        MainDetails.ChangeMode( DetailsViewMode.Edit );
        MainDetails.DataBind();
        LinkPanel.Visible = false;
    }

    void MainGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        // first, we need to set the selected index on the grid
        for (int i = 0; i < MainGrid.DataKeys.Count; i++)
        {
            if (MainGrid.DataKeys[i].Value.ToString() == e.CommandArgument.ToString())
            {
                MainGrid.SelectedIndex = i;
                MainGrid.DataBind();
            }
        }

        // a unit record was selected, so switch to edit view
        MainMultiView.ActiveViewIndex = (int)Views.DetailsView;
        MainDetails.ChangeMode( DetailsViewMode.Edit );
        MainDetails.DataBind();
        LinkPanel.Visible = false;
    }
    #endregion

    #region Business Methods
    private void ShowGrid()
    {
        MainMultiView.ActiveViewIndex = (int)Views.MainView;
        MainGrid.SelectedIndex = -1;
        MainGrid.DataBind();
        LinkPanel.Visible = true;
    }

    protected bool CanEdit( int id )
    {
        return id != 0 && id == User.ID;
    }

    protected string GetPostDate( SmartDate date )
    {
        string postDate = string.Empty;

        if ( date.Date.ToShortDateString() == DateTime.Today.ToShortDateString() )
            postDate = "Today, " + date.Date.ToShortTimeString();
        else if ( date.Date.ToShortDateString() == DateTime.Today.AddDays( -1 ).ToShortDateString() )
            postDate = "Yesterday, " + date.Date.ToShortTimeString();
        else
            postDate = date.Date.ToString( "f" );

        return postDate;
    }

    protected bool IsAdministrator()
    {
        return Csla.ApplicationContext.User.IsInRole( "Administrator" );
    }
    #endregion

    #region ForumPostListDataSource
    protected void ForumPostListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetForumPostList();
    }

    private ForumPostList GetForumPostList()
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is ForumPostList))
        {
            int id = 0;
            try { id = Int32.Parse( Request.QueryString["ID"] ); }
            catch { Response.Redirect( "ForumBoards.aspx" ); }

            businessObject = ForumPostList.GetForumPostList( id );
            Session["CurrentObject"] = businessObject;
        }
        return (ForumPostList)businessObject;
    }
    #endregion

    #region ForumPostDataSource
    protected void ForumPostDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        if (MainGrid.SelectedIndex < 0) return;
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        e.BusinessObject = GetForumPost( id );

        MainDetails.ChangeMode( DetailsViewMode.Edit );
    }

    protected void ForumPostDataSource_UpdateObject( object sender, Csla.Web.UpdateObjectArgs e )
    {
        int id = Convert.ToInt32( MainGrid.SelectedDataKey.Value );
        ForumPost obj = GetForumPost( id );
        Csla.Data.DataMapper.Map( e.Values, obj );

        e.RowsAffected = SaveDetails( obj );

        // if there were no errors, then update was successful so switch views
        if (e.RowsAffected > 0) ShowGrid();
    }

    protected void ForumPostDataSource_InsertObject( object sender, Csla.Web.InsertObjectArgs e )
    {
        ForumPost obj = ForumPost.NewForumPost();
        Csla.Data.DataMapper.Map( e.Values, obj, new string[] { "ID" } );

        int topicId = 0;
        try { topicId = Int32.Parse( Request.QueryString["ID"] ); }
        catch { Response.Redirect( "ForumBoards.aspx" ); }

        obj.TopicID = topicId;
        obj.UserID = User.ID; 

        e.RowsAffected = SaveDetails( obj );

        if (e.RowsAffected > 0)
        {
            MainMultiView.DataBind();
            ShowGrid();

            // new post was created so invalidate cached forum data
            ForumBoardList.InvalidateCache();
        }
    }

    private int SaveDetails( ForumPost item )
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

            PostError.Visible = true;
            PostError.Text = message.ToString();
            rowsAffected = 0;
        }
        catch (Csla.DataPortalException ex)
        {
            PostError.Visible = true;
            PostError.Text = ex.BusinessException.Message;
            rowsAffected = 0;
        }
        catch (Exception ex)
        {
            PostError.Visible = true;
            PostError.Text = ex.Message;
            rowsAffected = 0;
        }
        return rowsAffected;
    }

    private ForumPost GetForumPost( int id )
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is ForumPost))
        {
            businessObject = ForumPost.GetForumPost( id );
            Session["CurrentObject"] = businessObject;
        }
        return (ForumPost)businessObject;
    }
    #endregion

    #region ForumTopicDataSource
    protected void ForumTopicDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        int id = 0;
        try { id = Int32.Parse( Request.QueryString["ID"] ); }
        catch { Response.Redirect( "ForumBoards.aspx" ); }

        e.BusinessObject = ForumTopic.GetForumTopic( id );
    }

    protected void ForumTopicDataSource_UpdateObject( object sender, Csla.Web.UpdateObjectArgs e )
    {
        int id = 0;
        try { id = Int32.Parse( Request.QueryString["ID"] ); }
        catch { Response.Redirect( "ForumBoards.aspx" ); }

        ForumTopic obj = ForumTopic.GetForumTopic( id );
        Csla.Data.DataMapper.Map( e.Values, obj );

        e.RowsAffected = SaveTopic( obj );
    }

    private int SaveTopic( ForumTopic item )
    {
        int rowsAffected = 0;
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

            PostError.Visible = true;
            PostError.Text = message.ToString();
            rowsAffected = 0;
        }
        catch ( Csla.DataPortalException ex )
        {
            PostError.Visible = true;
            PostError.Text = ex.BusinessException.Message;
            rowsAffected = 0;
        }
        catch ( Exception ex )
        {
            PostError.Visible = true;
            PostError.Text = ex.Message;
            rowsAffected = 0;
        }
        return rowsAffected;
    }
    #endregion
}
