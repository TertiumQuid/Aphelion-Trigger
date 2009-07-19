using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library.Logs
{
    [Serializable()]
    public class SystemLog : BusinessBase<SystemLog>
    {
        #region Business Methods

        private int _id;
        private string _application;
        private string _source;
        private string _message;
        private string _details;

        private SystemLogType _systemLogType;

        private SmartDate _logDate;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public string Application
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _application;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _application != value )
                {
                    _application = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Source
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _source;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _source != value )
                {
                    _source = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Message
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _message;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _message != value )
                {
                    _message = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Details
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _details;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _details != value )
                {
                    _details = value;
                    PropertyHasChanged();
                }
            }
        }

        public SystemLogType LogType
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _systemLogType;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _systemLogType != value )
                {
                    _systemLogType = value;
                    PropertyHasChanged();
                }
            }
        }

        public int SystemTypeID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return (int)_systemLogType;
            }
        }

        public string SystemType
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _systemLogType.ToString();
            }
        }

        public SmartDate LogDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _logDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _logDate != value )
                {
                    _logDate = value;
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
        public static SystemLog NewSystemLog()
        {
            return DataPortal.Create<SystemLog>();
        }

        public static SystemLog GetSystemLog( int id )
        {
            return DataPortal.Fetch<SystemLog>( new Criteria( id ) );
        }

        public override SystemLog Save()
        {
            return base.Save();
        }

        private SystemLog()
        { /* require use of factory methods */ }

        // constructor without id or date - used by the systemlogcommand
        internal SystemLog( SystemLogType systemLogType, string application, string source, string message, string details )
        {
            _systemLogType = systemLogType;

            _application = application;
            _source = source;
            _message = message;
            _details = details;

            _logDate = new SmartDate( DateTime.Now );
        }

        internal SystemLog( int id, SystemLogType systemLogType, string application, string source, string message, string details, SmartDate logDate )
        {
            _id = id;

            _systemLogType = systemLogType;

            _application = application;
            _source = source;
            _message = message;
            _details = details;

            _logDate = logDate;
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
            // nothing to initialize
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
                    cm.CommandText = "GetSystemLog";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        if ( dr.Read() )
                        {
                            _id = dr.GetInt32( "ID" );

                            _application = dr.GetString( "Application" );
                            _details = dr.GetString( "Details" );
                            _source = dr.GetString( "Source" );
                            _message = dr.GetString( "Message" );

                            _systemLogType = (SystemLogType)dr.GetInt32( "SystemLogTypeID" );
                        }
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Insert()
        {
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddSystemLog";
                    cm.Parameters.AddWithValue( "@Application", _application );
                    cm.Parameters.AddWithValue( "@Source", _source );
                    cm.Parameters.AddWithValue( "@SystemTypeID", Convert.ToInt32( _systemLogType ) );
                    cm.Parameters.AddWithValue( "@Message", _message );
                    cm.Parameters.AddWithValue( "@Details", _details );
                    cm.Parameters.AddWithValue( "@LogDate", _logDate.DBValue );

                    cm.ExecuteNonQuery();
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion
    }
}
