using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace Test.Models
{
	public class Roles
	{
		public int RoleId { get; set; }
		public string RoleName { get; set; }
	}
	public class RolesManager : BaseManager
{
    public static List<Roles> GetRoles(string whereclause, MySqlConnection conn = null)
    {
        Roles objRoles = null;
        List<Roles> lstRoles = new List<Roles>();
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
                            objRoles = ReaderDataRoles(reader);
                            lstRoles.Add(objRoles);
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

        return lstRoles;
    }

    private static Roles ReaderDataRoles(MySqlDataReader reader)
    {
        Roles objRoles = new Roles();
        objRoles.RoleId = Utility.IsValidInt(reader["RoleId"]);
        objRoles.RoleName = Utility.IsValidString(reader["RoleName"]);
        return objRoles;
    }

    public static string SaveRoles(Roles objRoles, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sRoleId = "";
        sRoleId = objRoles.RoleId.ToString();
        var templstRoles = GetRoles("RoleId = '" + sRoleId + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstRoles.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO Roles(
RoleName
)
VALUES(
@RoleName
)";
                }
                else
                {
                    sql = @"Update Roles set
RoleId=@RoleId,
RoleName=@RoleName

Where RoleId=@RoleId";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@RoleId", objRoles.RoleId);
                }

                command.Parameters.AddWithValue("@RoleName", objRoles.RoleName);
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

    public static string DeleteRoles(string RoleId, MySqlConnection conn = null)
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
                sql = @"DELETE from Roles Where RoleId = @RoleId";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@RoleId", RoleId);
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

