using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class Versions : DBManager
    {

        #region BoringStuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public Versions(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Versions(MySqlConnection sqlConnection)
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

        public List<DataEntities.Version> GetAllVersions()
        {
            List<DataEntities.Version> p = new List<DataEntities.Version>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM version;", conn);
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

        public DataEntities.Version GetLatestVersion()
        {
            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM version ORDER BY CreatedDate DESC;", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return ConvertMySQLToEntity(reader);
                    }
                }
            }

            return new DataEntities.Version();
        }

        public DataEntities.Version GetVersionByID(int versionID)
        {
            DataEntities.Version person = new DataEntities.Version();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM version Where VersionID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", versionID);
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

        //public long? SavePriority(DataEntities.TicketPriority p)
        //{
        //    using (MySqlConnection conn = GetConnection())
        //    {
        //        conn.Open();

        //        string sql = "";

        //        if (p.TicketPriorityID == null)
        //        {
        //            sql = @"INSERT INTO `ticketpriority` (`TicketPriorityID`, `Name`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `ModifiedDate`, `Active`) VALUES (NULL, @Name, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @Active);
        //                    SELECT LAST_INSERT_ID();";
        //        }
        //        else
        //        {
        //            sql = @"UPDATE `ticketpriority` SET TicketPriorityID = @ID, Name = @Name, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate, Active = @Active WHERE TicketPriorityID = @ID;";
        //        }

        //        MySqlCommand cmd = new MySqlCommand(sql, conn);

        //        cmd.Parameters.AddWithValue("@ID", p.TicketPriorityID);
        //        cmd.Parameters.AddWithValue("@Name", p.Name);
        //        cmd.Parameters.AddWithValue("@CreatedBy", p.CreatedBy);
        //        cmd.Parameters.AddWithValue("@CreatedDate", p.CreatedDate);
        //        cmd.Parameters.AddWithValue("@ModifiedBy", p.ModifiedBy);
        //        cmd.Parameters.AddWithValue("@ModifiedDate", p.ModifiedDate);
        //        cmd.Parameters.AddWithValue("@Active", p.Active);

        //        if (p.TicketPriorityID == null) //Get new id number
        //        {
        //            return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
        //        }
        //        else //Return id if worked
        //        {
        //            cmd.ExecuteScalar();

        //            return p.TicketPriorityID;
        //        }
        //    }
        //}

        internal DataEntities.Version ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.Version p = new DataEntities.Version();

            p.VersionID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "VersionID"));
            p.ServerVersion = DBUtilities.ReturnSafeString(reader, "ServerVersion");
            p.CreatedDate = DBUtilities.ReturnSafeDateTime(reader, "CreatedDate").Value;

            return p;
        }
    }
}
