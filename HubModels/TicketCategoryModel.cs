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

        public TicketCategoryModel()
        {

        }

        public TicketCategoryModel(DataEntities.TicketCategory p)
        {
            this.TicketCategoryID = p.TicketCategoryID;
            this.Name = p.Name;
            this.CreatedBy = p.CreatedBy;
            this.CreatedDate = p.CreatedDate;
            this.ModifiedBy = p.ModifiedBy;
            this.ModifiedDate = p.ModifiedDate;
        }

        public DataEntities.TicketCategory ConvertToEntity()
        {
            DataEntities.TicketCategory p = new DataEntities.TicketCategory();

            p.TicketCategoryID = this.TicketCategoryID;
            p.Name = this.Name;
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;
            p.ModifiedBy = p.ModifiedBy;
            p.ModifiedDate = p.ModifiedDate;

            return p;
        }
    }
}
