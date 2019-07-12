using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    public class UserModel
    {
        public int? UserID
        {
            get; set;
        }

        public string Username
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public bool Active
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

        public UserModel()
        {

        }

        public UserModel(DataEntities.User p)
        {
            this.UserID = p.UserID;
            this.Username = p.Username;
            this.Password = p.Password;
            this.Active = p.Active;
            this.CreatedBy = p.CreatedBy;
            this.CreatedDate = p.CreatedDate;
            this.ModifiedBy = p.ModifiedBy;
            this.ModifiedDate = p.ModifiedDate;
        }

        public DataEntities.User ConvertToEntity()
        {
            DataEntities.User p = new DataEntities.User();

            p.UserID = this.UserID;
            p.Username = this.Username;
            p.Password = this.Password;
            p.Active = this.Active;
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;
            p.ModifiedBy = this.ModifiedBy;
            p.ModifiedDate = this.ModifiedDate;

            return p;
        }
    }
}
