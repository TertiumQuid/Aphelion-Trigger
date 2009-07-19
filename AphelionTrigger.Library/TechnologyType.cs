using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Csla;
using Csla.Data;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class TechnologyType : ReadOnlyBase<TechnologyType>
    {
        #region Business Methods

        private int _id;
        private string _name;

        public int Id
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

        private TechnologyType()
        { /* require use of factory methods */ }

        internal TechnologyType( int id, string name )
        {
          _id = id;
          _name = name;
        }
        #endregion
    }
}
