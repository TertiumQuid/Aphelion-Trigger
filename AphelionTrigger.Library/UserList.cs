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
    public class UserList : ReadOnlyListBase<UserList, User>
    {
        #region Factory Methods

        private static UserList _list;
        
        /// <summary>
        /// Return an empty user list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static UserList NewUserList()
        {
            return DataPortal.Create<UserList>();
        }

        /// <summary>
        /// Return a list of all users.
        /// </summary>
        public static UserList GetUserList()
        {
            if (_list == null) _list = DataPortal.Fetch<UserList>( new Criteria() );

            return _list;
        }

        /// <summary>
        /// Return a list of all user recipients for a secific type of system log.
        /// </summary>
        public static UserList GetUserSystemLogRecipients( int id )
        {
            if ( _list == null ) _list = DataPortal.Fetch<UserList>( new SystemLogRecipientsCriteria( id ) );

            return _list;
        }

        private UserList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static UserList that's been cached.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access
        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all users */ }

        [Serializable()]
        private class SystemLogRecipientsCriteria
        {
            private int _id;
            public int ID
            {
                get { return _id; }
            }

            public SystemLogRecipientsCriteria( int id )
            { _id = id; }
        }

        [RunLocal()]
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch();
        }

        private void DataPortal_Fetch( SystemLogRecipientsCriteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch()
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetUsers";
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            User user = new User(
                                dr.GetInt32( "ID" ),
                                dr.GetString( "UserName" ),
                                dr.GetString( "Password" ),
                                dr.GetString( "Email" ),
                                dr.GetString( "Locatoin" ),
                                dr.GetString( "Signature" ),
                                dr.GetString( "WebsiteURL" ),
                                dr.GetString( "PersonalText" ),
                                dr.GetString( "ICQ" ),
                                dr.GetString( "AIM" ),
                                dr.GetString( "MSN" ),
                                dr.GetString( "YIM" ),
                                dr.GetSmartDate( "RegistrationDate" ) );

                            this.Add( user );
                        }
                        IsReadOnly = true;
                    }
                }
            }
            this.RaiseListChangedEvents = true;
        }

        private void Fetch( SystemLogRecipientsCriteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetUserSystemLogRecipients";
                    cm.Parameters.AddWithValue( "@SystemTypeID", criteria.ID );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            User user = new User(
                                dr.GetInt32( "ID" ),
                                dr.GetString( "UserName" ),
                                dr.GetString( "Password" ),
                                dr.GetString( "Email" ),
                                dr.GetString( "Locatoin" ),
                                dr.GetString( "Signature" ),
                                dr.GetString( "WebsiteURL" ),
                                dr.GetString( "PersonalText" ),
                                dr.GetString( "ICQ" ),
                                dr.GetString( "AIM" ),
                                dr.GetString( "MSN" ),
                                dr.GetString( "YIM" ),
                                dr.GetSmartDate( "RegistrationDate" ) );

                            this.Add( user );
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
