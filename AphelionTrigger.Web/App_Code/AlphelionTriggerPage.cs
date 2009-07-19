using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger
{
    public class AphelionTriggerPage : System.Web.UI.Page
    {
        public string GetConfigurationValue( string key )
        {
            return string.Empty;
        }

        public void RegisterJavscript( string key, string url )
        {
            Type type = this.GetType();
            if (!ClientScript.IsClientScriptIncludeRegistered( type, key ))
            {
                ClientScript.RegisterClientScriptInclude( type, key, url );
            }
        }

        public void EnableTinyMCE()
        {
            // register the js include for the tinymce rich text editor
            RegisterJavscript( "tinymce", "../JScripts/tiny_mce/tiny_mce.js" );
        }

        #region Redirection / Authentication
        /// <summary>
        /// Requires a user to have been authenticated or else they are redirected to the default page.
        /// </summary>
        public void RequireAuthentication()
        {
            if (!User.IsAuthenticated) Response.Redirect( "~/Default.aspx", true );
        }

        /// <summary>
        /// Requires a user to be a member of the administrators role or else they are redirected to the default page.
        /// </summary>
        public void RequireAdministration()
        {
            if (!Csla.ApplicationContext.User.IsInRole( "Administrator" )) Response.Redirect( "~/Default.aspx", true );
        }

        /// <summary>
        /// Requires a user to have a house (i.e. not only be in staff roles) or else they are redirected to the default page.
        /// </summary>
        public void RequireHouse()
        {
            if (User.HouseID < 1) Response.Redirect( "~/Default.aspx", true );
        }

        /// <summary>
        /// Requires a currently active age or else the user is redirected to the default page
        /// </summary>
        public void RequireAge()
        {
            if (AgeList.CurrentAgeID() == 0) Response.Redirect( "~/Default.aspx", true );
        }
        #endregion

        #region String Manipulation
        protected string EscapeForJavascript( string text )
        {
            string jsText = text;
            jsText = text.Replace( "'", @"\'" );

            return jsText;
        }

        protected string Pluralize( string text )
        {
            if (text == null || text.Length == 0) return string.Empty;

            return (text.ToLower().EndsWith( "y" ) ? text.Remove( text.Length - 1, 1 ) + "ies" : text + "s");
        }
        #endregion

        #region Identity and User/House Class Wrappers
        public new ATIdentity User
        {
            get { return ((ATIdentity)Csla.ApplicationContext.User.Identity); }
        }

        public House UserHouse
        {
            get
            {
                House house;

                int userId = ((ATIdentity)Csla.ApplicationContext.User.Identity).ID;

                if ((Cache["HOUSE" + userId.ToString()] as House) == null)
                {
                    int id = ((ATIdentity)Csla.ApplicationContext.User.Identity).HouseID;
                    house = House.GetHouse( id );

                    HttpContext.Current.Cache.Insert( "HOUSE" + userId.ToString(), house, null, DateTime.Now.AddMinutes( 2 ), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null );
                }
                else
                {
                    house = (House)Cache["HOUSE" + userId.ToString()];
                }

                return house;
            }
        }

        protected void RefreshUserHouse()
        {
            Cache.Remove( "HOUSE" + User.ID.ToString() );
        }

        public HouseList Houses
        {
            get
            {
                HouseList houses;

                if (HttpContext.Current.Cache["HOUSELIST"] == null)
                {
                    houses = HouseList.GetHouseList();

                    HttpContext.Current.Cache.Insert( "HOUSELIST", houses, null, DateTime.Now.AddMinutes( 10 ), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.BelowNormal, null );
                }
                else
                {
                    houses = (HouseList)HttpContext.Current.Cache["HOUSELIST"];
                }

                return houses;
            }
        }

        public void HousesClear()
        {
            HttpContext.Current.Cache.Remove( "Houses" );
        }

        public bool HasUserHouse()
        {
            return (User.HouseID > 0);
        }
        #endregion

        #region Stubs
        public MessageStub MESSAGE
        {
            get
            {
                object stub = Session["_MESSAGESTUB"];
                if (stub == null || !(stub is MessageStub))
                {
                    Session.Add( "_MESSAGESTUB", new MessageStub( false ) );
                    stub = Session["_MESSAGESTUB"];
                }

                return (MessageStub)stub;
            }
            set { Session.Add( "_MESSAGESTUB", value ); }
        }

        public HouseStub HOUSE
        {
            get
            {
                object stub = Session["_HOUSESTUB"];
                if (stub == null || !(stub is HouseStub))
                {
                    Session.Add( "_HOUSESTUB", new HouseStub( false ) );
                    stub = Session["_HOUSESTUB"];
                }

                return (HouseStub)stub;
            }
            set { Session.Add( "_HOUSESTUB", value ); }
        }

        public GuildStub GUILD
        {
            get
            {
                object stub = Session["_GUILDSTUB"];
                if (stub == null || !(stub is GuildStub))
                {
                    Session.Add( "_GUILDSTUB", new GuildStub( false ) );
                    stub = Session["_GUILDSTUB"];
                }

                return (GuildStub)stub;
            }
            set { Session.Add( "_GUILDSTUB", value ); }
        }
        #endregion

        #region Events
        public event EventHandler RefreshHUD;

        protected void OnRefreshHUD( EventArgs e )
        {
            if (RefreshHUD != null)
            {
                RefreshHUD( this, e );
            }
        }
        #endregion
    }

    #region Page Stubs
    /// <summary>
    /// Used for storing temporary data over cross-page
    /// postbacks where a message request was made.
    /// </summary>
    public struct MessageStub
    {
        public bool Initialized;
        public int MessageID;
        public int ThreadID;
        public int RecipientHouseID;
        public string RecipientHouse;
        public string Subject;

        public MessageStub( bool init )
        {
            MessageID = 0;
            ThreadID = 0;
            RecipientHouseID = 0;
            RecipientHouse = string.Empty;
            Subject = string.Empty;
            Initialized = false;
        }

        public MessageStub( int messageId )
        {
            MessageID = messageId;
            ThreadID = 0;
            RecipientHouseID = 0;
            RecipientHouse = string.Empty;
            Subject = string.Empty;
            Initialized = true;
        }

        public MessageStub( int threadId, int recipientHouseId, string recipientHouse, string subject )
        {
            MessageID = 0;
            ThreadID = threadId;
            RecipientHouseID = recipientHouseId;
            RecipientHouse = recipientHouse;
            Subject = subject;
            Initialized = true;
        }
    }

    /// <summary>
    /// Used for storing temporary data over cross-page
    /// postbacks where a request for house details was made.
    /// </summary>
    public struct HouseStub
    {
        public bool Initialized;
        public int HouseID;
        public string HouseName;

        public HouseStub( bool init )
        {
            HouseID = 0;
            HouseName = string.Empty;
            Initialized = false;
        }

        public HouseStub( int houseID )
        {
            HouseID = houseID;
            HouseName = string.Empty;
            Initialized = true;
        }

        public HouseStub( int houseID, string name )
        {
            HouseID = houseID;
            HouseName = name;
            Initialized = true;
        }
    }

    /// <summary>
    /// Used for storing temporary data over cross-page
    /// postbacks where a request for guild details was made.
    /// </summary>
    public struct GuildStub
    {
        public bool Initialized;
        public int GuildID;
        public string GuildName;

        public GuildStub( bool init )
        {
            GuildID = 0;
            GuildName = string.Empty;
            Initialized = false;
        }

        public GuildStub( int guildID )
        {
            GuildID = guildID;
            GuildName = string.Empty;
            Initialized = true;
        }

        public GuildStub( int guildID, string name )
        {
            GuildID = guildID;
            GuildName = name;
            Initialized = true;
        }
    }
    #endregion
}