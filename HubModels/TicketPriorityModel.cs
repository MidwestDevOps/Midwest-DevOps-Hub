using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    class TicketPriorityModel : BaseModel
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
            this.Active = p.Active;
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
            p.Active = this.Active;
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;
            p.ModifiedBy = this.ModifiedBy;
            p.ModifiedDate = this.ModifiedDate;

            return p;
        }
    }
}
