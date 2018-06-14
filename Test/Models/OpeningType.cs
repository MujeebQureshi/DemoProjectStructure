using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace Test.Models
{
	public class OpeningType
	{
		public int OpeningTypeId { get; set; }
		public string Type { get; set; }
	}
	public class OpeningTypeManager : BaseManager
{
    public static List<OpeningType> GetOpeningType(string whereclause, MySqlConnection conn = null)
    {
        OpeningType objOpeningType = null;
        List<OpeningType> lstOpeningType = new List<OpeningType>();
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            string sql = "";
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = connection;
                command.CommandText = sql;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objOpeningType = ReaderDataOpeningType(reader);
                            lstOpeningType.Add(objOpeningType);
                        }
                    }
                    else
                    {
                    }
                }
            }

            if (isConnArgNull == true)
            {
                connection.Dispose();
            }
        }
        catch (Exception ex)
        {
        }

        return lstOpeningType;
    }

    private static OpeningType ReaderDataOpeningType(MySqlDataReader reader)
    {
        OpeningType objOpeningType = new OpeningType();
        objOpeningType.OpeningTypeId = Utility.IsValidInt(reader["OpeningTypeId"]);
        objOpeningType.Type = Utility.IsValidString(reader["Type"]);
        return objOpeningType;
    }

    public static string SaveOpeningType(OpeningType objOpeningType, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sOpeningTypeId = "";
        sOpeningTypeId = objOpeningType.OpeningTypeId.ToString();
        var templstOpeningType = GetOpeningType("OpeningTypeId = '" + sOpeningTypeId + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstOpeningType.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO OpeningType(
Type
)
VALUES(
@Type
)";
                }
                else
                {
                    sql = @"Update OpeningType set
OpeningTypeId=@OpeningTypeId,
Type=@Type

Where OpeningTypeId=@OpeningTypeId";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@OpeningTypeId", objOpeningType.OpeningTypeId);
                }

                command.Parameters.AddWithValue("@Type", objOpeningType.Type);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    returnMessage = "OK";
                }
                else
                {
                    returnMessage = "Unable to save, Please contact ISD";
                }
            }

            if (isConnArgNull == true)
            {
                connection.Dispose();
            }
        }
        catch (Exception ex)
        {
        }

        return returnMessage;
    }

    public static string DeleteOpeningType(string OpeningTypeId, MySqlConnection conn = null)
    {
        string returnMessage = "";
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                sql = @"DELETE from OpeningType Where OpeningTypeId = @OpeningTypeId";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@OpeningTypeId", OpeningTypeId);
                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    returnMessage = "OK";
                }
                else
                {
                    returnMessage = "Unable to save, Please contact ISD";
                }
            }

            if (isConnArgNull == true)
            {
                connection.Dispose();
            }
        }
        catch (Exception ex)
        {
        }

        return returnMessage;
    }
}}

