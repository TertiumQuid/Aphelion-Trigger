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
    public class AdvancementList : ReadOnlyListBase<AdvancementList, Advancement>
    {
        #region Factory Methods

        /// <summary>
        /// Return a list of all advancements for a given house.
        /// </summary>
        public static AdvancementList GetAdvancementList( int houseId )
        {
            return DataPortal.Fetch<AdvancementList>( new Criteria( houseId ) );
        }

        /// <summary>
        /// Return a list of all viewed/unviewed advancements.
        /// </summary>
        public static AdvancementList GetAdvancementList( int houseId, bool hasViewed )
        {
            return DataPortal.Fetch<AdvancementList>( new ViewedCriteria( houseId, hasViewed ) );
        }

        private AdvancementList()
        { /* require use of factory methods */ }

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

        [Serializable()]
        private class ViewedCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            private bool _hasViewed;
            public bool HasViewed
            {
                get { return _hasViewed; }
            }

            public ViewedCriteria( int id, bool hasViewed )
            { 
                _id = id;
                _hasViewed = hasViewed;
            }
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( ViewedCriteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch( Criteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetAdvancements";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Advancement advancement = new Advancement(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "HouseID" ),
                                dr.GetInt32( "LevelID" ),
                                dr.GetInt32( "Rank" ),
                                dr.GetInt32( "Experience" ),
                                dr.GetInt32( "Intelligence" ),
                                dr.GetInt32( "Affluence" ),
                                dr.GetInt32( "Power" ),
                                dr.GetInt32( "Protection" ),
                                dr.GetInt32( "Speed" ),
                                dr.GetInt32( "Free" ),
                                dr.GetInt32( "FreePlaced" ),
                                dr.GetString( "House" ),
                                dr.GetBoolean( "HasViewed" ),
                                dr.GetSmartDate( "AdvancementDate" ) );

                            this.Add( advancement );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( ViewedCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetAdvancements";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            if (dr.GetBoolean( "HasViewed" )==criteria.HasViewed)
                            {
                                Advancement advancement = new Advancement(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "HouseID" ),
                                    dr.GetInt32( "LevelID" ),
                                    dr.GetInt32( "Rank" ),
                                    dr.GetInt32( "Experience" ),
                                    dr.GetInt32( "Intelligence" ),
                                    dr.GetInt32( "Affluence" ),
                                    dr.GetInt32( "Power" ),
                                    dr.GetInt32( "Protection" ),
                                    dr.GetInt32( "Speed" ),
                                    dr.GetInt32( "Free" ),
                                    dr.GetInt32( "FreePlaced" ),
                                    dr.GetString( "House" ),
                                    dr.GetBoolean( "HasViewed" ),
                                    dr.GetSmartDate( "AdvancementDate" ) );

                                    this.Add( advancement );
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
