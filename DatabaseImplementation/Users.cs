using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class Users : DBManager
    {

        #region BoringStuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public Users(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Users(MySqlConnection sqlConnection)
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

        public List<DataEntities.User> GetAllUsers()
        {
            List<DataEntities.User> p = new List<DataEntities.User>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user", conn);
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

        public DataEntities.User GetUserByID(int userID)
        {
            DataEntities.User person = new DataEntities.User();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user Where UserID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", userID);
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

        public DataEntities.User GetUserByUsername(string userName)
        {
            DataEntities.User person = new DataEntities.User();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user Where Username = @Username;", conn);

                cmd.Parameters.AddWithValue("@Username", userName);
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

        public long? SaveUser(DataEntities.User p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (p.UserID == null)
                {
                    sql = @"INSERT INTO `user` (`UserID`, `Username`, `Password`, `CreatedBy`, `CreatedDate`, `ModifiedBy`, `ModifiedDate`) VALUES (NULL, @Username, @Password, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate);
                            SELECT LAST_INSERT_ID();";
                }
                else
                {
                    sql = @"UPDATE `user` SET UserID = @ID, Username = @Username, Password = @Password, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE UserID = @ID;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", p.UserID);
                cmd.Parameters.AddWithValue("@Username", p.Username);
                cmd.Parameters.AddWithValue("@Password", p.Password);



                if (p.UserID == null) //Get new id number
                {
                    return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
                }
                else //Return id if worked
                {
                    cmd.ExecuteScalar();

                    return p.UserID;
                }
            }
        }

        internal DataEntities.User ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.User p = new DataEntities.User();

            p.UserID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "UserID"));
            p.UUID = DBUtilities.ReturnSafeString(reader, "UUID");
            p.Username = DBUtilities.ReturnSafeString(reader, "Username");
            p.Password = DBUtilities.ReturnSafeString(reader, "Password");
            p.Active = DBUtilities.ReturnBoolean(reader, "Active");
            p.CreatedBy = DBUtilities.ReturnSafeInt(reader, "CreatedBy");
            p.CreatedDate = DBUtilities.ReturnSafeDateTime(reader, "CreatedDate");
            p.ModifiedBy = DBUtilities.ReturnSafeInt(reader, "ModifiedBy");
            p.ModifiedDate = DBUtilities.ReturnSafeDateTime(reader, "ModifiedDate");

            return p;
        }
    }
}
