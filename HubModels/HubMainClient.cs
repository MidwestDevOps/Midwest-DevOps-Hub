using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    public class HubMainClient
    {
        public int? ClientID
        {
            get; set;
        }

        public string UUID
        {
            get; set;
        }

        public string ClientName
        {
            get; set;
        }

        public string Server
        {
            get; set;
        }

        public string ClientDatabase
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public HubMainClient(DataEntities.HubMainClient p)
        {
            this.ClientID = p.ClientID;
            this.UUID = p.UUID;
            this.ClientName = p.ClientName;
            this.Server = p.Server;
            this.ClientDatabase = p.ClientDatabase;
            this.UserName = p.UserName;
            this.Password = p.Password;
        }

        public DataEntities.HubMainClient ConvertToEntity()
        {
            DataEntities.HubMainClient p = new DataEntities.HubMainClient();

            p.ClientID = this.ClientID;
            p.UUID = this.UUID;
            p.ClientName = this.ClientName;
            p.Server = this.Server;
            p.ClientDatabase = this.ClientDatabase;
            p.UserName = this.UserName;
            p.Password = this.Password;

            return p;
        }
    }
}
