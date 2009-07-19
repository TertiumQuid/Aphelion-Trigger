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
    public class ForumBoardList : ReadOnlyListBase<ForumBoardList, ForumBoard>
    {
        #region Factory Methods

        private static ForumBoardList _list;

        /// <summary>
        /// Return an empty board list, usually used as a placeholder
        /// for business operations or late binding.
        /// </summary>
        /// <returns></returns>
        public static ForumBoardList NewForumBoardList()
        {
            return DataPortal.Create<ForumBoardList>();
        }

        /// <summary>
        /// Return a list of all forum boards.
        /// </summary>
        public static ForumBoardList GetForumBoardList()
        {
            if (_list == null) _list = DataPortal.Fetch<ForumBoardList>();

            return _list;
        }

        /// <summary>
        /// Return a list of all forum boards for a given category.
        /// </summary>
        public static ForumBoardList GetForumBoardList( int id )
        {
            if (_list == null) _list = DataPortal.Fetch<ForumBoardList>();

            ForumBoardList boards = DataPortal.Create<ForumBoardList>();
            foreach (ForumBoard board in _list)
            {
                if (board.CategoryID == id) boards.Add( board );
            }

            return boards;
        }

        private ForumBoardList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static ForumBoardList that's been cached; do this if you update board information in 
        /// the database, otherwise just let the cache populate and do its work
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria { }

        [RunLocal()]
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

        private void DataPortal_Fetch()
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
                    cm.CommandText = "GetForumBoards";

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            ForumBoard board = new ForumBoard(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "CategoryID" ),
                                dr.GetInt32( "BoardTypeID" ),
                                dr.GetInt32( "LastTopicID" ),
                                dr.GetInt32( "TopicCount" ),
                                dr.GetInt32( "PostCount" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "BoardType" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "Category" ),
                                dr.GetString( "LastPostSubject" ),
                                dr.GetString( "LastPostUser" ),
                                dr.GetSmartDate( "LastPostDate" ) );

                            this.Add( board );
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
