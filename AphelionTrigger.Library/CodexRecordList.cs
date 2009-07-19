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
    public class CodexRecordList : ReadOnlyListBase<CodexRecordList, CodexRecord>
    {
        #region Factory Methods

        private static CodexRecordList _list;

        /// <summary>
        /// Return an empty codex record list, usually used as a placeholder
        /// for filtering operations in the presentation layer.
        /// </summary>
        /// <returns></returns>
        public static CodexRecordList NewCodexRecordList()
        {
            return DataPortal.Create<CodexRecordList>();
        }

        /// <summary>
        /// Return a list of all codex records.
        /// </summary>
        public static CodexRecordList GetCodexRecordList()
        {
            if ( _list == null ) { _list = DataPortal.Fetch<CodexRecordList>( new Criteria( 0 ) ); }
            return _list;
        }

        /// <summary>
        /// Returns a list of all codex records that are children of a given record (i.e. id)
        /// </summary>
        public static CodexRecordList GetCodexRecordList( int id )
        {
            if ( _list == null ) { _list = DataPortal.Fetch<CodexRecordList>( new Criteria( id ) ); }
            CodexRecordList list = CodexRecordList.NewCodexRecordList();

            foreach ( CodexRecord record in _list )
            {
                if ( record.ParentCodexRecordID == id )
                    list.Add( record );
            }

            return list;
        }

        /// <summary>
        /// Return a list of all codex records that share a common tag.
        /// </summary>
        public static CodexRecordList GetCodexRecordList( string tag )
        {
            if ( _list == null ) { _list = DataPortal.Fetch<CodexRecordList>( new Criteria( 1 ) ); }
            CodexRecordList list = CodexRecordList.NewCodexRecordList();

            foreach ( CodexRecord record in _list )
                if ( record.Tags.Contains( tag ) ) list.Add( record );

            return list;
        }

        private CodexRecordList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static CodexRecordList that's been cached;
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
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
        private void DataPortal_Create()
        {
            IsReadOnly = false;
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            Fetch( criteria );
        }

        private void Fetch( Criteria criteria )
        {
            this.RaiseListChangedEvents = false;
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetCodexRecords";
                    cm.Parameters.AddWithValue( "@ID", criteria.ID );
                    using ( SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ) )
                    {
                        IsReadOnly = false;
                        while ( dr.Read() )
                        {
                            CodexRecord record = new CodexRecord(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "ParentCodexRecordID" ),
                                dr.GetInt32( "NodeDepth" ),
                                dr.GetString( "Title" ),
                                dr.GetString( "Body" ),
                                dr.GetString( "Tags" ),
                                dr.GetSmartDate( "LastUpdatedDate" ) );

                            this.Add( record );
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
