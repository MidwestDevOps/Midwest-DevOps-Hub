using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class HubMainClients : BLLManager, IDisposable
    {

        public HubMainClients(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public HubMainClients(MySqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public List<DataEntities.HubMainClient> GetAllClients(bool nameOnly)
        {
            try
            {
                DatabaseLogicLayer.HubMainClients hubMainClientBLL = new DatabaseLogicLayer.HubMainClients(GetConnection());

                return hubMainClientBLL.GetAllClients(nameOnly);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });

                throw;
            }
        }

        public DataEntities.HubMainClient GetClientByID(int clientID)
        {
            try
            {
                DatabaseLogicLayer.HubMainClients hubMainClientBLL = new DatabaseLogicLayer.HubMainClients(GetConnection());

                return hubMainClientBLL.GetClientByID(clientID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.HubMainClient GetClientByUUID(string clientUUID)
        {
            try
            {
                DatabaseLogicLayer.HubMainClients hubMainClientBLL = new DatabaseLogicLayer.HubMainClients(GetConnection());

                return hubMainClientBLL.GetClientByUUID(clientUUID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        //public long? SavePriority(DataEntities.HubMainClient priority)
        //{
        //    try
        //    {
        //        DatabaseLogicLayer.TicketPriorities ticketPriorityDLL = new DatabaseLogicLayer.TicketPriorities(GetConnection());

        //        return ticketPriorityDLL.SavePriority(priority);
        //    }
        //    catch (Exception e)
        //    {
        //        Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
        //    }

        //    return null;
        //}
    }
}
