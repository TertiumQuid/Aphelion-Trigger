using System;
using System.Collections.Generic;
using System.Text;

namespace AphelionTrigger.Library.Logs
{
    [Serializable()]
    public enum SystemLogType
    {
        Debug = 1,
        Error = 2,
        Information = 3,
        Warning = 4
    }
}
