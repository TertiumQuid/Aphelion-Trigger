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
    public class UpdatePopulationCommand : CommandBase
    {
        public static void UpdatePopulation()
        {
            UpdatePopulationCommand command;
            command = DataPortal.Execute<UpdatePopulationCommand>( new UpdatePopulationCommand() );
        }

        protected override void DataPortal_Execute()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "UpdatePopulation";
                    cm.ExecuteNonQuery();
                }
            }
        }
    }
}
