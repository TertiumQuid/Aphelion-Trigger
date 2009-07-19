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
    public class UnitClassList : ReadOnlyListBase<UnitClassList, UnitClass>
    {
        #region Factory Methods

        private static UnitClassList _list;

        /// <summary>
        /// Return a list of all unit classes.
        /// </summary>
        public static UnitClassList GetUnitClassList()
        {
            if (_list == null) _list = DataPortal.Fetch<UnitClassList>(new Criteria());

            return _list;
        }

        private UnitClassList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static UnitClassList that's been cached.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all unit classes */ }

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
                    cm.CommandText = "GetUnitClasses";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            UnitClass type = new UnitClass( dr.GetInt32( "ID" ), dr.GetString( "Name" ) );
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
