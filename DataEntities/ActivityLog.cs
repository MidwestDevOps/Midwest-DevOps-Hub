using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class ActivityLog
    {
        public int? ActivityLogID
        {
            get; set;
        }

        public string IP
        {
            get; set;
        }

        public string Action
        {
            get; set;
        }

        public string Value
        {
            get; set;
        }

        public int ReturnedLID
        {
            get; set;
        }

        public int? CreatedBy
        {
            get; set;
        }

        public DateTime CreatedDate
        {
            get; set;
        }
    }
}
