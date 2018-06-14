using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace Test.Models
{
	public class Admin
	{
		public int AdminId { get; set; }
		public string AdminName { get; set; }
		public string AdminEmail { get; set; }
		public string AdminPassword { get; set; }
		public int RoleId { get; set; }
	}
	public class AdminManager : BaseManager
{
    public static List<Admin> GetAdmin(string whereclause, MySqlConnection conn = null)
    {
        Admin objAdmin = null;
        List<Admin> lstAdmin = new List<Admin>();
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
                            objAdmin = ReaderDataAdmin(reader);
                            lstAdmin.Add(objAdmin);
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

        return lstAdmin;
    }

    private static Admin ReaderDataAdmin(MySqlDataReader reader)
    {
        Admin objAdmin = new Admin();
        objAdmin.AdminId = Utility.IsValidInt(reader["AdminId"]);
        objAdmin.AdminName = Utility.IsValidString(reader["AdminName"]);
        objAdmin.AdminEmail = Utility.IsValidString(reader["AdminEmail"]);
        objAdmin.AdminPassword = Utility.IsValidString(reader["AdminPassword"]);
        objAdmin.RoleId = Utility.IsValidInt(reader["RoleId"]);
        return objAdmin;
    }

    public static string SaveAdmin(Admin objAdmin, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sAdminId = "";
        sAdminId = objAdmin.AdminId.ToString();
        var templstAdmin = GetAdmin("AdminId = '" + sAdminId + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstAdmin.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO Admin(
AdminName,
AdminEmail,
AdminPassword,
RoleId
)
VALUES(
@AdminName,
@AdminEmail,
@AdminPassword,
@RoleId
)";
                }
                else
                {
                    sql = @"Update Admin set
AdminId=@AdminId,
AdminName=@AdminName,
AdminEmail=@AdminEmail,
AdminPassword=@AdminPassword,
RoleId=@RoleId

Where AdminId=@AdminId";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@AdminId", objAdmin.AdminId);
                }

                command.Parameters.AddWithValue("@AdminName", objAdmin.AdminName);
                command.Parameters.AddWithValue("@AdminEmail", objAdmin.AdminEmail);
                command.Parameters.AddWithValue("@AdminPassword", objAdmin.AdminPassword);
                command.Parameters.AddWithValue("@RoleId", objAdmin.RoleId);
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

    public static string DeleteAdmin(string AdminId, MySqlConnection conn = null)
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
                sql = @"DELETE from Admin Where AdminId = @AdminId";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@AdminId", AdminId);
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

