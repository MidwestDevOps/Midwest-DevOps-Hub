using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class TicketCategory : BaseEntity
    {
        public int? TicketCategoryID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Color
        {
            get; set;
        }
    }
}
