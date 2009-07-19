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
    public class ReportList : ReadOnlyListBase<ReportList, Report>
    {
        #region Business Methods
        private bool CanViewReportByIntelligence( int intelligence, int reportLevelId )
        {
            if (intelligence >= 75)
                return (reportLevelId < 5);
            if (intelligence >= 50)
                return (reportLevelId < 4);
            if (intelligence >= 25)
                return (reportLevelId < 3);
            if (intelligence < 25)
                return (reportLevelId < 2);

            return false;
        }

        private bool CanViewReportByFaction( int factionId, int reportFactionId, int reportLevelId )
        {
            return (factionId == reportFactionId && reportFactionId < 3);
        }

        private bool CanViewReportByGuild( int guildId, int reportGuildId, int reportLevelId )
        {
            return ((guildId > 0) && (guildId == reportGuildId && reportLevelId < 4));
        }

        private bool CanViewReportByHouse( int houseId, int reportHouseId )
        {
            return (houseId == reportHouseId);
        }
        #endregion

        #region Factory Methods

        /// <summary>
        /// Return a list of all recent reports.
        /// </summary>
        public static ReportList GetReportList()
        {
            return DataPortal.Fetch<ReportList>( new Criteria() );
        }

        /// <summary>
        /// Return a list of recent reports filtered
        /// by a player's various data.
        /// </summary>
        public static ReportList GetReportList( int intelligence, int factionId, int guildId, int houseId )
        {
            return DataPortal.Fetch<ReportList>( new FilteredCriteria( intelligence, factionId, guildId, houseId ) );
        }
        /// <summary>
        /// Return a list of recent reports based
        /// on a prepopulated report list, then filtered 
        /// by a player's various data
        /// </summary>
        public static ReportList GetReportList( ReportList reports, int intelligence, int factionId, int guildId, int houseId )
        {
            return DataPortal.Fetch<ReportList>( new ReportListCriteria( reports, intelligence, factionId, guildId, houseId ) );
        }

        private ReportList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all projects */ }

        [Serializable()]
        private class FilteredCriteria
        {
            private int _intelligence;
            private int _factionId;
            private int _guildId;
            private int _houseId;

            public int Intelligence
            {
                get { return _intelligence; }
            }

            public int FactionID
            {
                get { return _factionId; }
            }

            public int GuildID
            {
                get { return _guildId; }
            }

            public int HouseID
            {
                get { return _houseId; }
            }

            public FilteredCriteria( int intelligence, int factionId, int guildId, int houseId )
            {
                _intelligence = intelligence;
                _factionId = factionId;
                _guildId = guildId;
                _houseId = houseId;
            }
        }

        [Serializable()]
        private class ReportListCriteria
        {
            private int _intelligence;
            private int _factionId;
            private int _guildId;
            private int _houseId;
            private ReportList _reports;

            public ReportList Reports
            {
                get { return _reports; }
            }

            public int Intelligence
            {
                get { return _intelligence; }
            }

            public int FactionID
            {
                get { return _factionId; }
            }

            public int GuildID
            {
                get { return _guildId; }
            }

            public int HouseID
            {
                get { return _houseId; }
            }

            public ReportListCriteria( ReportList reports, int intelligence, int factionId, int guildId, int houseId )
            {
                _reports = reports;
                _intelligence = intelligence;
                _factionId = factionId;
                _guildId = guildId;
                _houseId = houseId;
            }
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( FilteredCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( ReportListCriteria criteria )
        {
            Fetch( criteria );
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
                    cm.CommandText = "GetReports";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Report report = new Report(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "HouseID" ),
                                dr.GetInt32( "GuildID" ),
                                dr.GetInt32( "FactionID" ),
                                dr.GetInt32( "ReportLevelID" ),
                                dr.GetString( "House" ),
                                dr.GetString( "LevelName" ),
                                dr.GetString( "Signifier" ),
                                dr.GetString( "Message" ),
                                dr.GetSmartDate( "ReportDate" ) );

                            this.Add( report );
                        }
                        IsReadOnly = true;
                    }                    
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( FilteredCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
   
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetReports";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            int reportLevelId = dr.GetInt32( "ReportLevelID" );
                            int factionId = dr.GetInt32( "FactionID" );
                            int guildId = dr.GetInt32( "GuildID" );
                            int houseId = dr.GetInt32( "HouseID" );

                            if (CanViewReportByIntelligence( criteria.Intelligence, reportLevelId )
                                || CanViewReportByFaction( criteria.FactionID, factionId, reportLevelId )
                                || CanViewReportByGuild( criteria.GuildID, guildId, reportLevelId )
                                || CanViewReportByHouse( criteria.HouseID, houseId ))
                            {
                                Report report = new Report(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "HouseID" ),
                                    dr.GetInt32( "GuildID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetInt32( "ReportLevelID" ),
                                    dr.GetString( "House" ),
                                    dr.GetString( "LevelName" ),
                                    dr.GetString( "Signifier" ),
                                    dr.GetString( "Message" ),
                                    dr.GetSmartDate( "ReportDate" ) );

                                this.Add( report );
                            }
                        }
                        IsReadOnly = true;
                    }
                 
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( ReportListCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            IsReadOnly = false;
            foreach (Report report in criteria.Reports)
            {
                if (CanViewReportByIntelligence( criteria.Intelligence, report.ReportLevelID )
                    || CanViewReportByFaction( criteria.FactionID, report.FactionID, report.ReportLevelID )
                    || CanViewReportByGuild( criteria.GuildID, report.GuildID, report.ReportLevelID )
                    || CanViewReportByHouse( criteria.HouseID, report.HouseID ))
                {
                    Report rpt = new Report(
                        report.ID,
                        report.HouseID,
                        report.GuildID,
                        report.FactionID,
                        report.ReportLevelID,
                        report.House,
                        report.LevelName,
                        report.Signifier,
                        report.Message,
                        report.ReportDate );

                    this.Add( rpt );
                }
            }
            IsReadOnly = true;
            this.RaiseListChangedEvents = true;
        }

        #endregion
    }
}
