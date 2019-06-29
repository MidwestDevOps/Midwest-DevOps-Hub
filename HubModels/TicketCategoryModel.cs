using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    public class TicketCategoryModel
    {
        public int? TicketCategoryID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public TicketCategoryModel()
        {

        }

        public TicketCategoryModel(DataEntities.TicketCategory p)
        {
            this.TicketCategoryID = p.TicketCategoryID;
            this.Name = p.Name;
        }

        public DataEntities.TicketCategory ConvertToEntity()
        {
            DataEntities.TicketCategory p = new DataEntities.TicketCategory();

            p.TicketCategoryID = this.TicketCategoryID;
            p.Name = this.Name;

            return p;
        }
    }
}
