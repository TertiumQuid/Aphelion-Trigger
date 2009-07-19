using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class Technology : BusinessBase<Technology>
    {
        #region Business Methods

        private int _id;
        private int _factionId;
        private int _houseId;
        private int _guildId;
        private int _technologyTypeId;
        private int _unitId;
        private int _unitClassId;

        private string _name;
        private string _faction;
        private string _house;
        private string _guild;
        private string _description;
        private string _technologyType;
        private string _unit;
        private string _unitClass;

        private int _attack;
        private int _defense;
        private int _plunder;
        private int _capture;
        private int _stun;
        private int _experience;

        private decimal _repopulationRate;
        private decimal _depopulationRate;

        private int _researchCost;
        private int _researchTime;
        private int _researchTurns;

        private int _timeSpent;
        private int _turnsSpent;
        private int _creditsSpent;

        private int _researchStateId;
        private string _researchState;

        private SmartDate _researchStartedDate;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
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

        [System.ComponentModel.DataObjectField( true, true )]
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

        [System.ComponentModel.DataObjectField( true, true )]
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

        [System.ComponentModel.DataObjectField( true, true )]
        public int TechnologyTypeID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _technologyTypeId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_technologyTypeId != value)
                {
                    _technologyTypeId = value;
                    PropertyHasChanged();
                }
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
        public int UnitID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _unitId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_unitId != value)
                {
                    _unitId = value;
                    PropertyHasChanged();
                }
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
        public int UnitClassID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _unitClassId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_unitClassId != value)
                {
                    _unitClassId = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Name
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _name;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_name != value)
                {
                    _name = value;
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
                if (value == null) value = string.Empty;
                if (_description != value)
                {
                    _description = value;
                    PropertyHasChanged();
                }
            }
        }

        public string TechnologyType
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _technologyType;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_technologyType != value)
                {
                    _technologyType = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Unit
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _unit;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_unit != value)
                {
                    _unit = value;
                    PropertyHasChanged();
                }
            }
        }

        public string UnitClass
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _unitClass;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_unitClass != value)
                {
                    _unitClass = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Attack
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _attack;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_attack != value)
                {
                    _attack = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Defense
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _defense;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_defense != value)
                {
                    _defense = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Plunder
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _plunder;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_plunder != value)
                {
                    _plunder = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Capture
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _capture;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_capture != value)
                {
                    _capture = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Stun
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _stun;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _stun != value )
                {
                    _stun = value;
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
                if ( _experience != value )
                {
                    _experience = value;
                    PropertyHasChanged();
                }
            }
        }

        public decimal RepopulationRate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _repopulationRate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_repopulationRate != value)
                {
                    _repopulationRate = value;
                    PropertyHasChanged();
                }
            }
        }

        public decimal DepopulationRate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _depopulationRate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_depopulationRate != value)
                {
                    _depopulationRate = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ResearchCost
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _researchCost;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _researchCost != value )
                {
                    _researchCost = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ResearchTime
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _researchTime;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_researchTime != value)
                {
                    _researchTime = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ResearchTurns
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _researchTurns;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _researchTurns != value )
                {
                    _researchTurns = value;
                    PropertyHasChanged();
                }
            }
        }

        public int TimeSpent
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _timeSpent;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _timeSpent != value )
                {
                    _timeSpent = value;
                    PropertyHasChanged();
                }
            }
        }

        public int TurnsSpent
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _turnsSpent;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _turnsSpent != value )
                {
                    _turnsSpent = value;
                    PropertyHasChanged();
                }
            }
        }

        public int CreditsSpent
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _creditsSpent;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _creditsSpent != value )
                {
                    _creditsSpent = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ResearchStateID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _researchStateId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _researchStateId != value )
                {
                    _researchStateId = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ResearchState
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _researchState;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _researchState != value )
                {
                    _researchState = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate ResearchStartedDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _researchStartedDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _researchStartedDate != value )
                {
                    _researchStartedDate = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Summary
        {
            get
            {
                StringBuilder summary = new StringBuilder();

                summary.Append( "<table width='100%' cellpadding='4'>" );
                summary.Append( "<tr><td colspan='2'>" + _description + "</td></tr>" );
                summary.Append( "<tr><td colspan='2'>&nbsp;</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Applies To:</td><td>" + _unitClass + ( _unit.ToLower().EndsWith( "y" ) ? _unit.Remove( _unit.Length - 1, 1 ) + "ies" : _unit + "s" ) + "</td></tr>" );
                summary.Append( "<tr><td colspan='2'>&nbsp;</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Cost (Credits):</td><td>" + _researchCost.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Cost (Turns):</td><td>" + _researchTurns.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Research Time:</td><td>" + _researchTime.ToString() + "</td></tr>" );
                summary.Append( "<tr><td colspan='2'>&nbsp;</td></tr>" );
                if ( _attack > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Attack:</td><td>+" + _attack.ToString() + "</td></tr>" );
                if ( _defense > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Deffense:</td><td>+" + _defense.ToString() + "</td></tr>" );
                if ( _plunder > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Plunder:</td><td>+" + _plunder.ToString() + "</td></tr>" );
                if ( _capture > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Capture:</td><td>+" + _capture.ToString() + "</td></tr>" );
                if ( _stun > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Stun:</td><td>+" + _stun.ToString() + "</td></tr>" );
                if ( _experience > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Experience Value:</td><td>+" + _experience.ToString() + "</td></tr>" );
                if ( _repopulationRate > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Repopulation Rate:</td><td>" + _repopulationRate.ToString() + "</td></tr>" );
                if ( _depopulationRate > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Depopulation Rate:</td><td>" + _depopulationRate.ToString() + "</td></tr>" );
                summary.Append( "</table>" );

                return summary.ToString();
            }
        }

        public override string ToString()
        {
            return _name;
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
        public static Technology NewTechnology()
        {
            return DataPortal.Create<Technology>();
        }

        public static Technology GetTechnology( int id )
        {
            return DataPortal.Fetch<Technology>( new Criteria( id ) );
        }

        public override Technology Save()
        {
            return base.Save();
        }

        private Technology()
        { /* require use of factory methods */ }

        internal Technology( int id, int factionId, int houseId, int guildId, int technologyTypeId, int unitId, int unitClassId, string name, string faction, string house, string guild, string description, string technologyType, string unit, string unitClass, int attack, int defense, int plunder, int capture, int stun, int experience, decimal repopulationRate, decimal depopulationRate, int researchCost, int researchTime, int researchTurns, int timeSpent, int turnsSpent, int creditsSpent, int researchStateId, string researchState, SmartDate researchStartedDate )
        {
            _id = id;
            _factionId = factionId;
            _houseId = houseId;
            _guildId = guildId;
            _unitClassId = unitClassId;
            _technologyTypeId = technologyTypeId;

            _name = name;
            _faction = faction;
            _house = house;
            _guild = guild;
            _description = description;
            _technologyType = technologyType;
            _unit = unit;
            _unitClass = unitClass;

            _attack = attack;
            _defense = defense;
            _plunder = plunder;
            _capture = capture;
            _stun = stun;
            _experience = experience;
            
            _repopulationRate = repopulationRate;
            _depopulationRate = depopulationRate;
            
            _researchCost = researchCost;
            _researchTime = researchTime;
            _researchTurns = researchTurns;

            _timeSpent = timeSpent;
            _turnsSpent = turnsSpent;
            _creditsSpent = creditsSpent;

            _researchStateId = researchStateId;
            _researchState = researchState;

            _researchStartedDate = researchStartedDate;
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
                    cm.CommandText = "GetTechnology";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _factionId = dr.GetInt32( "FactionID" );
                        _houseId = dr.GetInt32( "HouseID" );
                        _guildId = dr.GetInt32( "GuildID" );
                        _technologyTypeId = dr.GetInt32( "TechnologyTypeID" );
                        _unitId = dr.GetInt32( "UnitID" );
                        _unitClassId = dr.GetInt32( "UnitClassID" );

                        _name = dr.GetString( "Name" );
                        _faction = dr.GetString( "Faction" );
                        _description = dr.GetString( "Description" );
                        _technologyType = dr.GetString( "TechnologyType" );
                        _unit = dr.GetString( "Unit" );
                        _unitClass = dr.GetString( "UnitClass" );

                        _attack = dr.GetInt32( "Attack" );
                        _defense = dr.GetInt32( "Defense" );
                        _plunder = dr.GetInt32( "Plunder" );
                        _capture = dr.GetInt32( "Capture" );
                        _experience = dr.GetInt32( "Experience" );

                        _repopulationRate = dr.GetDecimal( "RepopulationRate" );
                        _depopulationRate = dr.GetDecimal( "DepopulationRate" );

                        _researchCost = dr.GetInt32( "ResearchCost" );
                        _researchTime = dr.GetInt32( "ResearchTime" );
                        _researchTurns = dr.GetInt32( "ResearchTurns" );
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
                    cm.CommandText = "AddTechnology";
                    cm.Parameters.AddWithValue( "@FactionID", _factionId );
                    cm.Parameters.AddWithValue( "@HouseID", _houseId );
                    cm.Parameters.AddWithValue( "@GuildID", _guildId );
                    cm.Parameters.AddWithValue( "@TechnologyTypeID", _technologyTypeId );
                    cm.Parameters.AddWithValue( "@UnitID", _unitId );
                    cm.Parameters.AddWithValue( "@UnitClassID", _unitClassId );
                    cm.Parameters.AddWithValue( "@Name", _name );
                    cm.Parameters.AddWithValue( "@Description", _description );
                    cm.Parameters.AddWithValue( "@Attack", _attack );
                    cm.Parameters.AddWithValue( "@Defense", _defense );
                    cm.Parameters.AddWithValue( "@Plunder", _plunder );
                    cm.Parameters.AddWithValue( "@Capture", _capture );
                    cm.Parameters.AddWithValue( "@Experience", _experience );
                    cm.Parameters.AddWithValue( "@RepopulationRate", _repopulationRate );
                    cm.Parameters.AddWithValue( "@DepopulationRate", _depopulationRate );
                    cm.Parameters.AddWithValue( "@ResearchCost", _researchCost );
                    cm.Parameters.AddWithValue( "@ResearchTurns", _researchTurns );
                    cm.Parameters.AddWithValue( "@ResearchTime", _researchTime );
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
                        cm.CommandText = "UpdateTechnology";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@FactionID", _factionId );
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@GuildID", _guildId );
                        cm.Parameters.AddWithValue( "@TechnologyTypeID", _technologyTypeId );
                        cm.Parameters.AddWithValue( "@UnitID", _unitId );
                        cm.Parameters.AddWithValue( "@UnitClassID", _unitClassId );
                        cm.Parameters.AddWithValue( "@Name", _name );
                        cm.Parameters.AddWithValue( "@Description", _description );
                        cm.Parameters.AddWithValue( "@Attack", _attack );
                        cm.Parameters.AddWithValue( "@Defense", _defense );
                        cm.Parameters.AddWithValue( "@Plunder", _plunder );
                        cm.Parameters.AddWithValue( "@Capture", _capture );
                        cm.Parameters.AddWithValue( "@Experience", _experience );
                        cm.Parameters.AddWithValue( "@RepopulationRate", _repopulationRate );
                        cm.Parameters.AddWithValue( "@DepopulationRate", _depopulationRate );
                        cm.Parameters.AddWithValue( "@ResearchCost", _researchCost );
                        cm.Parameters.AddWithValue( "@ResearchTurns", _researchTurns );
                        cm.Parameters.AddWithValue( "@ResearchTime", _researchTime );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion

        #region Research
        public static void AddResearch( int houseId, int technologyId )
        {
            DataPortal.Execute<AddResearchCommand>( new AddResearchCommand( houseId, technologyId ) );
        }

        public static void SuspendResearch( int houseId, int technologyId )
        {
            DataPortal.Execute<SuspendResearchCommand>( new SuspendResearchCommand( houseId, technologyId ) );
        }

        public static void AbortResearch( int houseId, int technologyId )
        {
            DataPortal.Execute<AbortResearchCommand>( new AbortResearchCommand( houseId, technologyId ) );
        }

        [Serializable()]
        private class AddResearchCommand : CommandBase
        {
            private int _houseId;
            private int _technologyId;

            public AddResearchCommand( int houseId, int technologyId )
            {
                _houseId = houseId;
                _technologyId = technologyId;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "AddResearch";
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@TechnologyID", _technologyId );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }

        [Serializable()]
        private class SuspendResearchCommand : CommandBase
        {
            private int _houseId;
            private int _technologyId;

            public SuspendResearchCommand( int houseId, int technologyId )
            {
                _houseId = houseId;
                _technologyId = technologyId;
            }

            protected override void DataPortal_Execute()
            {
                using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                {
                    cn.Open();
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateResearchState";
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@TechnologyID", _technologyId );
                        cm.Parameters.AddWithValue( "@ResearchStateID", 2 );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }

        [Serializable()]
        private class AbortResearchCommand : CommandBase
        {
            private int _houseId;
            private int _technologyId;

            public AbortResearchCommand( int houseId, int technologyId )
            {
                _houseId = houseId;
                _technologyId = technologyId;
            }

            protected override void DataPortal_Execute()
            {
                using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                {
                    cn.Open();
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "DeleteResearch";
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@TechnologyID", _technologyId );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Technology
        public static void ResetTechnology( bool resetAll )
        {
            DataPortal.Execute<ResetTechnologyCommand>( new ResetTechnologyCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetTechnologyCommand : CommandBase
        {
            private bool _resetAll;

            public ResetTechnologyCommand( bool resetAll )
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
                        cm.CommandText = "ResetTechnology";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
