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
    public class FactionList : ReadOnlyListBase<FactionList, Faction>
    {
        #region Factory Methods

        private static FactionList _list;

        /// <summary>
        /// Return a list of all levels.
        /// </summary>
        public static FactionList GetFactionList()
        {
            if (_list == null) _list = DataPortal.Fetch<FactionList>( new Criteria() );

            return _list;
        }

        /// <summary>
        /// Clears the static FactionList that's been cached; rarely if ever will this need to happen.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        private FactionList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all factions */ }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch();
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
                    cm.CommandText = "GetFactions";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Faction faction = new Faction(
                                dr.GetInt32( "ID" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Display" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "RegistrationText" ),
                                dr.GetString( "SmallFactionIconPath" ),
                                dr.GetString( "LargeFactionIconPath" ),
                                dr.GetInt32( "HousesCount" ),
                                dr.GetInt32( "FactionLeaderHouseID" ),
                                dr.GetString( "FactionLeaderHouse" ) );

                            this.Add( faction );
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
