using System;
using System.Data;
using System.Collections.Generic;
using Shared;
using MySql.Data.MySqlClient;

namespace Test.Models
{
	public class UserProfile
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public int Age { get; set; }
		public int Experience { get; set; }
		public string Cell { get; set; }
		public string Gender { get; set; }
		public string IPAddress { get; set; }
		public string LinkedInProfile { get; set; }
		public string Password { get; set; }
		public DateTime? LastModifiedDate { get; set; }
	}
	public class UserProfileManager : BaseManager
{
    public static List<UserProfile> GetUserProfile(string whereclause, MySqlConnection conn = null)
    {
        UserProfile objUserProfile = null;
        List<UserProfile> lstUserProfile = new List<UserProfile>();
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
                            objUserProfile = ReaderDataUserProfile(reader);
                            lstUserProfile.Add(objUserProfile);
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

        return lstUserProfile;
    }

    private static UserProfile ReaderDataUserProfile(MySqlDataReader reader)
    {
        UserProfile objUserProfile = new UserProfile();
        objUserProfile.UserId = Utility.IsValidInt(reader["UserId"]);
        objUserProfile.UserName = Utility.IsValidString(reader["UserName"]);
        objUserProfile.Age = Utility.IsValidInt(reader["Age"]);
        objUserProfile.Experience = Utility.IsValidInt(reader["Experience"]);
        objUserProfile.Cell = Utility.IsValidString(reader["Cell"]);
        objUserProfile.Gender = Utility.IsValidString(reader["Gender"]);
        objUserProfile.IPAddress = Utility.IsValidString(reader["IPAddress"]);
        objUserProfile.LinkedInProfile = Utility.IsValidString(reader["LinkedInProfile"]);
        objUserProfile.Password = Utility.IsValidString(reader["Password"]);
        objUserProfile.LastModifiedDate = Utility.IsValidDateTime(reader["LastModifiedDate"]);
        return objUserProfile;
    }

    public static string SaveUserProfile(UserProfile objUserProfile, MySqlConnection conn = null)
    {
        string returnMessage = "";
        string sUserId = "";
        sUserId = objUserProfile.UserId.ToString();
        var templstUserProfile = GetUserProfile("UserId = '" + sUserId + "'", conn);
        try
        {
            bool isConnArgNull = (conn != null) ? false : true;
            MySqlConnection connection = (conn != null) ? conn : PrimaryConnection();
            tryOpenConnection(connection);
            using (MySqlCommand command = new MySqlCommand())
            {
                string sql;
                bool isEdit = true;
                if (templstUserProfile.Count <= 0)
                {
                    isEdit = false;
                    sql = @"INSERT INTO UserProfile(
UserName,
Age,
Experience,
Cell,
Gender,
IPAddress,
LinkedInProfile,
Password,
LastModifiedDate
)
VALUES(
@UserName,
@Age,
@Experience,
@Cell,
@Gender,
@IPAddress,
@LinkedInProfile,
@Password,
@LastModifiedDate
)";
                }
                else
                {
                    sql = @"Update UserProfile set
UserId=@UserId,
UserName=@UserName,
Age=@Age,
Experience=@Experience,
Cell=@Cell,
Gender=@Gender,
IPAddress=@IPAddress,
LinkedInProfile=@LinkedInProfile,
Password=@Password,
LastModifiedDate=@LastModifiedDate

Where UserId=@UserId";
                }

                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                if (isEdit)
                {
                    command.Parameters.AddWithValue("@UserId", objUserProfile.UserId);
                }

                command.Parameters.AddWithValue("@UserName", objUserProfile.UserName);
                command.Parameters.AddWithValue("@Age", objUserProfile.Age);
                command.Parameters.AddWithValue("@Experience", objUserProfile.Experience);
                command.Parameters.AddWithValue("@Cell", objUserProfile.Cell);
                command.Parameters.AddWithValue("@Gender", objUserProfile.Gender);
                command.Parameters.AddWithValue("@IPAddress", objUserProfile.IPAddress);
                command.Parameters.AddWithValue("@LinkedInProfile", objUserProfile.LinkedInProfile);
                command.Parameters.AddWithValue("@Password", objUserProfile.Password);
                command.Parameters.AddWithValue("@LastModifiedDate", objUserProfile.LastModifiedDate);
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

    public static string DeleteUserProfile(string UserId, MySqlConnection conn = null)
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
                sql = @"DELETE from UserProfile Where UserId = @UserId";
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@UserId", UserId);
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

