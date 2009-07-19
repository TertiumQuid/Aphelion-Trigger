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
    public class AgeList : ReadOnlyListBase<AgeList, Age>
    {
        #region Factory Methods

        private static AgeList _list;
        
        /// <summary>
        /// Return an empty age list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static AgeList NewAgeList()
        {
            return DataPortal.Create<AgeList>();
        }

        /// <summary>
        /// Return a list of all ages.
        /// </summary>
        public static AgeList GetAgeList()
        {
            if (_list == null) _list = DataPortal.Fetch<AgeList>( new Criteria() );

            return _list;
        }

        private AgeList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static AgeList that's been cached.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all ages */ }

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
                    cm.CommandText = "GetAges";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Age age = new Age(
                                dr.GetInt32( "ID" ),
                                dr.GetString( "Name" ),
                                dr.GetString( "Description" ),
                                dr.GetBoolean( "IsCurrent" ) );

                            this.Add( age );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion

        #region Current Age Command
        public static int CurrentAgeID()
        {
            AgeList list = AgeList.GetAgeList();

            foreach (Age age in list)
            {
                if (age.IsCurrent) return age.ID;
            }

            return 0;
        }

        public static void UpdateCurrentAge( int ageId )
        {
            DataPortal.Execute<UpdateCurrentAgeCommand>( new UpdateCurrentAgeCommand( ageId ) );
        }

        [Serializable()]
        private class UpdateCurrentAgeCommand : CommandBase
        {
            private int _ageId;

            public UpdateCurrentAgeCommand( int ageId )
            {
                _ageId = ageId;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateCurrentAge";
                        cm.Parameters.AddWithValue( "@AgeID", _ageId );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
