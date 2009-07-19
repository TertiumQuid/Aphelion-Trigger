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
    public class ForumBoard : BusinessBase<ForumBoard>
    {
        #region Business Methods

        private int _id;
        private int _categoryId;
        private int _boardTypeId;
        private int _lastTopicId;

        private int _topicCount;
        private int _postCount;

        private string _name;
        private string _boardType;
        private string _description;
        private string _category;
        private string _lastPostSubject;
        private string _lastPostUser;

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
        public int CategoryID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _categoryId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
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
                if (_boardTypeId != value)
                {
                    _boardTypeId = value;
                    PropertyHasChanged();
                }
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
        public int LastTopicID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _lastTopicId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_lastTopicId != value)
                {
                    _lastTopicId = value;
                    PropertyHasChanged();
                }
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
        public int TopicCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _topicCount;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_topicCount != value)
                {
                    _topicCount = value;
                    PropertyHasChanged();
                }
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
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

        public string BoardType
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _boardType;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_boardType != value)
                {
                    _boardType = value;
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

        public string Category
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _category;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_category != value)
                {
                    _category = value;
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
        }

        #endregion

        #region Factory Methods
        public static ForumBoard NewForumBoard()
        {
            return DataPortal.Create<ForumBoard>();
        }

        public static ForumBoard GetForumBoard( int id )
        {
            return DataPortal.Fetch<ForumBoard>( new Criteria( id ) );
        }

        public override ForumBoard Save()
        {
            return base.Save();
        }

        private ForumBoard()
        { /* require use of factory methods */ }

        internal ForumBoard( int id, int categoryId, int boardTypeId, int lastTopicId, int topicCount, int postCount, string name, string boardType, string description, string category, string lastPostSubject, string lastPostUser, SmartDate lastPostDate )
        {
            _id = id;
            _categoryId = categoryId;
            _boardTypeId = boardTypeId;
            _lastTopicId = lastTopicId;

            _topicCount = topicCount;
            _postCount = postCount;

            _name = name;
            _boardType = boardType;
            _description = description;
            _category = category;
            _lastPostSubject = lastPostSubject;
            _lastPostUser = lastPostUser;
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
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetForumBoard";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _categoryId = dr.GetInt32( "CategoryID" );
                        _boardTypeId = dr.GetInt32( "BoardTypeID" );
                        _lastTopicId = dr.GetInt32( "LastTopicID" );

                        _topicCount = dr.GetInt32( "TopicCount" );
                        _postCount = dr.GetInt32( "PostCount" );

                        _name = dr.GetString( "Name" );
                        _boardType = dr.GetString( "BoardType" );
                        _description = dr.GetString( "Description" );
                        _category = dr.GetString( "Category" );
                        _lastPostSubject = dr.GetString( "LastPostSubject" );
                        _lastPostUser = dr.GetString( "LastPostUser" );

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
                    cm.CommandText = "AddForumBoard";
                    cm.Parameters.AddWithValue( "@CategoryID", _categoryId );
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
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "UpdateForumBoard";
                    cm.Parameters.AddWithValue( "@ID", _id );
                    cm.Parameters.AddWithValue( "@CategoryID", _categoryId );
                    cm.Parameters.AddWithValue( "@Name", _name );
                    cm.Parameters.AddWithValue( "@Description", _description );

                    cm.ExecuteNonQuery();
                }

                // removing of item only needed for local data portal
                if (ApplicationContext.ExecutionLocation == ApplicationContext.ExecutionLocations.Client)
                    ApplicationContext.LocalContext.Remove( "cn" );
            }
        }

        #endregion
    }
}
