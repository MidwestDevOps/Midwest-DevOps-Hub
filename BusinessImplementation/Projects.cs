﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class Projects : IDisposable
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
