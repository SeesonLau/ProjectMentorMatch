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
        private int genderRID = RandomID.genderID();
        private int genderID;


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
                command.Parameters.AddWithValue("@genderID", genderRID);
                command.Parameters.AddWithValue("@genderClass", gender);
                command.Parameters.AddWithValue("@ProfileID", profileID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private bool CheckProfileExists(int profileID)
        {
            string query = "SELECT COUNT(*) FROM Profile WHERE [ProfileID] = @ProfileID";
            string checkGID = "SELECT COUNT (*) FROM Gender WHERE [GenderID] = @GenderID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileID);

                connection.Open();
                int countProfile = (int)command.ExecuteScalar();

                if (countProfile > 0)
                {
                    using (SqlCommand GIDcommand = new SqlCommand(checkGID, connection))
                    { 
                        command.Parameters.AddWithValue("@GenderID", genderID);
                        int countGID = (int)command.ExecuteScalar();
                        return countGID > 0;
                    }
                }
                
            }
            return false;
        }

    }
}
