using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Mentor : Database
    {
        private static readonly Random random = new Random();

        public static int GenerateUserID()
        {
            return random.Next(1, 50); // Generates a number from 1 to 49 inclusive
        }

        public static int CheckUserIDGenerator()
        {
            int userID;
            int? existingUserID;
            do
            {
                userID = GenerateUserID();
                existingUserID = UserIDExistsInDatabase(userID);
            } while (!existingUserID.HasValue);

            return existingUserID.Value;
        }

        private static int? UserIDExistsInDatabase(int userID)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Profile WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    int count = (int)command.ExecuteScalar();
                    return count > 0 ? userID : (int?)null;
                }
            }
        }

        public static int? GetProfileIDIfMentor(int userID)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT ProfileID FROM Profile WHERE UserID = @UserID AND isMentor = @isMentor";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@isMentor", 1);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}