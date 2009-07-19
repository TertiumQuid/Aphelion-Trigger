using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class User : BusinessBase<User>
    {
        #region Business Methods

        private int _id;

        private string _username;
        private string _password;
        private string _email;
        private string _location;
        private string _signature;
        private string _websiteUrl;
        private string _personalText;
        private string _icq;
        private string _aim;
        private string _msn;
        private string _yim;

        private SmartDate _registrationDate;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public string Username
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _username;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _username != value )
                {
                    _username = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Password
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _password;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _password != value )
                {
                    _password = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Email
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _email;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _email != value )
                {
                    _email = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Location
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _location;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _location != value )
                {
                    _location = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Signature
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _signature;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _signature != value )
                {
                    _signature = value;
                    PropertyHasChanged();
                }
            }
        }

        public string WebsiteURL
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _websiteUrl;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _websiteUrl != value )
                {
                    _websiteUrl = value;
                    PropertyHasChanged();
                }
            }
        }

        public string PersonalText
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _personalText;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _personalText != value )
                {
                    _personalText = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ICQ
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _icq;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _icq != value )
                {
                    _icq = value;
                    PropertyHasChanged();
                }
            }
        }

        public string AIM
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _aim;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _aim != value )
                {
                    _aim = value;
                    PropertyHasChanged();
                }
            }
        }

        public string MSN
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _msn;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _msn != value )
                {
                    _msn = value;
                    PropertyHasChanged();
                }
            }
        }

        public string YIM
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _yim;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _yim != value )
                {
                    _yim = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate RegistrationDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _registrationDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _registrationDate != value )
                {
                    _registrationDate = value;
                    PropertyHasChanged();
                }
            }
        }

        public override bool IsValid
        {
            get { return base.IsValid; }
        }

        public override bool IsDirty
        {
            get { return base.IsDirty; }
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
        }

        #endregion
        
        #region Factory Methods
        public static User NewUser()
        {
            return DataPortal.Create<User>();
        }

        public static User GetUser( int id )
        {
            return DataPortal.Fetch<User>( new Criteria( id ) );
        }

        public override User Save()
        {
            return base.Save();
        }

        private User()
        { /* require use of factory methods */ }

        internal User( int id, string username, string password, string email, string location, string signature, string websiteUrl, string personalText, string icq, string aim, string msn, string yim, SmartDate registrationDate )
        {
            _id = id;

            _username = username;
            _password = password;

            _email = email;
            _location = location;
            _signature = signature;
            _websiteUrl = websiteUrl;
            _personalText = personalText;
            _icq = icq;
            _aim = aim;
            _msn = msn;
            _yim = yim;

            _registrationDate = registrationDate;
        }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public Criteria( int id )
            { _id = id; }
        }

        [RunLocal()]
        protected override void DataPortal_Create()
        {
            ValidationRules.CheckRules();
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetUser";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );

                        _username = dr.GetString( "Username" );
                        _email = dr.GetString( "Email" );
                        _password = dr.GetString( "Password" );
                        _location = dr.GetString( "Location" );
                        _signature = dr.GetString( "Signature" );
                        _websiteUrl = dr.GetString( "WebsiteURL" );
                        _personalText = dr.GetString( "PersonalText" );
                        _icq = dr.GetString( "ICQ" );
                        _aim = dr.GetString( "AIM" );
                        _msn = dr.GetString( "MSN" );
                        _yim = dr.GetString( "YIM" );

                        _registrationDate = dr.GetSmartDate( "RegistrationDate" );
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Update()
        {
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                if ( base.IsDirty )
                {
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateUser";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@Username", _username );
                        cm.Parameters.AddWithValue( "@Email", _email );
                        cm.Parameters.AddWithValue( "@Password", _password );
                        cm.Parameters.AddWithValue( "@Location", _location );
                        cm.Parameters.AddWithValue( "@Signature", _signature );
                        cm.Parameters.AddWithValue( "@WebsiteURL", _websiteUrl );
                        cm.Parameters.AddWithValue( "@PersonalText", _personalText );
                        cm.Parameters.AddWithValue( "@ICQ", _icq );
                        cm.Parameters.AddWithValue( "@AIM", _aim );
                        cm.Parameters.AddWithValue( "@MSN", _msn );
                        cm.Parameters.AddWithValue( "@YIM", _yim );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion
    }
}
