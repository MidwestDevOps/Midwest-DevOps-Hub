﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class TicketCategories : DBManager
    {

        #region BoringStuff

        string ConnectionString { get; set; }

        MySqlConnection sqlConnection { get; set; }

        public TicketCategories(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public TicketCategories(MySqlConnection sqlConnection)
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

        public List<DataEntities.TicketCategory> GetAllCategories()
        {
            List<DataEntities.TicketCategory> p = new List<DataEntities.TicketCategory>();

            var s = GetConnection();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ticketcategory", conn);
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

        public DataEntities.TicketCategory GetCategoryByID(int ticketCategoryID)
        {
            DataEntities.TicketCategory person = new DataEntities.TicketCategory();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ticketcategory Where TicketCategoryID = @ID;", conn);

                cmd.Parameters.AddWithValue("@ID", ticketCategoryID);
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

        public long? SaveCategory(DataEntities.TicketCategory p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string sql = "";

                if (p.TicketCategoryID == null)
                {
                    sql = @"INSERT INTO `ticketcategory` (`TicketCategoryID`, `Name`) VALUES (NULL, @Name);
                            SELECT LAST_INSERT_ID();";
                }
                else
                {
                    sql = @"UPDATE `ticketcategory` SET TicketCategoryID = @ID, Name = @Name WHERE TicketCategoryID = @ID;";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", p.TicketCategoryID);
                cmd.Parameters.AddWithValue("@Name", p.Name);

                if (p.TicketCategoryID == null) //Get new id number
                {
                    return cmd.ExecuteScalar().ToString().ConvertToNullableInt();
                }
                else //Return id if worked
                {
                    cmd.ExecuteScalar();

                    return p.TicketCategoryID;
                }
            }
        }

        internal DataEntities.TicketCategory ConvertMySQLToEntity(MySqlDataReader reader)
        {
            DataEntities.TicketCategory p = new DataEntities.TicketCategory();

            p.TicketCategoryID = Convert.ToInt32(DBUtilities.ReturnSafeInt(reader, "TicketCategoryID"));
            p.Name = DBUtilities.ReturnSafeString(reader, "Name");

            return p;
        }
    }
}
