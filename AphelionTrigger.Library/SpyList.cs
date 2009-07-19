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
    public class SpyList : ReadOnlyListBase<SpyList, Spy>
    {
        #region Business Methods
        public int Larceny
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach (Spy spy in this)
                {
                    if ( spy.Larceny > 0 ) total += ( spy.Larceny * spy.Count );
                }

                return total;
            }
        }

        public int Surveillance
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.Surveillance > 0 ) total += ( spy.Surveillance * spy.Count );
                }

                return total;
            }
        }

        public int Reconnaissance
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.Reconnaissance > 0 ) total += ( spy.Reconnaissance * spy.Count );
                }

                return total;
            }
        }

        public int MICE
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.MICE > 0 ) total += ( spy.MICE * spy.Count );
                }

                return total;
            }
        }

        public int Ambush
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.Ambush > 0 ) total += ( spy.Ambush * spy.Count );
                }

                return total;
            }
        }

        public int Sabotage
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.Sabotage > 0 ) total += ( spy.Sabotage * spy.Count );
                }

                return total;
            }
        }

        public int Expropriation
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.Expropriation > 0 ) total += ( spy.Expropriation * spy.Count );
                }

                return total;
            }
        }

        public int Inspection
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.Inspection > 0 ) total += ( spy.Inspection * spy.Count );
                }

                return total;
            }
        }

        public int Subversion
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.Subversion > 0 ) total += ( spy.Subversion * spy.Count );
                }

                return total;
            }
        }

        public int CounterIntelligence
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                int total = 0;

                foreach ( Spy spy in this )
                {
                    if ( spy.CounterIntelligence > 0 ) total += ( spy.CounterIntelligence * spy.Count );
                }

                return total;
            }
        }
        #endregion

        #region Factory Methods

        /// <summary>
        /// Return an empty spy list, usually used as a placeholder
        /// for business operations or late binding.
        /// </summary>
        /// <returns></returns>
        public static SpyList NewSpyList()
        {
            return DataPortal.Create<SpyList>();
        }

        /// <summary>
        /// Return a list of all spies.
        /// </summary>
        public static SpyList GetSpyList()
        {
            return DataPortal.Fetch<SpyList>();
        }

        /// <summary>
        /// Return a list of all spies by faction.
        /// </summary>
        public static SpyList GetSpyList( int factionId )
        {
            return DataPortal.Fetch<SpyList>( new Criteria( factionId ) );
        }

        /// <summary>
        /// Return a list of all spies that can be recruited for a house.
        /// </summary>
        public static SpyList GetAgentSchema( int houseId, RecordScope scope )
        {
            return DataPortal.Fetch<SpyList>( new HouseCriteria( houseId, scope ) );
        }

        private SpyList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        {
            private int _factionId;
            public int FactionID
            {
                get { return _factionId; }
            }

            public Criteria( int factionId )
            { _factionId = factionId; }
        }

        [Serializable()]
        private class HouseCriteria
        {
            private int _houseId;
            public int HouseID
            {
                get { return _houseId; }
            }

            private RecordScope _scope;
            public RecordScope Scope
            {
                get { return _scope; }
            }

            public HouseCriteria( int houseId, RecordScope scope )
            { 
                _houseId = houseId;
                _scope = scope;
            }
        }

        [RunLocal()]
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

        private void DataPortal_Fetch()
        {
            Fetch();
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( HouseCriteria criteria )
        {
            Fetch( criteria );
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
                    cm.CommandText = "GetSpies";

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            Spy spy = new Spy(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "FactionID" ),
                                dr.GetInt32( "Larceny" ),
                                dr.GetInt32( "Surveillance" ),
                                dr.GetInt32( "Reconnaissance" ),
                                dr.GetInt32( "MICE" ),
                                dr.GetInt32( "Ambush" ),
                                dr.GetInt32( "Sabotage" ),
                                dr.GetInt32( "Expropriation" ),
                                dr.GetInt32( "Inspection" ),
                                dr.GetInt32( "Subversion" ),
                                dr.GetInt32( "CounterIntelligence" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "Faction" ),
                                dr.GetInt32( "Cost" ),
                                0 );

                            this.Add( spy );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
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
                    cm.CommandText = "GetSpies";

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            if ( dr.GetInt32( "FactionID" ) == criteria.FactionID )
                            {
                                Spy spy = new Spy(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetInt32( "Larceny" ),
                                    dr.GetInt32( "Surveillance" ),
                                    dr.GetInt32( "Reconnaissance" ),
                                    dr.GetInt32( "MICE" ),
                                    dr.GetInt32( "Ambush" ),
                                    dr.GetInt32( "Sabotage" ),
                                    dr.GetInt32( "Expropriation" ),
                                    dr.GetInt32( "Inspection" ),
                                    dr.GetInt32( "Subversion" ),
                                    dr.GetInt32( "CounterIntelligence" ),
                                    dr.GetString( "Name" ),
                                    dr.GetString( "Description" ),
                                    dr.GetString( "Faction" ),
                                    dr.GetInt32( "Cost" ),
                                    0 );

                                this.Add( spy );
                            }
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( HouseCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetAgencySchema";
                    cm.Parameters.AddWithValue( "@ID", criteria.HouseID );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            if ( criteria.Scope == RecordScope.ShowAll || dr.GetInt32( "Count" ) > 0 )
                            {
                                Spy spy = new Spy(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetInt32( "Larceny" ),
                                    dr.GetInt32( "Surveillance" ),
                                    dr.GetInt32( "Reconnaissance" ),
                                    dr.GetInt32( "MICE" ),
                                    dr.GetInt32( "Ambush" ),
                                    dr.GetInt32( "Sabotage" ),
                                    dr.GetInt32( "Expropriation" ),
                                    dr.GetInt32( "Inspection" ),
                                    dr.GetInt32( "Subversion" ),
                                    dr.GetInt32( "CounterIntelligence" ),
                                    dr.GetString( "Name" ),
                                    dr.GetString( "Description" ),
                                    dr.GetString( "Faction" ),
                                    dr.GetInt32( "Cost" ),
                                    dr.GetInt32( "Count" ) );

                                this.Add( spy );
                            }
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
