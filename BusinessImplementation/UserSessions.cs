using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class UserSessions : IDisposable
    {

        #region Boring Stuff

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

        public void Dispose()
        {
            if (sqlConnection != null)
                sqlConnection.Dispose();
        }

        #endregion

        public List<DataEntities.UserSession> GetAllUserSessions()
        {
            try
            {
                DatabaseLogicLayer.UserSessions userSessionDLL = new DatabaseLogicLayer.UserSessions(GetConnection());

                return userSessionDLL.GetAllUserSessions();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.UserSession GetUserSessionByID(int userSessionID)
        {
            try
            {
                DatabaseLogicLayer.UserSessions userSessionDLL = new DatabaseLogicLayer.UserSessions(GetConnection());

                return userSessionDLL.GetUserSessionByID(userSessionID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.UserSession GetUserSessionByGUID(string userSessionGUID)
        {
            try
            {
                DatabaseLogicLayer.UserSessions userSessionDLL = new DatabaseLogicLayer.UserSessions(GetConnection());

                return userSessionDLL.GetUserSessionByGUID(userSessionGUID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.UserSession GetUserSessionLatestRecordForUserID(int userID)
        {
            try
            {
                DatabaseLogicLayer.UserSessions userSessionDLL = new DatabaseLogicLayer.UserSessions(GetConnection());

                var userSession = userSessionDLL.GetUserSessionLatestRecordForUserID(userID);

                if (userSession.ExpireDate <= DateTime.Now)
                {
                    return null;
                }

                return userSession;
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public long? SaveUserSession(DataEntities.UserSession priority)
        {
            try
            {
                DatabaseLogicLayer.UserSessions userSessionDLL = new DatabaseLogicLayer.UserSessions(GetConnection());

                return userSessionDLL.SaveUserSession(priority);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }
    }
}
