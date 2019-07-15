using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public static class Lookup
    {
        public enum Application
        {
            WINDOWSNET = 1,
            MVCNET = 2
        }

        public enum ActivityLog
        {
            SUCCESS = 3,
            FAILED = 4,
            ACCESSNOTALLOWED = 5,
            INPROGRESS = 6
        }

        public enum UserSession
        {
            ACTIVE = 7,
            INACTIVE = 8,
            AWAY = 9,
            UNKNOWN = 10
        }
    }
}
