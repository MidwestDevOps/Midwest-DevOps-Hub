using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class Tickets : DBManager
    {

        #region BoringStuff

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
            List<DataEntities.Ticket> p = new List<DataEntities.Ticket>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT t.*, p.Name as ProjectName, tc.Name as CategoryName, tp.Name as PriorityName FROM ticket as t join Project as p on p.ProjectID = t.ProjectID join ticketcategory as tc on tc.TicketCategoryID = t.CategoryID join ticketpriority as tp on tp.TicketPriorityID = t.PriorityID;", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        p.Add(ConvertMySQLToEntity(reader));
                    }
                }
            }

            return p;
        }

        public DataEntities.Ticket GetTicketByID(int ticketID)
        {
            DataEntities.Ticket person = new DataEntities.Ticket();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT t.*, p.Name as ProjectName, tc.Name as CategoryName, tp.Name as PriorityName FROM ticket as t join Project as p on p.ProjectID = t.ProjectID join ticketcategory as tc on tc.TicketCategoryID = t.CategoryID join ticketpriority as tp on tp.TicketPriorityID = t.PriorityID Where TicketID = @ID ;", conn);

                cmd.Parameters.AddWithValue("@ID", ticketID);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return ConvertMySQLToEntity(reader);
                    }

                    return null;
                }
            }
        }

        public long? SaveTicket(DataEntities.Ticket p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (p.TicketID == null)
                {
                    sql = @"INSERT INTO `ticket` (`TicketID`, `ProjectID`, `CategoryID`, `PriorityID`, `Subject`, `Issue`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `ModifiedDate`, `Active`) VALUES (NULL, @ProjectID, @CategoryID, @PriorityID, @Subject, @Issue, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active);
                            SELECT LAST_INSERT_ID();";
                }
                else
                {
                    sql = @"UPDATE `ticket` SET TicketID = @ID, ProjectID = @ProjectID, CategoryID = @CategoryID, PriorityID = @PriorityID, Subject = @Subject, Issue = @Issue, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate, Active = @Active WHERE TicketID = @ID;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", p.TicketID);
                cmd.Parameters.AddWithValue("@ProjectID", p.ProjectID);
                cmd.Parameters.AddWithValue("@CategoryID", p.CategoryID);
                cmd.Parameters.AddWithValue("@PriorityID", p.PriorityID);
                cmd.Parameters.AddWithValue("@Subject", p.Subject);
                cmd.Parameters.AddWithValue("@Issue", p.Issue);
                cmd.Parameters.AddWithValue("@CreatedBy", p.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", p.CreatedDate);
                cmd.Parameters.AddWithValue("@ModifiedBy", p.ModifiedBy);
                cmd.Parameters.AddWithValue("@ModifiedDate", p.ModifiedDate);
                cmd.Parameters.AddWithValue("@Active", p.Active);



                if (p.TicketID == null) //Get new id number
                {
                    return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
                }
                else //Return id if worked
                {
                    cmd.ExecuteScalar();

                    return p.TicketID;
                }
            }
        }

        internal DataEntities.Ticket ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.Ticket p = new DataEntities.Ticket();

            p.TicketID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "TicketID"));
            p.ProjectID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "ProjectID"));
            p.ProjectName = DBUtilities.ReturnSafeString(reader, "ProjectName");
            p.CategoryID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "CategoryID"));
            p.CategoryName = DBUtilities.ReturnSafeString(reader, "CategoryName");
            p.PriorityID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "PriorityID"));
            p.PriorityName = DBUtilities.ReturnSafeString(reader, "PriorityName");
            p.Subject = DBUtilities.ReturnSafeString(reader, "Subject");
            p.Issue = DBUtilities.ReturnSafeString(reader, "Issue");
            p.Active = DBUtilities.ReturnBoolean(reader, "Active");
            p.CreatedBy = DBUtilities.ReturnSafeInt(reader, "CreatedBy");
            p.CreatedDate = DBUtilities.ReturnSafeDateTime(reader, "CreatedDate");
            p.ModifiedBy = DBUtilities.ReturnSafeInt(reader, "ModifiedBy");
            p.ModifiedDate = DBUtilities.ReturnSafeDateTime(reader, "ModifiedDate");

            return p;
        }
    }
}
