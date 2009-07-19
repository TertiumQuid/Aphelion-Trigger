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
    public class NewsPostList : ReadOnlyListBase<NewsPostList, NewsPost>
    {
        #region Factory Methods
        /// <summary>
        /// Return an empty news post list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static NewsPostList NewNewsPostList()
        {
            return DataPortal.Create<NewsPostList>();
        }

        /// <summary>
        /// Return a list of all guilds.
        /// </summary>
        public static NewsPostList GetNewsPostList()
        {
            return DataPortal.Fetch<NewsPostList>( new Criteria() );
        }

        private NewsPostList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all news posts */ }

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
                    cm.CommandText = "GetNewsPosts";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            NewsPost post = new NewsPost(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "PostedByUserID" ),
                                dr.GetString( "Title" ),
                                dr.GetString( "Body" ),
                                dr.GetString( "PostedByUser" ),
                                dr.GetDateTime( "NewsDate" ) );

                            this.Add( post );
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
