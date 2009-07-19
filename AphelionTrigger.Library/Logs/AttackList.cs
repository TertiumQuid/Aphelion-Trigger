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
    public class AttackList : ReadOnlyListBase<AttackList, Attack>
    {
        #region Factory Methods

        /// <summary>
        /// Return an empty attack log list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static AttackList NewAttackList()
        {
            return DataPortal.Create<AttackList>();
        }

        /// <summary>
        /// Return a list of all attacks involving a given house.
        /// </summary>
        public static AttackList GetAttackList( int id )
        {
            return DataPortal.Fetch<AttackList>( new Criteria( id ) );
        }

        /// <summary>
        /// Return a list of all houses recently attacking or attacked by a given house.
        /// </summary>
        public static AttackList GetEnemyAttackList( int id )
        {
            return DataPortal.Fetch<AttackList>( new EnemyCriteria( id ) );
        }

        /// <summary>
        /// Return a list of all attacks involving a given house in the last period of minutes.
        /// </summary>
        public static AttackList GetAttackList( int id, int minutes, int side )
        {
            return DataPortal.Fetch<AttackList>( new TimeCriteria( id, minutes, side ) );
        }

        private AttackList()
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

        [Serializable()]
        private class EnemyCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public EnemyCriteria( int id )
            {
                _id = id;
            }
        }

        [Serializable()]
        private class TimeCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            private int _minutes;
            public int Minutes
            {
                get { return _minutes; }
            }

            private int _side;
            public int Side
            {
                get { return _side; }
            }

            public TimeCriteria( int id, int minutes, int side )
            {
                _id = id;
                _minutes = minutes;

                // MOD to limit to stored procedure case selection 0-2
                _side = side % 3; 
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

        private void DataPortal_Fetch( EnemyCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( TimeCriteria criteria )
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
                    cm.CommandText = "GetAttacks";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Attack log = new Attack(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "AttackerHouseID" ),
                                dr.GetInt32( "DefenderHouseID" ),
                                dr.GetInt32( "Captured" ),
                                dr.GetInt32( "Plundered" ),
                                dr.GetInt32( "Stunned" ),
                                dr.GetInt32( "AttackerCasualties" ),
                                dr.GetInt32( "DefenderCasualties" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "AttackerHouseName" ),
                                dr.GetString( "DefenderHouseName" ),
                                dr.GetSmartDate( "AttackDate" ) );

                            this.Add( log );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( EnemyCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetAttacks";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Attack log = new Attack(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "AttackerHouseID" ),
                                dr.GetInt32( "DefenderHouseID" ),
                                dr.GetInt32( "Captured" ),
                                dr.GetInt32( "Plundered" ),
                                dr.GetInt32( "Stunned" ),
                                dr.GetInt32( "AttackerCasualties" ),
                                dr.GetInt32( "DefenderCasualties" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "AttackerHouseName" ),
                                dr.GetString( "DefenderHouseName" ),
                                dr.GetSmartDate( "AttackDate" ) );

                            bool addLob = true;
                            if (criteria.ID == log.AttackerHouseID) // house was attacking
                            {
                                foreach (Attack attack in this)
                                {
                                    if (attack.DefenderHouseID == log.AttackerHouseID || attack.DefenderHouseID == log.DefenderHouseID)
                                    {
                                        addLob = false;
                                        break;
                                    }
                                }
                            }
                            else // house was attacked
                            {
                                foreach (Attack attack in this)
                                {
                                    if (attack.AttackerHouseID == log.AttackerHouseID || attack.AttackerHouseID == log.DefenderHouseID)
                                    {
                                        addLob = false;
                                        break;
                                    }
                                }
                            }

                            if (addLob) this.Add( log );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( TimeCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetAttacks";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );
                    cm.Parameters.AddWithValue( "@BySide", criteria.Side );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            TimeSpan time = DateTime.Now - dr.GetSmartDate( "AttackDate" ).Date;
                            if (time.TotalMinutes < criteria.Minutes)
                            {
                                Attack log = new Attack(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "AttackerHouseID" ),
                                    dr.GetInt32( "DefenderHouseID" ),
                                    dr.GetInt32( "Captured" ),
                                    dr.GetInt32( "Plundered" ),
                                    dr.GetInt32( "Stunned" ),
                                    dr.GetInt32( "AttackerCasualties" ),
                                    dr.GetInt32( "DefenderCasualties" ),
                                    dr.GetString( "Description" ),
                                    dr.GetString( "AttackerHouseName" ),
                                    dr.GetString( "DefenderHouseName" ),
                                    dr.GetSmartDate( "AttackDate" ) );

                                this.Add( log );
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
