using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class TicketPriorities : DBManager
    {

        #region BoringStuff

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

        #endregion

        public List<DataEntities.TicketPriority> GetAllPriorities()
        {
            List<DataEntities.TicketPriority> p = new List<DataEntities.TicketPriority>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ticketpriority", conn);
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

        public DataEntities.TicketPriority GetPriorityByID(int ticketPriorityID)
        {
            DataEntities.TicketPriority person = new DataEntities.TicketPriority();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ticketpriority Where TicketPriorityID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", ticketPriorityID);
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

        public long? SavePriority(DataEntities.TicketPriority p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (p.TicketPriorityID == null)
                {
                    sql = @"INSERT INTO `ticketpriority` (`TicketPriorityID`, `Name`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `ModifiedDate`) VALUES (NULL, @Name, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate);
                            SELECT LAST_INSERT_ID();";
                }
                else
                {
                    sql = @"UPDATE `ticketpriority` SET TicketPriorityID = @ID, Name = @Name, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE TicketPriorityID = @ID;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", p.TicketPriorityID);
                cmd.Parameters.AddWithValue("@Name", p.Name);

                if (p.TicketPriorityID == null) //Get new id number
                {
                    return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
                }
                else //Return id if worked
                {
                    cmd.ExecuteScalar();

                    return p.TicketPriorityID;
                }
            }
        }

        internal DataEntities.TicketPriority ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.TicketPriority p = new DataEntities.TicketPriority();

            p.TicketPriorityID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "TicketPriorityID"));
            p.Name = DBUtilities.ReturnSafeString(reader, "Name");
            p.CreatedBy = DBUtilities.ReturnSafeInt(reader, "CreatedBy");
            p.CreatedDate = DBUtilities.ReturnSafeDateTime(reader, "CreatedDate");
            p.ModifiedBy = DBUtilities.ReturnSafeInt(reader, "ModifiedBy");
            p.ModifiedDate = DBUtilities.ReturnSafeDateTime(reader, "ModifiedDate");

            return p;
        }
    }
}
