using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace Test.Models
{
	public class CVBank
	{
		public int CVId { get; set; }
		public string CV { get; set; }
		public DateTime? AppliedDate { get; set; }
		public int UserId { get; set; }
		public int OpeningId { get; set; }
	}
	public class CVBankManager : BaseManager
{
    public static List<CVBank> GetCVBank(string whereclause, MySqlConnection conn = null)
    {
        CVBank objCVBank = null;
        List<CVBank> lstCVBank = new List<CVBank>();
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
                            objCVBank = ReaderDataCVBank(reader);
                            lstCVBank.Add(objCVBank);
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

        return lstCVBank;
    }

    private static CVBank ReaderDataCVBank(MySqlDataReader reader)
    {
        CVBank objCVBank = new CVBank();
        objCVBank.CVId = Utility.IsValidInt(reader["CVId"]);
        objCVBank.CV = Utility.IsValidString(reader["CV"]);
        objCVBank.AppliedDate = Utility.IsValidDateTime(reader["AppliedDate"]);
        objCVBank.UserId = Utility.IsValidInt(reader["UserId"]);
        objCVBank.OpeningId = Utility.IsValidInt(reader["OpeningId"]);
        return objCVBank;
    }

    public static string SaveCVBank(CVBank objCVBank, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sCVId = "";
        sCVId = objCVBank.CVId.ToString();
        var templstCVBank = GetCVBank("CVId = '" + sCVId + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstCVBank.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO CVBank(
CV,
AppliedDate,
UserId,
OpeningId
)
VALUES(
@CV,
@AppliedDate,
@UserId,
@OpeningId
)";
                }
                else
                {
                    sql = @"Update CVBank set
CVId=@CVId,
CV=@CV,
AppliedDate=@AppliedDate,
UserId=@UserId,
OpeningId=@OpeningId

Where CVId=@CVId";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@CVId", objCVBank.CVId);
                }

                command.Parameters.AddWithValue("@CV", objCVBank.CV);
                command.Parameters.AddWithValue("@AppliedDate", objCVBank.AppliedDate);
                command.Parameters.AddWithValue("@UserId", objCVBank.UserId);
                command.Parameters.AddWithValue("@OpeningId", objCVBank.OpeningId);
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

    public static string DeleteCVBank(string CVId, MySqlConnection conn = null)
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
                sql = @"DELETE from CVBank Where CVId = @CVId";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@CVId", CVId);
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

