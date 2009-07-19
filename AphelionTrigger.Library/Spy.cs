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
    public class Spy : BusinessBase<Spy>
    {
        #region Business Methods

        private int _id;
        private int _factionId;

        private string _name;
        private string _description;
        private string _faction;

        private int _larceny;
        private int _surveillance;
        private int _reconnaissance;
        private int _mice;
        private int _ambush;
        private int _sabotage;
        private int _expropriation;
        private int _inspection;
        private int _subversion;
        private int _counterIntelligence;

        private int _cost;

        private int _count = 0;

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
                if ( _factionId != value )
                {
                    _factionId = value;
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
                if ( value == null ) value = string.Empty;
                if ( _name != value )
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
                if ( value == null ) value = string.Empty;
                if ( _description != value )
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
                if ( value == null ) value = string.Empty;
                if ( _faction != value )
                {
                    _faction = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Larceny
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _larceny;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _larceny != value )
                {
                    _larceny = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Surveillance
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _surveillance;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _surveillance != value )
                {
                    _surveillance = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Reconnaissance
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _reconnaissance;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _reconnaissance != value )
                {
                    _reconnaissance = value;
                    PropertyHasChanged();
                }
            }
        }

        public int MICE
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _mice;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _mice != value )
                {
                    _mice = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Ambush
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _ambush;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _ambush != value )
                {
                    _ambush = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Sabotage
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _sabotage;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _sabotage != value )
                {
                    _sabotage = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Expropriation
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _expropriation;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _expropriation != value )
                {
                    _expropriation = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Inspection
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _inspection;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _inspection != value )
                {
                    _inspection = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Subversion
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _subversion;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _subversion != value )
                {
                    _subversion = value;
                    PropertyHasChanged();
                }
            }
        }

        public int CounterIntelligence
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _counterIntelligence;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _counterIntelligence != value )
                {
                    _counterIntelligence = value;
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
                if ( _cost != value )
                {
                    _cost = value;
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
                if ( _count != value )
                {
                    _count = value;
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
                summary.Append( "<tr><td style='font-weight:bold;'>Larceny:</td><td>" + _larceny.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Surveillance:</td><td>" + _surveillance.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Reconnaissance:</td><td>" + _reconnaissance.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>M.I.C.E.:</td><td>" + _mice.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Ambush:</td><td>" + _ambush.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Sabotage:</td><td>" + _sabotage.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Expropriation:</td><td>" + _expropriation.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Inspection:</td><td>" + _inspection.ToString() + "</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Subversion:</td><td>" + _subversion.ToString() + "</td></tr>" );
                summary.Append( "<tr><td colspan='2'>&nbsp;</td></tr>" );
                summary.Append( "<tr><td style='font-weight:bold;'>Cost:</td><td>" + _cost.ToString() + "</td></tr>" );
                if ( _count > 0 ) summary.Append( "<tr><td style='font-weight:bold;'>Count:</td><td>" + _count.ToString() + " (worth " + ( _count * _cost ).ToString() + ")</td></tr>" );
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

        /// <summary>
        /// Maximum number of spies that a House can have at one time
        /// </summary>
        public static int SpyCap( int intelligence )
        {
            return intelligence * 15;
        }
        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule( new RuleHandler( CommonRules.IntegerMinValue ), new CommonRules.IntegerMinValueRuleArgs( "FactionID", 1 ) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Name" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Name", 255 ) );
        }

        #endregion

        #region Factory Methods
        public static Spy NewSpy()
        {
            return DataPortal.Create<Spy>();
        }

        public static Spy GetSpy( int id )
        {
            return DataPortal.Fetch<Spy>( new Criteria( id ) );
        }

        public override Spy Save()
        {
            return base.Save();
        }

        private Spy()
        { /* require use of factory methods */ }

        internal Spy( int id, int factionId, int larceny, int surveillance, int reconnaissance, int mice, int ambush, int sabotage, int expropriation, int inspection, int subversion, int counterIntelligence, string name, string description, string faction, int cost, int count )
        {
            _id = id;
            _factionId = factionId;

            _larceny = larceny;
            _surveillance = surveillance;
            _reconnaissance = reconnaissance;
            _mice = mice;
            _ambush = ambush;
            _sabotage = sabotage;
            _expropriation = expropriation;
            _inspection = inspection;
            _subversion = subversion;
            _counterIntelligence = counterIntelligence;

            _name = name;
            _description = description;
            _faction = faction;

            _cost = cost;
            _count = count;
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
                    cm.CommandText = "GetSpy";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _factionId = dr.GetInt32( "FactionID" );

                        _larceny = dr.GetInt32( "Larceny" );
                        _surveillance = dr.GetInt32( "Surveillance" );
                        _reconnaissance = dr.GetInt32( "Reconnaissance" );
                        _mice = dr.GetInt32( "MICE" );
                        _ambush = dr.GetInt32( "Ambush" );
                        _sabotage = dr.GetInt32( "Sabotage" );
                        _expropriation = dr.GetInt32( "Expropriation" );
                        _inspection = dr.GetInt32( "Inspection" );
                        _subversion = dr.GetInt32( "Subversion" );
                        _counterIntelligence = dr.GetInt32( "CounterIntelligence" );

                        _name = dr.GetString( "Name" );
                        _description = dr.GetString( "Description" );
                        _faction = dr.GetString( "Faction" );

                        _cost = dr.GetInt32( "Cost" );
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
                    cm.CommandText = "AddSpy";
                    cm.Parameters.AddWithValue( "@FactionID", _factionId );
                    cm.Parameters.AddWithValue( "@Name", _name );
                    cm.Parameters.AddWithValue( "@Description", _description );
                    cm.Parameters.AddWithValue( "@Cost", _cost );
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
                        cm.CommandText = "UpdateSpy";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@FactionID", _factionId );
                        cm.Parameters.AddWithValue( "@Name", _name );
                        cm.Parameters.AddWithValue( "@Description", _description );
                        cm.Parameters.AddWithValue( "@Cost", _cost );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion

        #region Recruitment
        public static void AddAgents( int houseId, int spyId, int cost, int count )
        {
            DataPortal.Execute<AddAgentsCommand>( new AddAgentsCommand( houseId, spyId, cost, count ) );
        }

        [Serializable()]
        private class AddAgentsCommand : CommandBase
        {
            private int _houseId;
            private int _spyId;
            private int _cost;
            private int _count;

            public AddAgentsCommand( int houseId, int spyId, int cost, int count )
            {
                _houseId = houseId;
                _spyId = spyId;
                _cost = cost;
                _count = count;
            }

            protected override void DataPortal_Execute()
            {
                using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                {
                    cn.Open();
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "AddAgents";
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@SpyID", _spyId );
                        cm.Parameters.AddWithValue( "@Cost", _cost );
                        cm.Parameters.AddWithValue( "@Count", _count );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
