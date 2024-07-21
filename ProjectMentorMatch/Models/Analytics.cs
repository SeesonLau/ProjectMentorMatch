using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Analytics : ProfileInformation
    {
        private int analyticsID = RandomID.analyticsID();
        private int brainReact = 0;
        private double points = 0; // unsa ang formula para ani??
        private int profileID = 0;

        public int GetAnalyticsID()
        {
            return analyticsID;
        }

        // not yet tested
        public int GetBrainReact()
        {
            string query = "SELECT brainReact FROM Analytics WHERE analyticsID = @analyticsID AND profileID = @profileID";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@analyticsID", analyticsID);
                    command.Parameters.AddWithValue("@profileID", profileID);
                }
            }

            return brainReact;
        }
        public double GetPoints()
        {
            return points;
        }

        public void SetBrainReact(int brainReact)
        {
            this.brainReact = brainReact;
        }

        public void SetPoints(double points)
        {
            this.points = points;
        }

        // not yet tested
        public void UpdateBrainReact()
        {
            brainReact += 1;
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = "UPDATE Analytics SET brainReact = brainReact + 1, day = @day WHERE analyticsID = @analyticsID AND profileID = @profileID";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@analyticsID", analyticsID);
                    command.Parameters.AddWithValue("@profileID", profileID);
                    command.Parameters.AddWithValue("@day", currentDate);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
