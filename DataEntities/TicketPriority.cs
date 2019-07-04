using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class TicketPriority
    {
        public int? TicketPriorityID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public int? CreatedBy
        {
            get; set;
        }

        public DateTime? CreatedDate
        {
            get; set;
        }

        public int? ModifiedBy
        {
            get; set;
        }

        public DateTime? ModifiedDate
        {
            get; set;
        }
    }
}
