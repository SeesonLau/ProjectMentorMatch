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
        private double points = 0; // unsa ang formula para ani??
        private int profileID = 0;

        public int GetAnalyticsID(int profileID)
        {
            string analyticsIDQuery = "SELECTP analyticsID FROM Profile WHERE profileID = @profileID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(analyticsIDQuery, connection))
            {
                command.Parameters.AddWithValue("@profileID", profileID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    analyticsID = Convert.ToInt32(result);
                    SetProfileID(profileID);
                }
            }
            return analyticsID;
        }

        // not yet tested
        public int GetBrainReact(int profileID)
        {
            string query = "SELECT brainReact FROM Analytics WHERE profileID = @profileID";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@profileID", profileID);
                }
            }

            return brainReact;
        }
        public double GetPoints()
        {
            return points;
        }

        public void SetAnalyticsID(int analyticsID) { this.analyticsID = analyticsID; }
        public void SetBrainReact(int brainReact) { this.brainReact = brainReact; }
        public void SetPoints(double points) { this.points = points; }

        // not yet tested
        public void UpdateBrainReact(int profileID)
        {
            brainReact += 1;
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "UPDATE Analytics SET brainReact = brainReact + 1, day = @day WHERE profileID = @profileID";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@profileID", profileID);
                    command.Parameters.AddWithValue("@day", currentDate);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
