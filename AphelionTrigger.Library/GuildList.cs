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
    public class GuildList : ReadOnlyListBase<GuildList, Guild>
    {
        #region Factory Methods
        /// <summary>
        /// Return an empty guild list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static GuildList NewGuildList()
        {
            return DataPortal.Create<GuildList>();
        }

        /// <summary>
        /// Return a list of all guilds.
        /// </summary>
        public static GuildList GetGuildList()
        {
            return DataPortal.Fetch<GuildList>( new Criteria() );
        }

        private GuildList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all guilds */ }

        [RunLocal()]
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

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
                    cm.CommandText = "GetGuilds";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Guild guild = new Guild(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "LeaderHouseID" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "LeaderHouseName" ),
                                dr.GetString( "MemberNames" ) );

                            this.Add( guild );
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
