using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    public class TicketCategoryModel : BaseModel
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

        public TicketCategoryModel()
        {

        }

        public TicketCategoryModel(DataEntities.TicketCategory p)
        {
            this.TicketCategoryID = p.TicketCategoryID;
            this.Name = p.Name;
            this.Color = p.Color;
            this.Active = p.Active;
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
            p.Color = this.Color;
            p.Active = this.Active;
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;
            p.ModifiedBy = this.ModifiedBy;
            p.ModifiedDate = this.ModifiedDate;

            return p;
        }
    }
}
