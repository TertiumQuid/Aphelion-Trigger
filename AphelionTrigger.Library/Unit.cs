using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Csla.Validation;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class Unit : BusinessBase<Unit>
    {
        #region Business Methods

        private int _id;
        private int _factionId;
        private int _unitClassId;

        private string _name;
        private string _description;
        private string _faction;
        private string _unitClass;

        private int _cost;

        private int _attack;
        private int _defense;
        private int _plunder;
        private int _capture;
        private int _stun;
        private int _experience;

        private int _attackTech = 0;
        private int _defenseTech = 0;
        private int _plunderTech = 0;
        private int _captureTech = 0;
        private int _stunTech = 0;
        private int _experienceTech = 0;

        private decimal _repopulationRateTech = 0;
        private decimal _depopulationRateTech = 0;

        private decimal _repopulationRate;
        private decimal _depopulationRate;

        private int _count = 0;
        private int _casualties = 0;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
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

        public int Cost
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _cost;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_cost != value)
                {
                    _cost = value;
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
                if (_experience != value)
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

        public int AttackTech
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _attackTech;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_attackTech != value)
                {
                    _attackTech = value;
                    PropertyHasChanged();
                }
            }
        }

        public int DefenseTech
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _defenseTech;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_defenseTech != value)
                {
                    _defenseTech = value;
                    PropertyHasChanged();
                }
            }
        }

        public int PlunderTech
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _plunderTech;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_plunderTech != value)
                {
                    _plunderTech = value;
                    PropertyHasChanged();
                }
            }
        }

        public int CaptureTech
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _captureTech;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_captureTech != value)
                {
                    _captureTech = value;
                    PropertyHasChanged();
                }
            }
        }

        public int StunTech
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _stunTech;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _stunTech != value )
                {
                    _stunTech = value;
                    PropertyHasChanged();
                }
            }
        }


        public decimal RepopulationRateTech
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _repopulationRateTech;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_repopulationRateTech != value)
                {
                    _repopulationRateTech = value;
                    PropertyHasChanged();
                }
            }
        }

        public decimal DepopulationRateTech
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _depopulationRateTech;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_depopulationRateTech != value)
                {
                    _depopulationRateTech = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ExperienceTech
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _experienceTech;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _experienceTech != value )
                {
                    _experienceTech = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Count
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _count;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_count != value)
                {
                    _count = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Casualties
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _casualties;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_casualties != value)
                {
                    _casualties = value;
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
                summary.Append( "<tr><td colspan='11'>" + _description + "</td></tr>" );
                summary.Append( "<tr><td colspan='11'>&nbsp;</td></tr>" );
                summary.Append( "<tr style='font-weight:bold;'><td>Attack</td><td>Defense</td><td>Plunder</td><td>Capture</td><td>Stun</td><td>Experience Value</td></tr>" );
                summary.Append( "<tr><td>" + _attack.ToString() + ( _attackTech > 0 ? "+ <span style='color:rgb(100,100,255);'>(" + _attackTech.ToString() + ")</span>" : "" ) + "</td>" );
                summary.Append( "<td>" + _defense.ToString() + ( _defenseTech > 0 ? "+ <span style='color:rgb(100,100,255);'>(" + _defenseTech.ToString() + ")</span>" : "" ) + "</td>" );
                summary.Append( "<td>" + _plunder.ToString() + ( _plunderTech > 0 ? "+ <span style='color:rgb(100,100,255);'>(" + _plunderTech.ToString() + ")</span>" : "" ) + "</td>" );
                summary.Append( "<td>" + _capture.ToString() + ( _captureTech > 0 ? "+ <span style='color:rgb(100,100,255);'>(" + _captureTech.ToString() + ")</span>" : "" ) + "</td>" );
                summary.Append( "<td>" + _stun.ToString() + ( _stunTech > 0 ? "+ <span style='color:rgb(100,100,255);'>(" + _stunTech.ToString() + ")</span>" : "" ) + "</td>" );
                summary.Append( "<td>" + _experience.ToString() + ( _experienceTech > 0 ? "+ <span style='color:rgb(100,100,255);'>(" + _experienceTech.ToString() + ")</span>" : "" ) + "</td></tr>" );
                summary.Append( "<tr><td colspan='11'>&nbsp;</td></tr>" );
                if ( _repopulationRate > 0 ) summary.Append( "<tr><td style='font-weight:bold;' colspan='2'>Repopulation Rate:</td><td colspan='9'>" + _repopulationRate.ToString() + "</td></tr>" );
                if ( _depopulationRate > 0 ) summary.Append( "<tr><td style='font-weight:bold;' colspan='2'>Depopulation Rate:</td><td colspan='9'>" + _depopulationRate.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;' colspan='2'>Cost:</td><td colspan='9'>" + _cost.ToString() + "</td></tr>" );
                if ( _count > 0 ) summary.Append( "<tr><td style='font-weight:bold;' colspan='2'>Count:</td><td colspan='9'>" + _count.ToString() + " (worth " + ( _count * _cost ).ToString() + ")</td></tr>" );
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

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule( new RuleHandler( CommonRules.IntegerMinValue ), new CommonRules.IntegerMinValueRuleArgs( "FactionID", 1 ) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.IntegerMinValue ), new CommonRules.IntegerMinValueRuleArgs( "UnitClassID", 1 ) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Name" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Name", 300 ) );
        }

        #endregion

        #region Factory Methods
        public static Unit NewUnit()
        {
            return DataPortal.Create<Unit>();
        }

        public static Unit GetUnit( int id )
        {
            return DataPortal.Fetch<Unit>( new Criteria( id ) );
        }

        public override Unit Save()
        {
            return base.Save();
        }

        private Unit()
        { /* require use of factory methods */ }

        internal Unit( int id, int factionId, int unitClassId, string name, string description, string faction, string unitClass, int cost, int attack, int defense, int plunder, int capture, int stun, int experience, decimal repopulationRate, decimal depopulationRate, int count, int attackTech, int defenseTech, int captureTech, int plunderTech, int stunTech, int experienceTech, decimal repopulationRateTech, decimal depopulationRateTech )
        {
            _id = id;
            _factionId = factionId;
            _unitClassId = unitClassId;

            _name = name;
            _description = description;
            _faction = faction;
            _unitClass = unitClass;

            _cost = cost;
            _attack = attack;
            _defense = defense;
            _plunder = plunder;
            _capture = capture;
            _stun = stun;
            _experience = experience;
            
            _repopulationRate = repopulationRate;
            _depopulationRate = depopulationRate;
            _count = count;

            _attackTech = attackTech;
            _defenseTech = defenseTech;
            _captureTech = CaptureTech;
            _plunderTech = plunderTech;
            _experienceTech = experienceTech;
            _stunTech = stunTech;
            _repopulationRateTech = repopulationRateTech;
            _depopulationRateTech = depopulationRateTech;
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
                    cm.CommandText = "GetUnit";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _factionId = dr.GetInt32( "FactionID" );
                        _unitClassId = dr.GetInt32( "UnitClassID" );

                        _name = dr.GetString( "Name" );
                        _description = dr.GetString( "Description" );
                        _faction = dr.GetString( "Faction" );
                        _unitClass = dr.GetString( "UnitClass" );

                        _cost = dr.GetInt32( "Cost" );
                        _attack = dr.GetInt32( "Attack" );
                        _defense = dr.GetInt32( "Defense" );
                        _plunder = dr.GetInt32( "Plunder" );
                        _capture = dr.GetInt32( "Capture" );
                        _stun = dr.GetInt32( "Stun" );
                        _experience = dr.GetInt32( "Experience" );

                        _repopulationRate = dr.GetDecimal( "RepopulationRate" );
                        _depopulationRate = dr.GetDecimal( "DepopulationRate" );
                        _count = dr.GetInt32( "Count" );
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
                    cm.CommandText = "AddUnit";
                    cm.Parameters.AddWithValue( "@FactionID", _factionId );
                    cm.Parameters.AddWithValue( "@UnitClassID", _unitClassId );
                    cm.Parameters.AddWithValue( "@Name", _name );
                    cm.Parameters.AddWithValue( "@Description", _description );
                    cm.Parameters.AddWithValue( "@Cost", _cost );
                    cm.Parameters.AddWithValue( "@Attack", _attack );
                    cm.Parameters.AddWithValue( "@Defense", _defense );
                    cm.Parameters.AddWithValue( "@Plunder", _plunder );
                    cm.Parameters.AddWithValue( "@Capture", _capture );
                    cm.Parameters.AddWithValue( "@Stun", _stun );
                    cm.Parameters.AddWithValue( "@Experience", _experience );
                    cm.Parameters.AddWithValue( "@RepopulationRate", _repopulationRate );
                    cm.Parameters.AddWithValue( "@DepopulationRate", _depopulationRate );
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
                        cm.CommandText = "UpdateUnit";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@FactionID", _factionId );
                        cm.Parameters.AddWithValue( "@UnitClassID", _unitClassId );
                        cm.Parameters.AddWithValue( "@Name", _name );
                        cm.Parameters.AddWithValue( "@Description", _description );
                        cm.Parameters.AddWithValue( "@Cost", _cost );
                        cm.Parameters.AddWithValue( "@Attack", _attack );
                        cm.Parameters.AddWithValue( "@Defense", _defense );
                        cm.Parameters.AddWithValue( "@Plunder", _plunder );
                        cm.Parameters.AddWithValue( "@Capture", _capture );
                        cm.Parameters.AddWithValue( "@Stun", _stun );
                        cm.Parameters.AddWithValue( "@Experience", _experience );
                        cm.Parameters.AddWithValue( "@RepopulationRate", _repopulationRate );
                        cm.Parameters.AddWithValue( "@DepopulationRate", _depopulationRate );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion

        #region Recruitment
        public static void AddForces( int houseId, int unitId, int cost, int count )
        {
            DataPortal.Execute<AddForcesCommand>( new AddForcesCommand( houseId, unitId, cost, count ) );
        }

        [Serializable()]
        private class AddForcesCommand : CommandBase
        {
            private int _houseId;
            private int _unitId;
            private int _cost;
            private int _count;

            public AddForcesCommand( int houseId, int unitId, int cost, int count )
            {
                _houseId = houseId;
                _unitId = unitId;
                _cost = cost;
                _count = count;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "AddForces";
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@UnitID", _unitId );
                        cm.Parameters.AddWithValue( "@Cost", _cost );
                        cm.Parameters.AddWithValue( "@Count", _count );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Forces
        public static void ResetForces( bool resetAll )
        {
            DataPortal.Execute<ResetForcesCommand>( new ResetForcesCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetForcesCommand : CommandBase
        {
            private bool _resetAll;

            public ResetForcesCommand( bool resetAll )
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
                        cm.CommandText = "ResetForces";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
