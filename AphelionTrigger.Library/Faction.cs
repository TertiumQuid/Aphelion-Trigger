using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Csla;
using Csla.Data;
using Csla.Validation;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class Faction : BusinessBase<Faction>
    {
        #region Business Methods

        private int _id;

        private string _name;
        private string _display;
        private string _description;
        private string _registrationText;

        private string _smallFactionIconPath;
        private string _largeFactionIconPath;

        private int _housesCount;
        private int _factionLeaderHouseID;
        private string _factionLeaderHouse;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
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

        public string Display
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _display;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_display != value)
                {
                    _display = value;
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

        public string RegistrationText
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _registrationText;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_registrationText != value)
                {
                    _registrationText = value;
                    PropertyHasChanged();
                }
            }
        }

        public string SmallFactionIconPath
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _smallFactionIconPath;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_smallFactionIconPath != value)
                {
                    _smallFactionIconPath = value;
                    PropertyHasChanged();
                }
            }
        }

        public string LargeFactionIconPath
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _largeFactionIconPath;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_largeFactionIconPath != value)
                {
                    _largeFactionIconPath = value;
                    PropertyHasChanged();
                }
            }
        }

        public int HousesCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _housesCount;
            }
        }

        public int FactionLeaderHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _factionLeaderHouseID;
            }
        }

        public string FactionLeaderHouse
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _factionLeaderHouse;
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
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Name" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Name", 300 ) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Display" );

            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Name", 15 ) );
        }

        #endregion

        #region Factory Methods
        public static Faction NewFaction()
        {
            return DataPortal.Create<Faction>();
        }

        public static Faction GetFaction( int id )
        {
            return DataPortal.Fetch<Faction>( new Criteria( id ) );
        }

        public override Faction Save()
        {
            return base.Save();
        }

        private Faction()
        { /* require use of factory methods */ }

        internal Faction( int id, string name, string display, string description, string registrationText, string smallFactionIconPath, string largeFactionIconPath, int housesCount, int factionLeaderHouseID, string factionLeaderHouse )
        {
            _id = id;

            _name = name;
            _display = display;
            _description = description;
            _registrationText = registrationText;

            _smallFactionIconPath = smallFactionIconPath;
            _largeFactionIconPath = largeFactionIconPath;

            _housesCount = housesCount;
            _factionLeaderHouseID = factionLeaderHouseID;
            _factionLeaderHouse = factionLeaderHouse;
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
            {
                _id = id;
            }
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
                    cm.CommandText = "GetFaction";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );

                        _name = dr.GetString( "Name" );
                        _display = dr.GetString( "Display" );
                        _description = dr.GetString( "Description" );

                        _smallFactionIconPath = dr.GetString( "SmallFactionIconPath" );
                        _largeFactionIconPath = dr.GetString( "LargeFactionIconPath" );

                        _housesCount = dr.GetInt32( "HousesCount" );
                        _factionLeaderHouseID = dr.GetInt32( "FactionLeaderHouseID" );
                        _factionLeaderHouse = dr.GetString( "FactionLeaderHouse" );
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Insert()
        {
            throw new Exception( "Insert attempted for a Faction - Factions cannot be created through the API." );
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
                        cm.CommandText = "UpdateFaction";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@Name", _name );
                        cm.Parameters.AddWithValue( "@Display", _display );
                        cm.Parameters.AddWithValue( "@Description", _description );
                        cm.Parameters.AddWithValue( "@RegistrationText", _registrationText );
                        cm.Parameters.AddWithValue( "@SmallFactionIconPath", _smallFactionIconPath );
                        cm.Parameters.AddWithValue( "@LargeFactionIconPath", _largeFactionIconPath );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }

        #endregion
    }
}
