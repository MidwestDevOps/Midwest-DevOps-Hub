using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
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
    }
}
