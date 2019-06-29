using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class Projects : DBManager
    {

        #region BoringStuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public Projects(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public Projects(MySqlConnection sqlConnection)
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
            List<DataEntities.Project> p = new List<DataEntities.Project>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Project", conn);
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

        public DataEntities.Project GetProjectByID(int projectID)
        {
            DataEntities.Project person = new DataEntities.Project();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Project Where ProjectID = @ProjectID ;", conn);

                cmd.Parameters.AddWithValue("@ProjectID", projectID);
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

        public long? SaveProject(DataEntities.Project p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (p.ProjectID == null)
                {
                    sql = @"INSERT INTO `Project` (`ProjectID`, `Name`, `Description`, `CreatedDate`) VALUES (NULL, @Name, @Description, @CreatedDate);
                            SELECT LAST_INSERT_ID();";
                }
                else
                {
                    sql = @"UPDATE `Project` SET ProjectID = @ProjectID, Name = @Name, Description = @Description, CreatedDate = @CreatedDate WHERE ProjectID = @ProjectID;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ProjectID", p.ProjectID);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Description", p.Description);
                cmd.Parameters.AddWithValue("@CreatedDate", p.CreatedDate);

                return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
            }
        }

        public DataEntities.Project ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.Project p = new DataEntities.Project();

            p.ProjectID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "ProjectID"));
            p.Name = DBUtilities.ReturnSafeString(reader, "Name");
            p.Description = DBUtilities.ReturnSafeString(reader, "Description");
            p.CreatedDate = DBUtilities.ReturnSafeDateTime(reader, "CreatedDate");

            return p;
        }
    }
}
