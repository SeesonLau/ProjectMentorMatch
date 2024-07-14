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

        /*
        public Account(string fullname, string email, string password)
        {
            this.fullname = fullname;
            this.email = email;
            this.password = password;
        }*/
        public int GetUserID()
        {
            return userID;
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
        public void SetUserID(int userID) {this.userID = userID;}
        public void SetFullname(string fullname) {this.fullname = fullname;}  
        public void SetEmail(string email) {this.email = email;}
        public void SetPassword(string password) {this.password = password;}

        public void SignUp()
        {
            // POSSIBLE NULL REFERENCE example
            /*if (CheckEmailIsTaken(email))
            {
                throw new Exception("Email already exists. Please use a different email.");
            }*/

            // CORRECT WAY
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

        public bool LogIn()
        {
            string query = "SELECT COUNT(*) FROM CreateAccount WHERE email = @Email AND password = @Password";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int userCount = (int)command.ExecuteScalar();

                return userCount > 0;
            }
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
    }
}
