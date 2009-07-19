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
    public class QuoteList : ReadOnlyListBase<QuoteList, Quote>
    {
        #region Factory Methods

        private static QuoteList _list;

        /// <summary>
        /// Return a list of all quotes.
        /// </summary>
        public static QuoteList GetQuoteList()
        {
            if (_list == null) _list = DataPortal.Fetch<QuoteList>( new Criteria() );

            return _list;
        }

        /// <summary>
        /// Return a list of quotes filtered by author
        /// by project name.
        /// </summary>
        public static QuoteList GetQuoteList( string author )
        {
            return DataPortal.Fetch<QuoteList>
              ( new FilteredCriteria( author ) );
        }

        private QuoteList()
        { /* require use of factory methods */ }

        /// <summary>
        /// Clears the static QuoteList that's been cached; only do this is you've added more quotes to the database
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access

        [Serializable()]
        private class Criteria
        { /* no criteria - retrieve all quotes */ }

        [Serializable()]
        private class FilteredCriteria
        {
            private string _author;
            public string Author
            {
                get { return _author; }
            }

            public FilteredCriteria( string author )
            {
                _author = author;
            }
        }

        private void DataPortal_Fetch( Criteria criteria )
        {
            // fetch with no filter
            Fetch( "" );
        }

        private void DataPortal_Fetch( FilteredCriteria criteria )
        {
            Fetch( criteria.Author );
        }

        private void Fetch( string nameFilter )
        {
            this.RaiseListChangedEvents = false;
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetQuotes";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        IsReadOnly = false;
                        while (dr.Read())
                        {
                            Quote quote = new Quote( dr.GetInt32( "ID" ), dr.GetString( "Text" ), dr.GetString( "Author" ) );

                            // apply filter if necessary
                            if ((nameFilter.Length == 0) || (quote.Author.IndexOf( nameFilter ) == 0))
                                this.Add( quote );
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
