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
    public class Message : BusinessBase<Message>
    {
        #region Business Methods

        private int _id;
        private int _threadId = 0;
        private int _messageTypeId = 0;
        private int _senderHouseId = 0;
        private int _threadCount = 0;

        private string _messageType;
        private string _senderHouse;
        private string _subject;
        private string _body;

        private bool _hasRead = false;
        private bool _hasViewed = false;

        private List<string> _recipients = new List<string>();

        private SmartDate _writeDate;

        // used for tracking messages that are guild invitations
        private int _guildInvitationStatusTypeID = 0;
        private string _guildInvitationStatusType;

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
        public int ThreadID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _threadId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_threadId != value)
                {
                    _threadId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int MessageTypeID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _messageTypeId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_messageTypeId != value)
                {
                    _messageTypeId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int SenderHouseID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _senderHouseId;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_senderHouseId != value)
                {
                    _senderHouseId = value;
                    PropertyHasChanged();
                }
            }
        }

        public int ThreadCount
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _threadCount;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_threadCount != value)
                {
                    _threadCount = value;
                    PropertyHasChanged();
                }
            }
        }

        public string MessageType
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _messageType;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_messageType != value)
                {
                    _messageType = value;
                    PropertyHasChanged();
                }
            }
        }

        public string SenderHouse
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _senderHouse;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (value == null) value = string.Empty;
                if (_senderHouse != value)
                {
                    _senderHouse = value;
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

        public SmartDate WriteDate
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _writeDate;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_writeDate != value)
                {
                    _writeDate = value;
                    PropertyHasChanged();
                }
            }
        }

        public bool HasRead
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _hasRead;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_hasRead != value)
                {
                    _hasRead = value;
                    PropertyHasChanged();
                }
            }
        }

        public bool HasViewed
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _hasViewed;
            }
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            set
            {
                if (_hasViewed != value)
                {
                    _hasViewed = value;
                    PropertyHasChanged();
                }
            }
        }

        public int GuildInvitationStatusTypeID
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _guildInvitationStatusTypeID;
            }
        }

        public string GuildInvitationStatusType
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                return _guildInvitationStatusType;
            }
        }

        public string Recipients
        {
            [System.Runtime.CompilerServices.MethodImpl( System.Runtime.CompilerServices.MethodImplOptions.NoInlining )]
            get
            {
                string str = string.Empty;
                foreach (string s in _recipients)
                    str += s + ",";

                if (str.EndsWith( "," )) str = str.Remove( str.Length - 1, 1 );

                return str;
            }
        }

        public bool IsGuildMessage
        {
            get { return _guildInvitationStatusTypeID > 0; }
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
            ValidationRules.AddRule( new RuleHandler( CommonRules.IntegerMinValue ), new CommonRules.IntegerMinValueRuleArgs( "SenderHouseID", 1) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.IntegerMinValue ), new CommonRules.IntegerMinValueRuleArgs( "MessageTypeID", 1) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Subject" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Subject", 200 ) );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringRequired ), "Body" );
            ValidationRules.AddRule( new RuleHandler( CommonRules.StringMaxLength ), new CommonRules.MaxLengthRuleArgs( "Body", 3000 ) );
        }

        #endregion

        #region Factory Methods
        public static Message NewMessage()
        {
            return DataPortal.Create<Message>();
        }

        public static Message GetMessage( int id, int houseId )
        {
            return DataPortal.Fetch<Message>( new Criteria( id, houseId ) );
        }

        public override Message Save()
        {
            return base.Save();
        }

        private Message()
        { /* require use of factory methods */ }

        internal Message( int id, int threadId, int messageTypeId, int senderHouseId, int threadCount, string messageType, string senderHouse, string subject, string body, SmartDate writeDate, bool hasRead, bool hasViewed, string recipients )
        {
            _id = id;
            _threadId = threadId;
            _messageTypeId = messageTypeId;
            _senderHouseId = senderHouseId;
            _threadCount = threadCount;

            _messageType = messageType;
            _senderHouse = senderHouse;
            _subject = subject;
            _body = body;

            _writeDate = writeDate;

            _hasRead = hasRead;
            _hasViewed = hasViewed;

            string[] ra = recipients.Split( ',' );
            foreach (string r in ra)
            {
                if (r.Trim().Length > 0) _recipients.Add( r );
            }
        }

        internal Message( int id, int threadId, int messageTypeId, int senderHouseId, int threadCount, string messageType, string senderHouse, string subject, string body, SmartDate writeDate, bool hasRead, bool hasViewed, string recipients, int guildInvitationStatusTypeID, string guildInvitationStatusType )
        {
            _id = id;
            _threadId = threadId;
            _messageTypeId = messageTypeId;
            _senderHouseId = senderHouseId;
            _threadCount = threadCount;

            _messageType = messageType;
            _senderHouse = senderHouse;
            _subject = subject;
            _body = body;

            _writeDate = writeDate;

            _hasRead = hasRead;
            _hasViewed = hasViewed;

            string[] ra = recipients.Split( ',' );
            foreach (string r in ra)
            {
                if (r.Trim().Length > 0) _recipients.Add( r );
            }

            _guildInvitationStatusTypeID = guildInvitationStatusTypeID;
            _guildInvitationStatusType = guildInvitationStatusType;
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

            private int _houseId;
            public int HouseID
            {
                get { return _houseId; }
            }

            public Criteria( int id, int houseId )
            { 
                _id = id;
                _houseId = houseId;
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
                    cm.CommandText = "GetMessage";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );
                    cm.Parameters.AddWithValue( "@HouseID", criteria.HouseID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        dr.Read();

                        _id = dr.GetInt32( "ID" );
                        _threadId = dr.GetInt32( "ThreadID" );
                        _messageTypeId = dr.GetInt32( "MessageTypeID" );
                        _senderHouseId = dr.GetInt32( "SenderHouseID" );
                        _threadCount = dr.GetInt32( "ThreadCount" );

                        _messageType = dr.GetString( "MessageType" );
                        _senderHouse = dr.GetString( "SenderHouse" );
                        _subject = dr.GetString( "Subject" );
                        _body = dr.GetString( "Body" );

                        _writeDate = dr.GetSmartDate( "WriteDate" );

                        _hasRead = dr.GetBoolean( "HasRead" );
                        _hasViewed = dr.GetBoolean( "HasViewed" );

                        _guildInvitationStatusTypeID = dr.GetInt32( "GuildInvitationStatusTypeID" );
                        _guildInvitationStatusType = dr.GetString( "GuildInvitationStatusType" );

                        string[] recipients = dr.GetString( "Recipients" ).Split( ',' );
                        foreach (string r in recipients)
                        {
                            if (r.Trim().Length > 0) _recipients.Add( r );
                        }
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
                    cm.CommandText = "AddMessage";
                    cm.Parameters.AddWithValue( "@MessageTypeID", _messageTypeId );
                    cm.Parameters.AddWithValue( "@ThreadID", _threadId );
                    cm.Parameters.AddWithValue( "@SenderHouseID", _senderHouseId );
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

        #endregion

        #region Viewed/Read
        public static void UpdateMessageHasRead( int messageId, int recipientHouseId )
        {
            DataPortal.Execute<UpdateMessageHasReadCommand>( new UpdateMessageHasReadCommand( messageId, recipientHouseId ) );
        }

        public static void UpdateMessageHasViewed( int messageId, int recipientHouseId )
        {
            DataPortal.Execute<UpdateMessageHasViewedCommand>( new UpdateMessageHasViewedCommand( messageId, recipientHouseId ) );
        }

        public static void UpdateMessagesHasViewed( int recipientHouseId )
        {
            DataPortal.Execute<UpdateMessagesHasViewedCommand>( new UpdateMessagesHasViewedCommand( recipientHouseId ) );
        }

        [Serializable()]
        private class UpdateMessageHasReadCommand : CommandBase
        {
            private int _messageId;
            private int _recipientHouseId;

            public UpdateMessageHasReadCommand( int messageId, int recipientHouseId )
            {
                _messageId = messageId;
                _recipientHouseId = recipientHouseId;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateMessageHasRead";
                        cm.Parameters.AddWithValue( "@MessageID", _messageId );
                        cm.Parameters.AddWithValue( "@RecipientHouseID", _recipientHouseId );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }

        [Serializable()]
        private class UpdateMessageHasViewedCommand : CommandBase
        {
            private int _messageId;
            private int _recipientHouseId;

            public UpdateMessageHasViewedCommand( int messageId, int recipientHouseId )
            {
                _messageId = messageId;
                _recipientHouseId = recipientHouseId;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateMessageHasViewed";
                        cm.Parameters.AddWithValue( "@MessageID", _messageId );
                        cm.Parameters.AddWithValue( "@RecipientHouseID", _recipientHouseId );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }

        [Serializable()]
        private class UpdateMessagesHasViewedCommand : CommandBase
        {
            private int _recipientHouseId;

            public UpdateMessagesHasViewedCommand( int recipientHouseId )
            {
                _recipientHouseId = recipientHouseId;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateMessagesHasViewed";
                        cm.Parameters.AddWithValue( "@RecipientHouseID", _recipientHouseId );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Guild Invitations
        public static void UpdateMessageGuildInvitationStatus( int messageId, int recipientHouseId, int status )
        {
            DataPortal.Execute<UpdateMessageGuildInvitationStatusCommand>( new UpdateMessageGuildInvitationStatusCommand( messageId, recipientHouseId, status ) );
        }

        [Serializable()]
        private class UpdateMessageGuildInvitationStatusCommand : CommandBase
        {
            private int _messageId;
            private int _recipientHouseId;
            private int _guildInvitationStatusTypeID;

            public UpdateMessageGuildInvitationStatusCommand( int messageId, int recipientHouseId, int guildInvitationStatusTypeID )
            {
                _messageId = messageId;
                _recipientHouseId = recipientHouseId;
                _guildInvitationStatusTypeID = guildInvitationStatusTypeID;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateMessageGuildInvitationStatus";
                        cm.Parameters.AddWithValue( "@MessageID", _messageId );
                        cm.Parameters.AddWithValue( "@RecipientHouseID", _recipientHouseId );
                        cm.Parameters.AddWithValue( "@GuildInvitationStatusTypeID", _guildInvitationStatusTypeID );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Add Recipients
        public static void AddRecipient( int messageId, int recipientHouseId, bool isInvitation )
        {
            DataPortal.Execute<AddRecipientCommand>( new AddRecipientCommand( messageId, recipientHouseId, isInvitation ) );
        }

        [Serializable()]
        private class AddRecipientCommand : CommandBase
        {
            private int _messageId;
            private int _recipientHouseId;
            private bool _isInvitation;

            public AddRecipientCommand( int messageId, int recipientHouseId, bool isInvitation )
            {
                _messageId = messageId;
                _recipientHouseId = recipientHouseId;
                _isInvitation = isInvitation;
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "AddMessageRecipient";
                        cm.Parameters.AddWithValue( "@MessageID", _messageId );
                        cm.Parameters.AddWithValue( "@RecipientHouseID", _recipientHouseId );
                        cm.Parameters.AddWithValue( "@IsInvitation", (_isInvitation ? 1 : 0) );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        #region Reset Messages
        public static void ResetMessages( bool resetAll )
        {
            DataPortal.Execute<ResetMessagesCommand>( new ResetMessagesCommand( resetAll ) );
        }

        [Serializable()]
        private class ResetMessagesCommand : CommandBase
        {
            private bool _resetAll;

            public ResetMessagesCommand( bool resetAll )
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
                        cm.CommandText = "ResetMessages";
                        cm.Parameters.AddWithValue( "@ResetAll", _resetAll );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}