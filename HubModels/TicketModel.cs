using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    public class TicketModel
    {
        public int? TicketID
        {
            get; set;
        }

        public int ProjectID
        {
            get; set;
        }

        public int CategoryID
        {
            get; set;
        }

        public int PriorityID
        {
            get; set;
        }

        public string Subject
        {
            get; set;
        }

        public string Issue
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

        public TicketModel()
        {

        }

        public TicketModel(DataEntities.Ticket p)
        {
            this.TicketID = p.TicketID;
            this.ProjectID = p.ProjectID;
            this.CategoryID = p.CategoryID;
            this.PriorityID = p.PriorityID;
            this.Subject = p.Subject;
            this.Issue = p.Issue;
            this.CreatedBy = p.CreatedBy;
            this.CreatedDate = p.CreatedDate;
            this.ModifiedBy = p.ModifiedBy;
            this.ModifiedDate = p.ModifiedDate;
        }

        public DataEntities.Ticket ConvertToEntity()
        {
            DataEntities.Ticket p = new DataEntities.Ticket();

            p.TicketID = this.TicketID;
            p.ProjectID = this.ProjectID;
            p.CategoryID = this.CategoryID;
            p.PriorityID = this.PriorityID;
            p.Subject = this.Subject;
            p.Issue = this.Issue;
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;
            p.ModifiedBy = p.ModifiedBy;
            p.ModifiedDate = p.ModifiedDate;

            return p;
        }
    }
}
