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
    public class Advancement : BusinessBase<Advancement>
    {
        #region Business Methods

        private int _id;
        private int _houseId;
        private int _levelId;
        private int _rank;
        private int _experience;
        private int _intelligence;
        private int _affluence;
        private int _power;
        private int _protection;
        private int _speed;
        private int _free;
        private int _freePlaced;
        private bool _hasViewed;

        private string _house;

        private SmartDate _advancementDate;

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

        public int LevelID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _levelId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_levelId != value)
                {
                    _levelId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Rank
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _rank;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_rank != value)
                {
                    _rank = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Experience
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _experience;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_experience != value)
                {
                    _experience = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Intelligence
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _intelligence;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_intelligence != value)
                {
                    _intelligence = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Affluence
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _affluence;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_affluence != value)
                {
                    _affluence = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Power
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _power;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_power != value)
                {
                    _power = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Protection
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _protection;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_protection != value)
                {
                    _protection = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Speed
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _speed;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Free
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _free;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_free != value)
                {
                    _free = value;
                    PropertyHasChanged();
                }
            }
        }

        public int FreePlaced
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _freePlaced;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_freePlaced != value)
                {
                    _freePlaced = value;
                    PropertyHasChanged();
                }
            }
        }

        public bool HasViewed
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _hasViewed;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_hasViewed != value)
                {
                    _hasViewed = value;
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
                if (_house != value)
                {
                    _house = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate AdvancementDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _advancementDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_advancementDate != value)
                {
                    _advancementDate = value;
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
        public static Advancement NewAdvancement()
        {
            return DataPortal.Create<Advancement>();
        }

        public static Advancement GetAdvancement( int id )
        {
            return DataPortal.Fetch<Advancement>( new Criteria( id ) );
        }

        public override Advancement Save()
        {
            return base.Save();
        }

        private Advancement()
        { /* require use of factory methods */ }

        internal Advancement( int id, int houseId, int levelId, int rank, int experience, int intelligence, int affluence, int power, int protection, int speed, int free, int freePlaced, string house, bool hasViewed, SmartDate advancementDate )
        {
            _id = id;
            _houseId = houseId;
            _levelId = levelId;
            _rank = rank;
            _experience = experience;
            _intelligence = intelligence;
            _affluence = affluence;
            _power = power;
            _protection = protection;
            _speed = speed;
            _free = free;
            _freePlaced = freePlaced;

            _hasViewed = hasViewed;

            _house = house;

            _advancementDate = advancementDate;
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
                    cm.CommandText = "AddAdvancement";
                    cm.Parameters.AddWithValue( "@ID", _houseId );
                    cm.Parameters.AddWithValue( "@LevelID", _levelId );
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

        private void DataPortal_Fetch( Criteria criteria )
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetAdvancement";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _houseId = dr.GetInt32( "HouseID" );
                        _levelId = dr.GetInt32( "LevelID" );
                        _rank = dr.GetInt32( "Rank" );
                        _experience = dr.GetInt32( "Experience" );
                        _intelligence = dr.GetInt32( "Intelligence" );
                        _affluence = dr.GetInt32( "Affluence" );
                        _power = dr.GetInt32( "Power" );
                        _protection = dr.GetInt32( "Protection" );
                        _speed = dr.GetInt32( "Speed" );
                        _free = dr.GetInt32( "Free" );
                        _freePlaced = dr.GetInt32( "FreePlaced" );

                        _house = dr.GetString( "House" );

                        _hasViewed = dr.GetBoolean( "HasViewed" );

                        _advancementDate = dr.GetSmartDate( "AdvancementDate" );
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Update()
        {
            throw new Exception( "Update attempted on an Advancement - Advancements are immutable, and cannot be updated." );
        }
        #endregion

        #region Reset Advancement
        public static void ResetAdvancement( bool resetAll )
        {
            DataPortal.Execute<ResetAdvancementCommand>( new ResetAdvancementCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetAdvancementCommand : CommandBase
        {
            private bool _resetAll;

            public ResetAdvancementCommand( bool resetAll )
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
                        cm.CommandText = "ResetAdvancement";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Update Free Points
        public static void UpdateFreePlaced( int houseId, int advancementId, int freePlaced, string statFieldName )
        {
            DataPortal.Execute<UpdateHouseStatisticCommand>( new UpdateHouseStatisticCommand( houseId, advancementId, freePlaced, statFieldName ) );
        }

        [Serializable()]
        private class UpdateHouseStatisticCommand : CommandBase
        {
            private int _houseId;
            private int _advancementId;
            private int _freePlaced;
            private string _statFieldName;

            public UpdateHouseStatisticCommand( int houseId, int advancementId, int freePlaced, string statFieldName )
            {
                _houseId = houseId;
                _advancementId = advancementId;
                _freePlaced = freePlaced;
                _statFieldName = statFieldName;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateFreePlaced";
                        cm.Parameters.AddWithValue( "@AdvancementID", _advancementId );
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@FreePlaced", _freePlaced );
                        cm.Parameters.AddWithValue( "@StatFieldName", _statFieldName );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
