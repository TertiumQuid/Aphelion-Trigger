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
    public class ForumBoardTypeList : ReadOnlyListBase<ForumBoardTypeList, ForumBoardType>
    {
        #region Factory Methods

        private static ForumBoardTypeList _list;

        /// <summary>
        /// Return a list of all forum board types.
        /// </summary>
        public static ForumBoardTypeList GetQuoteList()
        {
            if (_list == null) _list = DataPortal.Fetch<ForumBoardTypeList>( new Criteria() );

            return _list;
        }

        private ForumBoardTypeList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static ForumBoardTypeList that's been cached.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all forum board types */ }

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
                    cm.CommandText = "GetForumBoardTypes";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            ForumBoardType type = new ForumBoardType( dr.GetInt32( "ID" ), dr.GetString( "Name" ) );
                            this.Add( type );
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
