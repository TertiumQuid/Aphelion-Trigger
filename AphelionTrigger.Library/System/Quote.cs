using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class Quote : ReadOnlyBase<Quote>
    {
        #region Business Methods

        private int _id;
        private string _text;
        private string _author;

        public int ID
        {
            get { return _id; }
        }

        public string Author
        {
            get { return _author; }
        }

        public string Text
        {
            get { return _text; }
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        public override string ToString()
        {
            return _text;
        }

        #endregion

        #region Constructors

        private Quote()
        { /* require use of factory methods */ }

        internal Quote( int id, string text, string author )
        {
            _id = id;
            _text = text;
            _author = author;
        }
        #endregion
    }
}