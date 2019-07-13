using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class TicketPriorities : IDisposable
    {

        #region Boring Stuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public TicketPriorities(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public TicketPriorities(MySqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        internal MySqlConnection GetConnection()
        {
            if (sqlConnection == null)
            {
                return new MySqlConnection(this.ConnectionString);
            }
            else
            {
                return this.sqlConnection;
            }
        }

        public void Dispose()
        {
            if (sqlConnection != null)
                sqlConnection.Dispose();
        }

        #endregion

        public List<DataEntities.TicketPriority> GetAllPriorities()
        {
            try
            {
                DatabaseLogicLayer.TicketPriorities ticketPriorityDLL = new DatabaseLogicLayer.TicketPriorities(GetConnection());

                return ticketPriorityDLL.GetAllPriorities();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.TicketPriority GetPriorityByID(int priorityID)
        {
            try
            {
                DatabaseLogicLayer.TicketPriorities ticketPriorityDLL = new DatabaseLogicLayer.TicketPriorities(GetConnection());

                return ticketPriorityDLL.GetPriorityByID(priorityID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public long? SavePriority(DataEntities.TicketPriority priority)
        {
            try
            {
                DatabaseLogicLayer.TicketPriorities ticketPriorityDLL = new DatabaseLogicLayer.TicketPriorities(GetConnection());

                return ticketPriorityDLL.SavePriority(priority);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }
    }
}
