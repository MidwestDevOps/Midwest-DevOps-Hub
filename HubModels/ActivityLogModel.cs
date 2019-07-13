using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    class ActivityLogModel
    {
        public int? ActivityLogID
        {
            get; set;
        }

        public string IP
        {
            get; set;
        }

        public string Action
        {
            get; set;
        }

        public string Value
        {
            get; set;
        }

        public int ReturnedLID
        {
            get; set;
        }

        public int ApplicationLID
        {
            get; set;
        }

        public int? CreatedBy
        {
            get; set;
        }

        public DateTime CreatedDate
        {
            get; set;
        }

        public ActivityLogModel()
        {

        }

        public ActivityLogModel(DataEntities.ActivityLog p)
        {
            this.ActivityLogID = p.ActivityLogID;
            this.IP = p.IP;
            this.Action = p.Action;
            this.Value = p.Value;
            this.ReturnedLID = p.ReturnedLID;
            this.ApplicationLID = p.ApplicationLID;
            this.CreatedBy = p.CreatedBy;
            this.CreatedDate = p.CreatedDate;
        }

        public DataEntities.ActivityLog ConvertToEntity()
        {
            DataEntities.ActivityLog p = new DataEntities.ActivityLog();

            p.ActivityLogID = this.ActivityLogID;
            p.IP = this.IP;
            p.Action = this.Action;
            p.Value = this.Value;
            p.ReturnedLID = this.ReturnedLID;
            p.ApplicationLID = this.ApplicationLID;
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;

            return p;
        }
    }
}
