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
using AphelionTrigger;
using AphelionTrigger.Library;

public partial class Codex_Index : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        CodexMenu.SelectedNodeChanged += new EventHandler( CodexMenu_SelectedNodeChanged );

        if ( !Page.IsPostBack ) { PopulateRootNodes(); }
    }

    void CodexMenu_SelectedNodeChanged( object sender, EventArgs e )
    {
        MainDetails.DataBind();
    }

    /// <summary>
    /// Recursively adds nodes to the treeview menu starting at a given set of peers of a parent node.
    /// </summary>
    /// <param name="nodes">The root treenode collection to which to add child cnodes</param>
    /// <param name="parentCodexRecordId">The parent node record's ID, used to fetch the children</param>
    private void BuildCodexMenu( TreeNodeCollection nodes, int parentCodexRecordId )
    {
        CodexRecordList list = parentCodexRecordId > 0 ? CodexRecordList.GetCodexRecordList( parentCodexRecordId ) : CodexRecordList.GetCodexRecordList();

        //no child nodes (parent will always be included), exit function
        if ( list.Count < 2 ) return;

        foreach ( CodexRecord record in list )
        {
            if ( record.ID == parentCodexRecordId || ( parentCodexRecordId == 0 && record.ParentCodexRecordID != 0 ) ) continue;

            TreeNode node = new TreeNode( record.Title, record.ID.ToString() );

            // automatically select the first root node
            if ( record.ParentCodexRecordID == 0 && CodexMenu.SelectedNode == null ) node.Select();

            nodes.Add( node );
            node.ToggleExpandState();

            BuildCodexMenu( node.ChildNodes, record.ID );
        }
    }

    #region CodexRecordListDataSource
    protected void PopulateRootNodes()
    {
        CodexRecordList.InvalidateCache();
        BuildCodexMenu( CodexMenu.Nodes, 0 );
    }
    #endregion

    #region CodexRecordDataSource
    protected void CodexRecordDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        if ( CodexMenu.SelectedValue.Length < 1 ) return;
        int id = Convert.ToInt32( CodexMenu.SelectedValue );
        e.BusinessObject = GetCodexRecord( id );

        MainDetails.ChangeMode( DetailsViewMode.Edit );
    }

    private CodexRecord GetCodexRecord( int id )
    {
        return CodexRecord.GetCodexRecord( id );
    }
    #endregion
}
