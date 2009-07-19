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
    public class Attack : BusinessBase<Attack>
    {
        #region Business Methods

        private int _id;
        private int _attackerHouseId;
        private int _defenderHouseId;
        private int _captured;
        private int _plundered;
        private int _stunned;
        private int _attackerCasualties;
        private int _defenderCasualties;

        private string _description;
        private string _attackerHouseName;
        private string _defenderHouseName;

        private SmartDate _attackDate;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int AttackerHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _attackerHouseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_attackerHouseId != value)
                {
                    _attackerHouseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int DefenderHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _defenderHouseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_defenderHouseId != value)
                {
                    _defenderHouseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Captured
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _captured;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_captured != value)
                {
                    _captured = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Plundered
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _plundered;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_plundered != value)
                {
                    _plundered = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Stunned
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _stunned;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _stunned != value )
                {
                    _stunned = value;
                    PropertyHasChanged();
                }
            }
        }

        public int AttackerCasualties
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _attackerCasualties;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_attackerCasualties != value)
                {
                    _attackerCasualties = value;
                    PropertyHasChanged();
                }
            }
        }

        public int DefenderCasualties
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _defenderCasualties;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_defenderCasualties != value)
                {
                    _defenderCasualties = value;
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
                if (value == null) value = string.Empty;
                if (_description != value)
                {
                    _description = value;
                    PropertyHasChanged();
                }
            }
        }

        public string AttackerHouseName
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _attackerHouseName;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_attackerHouseName != value)
                {
                    _attackerHouseName = value;
                    PropertyHasChanged();
                }
            }
        }

        public string DefenderHouseName
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _defenderHouseName;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_defenderHouseName != value)
                {
                    _defenderHouseName = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate AttackDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _attackDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_attackDate != value)
                {
                    _attackDate = value;
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
        public static Attack NewAttack()
        {
            return DataPortal.Create<Attack>();
        }

        public static Attack GetAttack( int id )
        {
            return DataPortal.Fetch<Attack>( new Criteria( id ) );
        }

        public override Attack Save()
        {
            return base.Save();
        }

        private Attack()
        { /* require use of factory methods */ }

        internal Attack( int id, int attackerHouseId, int defenderHouseId, int captured, int plundered, int stunned, int attackerCasualties, int defenderCasualties, string description, string attackerHouseName, string defenderHouseName, SmartDate attackDate )
        {
            _id = id;
            _attackerHouseId = attackerHouseId;
            _defenderHouseId = defenderHouseId;
            _captured = captured;
            _plundered = plundered;
            _stunned = stunned;
            _attackerCasualties = attackerCasualties;
            _defenderCasualties = defenderCasualties;
            _description = description;
            _attackerHouseName = attackerHouseName;
            _defenderHouseName = defenderHouseName;
            _attackDate = attackDate;
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

        [RunLocal()]
        protected override void DataPortal_Create()
        {
            ValidationRules.CheckRules();
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetAttack";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _attackerHouseId = dr.GetInt32( "AttackerHouseId" );
                        _defenderHouseId = dr.GetInt32( "DefenderHouseId" );
                        _captured = dr.GetInt32( "Captured" );
                        _plundered = dr.GetInt32( "Plundered" );
                        _stunned = dr.GetInt32( "Stunned" );
                        _attackerCasualties = dr.GetInt32( "AttackerCasualties" );
                        _defenderCasualties = dr.GetInt32( "DefenderCasualties" );

                        _description = dr.GetString( "Description" );
                        _attackerHouseName = dr.GetString( "AttackerHouseName" );
                        _defenderHouseName = dr.GetString( "DefenderHouseName" );

                        _attackDate = dr.GetSmartDate( "AttackDate" );
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Insert()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddAttack";
                    cm.Parameters.AddWithValue( "@AttackerHouseID", _attackerHouseId );
                    cm.Parameters.AddWithValue( "@DefenderHouseID", _defenderHouseId );
                    cm.Parameters.AddWithValue( "@Captured", _captured );
                    cm.Parameters.AddWithValue( "@Plundered", _plundered );
                    cm.Parameters.AddWithValue( "@Stunned", _stunned );
                    cm.Parameters.AddWithValue( "@AttackerCasualties", _attackerCasualties );
                    cm.Parameters.AddWithValue( "@DefenderCasualties", _defenderCasualties );
                    cm.Parameters.AddWithValue( "@Description", _description );
                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    _id = (int)cm.Parameters["@NewId"].Value;
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion

        #region Reset Attacks
        public static void ResetAttacks( bool resetAll )
        {
            DataPortal.Execute<ResetAttacksCommand>( new ResetAttacksCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetAttacksCommand : CommandBase
        {
            private bool _resetAll;

            public ResetAttacksCommand( bool resetAll )
            {
                _resetAll = resetAll;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "ResetAttacks";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
