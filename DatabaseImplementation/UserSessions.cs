using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class UserSessions : DBManager
    {

        #region BoringStuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public UserSessions(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public UserSessions(MySqlConnection sqlConnection)
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

        public List<DataEntities.UserSession> GetAllUserSessions()
        {
            List<DataEntities.UserSession> p = new List<DataEntities.UserSession>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usersession", conn);
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

        public DataEntities.UserSession GetUserSessionByID(int userSessionID)
        {
            DataEntities.UserSession person = new DataEntities.UserSession();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usersession Where UserSessionID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", userSessionID);
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

        public DataEntities.UserSession GetUserSessionByGUID(string userSessionGUID)
        {
            DataEntities.UserSession person = new DataEntities.UserSession();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM usersession Where GUID = @GUID;", conn);

                cmd.Parameters.AddWithValue("@GUID", userSessionGUID);
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

        public DataEntities.UserSession GetUserSessionLatestRecordForUserID(int userID)
        {
            DataEntities.UserSession person = new DataEntities.UserSession();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `usersession` WHERE UserID = @UserID ORDER BY CreatedDate DESC LIMIT 1;", conn);

                cmd.Parameters.AddWithValue("@UserID", userID);
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

        public long? SaveUserSession(DataEntities.UserSession p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (p.UserSessionID == null)
                {
                    sql = @"INSERT INTO `usersession` (`UserSessionID`, `GUID`, `UserID`, `StatusLID`, `ExpireDate`, `CreatedDate`) VALUES (NULL, @GUID, @UserID, @StatusLID, @ExpireDate, @CreatedDate);
                            SELECT LAST_INSERT_ID();";
                }
                else
                {
                    sql = @"UPDATE `usersession` SET UserSessionID = @ID, GUID = @GUID, UserID = @UserID, StatusLID = @StatusLID, ExpireDate = @ExpireDate, CreatedDate = @CreatedDate WHERE UserSessionID = @ID;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", p.UserSessionID);
                cmd.Parameters.AddWithValue("@GUID", p.GUID);
                cmd.Parameters.AddWithValue("@UserID", p.UserID);
                cmd.Parameters.AddWithValue("@StatusLID", p.StatusLID);
                cmd.Parameters.AddWithValue("@ExpireDate", p.ExpireDate);
                cmd.Parameters.AddWithValue("@CreatedDate", p.CreatedDate);

                if (p.UserSessionID == null) //Get new id number
                {
                    return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
                }
                else //Return id if worked
                {
                    cmd.ExecuteScalar();

                    return p.UserSessionID;
                }
            }
        }

        internal DataEntities.UserSession ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.UserSession p = new DataEntities.UserSession();

            p.UserSessionID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "UserSessionID"));
            p.GUID = DBUtilities.ReturnSafeString(reader, "GUID");
            p.UserID = DBUtilities.ReturnSafeInt(reader, "UserID").Value;
            p.StatusLID = DBUtilities.ReturnSafeInt(reader, "StatusLID").Value;
            p.ExpireDate = DBUtilities.ReturnSafeDateTime(reader, "ExpireDate").Value;
            p.CreatedDate = DBUtilities.ReturnSafeDateTime(reader, "CreatedDate").Value;

            return p;
        }
    }
}
