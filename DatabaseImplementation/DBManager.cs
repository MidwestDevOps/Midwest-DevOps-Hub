﻿using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace DatabaseLogicLayer
{
    public class DBManager
    {
        //public static string ConnectionString { get; set; }

        //public static MySqlConnection sqlConnection { get; set; }

        //public static MySqlConnection GetConnection()
        //{
        //    if (sqlConnection == null)
        //    {
        //        return new MySqlConnection(ConnectionString);
        //    }
        //    else
        //    {
        //        return sqlConnection;
        //    }
        //}
    }

    public static class DBUtilities
    {
        public static string ReturnSafeString(MySqlDataReader reader, String columnName)
        {
            int index = reader.GetOrdinal(columnName);
            if (!reader.IsDBNull(index))
            {
                return reader.GetString(columnName);
            }
            else
            {
                return null;
            }
        }

        public static int? ReturnSafeInt(MySqlDataReader reader, String columnName)
        {
            int index = reader.GetOrdinal(columnName);
            if (!reader.IsDBNull(index))
            {
                return reader.GetString(columnName).ConvertToNullableInt();
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ReturnSafeDateTime(MySqlDataReader reader, String columnName)
        {
            int index = reader.GetOrdinal(columnName);
            if (!reader.IsDBNull(index))
            {
                return reader.GetString(columnName).ConvertToNullableDateTime();
            }
            else
            {
                return null;
            }
        }
    }
}
