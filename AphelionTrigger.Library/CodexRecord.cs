using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Csla;
using Csla.Data;
using Csla.Validation;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class CodexRecord : BusinessBase<CodexRecord>
    {
        #region Business Methods

        private int _id;
        private int _parentCodexRecordId;

        private int _nodeDepth = 0;

        private string _title;
        private string _body;
        private List<string> _tags = new List<string>();

        private SmartDate _lastUpdatedDate;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        public int ParentCodexRecordID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _parentCodexRecordId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _parentCodexRecordId != value )
                {
                    _parentCodexRecordId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int NodeDepth
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _nodeDepth;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _nodeDepth != value )
                {
                    _nodeDepth = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Title
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _title;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _title != value )
                {
                    _title = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Body
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _body;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = string.Empty;
                if ( _body != value )
                {
                    _body = value;
                    PropertyHasChanged();
                }
            }
        }

        public List<string> Tags
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _tags ?? new List<string>();
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( value == null ) value = new List<string>();
                if ( _tags != value )
                {
                    _tags = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate LastUpdatedDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _lastUpdatedDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _lastUpdatedDate != value )
                {
                    _lastUpdatedDate = value;
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

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Title" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Title", 255 ) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Body" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Body", 8000 ) );
        }

        protected override void AddInstanceAuthorizationRules()
        {
            ValidationRules.AddInstanceRule( new RuleHandler( NoLoops ), "ParentCodexRecordID" );
        }

        private bool NoLoops( object target, Csla.Validation.RuleArgs e )
        {
            if ( _parentCodexRecordId == 0 && !this.IsNew ) return true;

            CodexRecordList list = CodexRecordList.GetCodexRecordList( _parentCodexRecordId );
            int id = _id;
            foreach ( CodexRecord record in list )
            {
                if ( id == record.ID )
                {
                    e.Description = "Parent Record Would Create Tree Loop";
                    return false;
                }
            }

            return true;
        }


        #endregion

        #region Factory Methods
        public static CodexRecord NewCodexRecord()
        {
            return DataPortal.Create<CodexRecord>();
        }

        public static CodexRecord GetCodexRecord( int id )
        {
            // retrieve the record from the list cache.
            CodexRecord record = CodexRecord.NewCodexRecord();
            CodexRecordList list = CodexRecordList.GetCodexRecordList();

            foreach ( CodexRecord listRecord in list )
            {
                if ( listRecord.ID == id )
                {
                    record = listRecord;
                    break;
                }
            }

            return record;
        }

        public override CodexRecord Save()
        {
            return base.Save();
        }

        private CodexRecord()
        { /* require use of factory methods */ }

        internal CodexRecord( int id, int parentCodexRecordId, int nodeDepth, string title, string body, string tags, SmartDate lastUpdatedDate)
        {
            _id = id;
            _parentCodexRecordId = parentCodexRecordId;

            _nodeDepth = nodeDepth;

            _title = title;
            _body = body;


            _tags.Clear();
            if ( tags.Length > 0 )
            {
                string[] splitTags = tags.Split( ',' );
                foreach ( string tag in splitTags ) _tags.Add( tag );
            }

            _lastUpdatedDate = lastUpdatedDate;
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
            {
                _id = id;
            }
        }

        [RunLocal()]
        protected override void DataPortal_Create()
        {
            // nothing to initialize
            ValidationRules.CheckRules();
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetCodexRecord";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        if ( dr["ParentCodexRecordID"] != System.DBNull.Value ) _parentCodexRecordId = dr.GetInt32( "ParentCodexRecordID" );

                        _title = dr.GetString( "Title" );
                        _body = dr.GetString( "Body" );

                        string tags = dr.GetString( "Tags" );
                        _tags.Clear();
                        string[] splitTags = tags.Split( ',' );
                        foreach ( string tag in splitTags ) _tags.Add( tag );

                        _lastUpdatedDate = dr.GetSmartDate( "LastUpdatedDate" );
                    }
                }
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Insert()
        {
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    string tags = string.Empty;
                    foreach ( string tag in _tags ) tags += tags + ",";
                    tags = ( tags.Trim().EndsWith( "," ) ? tags.Trim().Remove( 0, tags.Length - 1 ) : tags );

                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "AddCodexRecord";
                    cm.Parameters.AddWithValue( "@ParentCodexRecordID", _parentCodexRecordId );
                    cm.Parameters.AddWithValue( "@Title", _title );
                    cm.Parameters.AddWithValue( "@Body", _body );
                    cm.Parameters.AddWithValue( "@Tags", tags );
                    SqlParameter param = new SqlParameter( "@NewId", SqlDbType.Int );
                    param.Direction = ParameterDirection.Output;
                    cm.Parameters.Add( param );

                    cm.ExecuteNonQuery();

                    _id = (int)cm.Parameters["@NewId"].Value;
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }

        [Transactional( TransactionalTypes.TransactionScope )]
        protected override void DataPortal_Update()
        {
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                if ( base.IsDirty )
                {
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        string tags = string.Empty;
                        foreach ( string tag in _tags ) tags += tags + ",";
                        tags = ( tags.EndsWith( "," ) ? tags.Remove( 0, tags.Length - 1 ) : tags );

                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateCodexRecord";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.Parameters.AddWithValue( "@ParentCodexRecordID", _parentCodexRecordId );
                        cm.Parameters.AddWithValue( "@Title", _title );
                        cm.Parameters.AddWithValue( "@Body", _body );
                        cm.Parameters.AddWithValue( "@Tags", tags );

                        cm.ExecuteNonQuery();
                    }
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }

        #endregion
    }
}
