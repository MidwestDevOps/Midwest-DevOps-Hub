using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Projects
    {

        #region Boring Stuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public Projects (string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Projects (MySqlConnection sqlConnection)
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

        #endregion

        public List<DataEntities.Project> GetAllProjects()
        {
            try
            {
                DatabaseLogicLayer.Projects projectDLL = new DatabaseLogicLayer.Projects(GetConnection());

                return projectDLL.GetAllProjects();
            }
            catch (Exception e)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("logs1.txt"))
                {
                    sw.WriteLine(e.Message + e.StackTrace + e.Source + e.InnerException + e.TargetSite);
                    sw.Close();
                }
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
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("logs1.txt"))
                {
                    sw.WriteLine(e.Message + e.StackTrace + e.Source + e.InnerException + e.TargetSite);
                    sw.Close();
                }
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
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("logs1.txt"))
                {
                    sw.WriteLine(e.Message + e.StackTrace + e.Source + e.InnerException + e.TargetSite);
                    sw.Close();
                }
            }

            return null;
        }
    }
}
