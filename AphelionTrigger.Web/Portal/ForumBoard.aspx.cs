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

public partial class Portal_ForumBoard : AphelionTriggerPage
{
    private enum Views
    {
        MainView = 0,
        DetailsView = 1,
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        BindForumBoard();

        // locked, sticky, etc. options are only available to administrators
        TopicAdminPanel.Visible = IsAdministrator();

        EnableTinyMCE();

        MainDetails.ModeChanging += new DetailsViewModeEventHandler( MainDetails_ModeChanging );

        MainDetails.ItemInserted += new DetailsViewInsertedEventHandler( MainDetails_ItemInserted );

        InsertLink.Click += new EventHandler( InsertLink_Click );

        PostError.Visible = false;
        LinkPanel.Visible = MainMultiView.ActiveViewIndex == 0;
    }

    private void BindForumBoard()
    {
        int id = 0;
        try { id = Int32.Parse( Request.QueryString["ID"] ); }
        catch { Response.Redirect( "ForumBoards.aspx" ); }

        ForumBoard board = ForumBoard.GetForumBoard( id );

        switch (board.BoardTypeID)
        {
            // if an administrative board, check administrator authorization
            case 3:
                if ( !IsAdministrator() ) Response.Redirect( "ForumBoards.aspx", true );
                break;
        }

        ForumTitle.Text = board.Category + ": " + board.Name;

        Page.Title = "Aphelion Trigger - Forum Board: " + board.Name;
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
    #endregion

    #region Business Methods
    private void ShowGrid()
    {
        MainMultiView.ActiveViewIndex = (int)Views.MainView;
        LinkPanel.Visible = true;
    }

    protected string GetTopicImage( bool sticky, bool locked )
    {
        if (!sticky && !locked)
            return "~/Images/Forum/topic.gif";
        else if (sticky && !locked)
            return "~/Images/Forum/topic_sticky.gif";
        else if (locked && !sticky)
            return "~/Images/Forum/topic_lock.gif";
        else if (locked && sticky)
            return "~/Images/Forum/topic_sticky_lock.gif";
        else
            return string.Empty;
    }

    protected string GetPostDate( SmartDate date )
    {
        string postDate = string.Empty;

        if ( date.Date.ToShortDateString() == DateTime.Today.ToShortDateString() )
            postDate = "<strong>Today</strong>, " + date.Date.ToShortTimeString();
        else if ( date.Date.ToShortDateString() == DateTime.Today.AddDays( -1 ).ToShortDateString() )
            postDate = "<strong>Yesterday</strong>, " + date.Date.ToShortTimeString();
        else
            postDate = date.Date.ToString( "f" );

        return postDate;
    }

    protected bool IsAdministrator()
    {
        return Csla.ApplicationContext.User.IsInRole( "Administrator" );
    }
    #endregion

    #region ForumTopicListDataSource
    protected void ForumTopicListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetForumTopicList();
    }

    private ForumTopicList GetForumTopicList()
    {
        int id = 0;
        try { id = Int32.Parse( Request.QueryString["ID"] ); }
        catch { Response.Redirect( "ForumBoards.aspx" ); }

        return ForumTopicList.GetForumTopicList( id );
    }
    #endregion

    #region ForumPostDataSource
    protected void ForumPostDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        return;
    }

    protected void ForumPostDataSource_InsertObject( object sender, Csla.Web.InsertObjectArgs e )
    {
        ForumPost obj = ForumPost.NewForumPost();
        Csla.Data.DataMapper.Map( e.Values, obj, new string[] { "ID" } );

        e.RowsAffected = SaveDetails( obj );

        if (e.RowsAffected > 0)
        {
            MainMultiView.DataBind();
            ShowGrid();

            // new topic was created so invalidate cached forum data
            ForumBoardList.InvalidateCache();
        }
    }

    private int SaveDetails( ForumPost item )
    {
        int rowsAffected = 0;

        // before adding the post, we have to add a new topic
        ForumTopic topic = ForumTopic.NewForumTopic();
        try
        {
            int boardId = 0;
            try { boardId = Int32.Parse( Request.QueryString["ID"] ); } catch { Response.Redirect( "ForumBoards.aspx"); }

            topic.BoardID = boardId;
            topic.Title = item.Subject;
            topic.Sticky = Sticky.Checked;
            topic.Locked = Locked.Checked;

            if (item.IsValid) topic.Save();

            // now we can assign the new topic's id to the post
            item.TopicID = topic.ID;
            
            item.UserID = User.ID;
            item.Save();
            rowsAffected = 1;
        }
        catch (Csla.Validation.ValidationException ex)
        {
            System.Text.StringBuilder message = new System.Text.StringBuilder();
            message.AppendFormat( "{0}<br/>", ex.Message );
            if (item.BrokenRulesCollection.Count == 1)
                message.AppendFormat( "* {0}: {1}", item.BrokenRulesCollection[0].Property, item.BrokenRulesCollection[0].Description );
            else if (item.BrokenRulesCollection.Count > 1)
                foreach (Csla.Validation.BrokenRule rule in item.BrokenRulesCollection)
                    message.AppendFormat( "* {0}: {1}<br/>", rule.Property, rule.Description );

            if (topic.BrokenRulesCollection.Count == 1)
                message.AppendFormat( "* {0}: {1}", topic.BrokenRulesCollection[0].Property, topic.BrokenRulesCollection[0].Description );
            else if (topic.BrokenRulesCollection.Count > 1)
                foreach (Csla.Validation.BrokenRule rule in topic.BrokenRulesCollection)
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
    #endregion
}
