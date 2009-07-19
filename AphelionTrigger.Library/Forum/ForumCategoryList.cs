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
    public class ForumCategoryList : ReadOnlyListBase<ForumCategoryList, ForumCategory>
    {
        #region Factory Methods

        private static ForumCategoryList _list;

        /// <summary>
        /// Return a list of all category.
        /// </summary>
        public static ForumCategoryList GetForumCategoryList()
        {
            if (_list == null) _list = DataPortal.Fetch<ForumCategoryList>( new Criteria() );

            return _list;
        }

        private ForumCategoryList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static ForumCategoryList that's been cached; do this if you update level information in 
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
        { /* no criteria - retrieve all categories */ }

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
                    cm.CommandText = "GetForumCategories";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            ForumCategory category = new ForumCategory(
                                dr.GetInt32( "ID" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ) );

                            this.Add( category );
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
