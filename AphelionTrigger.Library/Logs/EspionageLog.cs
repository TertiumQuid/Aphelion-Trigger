using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Csla.Validation;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class EspionageLog : BusinessBase<EspionageLog>
    {
        #region Business Methods

        private int _id;
        private int _espionageOperationId;

        private int _operatingHouseId;
        private int _targetHouseId;
        private int _ageId;

        private string _description;
        private string _operation;

        private string _operatingHouse;
        private string _targetHouse;

        private bool _success;
        private bool _detection;

        private int _compromised;

        private SmartDate _espionageDate;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int EspionageOperationID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _espionageOperationId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _espionageOperationId != value )
                {
                    _espionageOperationId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int OperatingHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _operatingHouseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _operatingHouseId != value )
                {
                    _operatingHouseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int TargetHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _targetHouseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _targetHouseId != value )
                {
                    _targetHouseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int AgeID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _ageId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _ageId != value )
                {
                    _ageId = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Description
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _description;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _description != value )
                {
                    _description = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Operation
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _operation;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _operation != value )
                {
                    _operation = value;
                    PropertyHasChanged();
                }
            }
        }

        public string OperatingHouse
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _operatingHouse;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _operatingHouse != value )
                {
                    _operatingHouse = value;
                    PropertyHasChanged();
                }
            }
        }

        public string TargetHouse
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _targetHouse;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _targetHouse != value )
                {
                    _targetHouse = value;
                    PropertyHasChanged();
                }
            }
        }

        public bool Success
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _success;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _success != value )
                {
                    _success = value;
                    PropertyHasChanged();
                }
            }
        }

        public bool Detection
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _detection;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _detection != value )
                {
                    _detection = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Compromised
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _compromised;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _compromised != value )
                {
                    _compromised = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate EspionageDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _espionageDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _espionageDate != value )
                {
                    _espionageDate = value;
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
        public static EspionageLog NewEspionageLog()
        {
            return DataPortal.Create<EspionageLog>();
        }

        public static EspionageLog GetEspionageLog( int id )
        {
            return DataPortal.Fetch<EspionageLog>( new Criteria( id ) );
        }

        public override EspionageLog Save()
        {
            return base.Save();
        }

        private EspionageLog()
        { /* require use of factory methods */ }

        internal EspionageLog( int id, int espionageOperationId, int operatingHouseId, int targetHouseId, int ageId, string description, string operation, string operatingHouse, string targetHouse, SmartDate espionageDate, bool success, bool detection, int compromised )
        {
            _id = id;
            _espionageOperationId = espionageOperationId;
            _operatingHouseId = operatingHouseId;
            _targetHouseId = targetHouseId;
            _ageId = ageId;
            
            _operation = operation;
            _description = description;
            _operatingHouse = operatingHouse;
            _targetHouse = targetHouse;

            _espionageDate = espionageDate;

            _success = success;
            _detection = detection;

            _compromised = compromised;
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
                    cm.CommandText = "GetEspionage";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _espionageOperationId = dr.GetInt32( "EspionageOperationID" );
                        _operatingHouseId = dr.GetInt32( "OperatingHouseID" );
                        _targetHouseId = dr.GetInt32( "TargetHouseID" );
                        _ageId = dr.GetInt32( "AgeID" );

                        _operation = dr.GetString( "Operation" );
                        _description = dr.GetString( "Description" );
                        _operatingHouse = dr.GetString( "OperatingHouse" );
                        _targetHouse = dr.GetString( "TargetHouse" );

                        _espionageDate = dr.GetSmartDate( "EspionageDate" );

                        _success = Convert.ToBoolean( dr["Success"] );
                        _detection = Convert.ToBoolean( dr["Detection"] );

                        _compromised = dr.GetInt32( "Compromised" );
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
                    cm.CommandText = "AddEspionage";
                    cm.Parameters.AddWithValue( "@EspionageOperationID", _espionageOperationId );
                    cm.Parameters.AddWithValue( "@OperatingHouseID", _operatingHouseId );
                    cm.Parameters.AddWithValue( "@TargetHouseID", _targetHouseId );
                    cm.Parameters.AddWithValue( "@Description", _description );
                    cm.Parameters.AddWithValue( "@Success", _success );
                    cm.Parameters.AddWithValue( "@Detection", _detection );
                    cm.Parameters.AddWithValue( "@Compromised", _compromised );
                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    _id = (int)cm.Parameters["@NewId"].Value;
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion
    }
}
