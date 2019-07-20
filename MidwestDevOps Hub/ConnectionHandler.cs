using BusinessLogicLayer;
using HubModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MidwestDevOps_Hub
{
    public static class ConnectionHandler
    {
        public static Connections GetConnection()
        {
            try
            {
                var connections = JsonConvert.DeserializeObject<HubModels.Connections>(GetConnectionFileDecrypted());

                return connections;
            }
            catch
            {

            }

            return new Connections();
        }

        public static string GetConnectionFileDecrypted()
        {
            var s = File.ReadAllText(Utility.connectionPath);
            var ss = TextHasher.Decrypt(s);
            return TextHasher.Decrypt(s);
        }

        public static void SaveConnectionFileCrypted(Connections connections)
        {
            var s = JsonConvert.SerializeObject(connections, Formatting.Indented);

            File.WriteAllText(Utility.connectionPath, TextHasher.Crypt(s));
        }

        public static string LoadSpecificConnectionString(string connectionName)
        {
            var connections = GetConnection();

            var connection = connections.DatabaseConnections.Where(x => x.Name.ToUpper() == connectionName.ToUpper()).FirstOrDefault();

            if (connection != null)
            {
                Utility.connectionString = GetConnectionStringFromConnection(connection);
            }

            return Utility.connectionString;
        }

        public static string GetConnectionStringFromConnection(Connection con)
        {
            using (var bll = new BusinessLogicLayer.HubMainClients(TextHasher.Decrypt(Utility.HubMainConnectionString)))
            {
                var entity = bll.GetClientByUUID(con.UUID);

                string combinedString = "";

                if (entity != null)
                {

                    string userName = TextHasher.Decrypt(entity.UserName);
                    string password = TextHasher.Decrypt(entity.Password);
                    string database = TextHasher.Decrypt(entity.ClientDatabase);
                    string server = TextHasher.Decrypt(entity.Server);

                    combinedString = String.Format("server={3};port=3306;database={0};username={1};password={2}", database, userName, password, server);
                }

                return combinedString;
            }
        }

        public static void SaveConnectionString(Connection con)
        {

            HubModels.Connections connections = new Connections();

            try
            {
                connections = GetConnection();
            }
            catch { }

            if (connections != null && connections.DatabaseConnections.Where(x => x.Name.ToUpper() == con.Name.ToUpper()).Count() > 0)
            {
                BusinessLogicLayer.Logging.SaveLog(new BusinessLogicLayer.Log() { text = "Connection: " + con.Name + " already exists.", time = DateTime.Now });
                return;
            }

            connections.DatabaseConnections.Add(con);

            SaveConnectionFileCrypted(connections);
        }

        public static Connections GetAllConnections()
        {
            var connections = GetConnection();

            return connections;
        }

        public static int GetConnectionStringCount()
        {

            return GetAllConnections().DatabaseConnections.Count();
        }

        public static string LoadDefaultConnectionString()
        {
            var connections = GetConnection();

            var connection = connections.DatabaseConnections[0];

            Utility.connectionString = GetConnectionStringFromConnection(connection);

            return Utility.connectionString;
        }
    }
}
