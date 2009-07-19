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
    public class UpdateTurnsCommand : CommandBase
    {
        public static void UpdateTurns()
        {
            UpdateTurnsCommand command;
            command = DataPortal.Execute<UpdateTurnsCommand>(new UpdateTurnsCommand());
        }

        protected override void DataPortal_Execute()
        {
            using (SqlConnection cn = new SqlConnection(Database.AphelionTriggerConnection))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    ATConfiguration config = ATConfiguration.Instance;

                    // increment turn count based on speed. NB: houses will loose speed if they have large forces: 
                    // for every TurnUnitStep (i.e. 100) troops over TurnUnitCap (i.e. 400), -1 speed
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "UPDATE bbgHouses SET TurnCount = TurnCount + (((((CAST(Speed AS Decimal(18,2)) -(CASE WHEN dbo.GetHouseForcesForSpeed(ID,dbo.GetAge()) < " + config.TurnUnitStep.ToString() + " THEN 0 ELSE ((dbo.GetHouseForcesForSpeed(ID,dbo.GetAge()) - " + config.TurnUnitCap.ToString() + ") / " + config.TurnUnitStep.ToString() + ") END))/100)*" + config.TurnFactor.ToString() + ")+1)/6)";
                    cm.ExecuteNonQuery();

                    // increment turns, decrement counter - TODO?: mabye allow for a greater increment than 1
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "UPDATE bbgHouses SET Turns = Turns + 1, TurnCount = TurnCount - 1 WHERE TurnCount >= 1";
                    cm.ExecuteNonQuery();
                }
            }
        }
    }
}
