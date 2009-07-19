using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Security.Principal;
using Csla;

namespace AphelionTrigger.Library.Security
{
    [Serializable()]
    public class ATIdentity : ReadOnlyBase<ATIdentity>, IIdentity
    {
        #region Business Methods

        private bool _isAuthenticated;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _house = string.Empty;
        private string _faction = string.Empty;
        private string _guild = string.Empty;
        
        // used for lookups
        private int _id = 0;
        private int _houseId = 0;
        private int _factionId = 0;
        private int _guildId = 0;

        // house statistics
        private int _intelligence = 0;
        private int _power = 0;
        private int _protection = 0;
        private int _affluence = 0;
        private int _speed = 0;

        private List<string> _roles = new List<string>();

        private SpyList _agents;

        public string AuthenticationType
        {
            get { return "Csla"; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public string Name
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }

        public string House
        {
            get { return _house; }
        }

        public string Faction
        {
            get { return _faction; }
        }

        public string Guild
        {
            get { return _guild; }
        }

        public int ID
        {
            get { return _id; }
        }

        public int HouseID
        {
            get { return _houseId; }
        }

        public int FactionID
        {
            get { return _factionId; }
        }

        public int GuildID
        {
            get { return _guildId; }
        }

        public int Intelligence
        {
            get { return _intelligence; }
        }

        public int Power
        {
            get { return _power; }
        }

        public int Protection
        {
            get { return _protection; }
        }

        public int Affluence
        {
            get { return _affluence; }
        }

        public int Speed
        {
            get { return _speed; }
        }

        public SpyList Agents
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if ( _agents == null )
                {
                    _agents = SpyList.GetAgentSchema( _houseId, RecordScope.ShowNumbers );
                }

                return _agents;
            }
        }

        public void ClearAgentCache()
        {
            _agents = null;
        }

        public bool HasGuild
        {
            get { return _guildId > 0; }
        }

        public bool HasHouse
        {
            get { return _houseId > 0; }
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        internal bool IsInRole( string role )
        {
            foreach (string r in _roles)
            {
                if (r == role) return true;
            }

            return false;
        }

        internal static bool ExistsUsername( string username )
        {
            return DataPortal_ExistsUsername( username );
        }
        #endregion

        #region Factory Methods

        internal static ATIdentity UnauthenticatedIdentity()
        {
            return new ATIdentity();
        }

        internal static ATIdentity GetIdentity( string username, string password )
        {
            return DataPortal.Fetch<ATIdentity>( new Criteria( username, password ) );
        }

        internal static void Register( int ageId, int factionId, string name, string username, string email, string password )
        {
            DataPortal_Insert( new RegistrationCriteria( ageId, factionId, name, username, email, password ) );
        }

        private ATIdentity()
        {
            /* require use of factory methods */
        }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria
        {
            private string _username;
            public string Username
            {
                get { return _username; }
            }

            private string _password;
            public string Password
            {
                get { return _password; }
            }

            public Criteria( string username, string password )
            {
                _username = username;
                _password = password;
            }
        }

        [Serializable()]
        private class RegistrationCriteria
        {
            private int _ageId;
            private int _factionId;
            private string _name;
            private string _username;
            private string _password;
            private string _email;

            public int AgeID
            {
                get { return _ageId; }
            }

            public int FactionID
            {
                get { return _factionId; }
            }

            public string Name
            {
                get { return _name; }
            }

            public string Username
            {
                get { return _username; }
            }

            public string Email
            {
                get { return _email; }
            }

            public string Password
            {
                get { return _password; }
            }

            public RegistrationCriteria( int ageId, int factionId, string name, string username, string email, string password )
            {
                _ageId = ageId;
                _factionId = factionId;
                _name = name;
                _username = username;
                _email = email;
                _password = password;
            }
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandText = "Login";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue( "@Username", criteria.Username );
                    cm.Parameters.AddWithValue( "@Password", criteria.Password );
                    using (SqlDataReader dr = cm.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            _roles.Clear();
                            _id = dr.GetInt32( 0 );
                            _username = dr.GetString( 1 );
                            _password = dr.GetString( 2 );
                            _factionId = dr.GetInt32( 3 );
                            _faction = dr.GetString( 4 );
                            _houseId = dr.GetInt32( 5 );
                            _intelligence = dr.GetInt32( 6 );
                            _power = dr.GetInt32( 7 );
                            _protection = dr.GetInt32( 8 );
                            _affluence = dr.GetInt32( 9 );
                            _speed = dr.GetInt32( 10 );
                            _house = dr.GetString( 11 );
                            _guildId = dr.GetInt32( 12 );
                            _guild = dr.GetString( 13 );
                            _isAuthenticated = true;

                            if (dr.NextResult())
                            {
                                while (dr.Read())
                                {
                                    if (dr.GetString( 1 ).Length > 0) _roles.Add( dr.GetString( 1 ) );
                                }
                            }
                        }
                        else
                        {
                            _roles.Clear();
                            _id = 0;
                            _username = string.Empty;
                            _factionId = 0;
                            _faction = string.Empty;
                            _houseId = 0;
                            _intelligence = 0;
                            _power = 0;
                            _protection = 0;
                            _affluence = 0;
                            _speed = 0;
                            _isAuthenticated = false;
                        }
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        private static void DataPortal_Insert( RegistrationCriteria criteria )
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;

                ATConfiguration config = ATConfiguration.Instance;

                int userId = 0;

                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddUser";
                    cm.Parameters.AddWithValue( "@Username", criteria.Username );
                    cm.Parameters.AddWithValue( "@Email", criteria.Email );
                    cm.Parameters.AddWithValue( "@Password", criteria.Password );
                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    userId = (int)cm.Parameters["@NewId"].Value;
                }

                int houseId = 0;

                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddHouse";
                    cm.Parameters.AddWithValue( "@FactionID", criteria.FactionID );
                    cm.Parameters.AddWithValue( "@UserID", userId );
                    cm.Parameters.AddWithValue( "@Name", criteria.Name );
                    cm.Parameters.AddWithValue( "@Credits", config.StartingCredits );
                    cm.Parameters.AddWithValue( "@Turns", config.StartingTurns );
                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    houseId = (int)cm.Parameters["@NewId"].Value;
                }

                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddRegistration";
                    cm.Parameters.AddWithValue( "@AgeID", criteria.AgeID );
                    cm.Parameters.AddWithValue( "@UserID", userId );
                    cm.Parameters.AddWithValue( "@HouseID", houseId );

                    cm.ExecuteNonQuery();
                }

                House house = AphelionTrigger.Library.House.GetHouse( houseId );

                // for consistentcy's sake, add level advancement for 1st level
                Advancement advancement = Advancement.NewAdvancement();
                advancement.HouseID = house.ID;
                advancement.LevelID = house.Level.ID;
                advancement.Save();

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }

        private static bool DataPortal_ExistsUsername( string username )
        {
            bool exists = false;

            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandText = "ExistsUsername";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue( "@Username", username );
                    exists = Convert.ToBoolean( cm.ExecuteScalar() );
                }
            }

            return exists;
        }
        #endregion
    }
}
