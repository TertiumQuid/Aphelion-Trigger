using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AphelionTrigger.Library.Security
{
    public static class Database
    {
        public static string AphelionTriggerConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["AphelionTrigger"].ConnectionString;
            }
        }
    }

    public enum RecordScope
    {
        ShowNumbers = 1,
        ShowAll = 2
    }
}
