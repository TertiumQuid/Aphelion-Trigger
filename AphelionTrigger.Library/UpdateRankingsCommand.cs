using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using AphelionTrigger.Library.Security;
using Csla;
using Csla.Data;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class UpdateRankingsCommand : CommandBase
    {
        public static void UpdateRankings()
        {
            UpdateRankingsCommand command;
            command = DataPortal.Execute<UpdateRankingsCommand>( new UpdateRankingsCommand() );
        }

        private int GetPoints( House house )
        {
            return house.Power + house.Protection + house.Intelligence + house.Affluence + house.Speed + house.Level.Rank + (int)(house.ForcesCount * 0.005) + (int)(house.Ambition / 4);
        }

        protected override void DataPortal_Execute()
        {
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "UpdateRankings";
                    cm.ExecuteNonQuery();
                }
            }
        }
    }
}
