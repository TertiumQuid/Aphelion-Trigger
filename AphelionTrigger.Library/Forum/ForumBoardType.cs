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
    public class ForumBoardType : ReadOnlyBase<ForumBoardType>
    {
        #region Business Methods

        private int _id;
        private string _name;

        public int ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        protected override object GetIdValue()
        {
            return _id;
        }

        public override string ToString()
        {
            return _name;
        }

        #endregion

        #region Constructors

        private ForumBoardType()
        { /* require use of factory methods */ }

        internal ForumBoardType( int id, string name )
        {
            _id = id;
            _name = name;
        }
        #endregion
    }
}
