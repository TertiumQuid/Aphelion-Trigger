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
using AphelionTrigger.Security;
using AphelionTrigger.Library.Security;

public partial class Portal_UserProfile : AphelionTriggerPage
{
    protected void Page_Load( object sender, EventArgs e )
    {
        RequireAuthentication();

        if ( !IsPostBack ) Session["CurrentObject"] = null;

        TitleLabel.Text = "Profile for " + User.Name;

        BusinessError.Visible = false;
    }

    #region UserDataSource
    protected void UserDataSource_SelectObject( object sender, Csla.Web.SelectObjectArgs e )
    {
        e.BusinessObject = GetUser( User.ID );

        MainDetails.ChangeMode( DetailsViewMode.Edit );
    }

    protected void UserDataSource_UpdateObject( object sender, Csla.Web.UpdateObjectArgs e )
    {
        User obj = GetUser( User.ID );
        Csla.Data.DataMapper.Map( e.Values, obj );

        e.RowsAffected = SaveDetails( obj );
    }

    private int SaveDetails( User item )
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

            BusinessError.Visible = true;
            BusinessError.Text = message.ToString();
            rowsAffected = 0;
        }
        catch (Csla.DataPortalException ex)
        {
            BusinessError.Visible = true;
            BusinessError.Text = ex.BusinessException.Message;
            rowsAffected = 0;
        }
        catch (Exception ex)
        {
            BusinessError.Visible = true;
            BusinessError.Text = ex.Message;
            rowsAffected = 0;
        }
        return rowsAffected;
    }

    private User GetUser( int id )
    {
        object businessObject = Session["CurrentObject"];
        if (businessObject == null || !(businessObject is User))
        {
            businessObject = AphelionTrigger.Library.User.GetUser( id );
            Session["CurrentObject"] = businessObject;
        }
        return (User)businessObject;
    }
    #endregion
    
}
