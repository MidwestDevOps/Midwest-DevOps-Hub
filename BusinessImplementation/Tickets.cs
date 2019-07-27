using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Tickets : BLLManager, IDisposable
    {

        public Tickets(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Tickets(MySqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public List<DataEntities.Ticket> GetAllTickets()
        {
            try
            {
                DatabaseLogicLayer.Tickets ticketDLL = new DatabaseLogicLayer.Tickets(GetConnection());

                return ticketDLL.GetAllTickets();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.Ticket GetTicketByID(int ticketID)
        {
            try
            {
                DatabaseLogicLayer.Tickets ticketDLL = new DatabaseLogicLayer.Tickets(GetConnection());

                return ticketDLL.GetTicketByID(ticketID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public long? SaveTicket(DataEntities.Ticket ticket)
        {
            try
            {
                DatabaseLogicLayer.Tickets ticketDLL = new DatabaseLogicLayer.Tickets(GetConnection());

                return ticketDLL.SaveTicket(ticket);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }
    }
}
