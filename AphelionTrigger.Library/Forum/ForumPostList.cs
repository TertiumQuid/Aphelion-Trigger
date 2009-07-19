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
    public class ForumPostList : ReadOnlyListBase<ForumPostList, ForumPost>
    {
        #region Factory Methods

        /// <summary>
        /// Return an empty forum post list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static ForumPostList NewForumPostList()
        {
            return DataPortal.Create<ForumPostList>();
        }

        /// <summary>
        /// Return a list of all posts for a given forum topic.
        /// </summary>
        public static ForumPostList GetForumPostList( int id )
        {
            return DataPortal.Fetch<ForumPostList>( new Criteria( id ) );
        }

        private ForumPostList()
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
            {
                _id = id;
            }
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

        private void Fetch()
        {
            // Allow insertions to the list, since a parameterless
            // list means that the list is created as a placeholder.
            IsReadOnly = false;
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
                    cm.CommandText = "GetForumPosts";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            ForumPost post = new ForumPost(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "TopicID" ),
                                dr.GetInt32( "UserID" ),
                                dr.GetString( "Username" ),
                                dr.GetString( "Topic" ),
                                dr.GetString( "Subject" ),
                                dr.GetString( "Body" ),
                                dr.GetSmartDate( "PostDate" ) );

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
