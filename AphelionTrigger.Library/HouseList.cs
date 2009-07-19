using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class HouseList : BusinessListBase<HouseList, House>
    {
        #region Business Methods

        public int LowestRank
        {
            get
            {
                int rank = 0;
                foreach ( House house in this )
                    if ( rank < house.Rank ) rank = house.Rank;

                return rank;
            }
        }
        #endregion

        #region Factory Methods
        /// <summary>
        /// Return an empty house list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static HouseList NewHouseList()
        {
            return DataPortal.Create<HouseList>();
        }

        /// <summary>
        /// Return a list of all houses.
        /// </summary>
        public static HouseList GetHouseList()
        {
            return DataPortal.Fetch<HouseList>( new Criteria() );
        }

        /// <summary>
        /// Return a list of all houses for a given faction.
        /// </summary>
        public static HouseList GetHouseList( int factionId )
        {
            return DataPortal.Fetch<HouseList>( new FactionCriteria( factionId ) );
        }

        private HouseList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all houses */ }

        [Serializable()]
        private class FactionCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public FactionCriteria( int id )
            {
                _id = id;
            }
        }

        [RunLocal()]
        private void DataPortal_Create()
        {
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch();
        }

        private void DataPortal_Fetch( FactionCriteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch()
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetHouses";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        while (dr.Read())
                        {
                            House house = new House(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "UserID" ),
                                dr.GetInt32( "FactionID" ),
                                dr.GetString( "Faction" ),
                                dr.GetString( "FactionDisplay" ),
                                dr.GetString( "Name" ),
                                dr.GetInt32( "Intelligence" ),
                                dr.GetInt32( "Power" ),
                                dr.GetInt32( "Protection" ),
                                dr.GetInt32( "Affluence" ),
                                dr.GetInt32( "Speed" ),
                                dr.GetInt32( "Contingency" ),
                                dr.GetInt32( "LevelID" ),
                                (double)dr.GetDecimal( "Ambition" ),
                                dr.GetInt32( "Turns" ),
                                dr.GetInt32( "Credits" ),
                                dr.GetInt32( "MilitiaCount" ),
                                dr.GetInt32( "MilitaryCount" ),
                                dr.GetInt32( "MercenaryCount" ),
                                dr.GetInt32( "AgentCount" ),
                                dr.GetInt32( "Rank" ),
                                dr.GetInt32( "LastRank" ),
                                dr.GetInt32( "Points" ),
                                dr.GetInt32( "Experience" ),
                                dr.GetInt32( "Attack" ),
                                dr.GetInt32( "Defense" ),
                                dr.GetInt32( "Capture" ),
                                dr.GetInt32( "Plunder" ),
                                dr.GetInt32( "Stun" ),
                                dr.GetInt32( "GuildID" ),
                                dr.GetString( "Guild" ),
                                dr.GetString( "SmallFactionIconPath" ),
                                dr.GetInt32( "VotedFactionLeaderHouseID" ),
                                dr.GetInt32( "FactionLeaderHouseID" ),
                                dr.GetInt32( "FactionVotingPower" ) );

                            this.Add( house );
                        }
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( FactionCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetHouses";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        while (dr.Read())
                        {
                            if (dr.GetInt32( "FactionID" ) == criteria.ID)
                            {
                                House house = new House(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "UserID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetString( "Faction" ),
                                    dr.GetString( "FactionDisplay" ),
                                    dr.GetString( "Name" ),
                                    dr.GetInt32( "Intelligence" ),
                                    dr.GetInt32( "Power" ),
                                    dr.GetInt32( "Protection" ),
                                    dr.GetInt32( "Affluence" ),
                                    dr.GetInt32( "Speed" ),
                                    dr.GetInt32( "Contingency" ),
                                    dr.GetInt32( "LevelID" ),
                                    (double)dr.GetDecimal( "Ambition" ),
                                    dr.GetInt32( "Turns" ),
                                    dr.GetInt32( "Credits" ),
                                    dr.GetInt32( "MilitiaCount" ),
                                    dr.GetInt32( "MilitaryCount" ),
                                    dr.GetInt32( "MercenaryCount" ),
                                    dr.GetInt32( "AgentCount" ),
                                    dr.GetInt32( "Rank" ),
                                    dr.GetInt32( "LastRank" ),
                                    dr.GetInt32( "Points" ),
                                    dr.GetInt32( "Experience" ),
                                    dr.GetInt32( "Attack" ),
                                    dr.GetInt32( "Defense" ),
                                    dr.GetInt32( "Capture" ),
                                    dr.GetInt32( "Plunder" ),
                                    dr.GetInt32( "Stun" ),
                                    dr.GetInt32( "GuildID" ),
                                    dr.GetString( "Guild" ),
                                    dr.GetString( "SmallFactionIconPath" ),
                                    dr.GetInt32( "VotedFactionLeaderHouseID" ),
                                    dr.GetInt32( "FactionLeaderHouseID" ),
                                    dr.GetInt32( "FactionVotingPower" ) );

                                this.Add( house );
                            }
                        }
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion
    }
}
