using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class Report : BusinessBase<Report>
    {
        #region Business Methods

        private int _id;
        private int _houseId;
        private int _factionId;
        private int _guildId;
        private int _reportLevelId;

        private string _house;
        private string _levelName;
        private string _signifier;
        private string _message;

        private SmartDate _reportDate;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int HouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _houseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_houseId != value)
                {
                    _houseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int GuildID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _guildId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_guildId != value)
                {
                    _guildId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int FactionID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _factionId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_factionId != value)
                {
                    _factionId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ReportLevelID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _reportLevelId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_reportLevelId != value)
                {
                    _reportLevelId = value;
                    PropertyHasChanged();
                }
            }
        }

        public string House
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _house;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_house != value)
                {
                    _house = value;
                    PropertyHasChanged();
                }
            }
        }

        public string LevelName
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _levelName;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_levelName != value)
                {
                    _levelName = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Signifier
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _signifier;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_signifier != value)
                {
                    _signifier = value;
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
                if (value == null) value = string.Empty;
                if (_message != value)
                {
                    _message = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate ReportDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _reportDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_reportDate != value)
                {
                    _reportDate = value;
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
        
        #region Factory Methods
        public static Report NewReport()
        {
            return DataPortal.Create<Report>();
        }

        public static Report GetReport( int id )
        {
            return DataPortal.Fetch<Report>( new Criteria( id ) );
        }

        public override Report Save()
        {
            return base.Save();
        }

        private Report()
        { /* require use of factory methods */ }

        internal Report( int id, int houseId, int guildId, int factionId, int reportLevelID, string house, string levelName, string signifier, string message, SmartDate reportDate )
        {
            _id = id;
            _houseId = houseId;
            _guildId = guildId;
            _factionId = factionId;
            _reportLevelId = reportLevelID;

            _house = house;
            _levelName = levelName;
            _signifier = signifier;
            _message = message;

            _reportDate = reportDate;
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
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetReport";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _houseId = dr.GetInt32( "HouseID" );
                        _guildId = dr.GetInt32( "GuildID" );
                        _factionId = dr.GetInt32( "FactionID" );
                        _reportLevelId = dr.GetInt32( "ReportLevelId" );

                        _house = dr.GetString( "House" );
                        _levelName = dr.GetString( "LevelName" );
                        _signifier = dr.GetString( "Signifier" );
                        _message = dr.GetString( "Message" );

                        _reportDate = dr.GetSmartDate( "ReportDate" );
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Insert()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddReport";
                    cm.Parameters.AddWithValue( "@HouseID", _houseId );
                    cm.Parameters.AddWithValue( "@ReportLevelID", _reportLevelId );
                    cm.Parameters.AddWithValue( "@Message", _message );
                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    _id = (int)cm.Parameters["@NewId"].Value;
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion

        #region Reset Reports
        public static void ResetReports( bool resetAll )
        {
            DataPortal.Execute<ResetReportsCommand>( new ResetReportsCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetReportsCommand : CommandBase
        {
            private bool _resetAll;

            public ResetReportsCommand( bool resetAll )
            {
                _resetAll = resetAll;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "ResetReports";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
