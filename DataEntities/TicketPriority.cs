using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class TicketPriority : BaseEntity
    {
        public int? TicketPriorityID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }
}
