using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class ActivityLogs : DBManager
    {

        #region BoringStuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public ActivityLogs(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public ActivityLogs(MySqlConnection sqlConnection)
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

        public List<DataEntities.ActivityLog> GetAllActivityLogs()
        {
            List<DataEntities.ActivityLog> p = new List<DataEntities.ActivityLog>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM activitylog", conn);
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

        public DataEntities.ActivityLog GetActivityLogByID(int activityLogID)
        {
            DataEntities.ActivityLog person = new DataEntities.ActivityLog();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM activitylog Where ActivityLogID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", activityLogID);
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

        public long? SaveActivityLog(DataEntities.ActivityLog p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (p.ActivityLogID == null)
                {
                    sql = @"INSERT INTO `activitylog` (`ActivityLogID`, `IP`, `Action`, `Value`, `ReturnedLID`, `ApplicationLID`, `CreatedBy`, `CreatedDate`) VALUES (NULL, @IP, @Action, @Value, @ReturnedLID, @ApplicationLID, @CreatedBy, @CreatedDate);
                            SELECT LAST_INSERT_ID();";
                }
                else
                {
                    sql = @"UPDATE `ticketcategory` SET ActivityLogID = @ID, IP = @IP, Action = @Action, Value = @Value, ReturnedLID = @ReturnedLID, ApplicationLID = @ApplicationLID, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate WHERE ActivityLogID = @ID;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", p.ActivityLogID);
                cmd.Parameters.AddWithValue("@IP", p.IP);
                cmd.Parameters.AddWithValue("@Action", p.Action);
                cmd.Parameters.AddWithValue("@Value", p.Value);
                cmd.Parameters.AddWithValue("@ReturnedLID", p.ReturnedLID);
                cmd.Parameters.AddWithValue("@ApplicationLID", p.ApplicationLID);
                cmd.Parameters.AddWithValue("@CreatedBy", p.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", p.CreatedDate);

                if (p.ActivityLogID == null) //Get new id number
                {
                    return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
                }
                else //Return id if worked
                {
                    cmd.ExecuteScalar();

                    return p.ActivityLogID;
                }
            }
        }

        internal DataEntities.ActivityLog ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.ActivityLog p = new DataEntities.ActivityLog();

            p.ActivityLogID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "ActivityLogID"));
            p.IP = DBUtilities.ReturnSafeString(reader, "IP");
            p.Action = DBUtilities.ReturnSafeString(reader, "Action");
            p.Value = DBUtilities.ReturnSafeString(reader, "Value");
            p.ReturnedLID = DBUtilities.ReturnSafeInt(reader, "ReturnedLID").Value;
            p.CreatedBy = DBUtilities.ReturnSafeInt(reader, "CreatedBy");
            p.CreatedDate = DBUtilities.ReturnDateTime(reader, "CreatedDate");

            return p;
        }
    }
}
