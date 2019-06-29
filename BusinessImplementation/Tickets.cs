using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Tickets
    {

        #region Boring Stuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public Tickets(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Tickets(MySqlConnection sqlConnection)
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

        #endregion

        public List<DataEntities.Ticket> GetAllTickets()
        {
            try
            {
                DatabaseLogicLayer.Tickets ticketDLL = new DatabaseLogicLayer.Tickets(GetConnection());

                return ticketDLL.GetAllTickets();
            }
            catch (Exception e)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("logs1.txt"))
                {
                    sw.WriteLine(e.Message + e.StackTrace + e.Source + e.InnerException + e.TargetSite);
                    sw.Close();
                }
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
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("logs1.txt"))
                {
                    sw.WriteLine(e.Message + e.StackTrace + e.Source + e.InnerException + e.TargetSite);
                    sw.Close();
                }
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
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("logs1.txt"))
                {
                    sw.WriteLine(e.Message + e.StackTrace + e.Source + e.InnerException + e.TargetSite);
                    sw.Close();
                }
            }

            return null;
        }
    }
}
