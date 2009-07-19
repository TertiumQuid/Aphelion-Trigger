using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library.Logs
{
    [Serializable()]
    public class SystemLogList : ReadOnlyListBase<SystemLogList, SystemLog>
    {

        #region Factory Methods

        /// <summary>
        /// Return a list of all system logs.
        /// </summary>
        public static SystemLogList GetSystemLogList()
        {
            return DataPortal.Fetch<SystemLogList>( new Criteria() );
        }

        /// <summary>
        /// Return a list of system logs filtered by system log type
        /// </summary>
        public static SystemLogList GetSystemLogList( int systemTypeId )
        {
            return DataPortal.Fetch<SystemLogList>( new TypeCriteria( systemTypeId ) );
        }

        /// <summary>
        /// Return a list of system logs filtered by system log date range
        /// </summary>
        public static SystemLogList GetSystemLogList( DateTime startDate, DateTime endDate )
        {
            return DataPortal.Fetch<SystemLogList>( new DateCriteria( new SmartDate( startDate ), new SmartDate( endDate ) ) );
        }

        private SystemLogList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all projects */ }

        [Serializable()]
        private class TypeCriteria
        {
            private int _systemTypeId;

            public int SystemTypeID
            {
                get { return _systemTypeId; }
            }

            public TypeCriteria( int systemTypeId )
            {
                _systemTypeId = systemTypeId;
            }
        }

        [Serializable()]
        private class DateCriteria
        {
            private SmartDate _startDate;
            private SmartDate _endDate;

            public SmartDate StartDate
            {
                get { return _startDate; }
            }

            public SmartDate EndDate
            {
                get { return _endDate; }
            }

            public DateCriteria( SmartDate startDate, SmartDate endDate )
            {
                _startDate = startDate;
                _endDate = endDate;
            }
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( TypeCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( DateCriteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch( Criteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetSystemLogs";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            SystemLog log = new SystemLog(
                                dr.GetInt32( "ID" ),
                                ( (SystemLogType)dr.GetInt32( "SystemTypeID" ) ),
                                dr.GetString( "Application" ),
                                dr.GetString( "Source" ),
                                dr.GetString( "Message" ),
                                dr.GetString( "Details" ),
                                dr.GetSmartDate( "LogDate" ) );

                            this.Add( log );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( TypeCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {

                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetSystemLogs";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            int systemTypeId = dr.GetInt32( "SystemTypeID" );

                            if ( systemTypeId == criteria.SystemTypeID )
                            {
                                SystemLog log = new SystemLog(
                                    dr.GetInt32( "ID" ),
                                    ( (SystemLogType)dr.GetInt32( "SystemTypeID" ) ),
                                    dr.GetString( "Application" ),
                                    dr.GetString( "Source" ),
                                    dr.GetString( "Message" ),
                                    dr.GetString( "Details" ),
                                    dr.GetSmartDate( "LogDate" ) );

                                this.Add( log );
                            }
                        }
                        IsReadOnly = true;
                    }

                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( DateCriteria criteria )
        {
            // TODO
        }

        #endregion
    }
}
