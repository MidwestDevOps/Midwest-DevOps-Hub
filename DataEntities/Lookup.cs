using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public static class Lookup
    {
        public enum ActivityLog
        {
            SUCCESS = 0,
            FAILED = 1,
            ACCESSNOTALLOWED = 2,
            INPROGRESS = 3
        }
    }
}
