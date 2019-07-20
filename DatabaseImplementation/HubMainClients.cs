using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class HubMainClients : DBManager
    {

        #region BoringStuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public HubMainClients(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public HubMainClients(MySqlConnection sqlConnection)
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

        public List<DataEntities.HubMainClient> GetAllClients(bool nameOnly)
        {
            List<DataEntities.HubMainClient> p = new List<DataEntities.HubMainClient>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (nameOnly)
                {
                    sql = "SELECT ClientID, ClientName, UUID FROM client WHERE Display = 1;";
                }
                else
                {
                    sql = "SELECT * FROM client WHERE Display = 1;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        p.Add(ConvertMySQLToEntity(reader, nameOnly));
                    }
                }
            }

            return p;
        }

        public DataEntities.HubMainClient GetClientByID(int clientID)
        {
            DataEntities.HubMainClient person = new DataEntities.HubMainClient();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM client Where ClientID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", clientID);
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

        public DataEntities.HubMainClient GetClientByUUID(string clientUUID)
        {
            DataEntities.HubMainClient person = new DataEntities.HubMainClient();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM client Where UUID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", clientUUID);
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

        //public long? SaveCategory(DataEntities.TicketCategory p)
        //{
        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();

        //        string sql = "";

        //        if (p.TicketCategoryID == null)
        //        {
        //            sql = @"INSERT INTO `ticketcategory` (`TicketCategoryID`, `Name`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `ModifiedDate`, `Active`) VALUES (NULL, @Name, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active);
        //                    SELECT LAST_INSERT_ID();";
        //        }
        //        else
        //        {
        //            sql = @"UPDATE `ticketcategory` SET TicketCategoryID = @ID, Name = @Name, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate, Active = @Active WHERE TicketCategoryID = @ID;";
        //        }

        //        MySqlCommand cmd = new MySqlCommand(sql, conn);

        //        cmd.Parameters.AddWithValue("@ID", p.TicketCategoryID);
        //        cmd.Parameters.AddWithValue("@Name", p.Name);
        //        cmd.Parameters.AddWithValue("@CreatedBy", p.CreatedBy);
        //        cmd.Parameters.AddWithValue("@CreatedDate", p.CreatedDate);
        //        cmd.Parameters.AddWithValue("@ModifiedBy", p.ModifiedBy);
        //        cmd.Parameters.AddWithValue("@ModifiedDate", p.ModifiedDate);
        //        cmd.Parameters.AddWithValue("@Active", p.Active);

        //        if (p.TicketCategoryID == null) //Get new id number
        //        {
        //            return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
        //        }
        //        else //Return id if worked
        //        {
        //            cmd.ExecuteScalar();

        //            return p.TicketCategoryID;
        //        }
        //    }
        //}

        internal DataEntities.HubMainClient ConvertMySQLToEntity(MySqlDataReader reader, bool nameOnly = false)
        {
            DataEntities.HubMainClient p = new DataEntities.HubMainClient();

            p.ClientID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "ClientID"));
            p.ClientName = DBUtilities.ReturnSafeString(reader, "ClientName");
            p.UUID = DBUtilities.ReturnSafeString(reader, "UUID");

            if (nameOnly)
            {
                p.Server = "";
                p.ClientDatabase = "";
                p.UserName = "";
                p.Password = "";

                return p;
            }

            p.Server = DBUtilities.ReturnSafeString(reader, "Server");
            p.ClientDatabase = DBUtilities.ReturnSafeString(reader, "ClientDatabase");
            p.UserName = DBUtilities.ReturnSafeString(reader, "UserName");
            p.Password = DBUtilities.ReturnSafeString(reader, "Password");

            return p;
        }
    }
}
