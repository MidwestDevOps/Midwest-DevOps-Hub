using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class ActivityLogs : IDisposable
    {

        #region Boring Stuff

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

        public void Dispose()
        {
            if (sqlConnection != null)
                sqlConnection.Dispose();
        }

        #endregion

        public List<DataEntities.ActivityLog> GetAllActivityLogs()
        {
            try
            {
                DatabaseLogicLayer.ActivityLogs activityLogDLL = new DatabaseLogicLayer.ActivityLogs(GetConnection());

                return activityLogDLL.GetAllActivityLogs();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.ActivityLog GetActivityLogByID(int activityLogID)
        {
            try
            {
                DatabaseLogicLayer.ActivityLogs activityLogDLL = new DatabaseLogicLayer.ActivityLogs(GetConnection());

                return activityLogDLL.GetActivityLogByID(activityLogID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public long? SaveActivityLog(DataEntities.ActivityLog activityLog)
        {
            try
            {
                DatabaseLogicLayer.ActivityLogs activityLogDLL = new DatabaseLogicLayer.ActivityLogs(GetConnection());

                return activityLogDLL.SaveActivityLog(activityLog);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }
    }
}
