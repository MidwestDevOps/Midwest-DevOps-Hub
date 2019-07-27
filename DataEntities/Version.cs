using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class Version
    {
        public int? VersionID
        {
            get; set;
        }

        public string ServerVersion
        {
            get; set;
        }

        public DateTime CreatedDate
        {
            get; set;
        }
    }
}
