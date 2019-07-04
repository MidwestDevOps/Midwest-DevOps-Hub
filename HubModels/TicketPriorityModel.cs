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

        public TicketPriorityModel()
        {

        }

        public TicketPriorityModel(DataEntities.TicketPriority p)
        {
            this.TicketPriorityID = p.TicketPriorityID;
            this.Name = p.Name;
            this.CreatedBy = p.CreatedBy;
            this.CreatedDate = p.CreatedDate;
            this.ModifiedBy = p.ModifiedBy;
            this.ModifiedDate = p.ModifiedDate;
        }

        public DataEntities.TicketPriority ConvertToEntity()
        {
            DataEntities.TicketPriority p = new DataEntities.TicketPriority();

            p.TicketPriorityID = this.TicketPriorityID;
            p.Name = this.Name;
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;
            p.ModifiedBy = p.ModifiedBy;
            p.ModifiedDate = p.ModifiedDate;

            return p;
        }
    }
}
