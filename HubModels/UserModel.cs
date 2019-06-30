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

        public UserModel()
        {

        }

        public UserModel(DataEntities.User p)
        {
            this.UserID = p.UserID;
            this.Username = p.Username;
            this.Password = p.Password;
        }

        public DataEntities.User ConvertToEntity()
        {
            DataEntities.User p = new DataEntities.User();

            p.UserID = p.UserID;
            p.Username = p.Username;
            p.Password = p.Password;

            return p;
        }
    }
}
