using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class Ticket
    {
        public int? TicketID
        {
            get; set;
        }

        public int ProjectID
        {
            get; set;
        }

        public string ProjectName
        {
            get; set;
        }

        public int CategoryID
        {
            get; set;
        }

        public string CategoryName
        {
            get; set;
        }

        public int PriorityID
        {
            get; set;
        }

        public string PriorityName
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
    }
}
