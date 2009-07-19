using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class ForumCategory : BusinessBase<ForumCategory>
    {
        #region Business Methods

        private int _id;
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
        public static ForumCategory NewForumCategory()
        {
            return DataPortal.Create<ForumCategory>();
        }

        public static ForumCategory GetForumCategory( int id )
        {
            return DataPortal.Fetch<ForumCategory>( new Criteria( id ) );
        }

        public override ForumCategory Save()
        {
            return base.Save();
        }

        private ForumCategory()
        { /* require use of factory methods */ }

        internal ForumCategory( int id, string name, string description )
        {
            _id = id;

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
                    cm.CommandText = "GetForumCategory";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );

                        _name = dr.GetString( "Name" );
                        _description = dr.GetString( "Description" );
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
                    cm.CommandText = "AddForumCategory";
                    cm.Parameters.AddWithValue( "@Name", _name );
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

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Update()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                if (base.IsDirty)
                {
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateForumCategory";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@Name", _name );
                        cm.Parameters.AddWithValue( "@Description", _description );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }
        #endregion
    }
}
