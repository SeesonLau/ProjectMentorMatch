using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMentorMatch.Views;
using Microsoft.Maui.ApplicationModel.Communication;

namespace ProjectMentorMatch.Models
{
    public class ProfileInformation : ProfileModels
    {
        private int courseID = RandomID.courseID();
        private int subjectTaughtID = RandomID.subject_taughtID();
        private int subjectTakenID = RandomID.subject_takenID();
        private int scheduleID = RandomID.schedID();
        private int addressID = RandomID.addressID();
        private int genderID = RandomID.genderID();

        private string? subjectTaught;
        private string? subjectTaken;
   
        private string? courseName;

        private string? day; //e.g. Monday, Tuesday, Wednesday, Thursday, Friday
        private double initialTime;
        private double finalTime;
        private string? gender;
        private string? addressCity; 
        private string? addressProvince;

        public string? GetSubjectTaught()
        {
            return subjectTaught;
        }
        public string? GetSubjectTakern()
        {
            return subjectTaken;
        }
        public string? GetCourseName()
        {
            return courseName;
        }
        public string? GetDay()
        {
            return day;
        }
        public double GetInitialTime()
        {
            return initialTime;
        }
        public double GetFinalTime()
        {
            return finalTime;
        }
        public string? GetAddressCity()
        {
            return addressCity;
        }
        public string? GetAddressProvince()
        {
            return addressProvince;
        }
        public void SetSubjectTaught(string subjectTaught)
        {
            this.subjectTaught = subjectTaught;
        }
        public void SetSubjectTaken(string subjectTaken)
        {
            this.subjectTaken = subjectTaken;
        }
        public void SetCourseName(string courseName)
        {
            this.courseName = courseName;
        }
        public void SetDay(string day)
        {
            this.day = day;
        }
        public void SetInitialTime(double initialTime)
        {
            this.initialTime = initialTime;
        }
        public void SetFinalTime(double finalTime)
        {
            this.finalTime = finalTime;
        }
        public void SetAddressCity(string addressCity)
        {
            this.addressCity = addressCity;
        }
        public void SetAddressProvince(string addressProvince)
        {
            this.addressProvince = addressProvince;
        }
        public void SetGender(string? gender) { this.gender = gender; }

        public void InserProfileData2(int profileID)
        {
            // Check if a profile for the profileID already exists
            bool profileExists = CheckProfileExists(profileID);

            string queryGender;
            if (profileExists)
            {
                // Update the existing profile
                queryGender = "UPDATE Gender SET [genderID] = @genderID, [genderClass] = @genderClass " +
                        "WHERE [ProfileID] = @ProfileID"; 
            }
            else
            {
                // Insert if profile not exist
                queryGender = "INSERT INTO Gender ([genderID], [genderClass], [ProfileID]) " +
                        "VALUES (@genderID, @genderClass, @ProfileID)"; 
     

            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(queryGender, connection))
            {
                command.Parameters.AddWithValue("@genderID", genderID);
                command.Parameters.AddWithValue("@genderClass", gender);
                command.Parameters.AddWithValue("@ProfileID", profileID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private bool CheckProfileExists(int userID)
        {
            string query = "SELECT COUNT(*) FROM Profile WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

    }
}
