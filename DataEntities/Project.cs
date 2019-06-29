using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class Project
    {
        public int? ProjectID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public DateTime? CreatedDate
        {
            get; set;
        }
    }
}
