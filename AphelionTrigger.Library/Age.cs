using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class Age : BusinessBase<Age>
    {
        #region Business Methods

        private int _id;
        private string _name;
        private string _description;

        private bool _isCurrent;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
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
                if (value == null) value = string.Empty;
                if (_name != value)
                {
                    _name = value;
                    PropertyHasChanged();
                }
            }
        }

        public bool IsCurrent
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _isCurrent;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_isCurrent != value)
                {
                    _isCurrent = value;
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
        public static Age NewAge()
        {
            return DataPortal.Create<Age>();
        }

        public override Age Save()
        {
            return base.Save();
        }

        private Age()
        { /* require use of factory methods */ }

        internal Age( int id, string name, string description, bool isCurrent )
        {
            _id = id;

            _name = name;
            _description = description;

            _isCurrent = isCurrent;
        }

        #endregion

        #region Data Access
        [RunLocal()]
        protected override void DataPortal_Create()
        {
            ValidationRules.CheckRules();
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
                    cm.CommandText = "AddAge";
                    cm.Parameters.AddWithValue( "@Name", _name );
                    cm.Parameters.AddWithValue( "@Description", _description );
                    cm.Parameters.AddWithValue( "@IsCurrent", _isCurrent );
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

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Update()
        {
            throw new Exception( "Update attempted on an Age - Ages are immutable, and cannot be updated." );
        }
        #endregion
    }
}
