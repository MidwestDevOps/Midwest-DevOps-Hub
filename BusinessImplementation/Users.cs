using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Users : IDisposable
    {

        #region Boring Stuff

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

        public MySqlConnection GetConnection()
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

        public void Dispose()
        {
            if (sqlConnection != null)
                sqlConnection.Dispose();
        }

        #endregion

        public bool VerifyPassword(DataEntities.User user, string userPassword)
        {
            string s = TextHasher.Hash(userPassword, user.UUID);
            return TextHasher.Verify(userPassword, user.Password, user.UUID);
        }

        public string HashPassword(DataEntities.User user, string userPassword)
        {
            string s = TextHasher.Hash(userPassword, user.UUID);
            return s;
        }

        public List<DataEntities.User> GetAllUsers()
        {
            try
            {
                DatabaseLogicLayer.Users userDLL = new DatabaseLogicLayer.Users(GetConnection());

                return userDLL.GetAllUsers();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.User GetUserByID(int userID)
        {
            try
            {
                DatabaseLogicLayer.Users userDLL = new DatabaseLogicLayer.Users(GetConnection());

                return userDLL.GetUserByID(userID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.User GetUserByUsername(string userName)
        {
            try
            {
                DatabaseLogicLayer.Users userDLL = new DatabaseLogicLayer.Users(GetConnection());

                return userDLL.GetUserByUsername(userName);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public long? SaveProject(DataEntities.User user)
        {
            try
            {
                DatabaseLogicLayer.Users userDLL = new DatabaseLogicLayer.Users(GetConnection());

                return userDLL.SaveUser(user);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }
    }
}
