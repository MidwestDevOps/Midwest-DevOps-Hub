using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Versions : BLLManager, IDisposable
    {

        public Versions(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Versions(MySqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public List<DataEntities.Version> GetAllVersions()
        {
            try
            {
                DatabaseLogicLayer.Versions versionDLL = new DatabaseLogicLayer.Versions(GetConnection());

                return versionDLL.GetAllVersions();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.Version GetLatestVersion()
        {
            try
            {
                DatabaseLogicLayer.Versions versionDLL = new DatabaseLogicLayer.Versions(GetConnection());

                return versionDLL.GetLatestVersion();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.Version GetVersionByID(int versionID)
        {
            try
            {
                DatabaseLogicLayer.Versions versionDLL = new DatabaseLogicLayer.Versions(GetConnection());

                return versionDLL.GetVersionByID(versionID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        //public long? SavePriority(DataEntities.Version priority)
        //{
        //    try
        //    {
        //        DatabaseLogicLayer.Versions versionDLL = new DatabaseLogicLayer.Versions(GetConnection());

        //        return versionDLL.SavePriority(priority);
        //    }
        //    catch (Exception e)
        //    {
        //        Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
        //    }

        //    return null;
        //}
    }
}
