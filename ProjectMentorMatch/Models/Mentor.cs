using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMentorMatch.ViewModels;

namespace ProjectMentorMatch.Models
{
    public class Mentor : Database
    {
        ScheduleViewModel svm = new ScheduleViewModel();

        private static readonly Random random = new Random();
        private string? aboutMe;
        private string? mentorFee;
        private string? academic;
        private string? nonacademic;
        private string? day;
        private int isMentor = 1;
        private string? setup;
        private string? interaction;

        //GET
        public string? GetAboutMe(int userID)
        {
            string query = "SELECT [AboutMe] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    aboutMe = result.ToString();
                }
            }
            return aboutMe;
        }
        public string? GetMentorFee(int userID)
        {
            string query = "SELECT [MentorFee] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    mentorFee = result.ToString();
                }
            }
            return mentorFee;
        }
        public string? GetAcademic(int userID)
        {
            string query = "SELECT [Academic] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    academic = result.ToString();
                }
            }
            return academic;
        }
        public string? GetNonAcademic(int userID)
        {
            string query = "SELECT [NonAcademic] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    nonacademic = result.ToString();
                }
            }
            return nonacademic;
        }
        public string? GetDay(int userID)
        {
            string query = "SELECT [Day] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    day = result.ToString();
                }
            }
            return day;
        }
        public string? GetSetup(int userID)
        {
            string query = "SELECT [Setup] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    setup = result.ToString();
                }
            }
            return setup;
        }
        public string? GetInteraction(int userID)
        {
            string query = "SELECT [Interaction] FROM [Mentor] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    interaction = result.ToString();
                }
            }
            return interaction;
        }
        //SET
        public void SetAboutMe(string? aboutMe) {  this.aboutMe = aboutMe; }
        public void SetMentorFee(string? mentorFee) { this.mentorFee = mentorFee; }
        public void SetAcademic(string? academic) { this.academic = academic; }
        public void SetNonAcademic (string? nonacademic) { this.nonacademic = nonacademic; }
        public void SetDay(string day) { this.day = day; }
        public void SetSetup(string? setup) { this.setup = setup; }
        public void SetInteraction(string? interaction) { this.interaction = interaction; }


        public void InsertApplyAsMentor(int userID)
        {
            bool profileExists = CheckMentorExist(userID);
            string query;
            if (profileExists)
            {
                query = "UPDATE Mentor SET [isMentor] = @isMentor, [AboutMe] = @AboutMe, [MentorFee] = @MentorFee " +
                        "WHERE [UserID] = @UserID";
            }
            else
            {
                query = "INSERT INTO Profile ([UserID], [isMentor], [AboutMe], [MentorFee])" +

                        "VALUES (@UserID, @isMentor, @AboutMe, @MentorFee)";
            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@isMentor", isMentor);
                command.Parameters.AddWithValue("@AboutMe", aboutMe);
                command.Parameters.AddWithValue("@MentorFee", mentorFee);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public bool CheckMentorExist(int userID)
        {
            string query = "SELECT COUNT(*) FROM Mentor WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
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