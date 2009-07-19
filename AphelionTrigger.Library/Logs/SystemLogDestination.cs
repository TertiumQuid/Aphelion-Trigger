using System;
using System.Collections.Generic;
using System.Text;

namespace AphelionTrigger.Library.Logs
{
    [Serializable()]
    public enum SystemLogDestination
    {
        Database = 1,
        WinidowsEventLog = 2,
        Email = 3,
        TextFile = 4
    }
}
