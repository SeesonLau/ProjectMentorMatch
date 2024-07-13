using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    internal class Account : Database
    {
        RandomID randomID = new RandomID();

        private int userID = RandomID.userID();
        private string fullname;
        private string email;
        private string password;


        //private static string connectionString = "Server=tcp:mentor-mentee.database.windows.net,1433;Initial Catalog=mentorMentee_DB;Persist Security Info=False;User ID=mentor_mentee;Password=(MSAD12345);MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public Account(string fullname, string email, string password)
        {
            this.fullname = fullname;
            this.email = email;
            this.password = password;
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        /*
        public void SignUp()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO CreateAccount (userID, fullname, email, password) VALUES (@UserID, @Fullname, @Email, @Password)";

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
        }
        */
        public void SignUp()
        {

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
    }
}
