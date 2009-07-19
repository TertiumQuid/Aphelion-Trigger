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
    public class MessageList : ReadOnlyListBase<MessageList, Message>
    {
        #region Factory Methods

        /// <summary>
        /// Return an empty message list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static MessageList NewMessageList()
        {
            return DataPortal.Create<MessageList>();
        }

        /// <summary>
        /// Return a list of all messages involving a given house.
        /// </summary>
        public static MessageList GetMessageList( int id )
        {
            return DataPortal.Fetch<MessageList>( new Criteria( id ) );
        }

        /// <summary>
        /// Return a list of all messages involving a given house that 
        /// have not already been viewed.
        /// </summary>
        public static MessageList GetMessageList( int id, bool hasViewed )
        {
            return DataPortal.Fetch<MessageList>( new HasViewedCriteria( id, hasViewed ) );
        }

        /// <summary>
        /// Return a list of all messages in a thread
        /// </summary>
        public static MessageList GetMessageList( int id, int threadId )
        {
            return DataPortal.Fetch<MessageList>( new ThreadCriteria( id, threadId ) );
        }

        /// <summary>
        /// Return a list of all messages involving a given house.
        /// </summary>
        public static MessageList GetGuildMessageList( int id )
        {
            return DataPortal.Fetch<MessageList>( new GuildCriteria( id ) );
        }

        private MessageList()
        { /* require use of factory methods */ }

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

        [Serializable()]
        private class GuildCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public GuildCriteria( int id )
            {
                _id = id;
            }
        }

        [Serializable()]
        private class HasViewedCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            private bool _hasViewed;
            public bool HasViewed
            {
                get { return _hasViewed; }
            }

            public HasViewedCriteria( int id, bool hasViewed )
            {
                _id = id;
                _hasViewed = hasViewed;
            }
        }

        [Serializable()]
        private class ThreadCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            private int _threadId;
            public int ThreadID
            {
                get { return _threadId; }
            }

            public ThreadCriteria( int id, int threadId )
            {
                _id = id;
                _threadId = threadId;
            }
        }
        
        [RunLocal()]
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

        private void DataPortal_Fetch()
        {
            Fetch();
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( HasViewedCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( ThreadCriteria criteria )
        {
            Fetch( criteria );
        }

        private void DataPortal_Fetch( GuildCriteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch()
        {
            // Allow insertions to the list, since a parameterless
            // list means that the list is created as a placeholder.
            IsReadOnly = false;
        }

        private void Fetch( Criteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetMessages";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Message message = new Message(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "ThreadID" ),
                                dr.GetInt32( "MessageTypeID" ),
                                dr.GetInt32( "SenderHouseID" ),
                                dr.GetInt32( "ThreadCount" ),
                                dr.GetString( "MessageType" ),
                                dr.GetString( "SenderHouse" ),
                                dr.GetString( "Subject" ),
                                dr.GetString( "Body" ),
                                dr.GetSmartDate( "WriteDate" ),
                                dr.GetBoolean( "HasRead" ),
                                dr.GetBoolean( "HasViewed" ),
                                dr.GetString( "Recipients" ),
                                dr.GetInt32( "GuildInvitationStatusTypeID" ),
                                dr.GetString( "GuildInvitationStatusType" ) );

                            this.Add( message );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( HasViewedCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetMessages";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Message message = new Message(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "ThreadID" ),
                                dr.GetInt32( "MessageTypeID" ),
                                dr.GetInt32( "SenderHouseID" ),
                                dr.GetInt32( "ThreadCount" ),
                                dr.GetString( "MessageType" ),
                                dr.GetString( "SenderHouse" ),
                                dr.GetString( "Subject" ),
                                dr.GetString( "Body" ),
                                dr.GetSmartDate( "WriteDate" ),
                                dr.GetBoolean( "HasRead" ),
                                dr.GetBoolean( "HasViewed" ),
                                dr.GetString( "Recipients" ) );

                            // Add a message to the list only if the house is the recipient, 
                            // and they haven't viewed the message
                            if (message.SenderHouseID != criteria.ID && message.HasViewed == criteria.HasViewed)
                                this.Add( message );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( ThreadCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetThread";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );
                    cm.Parameters.AddWithValue( "@ThreadID", criteria.ThreadID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Message message = new Message(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "ThreadID" ),
                                dr.GetInt32( "MessageTypeID" ),
                                dr.GetInt32( "SenderHouseID" ),
                                dr.GetInt32( "ThreadCount" ),
                                dr.GetString( "MessageType" ),
                                dr.GetString( "SenderHouse" ),
                                dr.GetString( "Subject" ),
                                dr.GetString( "Body" ),
                                dr.GetSmartDate( "WriteDate" ),
                                dr.GetBoolean( "HasRead" ),
                                dr.GetBoolean( "HasViewed" ),
                                dr.GetString( "Recipients" ) );

                            this.Add( message );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( GuildCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetGuildMessages";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Message message = new Message(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "ThreadID" ),
                                dr.GetInt32( "MessageTypeID" ),
                                dr.GetInt32( "SenderHouseID" ),
                                dr.GetInt32( "ThreadCount" ),
                                dr.GetString( "MessageType" ),
                                dr.GetString( "SenderHouse" ),
                                dr.GetString( "Subject" ),
                                dr.GetString( "Body" ),
                                dr.GetSmartDate( "WriteDate" ),
                                dr.GetBoolean( "HasRead" ),
                                dr.GetBoolean( "HasViewed" ),
                                dr.GetString( "Recipients" ),
                                dr.GetInt32( "GuildInvitationStatusTypeID" ),
                                dr.GetString( "GuildInvitationStatusType" ) );

                            this.Add( message );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }
        #endregion
    }
}
