using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Csla.Validation;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class EspionageOperation : BusinessBase<EspionageOperation>
    {
        #region Business Methods

        private int _id;

        private int _cost;
        private int _turns;
        private int _experience;

        private int _detection;
        private int _success;

        private string _name;
        private string _description;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int Cost
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _cost;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _cost != value )
                {
                    _cost = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Turns
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _turns;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _turns != value )
                {
                    _turns = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Experience
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _experience;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _experience != value )
                {
                    _experience = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Detection
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _detection;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _detection != value )
                {
                    _detection = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Success
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _success;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _success != value )
                {
                    _success = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Name
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _name;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _name != value )
                {
                    _name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Description
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _description;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _description != value )
                {
                    _description = value;
                    PropertyHasChanged();
                }
            }
        }

        public override bool IsValid
        {
            get { return base.IsValid; }
        }

        public override bool IsDirty
        {
            get { return base.IsDirty; }
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        #endregion

        #region Factory Methods
        public static EspionageOperation GetEspionageOperation( int id )
        {
            return DataPortal.Fetch<EspionageOperation>( new Criteria( id ) );
        }

        private EspionageOperation()
        { /* require use of factory methods */ }

        internal EspionageOperation( int id, int cost, int turns, int experience, int detection, int success, string name, string description )
        {
            _id = id;

            _cost = cost;
            _turns = turns;
            _experience = experience;

            _detection = detection;
            _success = success;

            _name = name;
            _description = description;
        }

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
            { _id = id; }
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetEspionageOperation";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );

                        _name = dr.GetString( "Name" );
                        _description = dr.GetString( "Description" );

                        _cost = dr.GetInt32( "Cost" );
                        _experience = dr.GetInt32( "Experience" );
                        _turns = dr.GetInt32( "Turns" );

                        _detection = dr.GetInt32( "Detection" );
                        _success = dr.GetInt32( "Success" );
                    }
                }
            }
        }
        #endregion
    }
}
