using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class TicketCategories : BLLManager, IDisposable
    {

        public TicketCategories(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public TicketCategories(MySqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public List<DataEntities.TicketCategory> GetAllCategories()
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.GetAllCategories();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.TicketCategory GetCategoryByID(int categoryID)
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.GetCategoryByID(categoryID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public int? NumberOfDependentTickets(int categoryID)
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.NumberOfDependentTickets(categoryID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public long? ReplaceDependentTickets(int newCategoryID, int oldCategoryID)
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.ReplaceDependentTickets(newCategoryID, oldCategoryID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public bool DeleteCategory(int categoryID)
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.DeleteCategory(categoryID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return false;
        }

        public long? SaveCategory(DataEntities.TicketCategory category)
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.SaveCategory(category);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }
    }
}
