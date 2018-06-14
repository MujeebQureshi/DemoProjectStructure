using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace Test.Models
{
	public class LogInEmail
	{
		public string Email { get; set; }
		public int UserId { get; set; }
	}
	public class LogInEmailManager : BaseManager
{
    public static List<LogInEmail> GetLogInEmail(string whereclause, MySqlConnection conn = null)
    {
        LogInEmail objLogInEmail = null;
        List<LogInEmail> lstLogInEmail = new List<LogInEmail>();
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
                            objLogInEmail = ReaderDataLogInEmail(reader);
                            lstLogInEmail.Add(objLogInEmail);
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

        return lstLogInEmail;
    }

    private static LogInEmail ReaderDataLogInEmail(MySqlDataReader reader)
    {
        LogInEmail objLogInEmail = new LogInEmail();
        objLogInEmail.Email = Utility.IsValidString(reader["Email"]);
        objLogInEmail.UserId = Utility.IsValidInt(reader["UserId"]);
        return objLogInEmail;
    }

    public static string SaveLogInEmail(LogInEmail objLogInEmail, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sEmail = "";
        sEmail = objLogInEmail.Email.ToString();
        var templstLogInEmail = GetLogInEmail("Email = '" + sEmail + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstLogInEmail.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO LogInEmail(
Email,
UserId
)
VALUES(
@Email,
@UserId
)";
                }
                else
                {
                    sql = @"Update LogInEmail set
Email=@Email,
UserId=@UserId

Where Email=@Email";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@Email", objLogInEmail.Email);
                }

                command.Parameters.AddWithValue("@UserId", objLogInEmail.UserId);
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

    public static string DeleteLogInEmail(string Email, MySqlConnection conn = null)
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
                sql = @"DELETE from LogInEmail Where Email = @Email";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@Email", Email);
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

