using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class TicketCategories : DBManager
    {

        #region BoringStuff

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
            List<DataEntities.TicketCategory> p = new List<DataEntities.TicketCategory>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ticketcategory", conn);
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

        public DataEntities.TicketCategory GetCategoryByID(int ticketCategoryID)
        {
            DataEntities.TicketCategory person = new DataEntities.TicketCategory();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ticketcategory Where TicketCategoryID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", ticketCategoryID);
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

        public int? NumberOfDependentTickets(int ticketCategoryID)
        {
            DataEntities.TicketCategory person = new DataEntities.TicketCategory();

            int? count = 0;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ticket Where CategoryID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", ticketCategoryID);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count++;
                    }

                    return count;
                }
            }
        }

        public long? ReplaceDependentTickets(int newCategoryID, int oldCategoryID)
        {
            DataEntities.TicketCategory person = new DataEntities.TicketCategory();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE ticket SET CategoryID = @NewID Where CategoryID = @ID;", conn);

                cmd.Parameters.AddWithValue("@NewID", newCategoryID);
                cmd.Parameters.AddWithValue("@ID", oldCategoryID);

                return cmd.ExecuteNonQuery();
            }
        }

        public bool DeleteCategory(int ticketCategoryID)
        {
            DataEntities.TicketCategory person = new DataEntities.TicketCategory();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM ticketcategory Where TicketCategoryID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", ticketCategoryID);
                var rtnValue = cmd.ExecuteNonQuery();

                return rtnValue == 1 ? true : false;
            }
        }

        public long? SaveCategory(DataEntities.TicketCategory p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (p.TicketCategoryID == null)
                {
                    sql = @"INSERT INTO `ticketcategory` (`TicketCategoryID`, `Name`, `Color`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `ModifiedDate`, `Active`) VALUES (NULL, @Name, @Color, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active);
                            SELECT LAST_INSERT_ID();";
                }
                else
                {
                    sql = @"UPDATE `ticketcategory` SET TicketCategoryID = @ID, Name = @Name, Color = @Color, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate, Active = @Active WHERE TicketCategoryID = @ID;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", p.TicketCategoryID);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Color", p.Color);
                cmd.Parameters.AddWithValue("@CreatedBy", p.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", p.CreatedDate);
                cmd.Parameters.AddWithValue("@ModifiedBy", p.ModifiedBy);
                cmd.Parameters.AddWithValue("@ModifiedDate", p.ModifiedDate);
                cmd.Parameters.AddWithValue("@Active", p.Active);

                if (p.TicketCategoryID == null) //Get new id number
                {
                    return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
                }
                else //Return id if worked
                {
                    cmd.ExecuteScalar();

                    return p.TicketCategoryID;
                }
            }
        }

        internal DataEntities.TicketCategory ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.TicketCategory p = new DataEntities.TicketCategory();

            p.TicketCategoryID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "TicketCategoryID"));
            p.Name = DBUtilities.ReturnSafeString(reader, "Name");
            p.Color = DBUtilities.ReturnSafeString(reader, "Color");
            p.Active = DBUtilities.ReturnBoolean(reader, "Active");
            p.CreatedBy = DBUtilities.ReturnSafeInt(reader, "CreatedBy");
            p.CreatedDate = DBUtilities.ReturnSafeDateTime(reader, "CreatedDate");
            p.ModifiedBy = DBUtilities.ReturnSafeInt(reader, "ModifiedBy");
            p.ModifiedDate = DBUtilities.ReturnSafeDateTime(reader, "ModifiedDate");

            return p;
        }
    }
}
