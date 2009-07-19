using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AphelionTrigger.Library.Security;
using Csla;
using Csla.Data;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class UpdateCreditsCommand : CommandBase
    {
        public static void UpdateCredits()
        {
            UpdateCreditsCommand command;
            command = DataPortal.Execute<UpdateCreditsCommand>( new UpdateCreditsCommand() );
        }

        protected override void DataPortal_Execute()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    ATConfiguration config = ATConfiguration.Instance;

                    // update credits based on affluence. credits increment from 0 to CreditsFactor (i.e. 1000) every hour
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "UPDATE bbgHouses SET Credits = Credits + ((" + config.CreditsFactor.ToString() + " * Affluence)/6)";
                    cm.ExecuteNonQuery();
                }
            }
        }
    }
}
