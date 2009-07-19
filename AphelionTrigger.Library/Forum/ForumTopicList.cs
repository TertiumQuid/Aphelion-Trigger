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
    public class ForumTopicList : ReadOnlyListBase<ForumTopicList, ForumTopic>
    {
        #region Factory Methods

        /// <summary>
        /// Return an empty forum topic list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static ForumTopicList NewForumTopicList()
        {
            return DataPortal.Create<ForumTopicList>();
        }

        /// <summary>
        /// Return a list of all topics for a given forum board.
        /// </summary>
        public static ForumTopicList GetForumTopicList( int id )
        {
            return DataPortal.Fetch<ForumTopicList>( new Criteria( id ) );
        }

        private ForumTopicList()
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
                    cm.CommandText = "GetForumTopics";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            ForumTopic topic = new ForumTopic(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "BoardID" ),
                                dr.GetInt32( "BoardTypeID" ),
                                dr.GetInt32( "Views" ),
                                dr.GetInt32( "PostCount" ),
                                dr.GetString( "Title" ),
                                dr.GetString( "Board" ),
                                dr.GetString( "LastPostSubject" ),
                                dr.GetString( "LastPostUser" ),
                                dr.GetString( "FirstPostUser" ),
                                dr.GetBoolean( "Sticky" ),
                                dr.GetBoolean( "Locked" ),
                                dr.GetSmartDate( "PostDate" ),
                                dr.GetSmartDate( "LastPostDate" ) );

                            this.Add( topic );
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
