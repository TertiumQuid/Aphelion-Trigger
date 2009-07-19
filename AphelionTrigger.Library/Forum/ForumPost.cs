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
    public class ForumPost : BusinessBase<ForumPost>
    {
        #region Business Methods

        private int _id;
        private int _topicId;
        private int _userId;
        
        private string _topic;
        private string _username;
        private string _subject;
        private string _body;

        private SmartDate _postDate;

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
        public int TopicID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _topicId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_topicId != value)
                {
                    _topicId = value;
                    PropertyHasChanged();
                }
            }
        }

        [System.ComponentModel.DataObjectField( true, true )]
        public int UserID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _userId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Topic
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _topic;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_topic != value)
                {
                    _topic = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Username
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _username;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_username != value)
                {
                    _username = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Subject
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _subject;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_subject != value)
                {
                    _subject = value;
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
                if (value == null) value = string.Empty;
                if (_body != value)
                {
                    _body = value;
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
                if (value == null) value = new SmartDate( DateTime.Today );
                if (_postDate != value)
                {
                    _postDate = value;
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
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Subject", 300 ) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Body" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Body", 5000 ) );
        }

        #endregion

        #region Factory Methods
        public static ForumPost NewForumPost()
        {
            return DataPortal.Create<ForumPost>();
        }

        public static ForumPost GetForumPost( int id )
        {
            return DataPortal.Fetch<ForumPost>( new Criteria( id ) );
        }

        public override ForumPost Save()
        {
            return base.Save();
        }

        private ForumPost()
        { /* require use of factory methods */ }

        internal ForumPost( int id, int topicId, int userId, string username, string topic, string subject, string body, SmartDate postDate )
        {
            _id = id;
            _topicId = topicId;
            _userId = userId;

            _username = username;
            _topic = topic;
            _subject = subject;
            _body = body;

            _postDate = postDate;
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
                    cm.CommandText = "GetForumPost";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _topicId = dr.GetInt32( "TopicID" );
                        _userId = dr.GetInt32( "UserID" );

                        _topic = dr.GetString( "Topic" );
                        _username = dr.GetString( "Username" );
                        _subject = dr.GetString( "Subject" );
                        _body = dr.GetString( "Body" );
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
                    cm.CommandText = "AddForumPost";
                    cm.Parameters.AddWithValue( "@TopicID", _topicId );
                    cm.Parameters.AddWithValue( "@UserID", _userId );
                    cm.Parameters.AddWithValue( "@Subject", _subject );
                    cm.Parameters.AddWithValue( "@Body", _body );
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
                    cm.CommandText = "UpdateForumPost";
                    cm.Parameters.AddWithValue( "@ID", _id );
                    cm.Parameters.AddWithValue( "@Subject", _subject );
                    cm.Parameters.AddWithValue( "@Body", _body );

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
