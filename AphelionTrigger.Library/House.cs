using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class House : BusinessBase<House>
    {
        #region Business Methods

        private int _id;
        private int _userId;
        private int _factionId;
        private int _guildId;

        private string _name;
        private string _faction;
        private string _factionDisplay;
        private string _guild;

        private string _smallFactionIconPath;

        private int _intelligence;
        private int _power;
        private int _protection;
        private int _affluence;
        private int _speed;
        private int _contingency;

        private int _credits;
        private int _turns;

        private int _rank;
        private int _lastRank;
        private int _points;
        private int _experience;

        private int _levelId;

        private double _ambition;

        private int _militiaCount;
        private int _militaryCount;
        private int _mercenaryCount;
        private int _agentCount;

        private int _attack;
        private int _defense;
        private int _capture;
        private int _plunder;
        private int _stun;

        private int _votedFactionLeaderHouseId;
        private int _factionLeaderHouseId;
        private int _factionVotingPower;

        private Level _level;
        private Level _nextLevel;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int UserID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _userId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_userId != value)
                {
                    _userId = value;
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

        public string FactionDisplay
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _factionDisplay;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_factionDisplay != value)
                {
                    _factionDisplay = value;
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

        public string Guild
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _guild;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_guild != value)
                {
                    _guild = value;
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

        public int LastRank
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _lastRank;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _lastRank != value )
                {
                    _lastRank = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Points
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _points;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_points != value)
                {
                    _points = value;
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

        public int Defense
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _defense;
            }
        }

        public int Attack
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _attack;
            }
        }

        public int Capture
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _capture;
            }
        }

        public int Plunder
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _plunder;
            }
        }

        public int Stun
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _stun;
            }
        }

        public int Turns
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _turns;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_turns != value)
                {
                    _turns = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Credits
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _credits;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_credits != value)
                {
                    _credits = value;
                    PropertyHasChanged();
                }
            }
        }

        public double Ambition
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _ambition;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_ambition != value)
                {
                    _ambition = value;
                    PropertyHasChanged();
                }
            }
        }

        public int VotedFactionLeaderHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _votedFactionLeaderHouseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_votedFactionLeaderHouseId != value)
                {
                    _votedFactionLeaderHouseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int FactionLeaderHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _factionLeaderHouseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _factionLeaderHouseId != value )
                {
                    _factionLeaderHouseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int FactionVotingPower
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _factionVotingPower;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_factionVotingPower != value)
                {
                    _factionVotingPower = value;
                    PropertyHasChanged();
                }
            }
        }

        public int AgentCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _agentCount;
            }
        }

        public int UnitCapForcesCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _militiaCount + _militaryCount;
            }
        }

        public int ForcesCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _militiaCount + _militaryCount + _mercenaryCount;
            }
        }

        public int MilitiaCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _militiaCount;
            }
        }

        public int MilitaryCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _militaryCount;
            }
        }

        public int MercenaryCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _mercenaryCount;
            }
        }

        /// <summary>
        /// Returns, for a given house, which quarter of 100% that house's intelligence has reached. If intelligence is
        /// greater than 100, the quarter can potentially be greater than 4. 
        /// </summary>
        /// <param name="intelligence">A house's intelligence</param>
        /// <returns>Which quarter of 100% a given intelligence has reached: 1-4 (oo)</returns>
        public static int GetSecreyLevel( int intelligence )
        {
            double quarter = ((double)intelligence) / 25.00;
            return (int)Math.Ceiling( quarter );
        }

        /// <summary>
        /// Returns, for a given house, whether and how much the secrecy of a game action (like creating a report) 
        /// should be modified based on a given house's intellgience.
        /// </summary>
        /// <param name="intelligence">A house's intelligence</param>
        /// <returns>The secrecy offset: 0-2</returns>
        public static int GetSecrecyBonus( int intelligence )
        {
            return (intelligence < 50 ? 0 : (intelligence < 125 ? 1 : 2));
        }

        public Level Level
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if (_level == null)
                {
                    LevelList list = LevelList.GetLevelList();
                    _level = list.GetLevel( _levelId );
                }

                return _level;
            }
        }

        public Level NextLevel
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                if (_nextLevel == null)
                {
                    LevelList list = LevelList.GetLevelList();
                    _nextLevel = list.GetNextLevel( _levelId );
                }

                return _nextLevel;
            }
        }

        public bool Exists
        {
            get { return _id > 0; }
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
        /// Applies the modifier for a House's ambition to a given value.
        /// </summary>
        /// <param name="value">The numeric value to be modified by ambition</param>
        /// <param name="influence">On a scale of 0-1, how much influence ambition should have on the given value. Larger values may produce odd effects.</param>
        /// <returns>Modified value.</returns>
        public double ApplyAmbition( double value, double influence )
        {
            return value * ( ( ( _ambition / 100 ) * influence ) + ( 1 - influence ) );
        }

        public double ApplyFactionLeaderBonus( double value )
        {
            if ( FactionLeaderHouseID != ID ) return value;

            ATConfiguration config = ATConfiguration.Instance;
            return value + ( value * config.FactionLeaderBonus );
        }
        #endregion

        #region Factory Methods

        public static House NewHouse()
        {
            return DataPortal.Create<House>();
        }

        public static House GetHouse( int id )
        {
            return DataPortal.Fetch<House>( new Criteria( id ) );
        }

        public override House Save()
        {
            return base.Save();
        }

        private House()
        { /* require use of factory methods */ }

        internal House( int id, int userId, int factionId, string faction, string factionDisplay, string name, int intelligence, int power, int protection, int affluence, int speed, int contingency, int levelId, double ambition, int turns, int credits, int militiaCount, int militaryCount, int mercenaryCount, int agentCount, int rank, int lastRank, int points, int experience, int attack, int defense, int capture, int plunder, int stun, int guildId, string guild, string smallFactionIconPath, int votedFactionLeaderHouseId, int factionLeaderHouseId, int factionVotingPower )
        {
            _id = id;
            _userId = userId;
            _factionId = factionId;

            _name = name;
            _faction = faction;
            _factionDisplay = factionDisplay;

            _intelligence = intelligence;
            _power = power;
            _protection = protection;
            _affluence = affluence;
            _speed = speed;
            _contingency = contingency;

            _levelId = levelId;
            _ambition = ambition;
            _turns = turns;
            _credits = credits;
            _militiaCount = militiaCount;
            _militaryCount = militaryCount;
            _mercenaryCount = mercenaryCount;
            _agentCount = agentCount;

            _rank = rank;
            _lastRank = lastRank;

            _points = points;
            _experience = experience;
            _attack = attack;
            _defense = defense;
            _capture = capture;
            _plunder = plunder;
            _stun = stun;

            _guildId = guildId;
            _guild = guild;

            _votedFactionLeaderHouseId = votedFactionLeaderHouseId;
            _factionLeaderHouseId = factionLeaderHouseId;
            _factionVotingPower = factionVotingPower;

            _smallFactionIconPath = smallFactionIconPath;
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
                    cm.CommandText = "GetHouse";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        if (dr.Read())
                        {
                            _id = dr.GetInt32( "ID" );
                            _userId = dr.GetInt32( "UserID" );
                            _factionId = dr.GetInt32( "FactionID" );

                            _name = dr.GetString( "Name" );
                            _faction = dr.GetString( "Faction" );
                            _factionDisplay = dr.GetString( "FactionDisplay" );

                            _intelligence = dr.GetInt32( "Intelligence" );
                            _power = dr.GetInt32( "Power" );
                            _protection = dr.GetInt32( "Protection" );
                            _affluence = dr.GetInt32( "Affluence" );
                            _speed = dr.GetInt32( "Speed" );
                            _contingency = dr.GetInt32( "Contingency" );

                            _levelId = dr.GetInt32( "LevelID" );
                            _ambition = (double)dr.GetDecimal( "Ambition" );
                            _turns = dr.GetInt32( "Turns" );
                            _credits = dr.GetInt32( "Credits" );
                            _militiaCount = dr.GetInt32( "MilitiaCount" );
                            _militaryCount = dr.GetInt32( "MilitaryCount" );
                            _mercenaryCount = dr.GetInt32( "MercenaryCount" );
                            _agentCount = dr.GetInt32( "AgentCount" );

                            _rank = dr.GetInt32( "Rank" );
                            _lastRank = dr.GetInt32( "LastRank" );
                            _points = dr.GetInt32( "Points" );
                            _experience = dr.GetInt32( "Experience" );

                            _attack = dr.GetInt32( "Attack" );
                            _defense = dr.GetInt32( "Defense" );
                            _capture = dr.GetInt32( "Capture" );
                            _plunder = dr.GetInt32( "Plunder" );
                            _stun = dr.GetInt32( "Stun" );

                            _guildId = dr.GetInt32( "GuildID" );
                            _guild = dr.GetString( "Guild" );

                            _votedFactionLeaderHouseId = dr.GetInt32( "VotedFactionLeaderHouseID" );
                            _factionLeaderHouseId = dr.GetInt32( "FactionLeaderHouseID" );
                            _factionVotingPower = dr.GetInt32( "FactionVotingPower" );

                            _smallFactionIconPath = dr.GetString( "SmallFactionIconPath" );
                        }
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
                    cm.CommandText = "AddHouse";
                    cm.Parameters.AddWithValue( "@UserID", _userId );
                    cm.Parameters.AddWithValue( "@FactionID", _factionId );
                    cm.Parameters.AddWithValue( "@Name", _name );
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

        #region Update Faction Leader Vote
        public static void UpdateFactionLeaderVote( int houseId, int votedFactionLeaderHouseId )
        {
            DataPortal.Execute<UpdateFactionLeaderVoteCommand>( new UpdateFactionLeaderVoteCommand( houseId, votedFactionLeaderHouseId ) );
        }

        [Serializable()]
        private class UpdateFactionLeaderVoteCommand : CommandBase
        {
            private int _houseId;
            private int _votedFactionLeaderHouseId;

            public UpdateFactionLeaderVoteCommand( int houseId, int votedFactionLeaderHouseId )
            {
                _houseId = houseId;
                _votedFactionLeaderHouseId = votedFactionLeaderHouseId;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateFactionLeaderVote";
                        cm.Parameters.AddWithValue( "@HouseID", _houseId );
                        cm.Parameters.AddWithValue( "@VotedFactionLeaderHouseId", _votedFactionLeaderHouseId );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Turns
        public static void UpdateTurns( int id, int turns )
        {
            DataPortal.Execute<UpdateTurnsCommand>( new UpdateTurnsCommand( id, turns ) );
        }

        [Serializable()]
        private class UpdateTurnsCommand : CommandBase
        {
            private int _id;
            private int _turns;

            public UpdateTurnsCommand( int id, int turns )
            {
                _id = id;
                _turns = turns;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateTurns";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@Turns", _turns );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Credits
        public static void ResetCredits( bool resetAll )
        {
            DataPortal.Execute<ResetCreditsCommand>( new ResetCreditsCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetCreditsCommand : CommandBase
        {
            private bool _resetAll;

            public ResetCreditsCommand( bool resetAll )
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
                        cm.CommandText = "ResetCredits";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Ambition
        public static void ResetAmbition( bool resetAll )
        {
            DataPortal.Execute<ResetAmbitionCommand>( new ResetAmbitionCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetAmbitionCommand : CommandBase
        {
            private bool _resetAll;

            public ResetAmbitionCommand( bool resetAll )
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
                        cm.CommandText = "ResetAmbition";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Turns
        public static void ResetTurns( bool resetAll )
        {
            DataPortal.Execute<ResetTurnsCommand>( new ResetTurnsCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetTurnsCommand : CommandBase
        {
            private bool _resetAll;

            public ResetTurnsCommand( bool resetAll )
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
                        cm.CommandText = "ResetTurns";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Rankings
        public static void ResetRankings( bool resetAll )
        {
            DataPortal.Execute<ResetRankingsCommand>( new ResetRankingsCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetRankingsCommand : CommandBase
        {
            private bool _resetAll;

            public ResetRankingsCommand( bool resetAll )
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
                        cm.CommandText = "ResetRankings";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Stats
        public static void ResetStats( bool resetAll )
        {
            DataPortal.Execute<ResetStatsCommand>( new ResetStatsCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetStatsCommand : CommandBase
        {
            private bool _resetAll;

            public ResetStatsCommand( bool resetAll )
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
                        cm.CommandText = "ResetStats";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
