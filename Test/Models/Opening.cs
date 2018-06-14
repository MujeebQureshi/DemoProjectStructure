using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace Test.Models
{
	public class Opening
	{
		public int OpeningId { get; set; }
		public int OpeningTypeId { get; set; }
	}
	public class OpeningManager : BaseManager
{
    public static List<Opening> GetOpening(string whereclause, MySqlConnection conn = null)
    {
        Opening objOpening = null;
        List<Opening> lstOpening = new List<Opening>();
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
                            objOpening = ReaderDataOpening(reader);
                            lstOpening.Add(objOpening);
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

        return lstOpening;
    }

    private static Opening ReaderDataOpening(MySqlDataReader reader)
    {
        Opening objOpening = new Opening();
        objOpening.OpeningId = Utility.IsValidInt(reader["OpeningId"]);
        objOpening.OpeningTypeId = Utility.IsValidInt(reader["OpeningTypeId"]);
        return objOpening;
    }

    public static string SaveOpening(Opening objOpening, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sOpeningId = "";
        sOpeningId = objOpening.OpeningId.ToString();
        var templstOpening = GetOpening("OpeningId = '" + sOpeningId + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstOpening.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO Opening(
OpeningTypeId
)
VALUES(
@OpeningTypeId
)";
                }
                else
                {
                    sql = @"Update Opening set
OpeningId=@OpeningId,
OpeningTypeId=@OpeningTypeId

Where OpeningId=@OpeningId";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@OpeningId", objOpening.OpeningId);
                }

                command.Parameters.AddWithValue("@OpeningTypeId", objOpening.OpeningTypeId);
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

    public static string DeleteOpening(string OpeningId, MySqlConnection conn = null)
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
                sql = @"DELETE from Opening Where OpeningId = @OpeningId";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@OpeningId", OpeningId);
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

