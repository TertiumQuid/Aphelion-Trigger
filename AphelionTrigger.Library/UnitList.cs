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
    public class UnitList : ReadOnlyListBase<UnitList, Unit>
    {
        #region Business Methods
        public void ApplyTechnologies( TechnologyList technologies )
        {
            foreach (Unit unit in this)
            {
                unit.AttackTech = 0;
                unit.DefenseTech = 0;
                unit.CaptureTech = 0;
                unit.PlunderTech = 0;
                unit.DepopulationRateTech = 0;
                unit.RepopulationRateTech = 0;

                foreach (Technology tech in technologies)
                {
                    if ((unit.UnitClassID == tech.UnitClassID || unit.ID == tech.UnitID) && (unit.FactionID == tech.FactionID))
                    {
                        unit.AttackTech += tech.Attack;
                        unit.DefenseTech += tech.Defense;
                        unit.CaptureTech += tech.Capture;
                        unit.PlunderTech += tech.Plunder;
                        unit.DepopulationRateTech += tech.DepopulationRate;
                        unit.RepopulationRateTech += tech.RepopulationRate;
                    }
                }
            }
        }

        public int TotalAttack
        {
            get
            {
                int attack = 0;
                foreach ( Unit unit in this )
                {
                    attack += ( ( unit.Attack + unit.AttackTech ) * unit.Count );
                }
                return attack;
            }
        }

        public int TotalDefense
        {
            get
            {
                int defense = 0;
                foreach ( Unit unit in this )
                {
                    defense += ( ( unit.Defense + unit.DefenseTech ) * unit.Count );
                }
                return defense;
            }
        }

        public int TotalPlunder
        {
            get
            {
                int plunder = 0;
                foreach ( Unit unit in this )
                {
                    plunder += ( ( unit.Plunder + unit.PlunderTech ) * unit.Count );
                }
                return plunder;
            }
        }

        public int TotalCapture
        {
            get
            {
                int capture = 0;
                foreach ( Unit unit in this )
                {
                    capture += ( ( unit.Capture + unit.CaptureTech ) * unit.Count );
                }
                return capture;
            }
        }

        public int MilitiaCount
        {
            get
            {
                int count = 0;
                foreach ( Unit unit in this )
                {
                    if ( unit.UnitClassID == 1 ) count += unit.Count;
                }
                return count;
            }
        }

        public int MilitaryCount
        {
            get
            {
                int count = 0;
                foreach ( Unit unit in this )
                {
                    if ( unit.UnitClassID == 2 ) count += unit.Count;
                }
                return count;
            }
        }

        public int MercenaryCount
        {
            get
            {
                int count = 0;
                foreach ( Unit unit in this )
                {
                    if ( unit.UnitClassID == 3 ) count += unit.Count;
                }
                return count;
            }
        }

        public int ForcesCount
        {
            get
            {
                int count = 0;
                foreach ( Unit unit in this )
                {
                    count += unit.Count;
                }
                return count;
            }
        }
        #endregion

        #region Factory Methods

        /// <summary>
        /// Return an empty unit list, usually used as a placeholder
        /// for business operations or late binding.
        /// </summary>
        /// <returns></returns>
        public static UnitList NewUnitList()
        {
            return DataPortal.Create<UnitList>();
        }

        /// <summary>
        /// Return a list of all units.
        /// </summary>
        public static UnitList GetUnits()
        {
            return DataPortal.Fetch<UnitList>( new Criteria() );
        }

        /// <summary>
        /// Return a list of all units controlled by a house.
        /// </summary>
        public static UnitList GetForces( int houseId )
        {
            return DataPortal.Fetch<UnitList>( new ForcesCriteria( houseId ) );
        }

        /// <summary>
        /// Return a list of all units available for recruitment to a house,
        /// and the house's current unit counts.
        /// </summary>
        public static UnitList GetRecruitmentSchema( int houseId )
        {
            return DataPortal.Fetch<UnitList>( new RecruitmentCriteria( houseId ) );
        }

        private UnitList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all units */ }

        [Serializable()]
        private class ForcesCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public ForcesCriteria( int id )
            { _id = id; }
        }

        [Serializable()]
        private class RecruitmentCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public RecruitmentCriteria( int id )
            { _id = id; }
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

        private void DataPortal_Fetch( ForcesCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( RecruitmentCriteria criteria )
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
                    cm.CommandText = "GetUnits";

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Unit unit = new Unit(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "FactionID" ),
                                dr.GetInt32( "UnitClassID" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "Faction" ),
                                dr.GetString( "UnitClass" ),
                                dr.GetInt32( "Cost" ),
                                dr.GetInt32( "Attack" ),
                                dr.GetInt32( "Defense" ),
                                dr.GetInt32( "Plunder" ),
                                dr.GetInt32( "Capture" ),
                                dr.GetInt32( "Stun" ),
                                dr.GetInt32( "Experience" ),
                                dr.GetDecimal( "RepopulationRate" ),
                                dr.GetDecimal( "DepopulationRate" ),
                                dr.GetInt32( "Count" ),
                                0,
                                0,
                                0,
                                0,
                                0,
                                0,
                                0,
                                0 );

                            this.Add( unit );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( ForcesCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetForces";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            if (dr.GetInt32( "Count" ) > 0)
                            {
                                Unit unit = new Unit(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetInt32( "UnitClassID" ),
                                    dr.GetString( "Name" ),
                                    dr.GetString( "Description" ),
                                    dr.GetString( "Faction" ),
                                    dr.GetString( "UnitClass" ),
                                    dr.GetInt32( "Cost" ),
                                    dr.GetInt32( "Attack" ),
                                    dr.GetInt32( "Defense" ),
                                    dr.GetInt32( "Plunder" ),
                                    dr.GetInt32( "Capture" ),
                                    dr.GetInt32( "Stun" ),
                                    dr.GetInt32( "Experience" ),
                                    dr.GetDecimal( "RepopulationRate" ),
                                    dr.GetDecimal( "DepopulationRate" ),
                                    dr.GetInt32( "Count" ),
                                    dr.GetInt32( "AttackTech" ),
                                    dr.GetInt32( "DefenseTech" ),
                                    dr.GetInt32( "CaptureTech" ),
                                    dr.GetInt32( "PlunderTech" ),
                                    dr.GetInt32( "StunTech" ),
                                    dr.GetInt32( "ExperienceTech" ),
                                    dr.GetDecimal( "RepopulationRateTech" ),
                                    dr.GetDecimal( "DepopulationRateTech" ) );

                                this.Add( unit );
                            }
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( RecruitmentCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetRecruitmentSchema";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Unit unit = new Unit(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "FactionID" ),
                                dr.GetInt32( "UnitClassID" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetString( "Faction" ),
                                dr.GetString( "UnitClass" ),
                                dr.GetInt32( "Cost" ),
                                dr.GetInt32( "Attack" ),
                                dr.GetInt32( "Defense" ),
                                dr.GetInt32( "Plunder" ),
                                dr.GetInt32( "Capture" ),
                                dr.GetInt32( "Stun" ),
                                dr.GetInt32( "Experience" ),
                                dr.GetDecimal( "RepopulationRate" ),
                                dr.GetDecimal( "DepopulationRate" ),
                                dr.GetInt32( "Count" ),
                                dr.GetInt32( "AttackTech" ),
                                dr.GetInt32( "DefenseTech" ),
                                dr.GetInt32( "CaptureTech" ),
                                dr.GetInt32( "PlunderTech" ),
                                dr.GetInt32( "StunTech" ),
                                dr.GetInt32( "ExperienceTech" ),
                                dr.GetDecimal( "RepopulationRateTech" ),
                                dr.GetDecimal( "DepopulationRateTech" ) );

                            this.Add( unit );
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
