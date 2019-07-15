using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class UserSession
    {
        public int? UserSessionID
        {
            get; set;
        }

        public string GUID
        {
            get; set;
        }

        public int UserID
        {
            get; set;
        }

        public int StatusLID
        {
            get; set;
        }

        public DateTime ExpireDate
        {
            get; set;
        }

        public DateTime CreatedDate
        {
            get; set;
        }
    }
}
