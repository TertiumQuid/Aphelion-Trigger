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
    public class Level : BusinessBase<Level>
    {
        #region Business Methods

        private int _id;
        private int _rank;
        private int _factionId;
        private int _experience;
        private int _unitCapacity;
        private int _spyCapacity;
        private int _intelligence;
        private int _affluence;
        private int _power;
        private int _protection;
        private int _speed;
        private int _contingency;
        private int _free;

        private string _faction;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
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

        public int UnitCapacity
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _unitCapacity;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_unitCapacity != value)
                {
                    _unitCapacity = value;
                    PropertyHasChanged();
                }
            }
        }

        public int SpyCapacity
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _spyCapacity;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _spyCapacity != value )
                {
                    _spyCapacity = value;
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

        public int Contingency
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _contingency;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _contingency != value )
                {
                    _contingency = value;
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

        public string Faction
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _faction;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_faction != value)
                {
                    _faction = value;
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
        public static Level GetLevel( int id )
        {
            return DataPortal.Fetch<Level>( new Criteria( id ) );
        }

        public override Level Save()
        {
            return base.Save();
        }

        private Level()
        { /* require use of factory methods */ }

        internal Level( int id, int rank, int factionId, int experience, int unitCapacity, int spyCapacity, int intelligence, int affluence, int power, int protection, int speed, int contingency, int free, string faction )
        {
            _id = id;
            _rank = rank;
            _factionId = factionId;
            _experience = experience;
            _unitCapacity = unitCapacity;
            _spyCapacity = spyCapacity;
            _intelligence = intelligence;
            _affluence = affluence;
            _power = power;
            _protection = protection;
            _speed = speed;
            _contingency = contingency;
            _free = free;

            _faction = faction;
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

        private void DataPortal_Fetch( Criteria criteria )
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetLevel";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _rank = dr.GetInt32( "Rank" );
                        _factionId = dr.GetInt32( "FactionID" );
                        _experience = dr.GetInt32( "Experience" );
                        _unitCapacity = dr.GetInt32( "UnitCapacity" );
                        _spyCapacity = dr.GetInt32( "SpyCapacity" );
                        _intelligence = dr.GetInt32( "Intelligence" );
                        _affluence = dr.GetInt32( "Affluence" );
                        _power = dr.GetInt32( "Power" );
                        _protection = dr.GetInt32( "Protection" );
                        _speed = dr.GetInt32( "Speed" );
                        _contingency = dr.GetInt32( "Contingency" );
                        _free = dr.GetInt32( "Free" );

                        _faction = dr.GetString( "Faction" );
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Update()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                if (base.IsDirty)
                {
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateLevel";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@Experience", _experience );
                        cm.Parameters.AddWithValue( "@UnitCapacity", _unitCapacity );
                        cm.Parameters.AddWithValue( "@Intelligence", _intelligence );
                        cm.Parameters.AddWithValue( "@Affluence", _affluence );
                        cm.Parameters.AddWithValue( "@Power", _power );
                        cm.Parameters.AddWithValue( "@Protection", _protection );
                        cm.Parameters.AddWithValue( "@Speed", _speed );
                        cm.Parameters.AddWithValue( "@Contingency", _contingency );
                        cm.Parameters.AddWithValue( "@Free", _free );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion

        #region Update Experience
        public static void UpdateExperience( House house, int experience )
        {
            DataPortal.Execute<UpdateExperienceCommand>( new UpdateExperienceCommand( house, experience ) );
        }

        [Serializable()]
        private class UpdateExperienceCommand : CommandBase
        {
            private House _house;
            private int _experience;

            public UpdateExperienceCommand( House house, int experience )
            {
                _house = house;
                _experience = experience;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    int experience = _house.Experience + _experience;
                    int levelId = ((_house.NextLevel != null && experience >= _house.NextLevel.Experience) ? _house.NextLevel.ID : _house.Level.ID);

                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateExperience";
                        cm.Parameters.AddWithValue( "@ID", _house.ID );
                        cm.Parameters.AddWithValue( "@Experience", experience );
                        cm.Parameters.AddWithValue( "@LevelID", levelId );
                        cm.ExecuteNonQuery();
                    }

                    #region Advancement Report
                    if (levelId > _house.Level.ID)
                    {
                        AphelionTrigger.Library.Report report1 = Report.NewReport();
                        report1.FactionID = _house.FactionID;
                        report1.GuildID = _house.GuildID;
                        report1.HouseID = _house.ID;
                        report1.Message = _house.Name + " advanced a level.";
                        report1.ReportLevelID = 2 + House.GetSecrecyBonus( _house.Intelligence );
                        report1.Save();
                    }
                    #endregion
                }
            }
        }
        #endregion
    }
}
