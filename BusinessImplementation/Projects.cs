using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Projects : BLLManager, IDisposable
    {

        public Projects(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Projects(MySqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public List<DataEntities.Project> GetAllProjects()
        {
            try
            {
                DatabaseLogicLayer.Projects projectDLL = new DatabaseLogicLayer.Projects(GetConnection());

                return projectDLL.GetAllProjects();
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public DataEntities.Project GetProjectByID(int projectID)
        {
            try
            {
                DatabaseLogicLayer.Projects projectDLL = new DatabaseLogicLayer.Projects(GetConnection());

                return projectDLL.GetProjectByID(projectID);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }

        public long? SaveProject(DataEntities.Project project)
        {
            try
            {
                DatabaseLogicLayer.Projects projectDLL = new DatabaseLogicLayer.Projects(GetConnection());

                return projectDLL.SaveProject(project);
            }
            catch (Exception e)
            {
                Logging.SaveLog(new Log() { time = DateTime.Now, exception = e });
            }

            return null;
        }
    }
}
