using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Database
    {
        // Inheritance is very useful, thank you very much 1 line of code ra e change aning connection string
        private static string connectionString = "Server=tcp:mentor2.database.windows.net,1433;Initial Catalog=mentor-mentee2;Persist Security Info=False;User ID=mentormentee2;Password=#MSADSaging;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static SqlConnection? connection;
        //TEST CONNECTION
        public static async Task<bool> TestConnectionAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
      
        //TEST TO INSERT DATA
        public static async Task<bool> InsertDataAsync(string dataToInsert)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO TestTable (TestData) VALUES (@TestData)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@TestData", dataToInsert);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        //Here lies the connection string that eveyone inherits
        // Making the GetConnection() static to be able to call it without creating an instance of the class
        public static SqlConnection GetConnection()
        {
            connection = new SqlConnection(connectionString);
            return connection;
        }
    }
    
  
}

