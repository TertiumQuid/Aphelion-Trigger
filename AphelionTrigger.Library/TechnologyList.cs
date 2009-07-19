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
    public class TechnologyList : ReadOnlyListBase<TechnologyList, Technology>
    {
        #region Factory Methods

        /// <summary>
        /// Return an empty technology list, usually used as a placeholder
        /// for business operations or late binding.
        /// </summary>
        /// <returns></returns>
        public static TechnologyList NewTechnologyList()
        {
            return DataPortal.Create<TechnologyList>();
        }

        /// <summary>
        /// Return a list of all technologies. Passing a House ID will return
        /// available technologies and build progress for the given house.
        /// </summary>
        public static TechnologyList GetTechnologies( int id )
        {
            return DataPortal.Fetch<TechnologyList>( new Criteria( id ) );
        }

        /// <summary>
        /// Return a list of all technologies whose research has completed for a given house
        /// </summary>
        public static TechnologyList GetResearchedTechnologies( int houseId )
        {
            return DataPortal.Fetch<TechnologyList>( new ResearchedCriteria( houseId ) );
        }

        private TechnologyList()
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
        private class ResearchedCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public ResearchedCriteria( int id )
            { _id = id; }
        }

        [RunLocal()]
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( ResearchedCriteria criteria )
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
                    cm.CommandText = "GetTechnologies";
                    cm.Parameters.AddWithValue( "@HouseID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Technology tech = new Technology(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "FactionID" ),
                                dr.GetInt32( "HouseID" ),
                                dr.GetInt32( "GuildID" ),
                                dr.GetInt32( "TechnologyTypeID" ),
                                dr.GetInt32( "UnitID" ),
                                dr.GetInt32( "UnitClassID" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Faction" ),
                                dr.GetString( "House" ),
                                dr.GetString( "Guild" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "TechnologyType" ),
                                dr.GetString( "Unit" ),
                                dr.GetString( "UnitClass" ),
                                dr.GetInt32( "Attack" ),
                                dr.GetInt32( "Defense" ),
                                dr.GetInt32( "Plunder" ),
                                dr.GetInt32( "Capture" ),
                                dr.GetInt32( "Stun" ),
                                dr.GetInt32( "Experience" ),
                                dr.GetDecimal( "RepopulationRate" ),
                                dr.GetDecimal( "DepopulationRate" ),
                                dr.GetInt32( "ResearchCost" ),
                                dr.GetInt32( "ResearchTime" ),
                                dr.GetInt32( "ResearchTurns" ),
                                dr.GetInt32( "TimeSpent" ),
                                dr.GetInt32( "TurnsSpent" ),
                                dr.GetInt32( "CreditsSpent" ),
                                dr.GetInt32( "ResearchStateID" ),
                                dr.GetString( "ResearchState" ),
                                dr.GetSmartDate( "ResearchStartedDate" ) );

                            this.Add( tech );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( ResearchedCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetTechnologies";
                    cm.Parameters.AddWithValue( "@HouseID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            // if the research has been completed
                            if (dr.GetInt32( "ResearchStateID" ) == 4)
                            {
                                Technology tech = new Technology(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetInt32( "HouseID" ),
                                    dr.GetInt32( "GuildID" ),
                                    dr.GetInt32( "TechnologyTypeID" ),
                                    dr.GetInt32( "UnitID" ),
                                    dr.GetInt32( "UnitClassID" ),
                                    dr.GetString( "Name" ),
                                    dr.GetString( "Faction" ),
                                    dr.GetString( "House" ),
                                    dr.GetString( "Guild" ),
                                    dr.GetString( "Description" ),
                                    dr.GetString( "TechnologyType" ),
                                    dr.GetString( "Unit" ),
                                    dr.GetString( "UnitClass" ),
                                    dr.GetInt32( "Attack" ),
                                    dr.GetInt32( "Defense" ),
                                    dr.GetInt32( "Plunder" ),
                                    dr.GetInt32( "Capture" ),
                                    dr.GetInt32( "Stun" ),
                                    dr.GetInt32( "Experience" ),
                                    dr.GetDecimal( "RepopulationRate" ),
                                    dr.GetDecimal( "DepopulationRate" ),
                                    dr.GetInt32( "ResearchCost" ),
                                    dr.GetInt32( "ResearchTime" ),
                                    dr.GetInt32( "ResearchTurns" ),
                                    dr.GetInt32( "TimeSpent" ),
                                    dr.GetInt32( "TurnsSpent" ),
                                    dr.GetInt32( "CreditsSpent" ),
                                    dr.GetInt32( "ResearchStateID" ),
                                    dr.GetString( "ResearchState" ),
                                    dr.GetSmartDate( "ResearchStartedDate" ) );

                                this.Add( tech );
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
