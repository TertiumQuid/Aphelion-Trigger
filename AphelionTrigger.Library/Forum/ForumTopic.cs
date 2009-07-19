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
    public class ForumTopic : BusinessBase<ForumTopic>
    {
        #region Business Methods

        private int _id;
        private int _boardId;
        private int _boardTypeId;
        private int _views = 0;
        private int _postCount = 0;

        private string _title;
        private string _board;
        private string _lastPostSubject;
        private string _lastPostUser;
        private string _firstPostUser;

        private bool _sticky = false;
        private bool _locked = false;

        private SmartDate _postDate;
        private SmartDate _lastPostDate;

        [System.ComponentModel.DataObjectField( true, true )]
        public int ID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _id;
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
        public int BoardID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _boardId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_boardId != value)
                {
                    _boardId = value;
                    PropertyHasChanged();
                }
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
        public int BoardTypeID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _boardTypeId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if ( _boardTypeId != value )
                {
                    _boardTypeId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int Views
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _views;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_views != value)
                {
                    _views = value;
                    PropertyHasChanged();
                }
            }
        }

        public int PostCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _postCount;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_postCount != value)
                {
                    _postCount = value;
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
                if (value == null) value = string.Empty;
                if (_title != value)
                {
                    _title = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Board
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _board;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_board != value)
                {
                    _board = value;
                    PropertyHasChanged();
                }
            }
        }

        public string LastPostSubject
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _lastPostSubject;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_lastPostSubject != value)
                {
                    _lastPostSubject = value;
                    PropertyHasChanged();
                }
            }
        }

        public string LastPostUser
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _lastPostUser;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_lastPostUser != value)
                {
                    _lastPostUser = value;
                    PropertyHasChanged();
                }
            }
        }

        public string FirstPostUser
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _firstPostUser;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_firstPostUser != value)
                {
                    _firstPostUser = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate PostDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _postDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_postDate != value)
                {
                    _postDate = value;
                    PropertyHasChanged();
                }
            }
        }

        public SmartDate LastPostDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _lastPostDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_lastPostDate != value)
                {
                    _lastPostDate = value;
                    PropertyHasChanged();
                }
            }
        }

        public bool Sticky
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _sticky;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_sticky != value)
                {
                    _sticky = value;
                    PropertyHasChanged();
                }
            }
        }

        public bool Locked
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _locked;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_locked != value)
                {
                    _locked = value;
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
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Title", 100 ) );
        }

        #endregion

        #region Factory Methods
        public static ForumTopic NewForumTopic()
        {
            return DataPortal.Create<ForumTopic>();
        }

        public static ForumTopic GetForumTopic( int id )
        {
            return DataPortal.Fetch<ForumTopic>( new Criteria( id ) );
        }

        public override ForumTopic Save()
        {
            return base.Save();
        }

        private ForumTopic()
        { /* require use of factory methods */ }

        internal ForumTopic( int id, int boardId, int boardTypeId, int views, int postCount, string title, string board, string lastPostSubject, string lastPostUser, string firstPostUser, bool sticky, bool locked, SmartDate postDate, SmartDate lastPostDate )
        {
            _id = id;
            _boardId = boardId;
            _boardTypeId = boardTypeId;

            _views = views;
            _postCount = postCount;

            _title = title;
            _board = board;
            _lastPostSubject = lastPostSubject;
            _lastPostUser = lastPostUser;
            _firstPostUser = firstPostUser;

            _sticky = sticky;
            _locked = locked;

            _postDate = postDate;
            _lastPostDate = lastPostDate;
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

            public Criteria( int id)
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
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetForumTopic";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _boardId = dr.GetInt32( "BoardID" );
                        _boardTypeId = dr.GetInt32( "BoardTypeID" );
                                                
                        _views = dr.GetInt32( "Views" );
                        _postCount = dr.GetInt32( "PostCount" );

                        _title = dr.GetString( "Title" );
                        _board = dr.GetString( "Board" );
                        _lastPostSubject = dr.GetString( "LastPostSubject" );
                        _lastPostUser = dr.GetString( "LastPostUser" );
                        _firstPostUser = dr.GetString( "FirstPostUser" );

                        _sticky = dr.GetBoolean( "Sticky" );
                        _locked = dr.GetBoolean( "Locked" );

                        _postDate = dr.GetSmartDate( "PostDate" );
                        _lastPostDate = dr.GetSmartDate( "LastPostDate" );
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
                    cm.CommandText = "AddForumTopic";
                    cm.Parameters.AddWithValue( "@BoardID", _boardId );
                    cm.Parameters.AddWithValue( "@Title", _title );
                    cm.Parameters.AddWithValue( "@Sticky", _sticky );
                    cm.Parameters.AddWithValue( "@Locked", _locked );
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
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                ApplicationContext.LocalContext["cn"] = cn;
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "UpdateForumTopic";
                    cm.Parameters.AddWithValue( "@ID", _id );
                    cm.Parameters.AddWithValue( "@Title", _title );
                    cm.Parameters.AddWithValue( "@Sticky", _sticky );
                    cm.Parameters.AddWithValue( "@Locked", _locked );

                    cm.ExecuteNonQuery();
                }

                // removing of item only needed for local data portal
                if ( ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client )
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }

        #endregion

        #region Topic Views
        public static void UpdateForumTopicViews( int id )
        {
            DataPortal.Execute<UpdateForumTopicViewsCommand>( new UpdateForumTopicViewsCommand( id ) );
        }

        [Serializable()]
        private class UpdateForumTopicViewsCommand : CommandBase
        {
            private int _id;

            public UpdateForumTopicViewsCommand( int id )
            {
                _id = id;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateForumTopicViews";
                        cm.Parameters.AddWithValue( "@ID", _id );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
