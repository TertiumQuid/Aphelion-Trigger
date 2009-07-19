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
    public class EspionageOperationList : ReadOnlyListBase<EspionageOperationList, EspionageOperation>
    {
        #region Factory Methods

        private static EspionageOperationList _list;

        /// <summary>
        /// Return a list of all operations.
        /// </summary>
        public static EspionageOperationList GetEspionageOperationList()
        {
            if ( _list == null ) _list = DataPortal.Fetch<EspionageOperationList>();

            return _list;
        }

        private EspionageOperationList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static EspionageOperationList that's been cached; do this if you update operation information in 
        /// the database, otherwise just let the cache populate and do its work
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch()
        {
            Fetch();
        }

        private void Fetch()
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetEspionageOperations";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            EspionageOperation operation = new EspionageOperation(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "Cost" ),
                                dr.GetInt32( "Turns" ),
                                dr.GetInt32( "Experience" ),
                                dr.GetInt32( "Detection" ),
                                dr.GetInt32( "Success" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ) );

                            this.Add( operation );
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
