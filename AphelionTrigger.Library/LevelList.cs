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
    public class LevelList : ReadOnlyListBase<LevelList, Level>
    {
        #region Business Methods
        public Level GetNextLevel( int currentLevelId )
        {
            Level current = GetLevel( currentLevelId );
            foreach (Level l in this)
            {
                if (l.Rank == (current.Rank + 1) && l.FactionID == current.FactionID) return l;
            }

            return null;
        }

        public Level GetLevel( int levelId )
        {
            foreach (Level l in this)
            {
                if (levelId == l.ID) return l;
            }

            return null;
        }
        #endregion

        #region Factory Methods

        private static LevelList _list;

        /// <summary>
        /// Return a list of all levels.
        /// </summary>
        public static LevelList GetLevelList()
        {
            if (_list == null) _list = DataPortal.Fetch<LevelList>( new Criteria() );

            return _list;
        }

        /// <summary>
        /// Return a list of all levels for a given faction.
        /// </summary>
        public static LevelList GetLevelList( int factionId )
        {
            return DataPortal.Fetch<LevelList>( new FactionCriteria( factionId ) );
        }

        private LevelList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static LevelList that's been cached; do this if you update level information in 
        /// the database, otherwise just let the cache populate and do its work
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all guilds */ }

        [Serializable()]
        private class FactionCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public FactionCriteria( int id )
            { _id = id; }
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
                    cm.CommandText = "GetLevels";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Level level = new Level(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "Rank" ),
                                dr.GetInt32( "FactionID" ),
                                dr.GetInt32( "Experience" ),
                                dr.GetInt32( "UnitCapacity" ),
                                dr.GetInt32( "SpyCapacity" ),
                                dr.GetInt32( "Intelligence" ),
                                dr.GetInt32( "Affluence" ),
                                dr.GetInt32( "Power" ),
                                dr.GetInt32( "Protection" ),
                                dr.GetInt32( "Speed" ),
                                dr.GetInt32( "Contingency" ),
                                dr.GetInt32( "Free" ),
                                dr.GetString( "Faction" ) );

                            this.Add( level );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( FactionCriteria criteria)
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetLevels";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            if (dr.GetInt32( "FactionID" ) == criteria.ID)
                            {
                                Level level = new Level(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "Rank" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetInt32( "Experience" ),
                                    dr.GetInt32( "UnitCapacity" ),
                                    dr.GetInt32( "SpyCapacity" ),
                                    dr.GetInt32( "Intelligence" ),
                                    dr.GetInt32( "Affluence" ),
                                    dr.GetInt32( "Power" ),
                                    dr.GetInt32( "Protection" ),
                                    dr.GetInt32( "Speed" ),
                                    dr.GetInt32( "Contingency" ),
                                    dr.GetInt32( "Free" ),
                                    dr.GetString( "Faction" ) );

                                this.Add( level );
                            }
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion
    }
}
