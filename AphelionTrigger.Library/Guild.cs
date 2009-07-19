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
    public class Guild : BusinessBase<Guild>
    {
        #region Business Methods

        private int _id;
        private int _leaderHouseId;

        private int _cost = 0;

        private string _name;
        private string _description;
        private string _leaderHouseName;

        private string[] _memberNames;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int LeaderHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _leaderHouseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_leaderHouseId != value)
                {
                    _leaderHouseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Cost
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_cost != value)
                {
                    _cost = value;
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

        public string LeaderHouseName
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _leaderHouseName;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_leaderHouseName != value)
                {
                    _leaderHouseName = value;
                    PropertyHasChanged();
                }
            }
        }

        public string[] MemberNames
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _memberNames;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_memberNames != value)
                {
                    _memberNames = value;
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
            ValidationRules.AddRule( new RuleHandler( CommonRules.IntegerMinValue ), new CommonRules.IntegerMinValueRuleArgs( "LeaderHouseID", 1 ) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Name" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Name", 300 ) );
        }

        #endregion
        
        #region Factory Methods
        public static Guild NewGuild()
        {
            return DataPortal.Create<Guild>();
        }

        public static Guild GetGuild( int id )
        {
            return DataPortal.Fetch<Guild>( new Criteria( id ) );
        }

        public override Guild Save()
        {
            return base.Save();
        }

        private Guild()
        { /* require use of factory methods */ }

        internal Guild( int id, int leaderHouseId, string name, string description, string leaderHouseName, string memberNames )
        {
            _id = id;
            _leaderHouseId = leaderHouseId;

            _name = name;
            _description = description;
            _leaderHouseName = leaderHouseName;
            _memberNames = memberNames.EndsWith( "," ) ? memberNames.Substring( 0, memberNames.Length - 1 ).Split( ',' ) : memberNames.Split( ',' );
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
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetGuild";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _leaderHouseId = dr.GetInt32( "LeaderHouseID" );

                        _name = dr.GetString( "Name" );
                        _description = dr.GetString( "Description" );
                        _leaderHouseName = dr.GetString( "LeaderHouseName" );
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
                    cm.CommandText = "AddGuild";
                    cm.Parameters.AddWithValue( "@LeaderHouseID", _leaderHouseId );
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
                        cm.Parameters.AddWithValue( "@Name", _name );
                        cm.Parameters.AddWithValue( "@Description", _description );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion

        #region Membership
        public static void AddGuildMember( int guildId, int houseId )
        {
            DataPortal.Execute<AddGuildMemberCommand>( new AddGuildMemberCommand( guildId, houseId ) );
        }

        [Serializable()]
        private class AddGuildMemberCommand : CommandBase
        {
            private int _houseId;
            private int _guildId;

            public AddGuildMemberCommand( int guildId, int houseId )
            {
                _guildId = guildId;
                _houseId = houseId;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "AddGuildMember";
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@GuildID", _guildId );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Guilds
        public static void ResetGuilds( bool resetAll )
        {
            DataPortal.Execute<ResetGuildsCommand>( new ResetGuildsCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetGuildsCommand : CommandBase
        {
            private bool _resetAll;

            public ResetGuildsCommand( bool resetAll )
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
                        cm.CommandText = "ResetGuilds";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
