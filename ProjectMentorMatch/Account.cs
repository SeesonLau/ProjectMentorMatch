using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch
{
    internal class Account
    {
        private int userID;
        private string fullname;
        private string email;
        private string password;

        private static string connectionString = "Server=tcp:mentor-mentee.database.windows.net,1433;Initial Catalog=mentorMentee_DB;Persist Security Info=False;User ID=mentor_mentee;Password=(MSAD12345);MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public Account(string fullname, string email, string password)
        {
            Random random = new Random();
            this.userID = random.Next(100000, 999999); // Generates a random 6-digit number
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
    }
}
