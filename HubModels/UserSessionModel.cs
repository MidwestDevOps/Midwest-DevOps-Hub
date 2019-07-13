using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    public class UserSessionModel
    {
        public int? UserSessionID
        {
            get; set;
        }

        public string GUID
        {
            get; set;
        }

        public int UserID
        {
            get; set;
        }

        public int StatusLID
        {
            get; set;
        }

        public DateTime CreatedDate
        {
            get; set;
        }

        public UserSessionModel()
        {

        }

        public UserSessionModel(DataEntities.UserSession p)
        {
            this.UserSessionID = p.UserSessionID;
            this.GUID = p.GUID;
            this.UserID = p.UserID;
            this.StatusLID = p.StatusLID;
            this.CreatedDate = p.CreatedDate;
        }

        public DataEntities.UserSession ConvertToEntity()
        {
            DataEntities.UserSession p = new DataEntities.UserSession();

            p.UserSessionID = this.UserSessionID;
            p.GUID = this.GUID;
            p.UserID = this.UserID;
            p.StatusLID = this.StatusLID;
            p.CreatedDate = this.CreatedDate;

            return p;
        }
    }
}
