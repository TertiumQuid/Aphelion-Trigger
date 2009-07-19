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
    public class TechnologyTypeList : ReadOnlyListBase<TechnologyTypeList, TechnologyType>
    {
        #region Factory Methods

        private static TechnologyTypeList _list;

        /// <summary>
        /// Return a list of all technology types.
        /// </summary>
        public static TechnologyTypeList GetTechnologyTypeList()
        {
            if (_list == null) _list = DataPortal.Fetch<TechnologyTypeList>(new Criteria());

            return _list;
        }

        private TechnologyTypeList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static TechnologyTypeList that's been cached.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all technology types */ }

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
                    cm.CommandText = "GetTechnologyTypes";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            TechnologyType type = new TechnologyType( dr.GetInt32( "ID" ), dr.GetString( "Name" ) );
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
