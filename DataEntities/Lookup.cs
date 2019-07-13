using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public static class Lookup
    {
        public enum ActivityLog
        {
            SUCCESS = 9,
            FAILED = 8,
            ACCESSNOTALLOWED = 7,
            INPROGRESS = 6
        }

        public enum Application
        {
            WINDOWSNET = 1,
            MVCNET = 2
        }
    }
}
