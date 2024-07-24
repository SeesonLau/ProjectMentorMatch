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
        private int profileID = 0;

        public int GetAnalyticsID(int profileID)
        {
            string analyticsIDQuery = "SELECT analyticsID FROM Profile WHERE ProfileID = @ProfileID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(analyticsIDQuery, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileID);
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

        // not yet tested
        public void UpdateBrainReact(int profileID)
        {
            brainReact += 1;
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "UPDATE Analytics SET brainReact = brainReact + 1, day = @day WHERE ProfileID = @ProfileID";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProfileID", profileID);
                    command.Parameters.AddWithValue("@day", currentDate);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
