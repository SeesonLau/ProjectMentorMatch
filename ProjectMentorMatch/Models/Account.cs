using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Account : Database
    {
        RandomID randomID = new RandomID();
        private int userID = RandomID.userID();

        private string? fullname;
        private string? email;
        private string? password;

        private int retuserID;

        public int GetUserID()
        {
            return retuserID;
        }
        public string? GetFullname()
        {
            return fullname;
        }
        public string? GetEmail()
        {
            return email;
        }
        public string? GetPassword()
        {
            return password;
        }
        public void SetUserID(int retuserID) {this.retuserID = retuserID; }
        public void SetFullname(string fullname) {this.fullname = fullname;}  
        public void SetEmail(string email) {this.email = email;}
        public void SetPassword(string password) {this.password = password;}

        public void SignUp()
        {
            if (email != null && CheckEmailIsTaken(email))
            {
                throw new Exception("Email already exists. Please use a different email.");
            }

            string query = "INSERT INTO CreateAccount (userID, fullname, email, password) VALUES (@UserID, @Fullname, @Email, @Password)";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Fullname", fullname);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        /*
        public bool LogIn()
        {
            string query = "SELECT userID FROM CreateAccount WHERE email = @Email AND password = @Password";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    userID = Convert.ToInt32(result);
                    SetUserID(userID);
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
        }
        */
        // THE COOLER BOOL LOGIN

        public bool LogIn(string email, string password)
        {
            string countQuery = "SELECT COUNT(*) FROM [CreateAccount] WHERE [Email] = @Email AND [Password] = @Password";
            string userIDQuery = "SELECT UserID FROM CreateAccount WHERE Email = @Email";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand countCommand = new SqlCommand(countQuery, connection))
                {
                    countCommand.Parameters.AddWithValue("@Email", email);
                    countCommand.Parameters.AddWithValue("@Password", password);

                    int count = (int)countCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        using (SqlCommand userIDCommand = new SqlCommand(userIDQuery, connection))
                        {
                            userIDCommand.Parameters.AddWithValue("@Email", email);

                            object result = userIDCommand.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                int userID = Convert.ToInt32(result);
                                SetUserID(userID);
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }





        public bool CheckEmailIsTaken(string email)
        {
            string query = "SELECT COUNT(*) FROM CreateAccount WHERE email = @Email";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int emailCount = (int)command.ExecuteScalar();

                return emailCount > 0;
            }
        }

        // Fetching data from the database
        public static List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();

            string query = "SELECT fullname, email, password FROM CreateAccount";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(new Account
                        {
                            fullname = reader["fullname"].ToString(),
                            email = reader["email"].ToString(),
                            password = reader["password"].ToString()
                        });
                    }
                }
            }

            return accounts;
        }
    }
}
