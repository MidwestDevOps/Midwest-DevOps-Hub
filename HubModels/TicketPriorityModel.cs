using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    class TicketPriorityModel
    {
        public int? TicketPriorityID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public TicketPriorityModel()
        {

        }

        public TicketPriorityModel(DataEntities.TicketPriority p)
        {
            this.TicketPriorityID = p.TicketPriorityID;
            this.Name = p.Name;
        }

        public DataEntities.TicketPriority ConvertToEntity()
        {
            DataEntities.TicketPriority p = new DataEntities.TicketPriority();

            p.TicketPriorityID = this.TicketPriorityID;
            p.Name = this.Name;

            return p;
        }
    }
}
