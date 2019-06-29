using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class TicketCategories
    {

        #region Boring Stuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public TicketCategories(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public TicketCategories(MySqlConnection sqlConnection)
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

        public List<DataEntities.TicketCategory> GetAllCategories()
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.GetAllCategories();
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

        public DataEntities.TicketCategory GetProjectByID(int categoryID)
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.GetCategoryByID(categoryID);
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

        public long? SaveProject(DataEntities.TicketCategory category)
        {
            try
            {
                DatabaseLogicLayer.TicketCategories ticketCategoryDLL = new DatabaseLogicLayer.TicketCategories(GetConnection());

                return ticketCategoryDLL.SaveCategory(category);
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
