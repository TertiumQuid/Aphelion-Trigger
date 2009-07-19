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
    public class UpdateResearchCommand : CommandBase
    {
        public static void UpdateResearch()
        {
            UpdateResearchCommand command;
            command = DataPortal.Execute<UpdateResearchCommand>( new UpdateResearchCommand() );
        }

        protected override void DataPortal_Execute()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    // research is tracked in minutes, so update by 1 every minute - important! the stored procedure assumes this
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "UpdateResearch";
                    cm.ExecuteNonQuery();
                }
            }
        }
    }
}
