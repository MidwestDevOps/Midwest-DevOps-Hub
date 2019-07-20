using System;
using System.Collections.Generic;

namespace HubModels
{
    public class Connections
    {
        public List<Connection> DatabaseConnections = new List<Connection>();
    }

    public class Connection
    {
        public string Name
        {
            get; set;
        }

        public string UUID
        {
            get; set;
        }
    }
}
