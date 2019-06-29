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

            return p;
        }
    }
}
