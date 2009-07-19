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

public partial class Portal_Forum_Boards : AphelionTriggerPage
{
    // In order to track the last bound category for the purpose of filtering the forum board lists, the value must be stored somehow
    int lastBoundCategoryId = 0;

    protected void Page_Load( object sender, EventArgs e )
    {
        CategoryRepeater.ItemCreated += new RepeaterItemEventHandler( CategoryRepeater_ItemCreated );
    }

    void CategoryRepeater_ItemCreated( object sender, RepeaterItemEventArgs e )
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            lastBoundCategoryId = ((ForumCategory)e.Item.DataItem).ID;
        }
    }

    #region Business Methods
    protected bool IsAdministrator()
    {
        return Csla.ApplicationContext.User.IsInRole( "Administrator" );
    }
    #endregion

    #region ForumCategoryListDataSource
    protected void ForumCategoryListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetForumCategoryList();
    }

    private ForumCategoryList GetForumCategoryList()
    {
        return ForumCategoryList.GetForumCategoryList();
    }
    #endregion

    #region ForumBoardListDataSource
    protected void ForumBoardListDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetForumBoardList();
    }

    private ForumBoardList GetForumBoardList()
    {
        ForumBoardList list = ForumBoardList.NewForumBoardList();
        ForumBoardList boards = ForumBoardList.GetForumBoardList( lastBoundCategoryId );

        foreach (ForumBoard board in boards)
        {
            switch (board.BoardTypeID)
            {
                case 1:
                    list.Add( board );
                    break;
                case 2:
                    list.Add( board );
                    break;
                case 3:
                    if (IsAdministrator()) list.Add( board );
                    break;
            }
        }

        return list;
    }
    #endregion
}
