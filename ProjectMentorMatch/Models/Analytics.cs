using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMentorMatch.ViewModels;
using static ProjectMentorMatch.ViewModels.SubjectsViewModel;
using Microsoft.Maui.Controls;


namespace ProjectMentorMatch.Models
{
    public class AnalyticsModel : ProfileInformation
    {
        private int analyticsID = RandomID.analyticsID();
        private int brainReact = 0;
        private double pointsMax = 0; // unsa ang formula para ani??
        private double pointsGain = 0;
        private int ProfileID { get; set; }

        public string? GetAnalyticsID(int profileID)
        {
            string query = "SELECT AnalyticsID FROM Profile WHERE ProfileID = @ProfileID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AnalyticsID", analyticsID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }

            return null; // Return null if no city is found
        }


        // not yet tested
        public int GetBrainReact(int profileID)
        {
            string query = "SELECT brainReact FROM Analytics WHERE ProfileID = @ProfileID";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProfileID", profileID);
                }
            }

            return brainReact;
        }
        public double GetPointsMax()
        {
            return pointsMax;
        }
        public double GetPointsGain()
        {
            return pointsGain;
        }

        public void SetAnalyticsID(int analyticsID) { this.analyticsID = analyticsID; }
        public void SetBrainReact(int brainReact) { this.brainReact = brainReact; }
        public void SetPointsMax(double pointsMax) { this.pointsMax = pointsMax; }
        public void SetPointsGain(double pointsGain) { this.pointsGain = pointsGain; }

        public static void CheckProfileIDInAnalytics(int profileID)
        {
            string checkQuery = "SELECT COUNT(*) FROM Analytics WHERE ProfileID = @ProfileID";

            using (var connection = Database.GetConnection())
            {
                connection.Open();

                // Check if the ProfileID already exists
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@ProfileID", profileID);
                    int count = (int)checkCommand.ExecuteScalar();
                }
            }
        }

        public void UpdateBrainReact(int profileID)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Ensure the ProfileID is logged with default values if not already present
            CheckProfileIDInAnalytics(profileID);

            // Query to check if an entry exists for the current date and profileID
            string checkQuery = "SELECT COUNT(*) FROM Analytics WHERE ProfileID = @ProfileID AND day = @day";

            // Query to update brainReact if entry exists
            string updateQuery = "UPDATE Analytics SET brainReact = ISNULL(brainReact, 0) + 1 WHERE ProfileID = @ProfileID AND day = @day";

            // Query to insert a new entry if no entry exists for the current date and profileID
            string insertQuery = "INSERT INTO Analytics (ProfileID, brainReact, day) VALUES (@ProfileID, 1, @day)";

            using (var connection = GetConnection())
            {
                connection.Open();

                // Check if the entry for the current date exists
                using (SqlCommand checkDateCommand = new SqlCommand(checkQuery, connection))
                {
                    checkDateCommand.Parameters.AddWithValue("@ProfileID", profileID);
                    checkDateCommand.Parameters.AddWithValue("@day", currentDate);
                    int dateCount = (int)checkDateCommand.ExecuteScalar();

                    if (dateCount > 0)
                    {
                        // Date entry exists, update brainReact
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@ProfileID", profileID);
                            updateCommand.Parameters.AddWithValue("@day", currentDate);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Date entry does not exist, insert a new entry
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@ProfileID", profileID);
                            insertCommand.Parameters.AddWithValue("@day", currentDate);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
