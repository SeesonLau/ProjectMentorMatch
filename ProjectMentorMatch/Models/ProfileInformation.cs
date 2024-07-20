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
        private int addressRID = RandomID.addressID();
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
        public string? GetGender(int profileID)
        {
            string? gender = "";

            string query = "SELECT Gender FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", profileID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    gender = result.ToString();
                }
            }
            return gender;
        }
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
        public string? GetAddressCity(int profileID)
        {
            string query = "SELECT city FROM Address WHERE ProfileID = @ProfileID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    addressCity = result.ToString();
                }
            }
            return addressCity;
        }
        public string? GetAddressProvince(int profileID)
        {
            string query = "SELECT province FROM Address WHERE ProfileID = @ProfileID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    addressProvince = result.ToString();
                }
            }
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

        public void InsertProfileGender(int profileID)
        {
            // Check if a profile for the profileID already exists
            bool profileExists = CheckProfileExists(profileID);

            string queryGender;
            if (profileExists)
            {
                // Update the existing profile
                queryGender = "UPDATE Gender SET [genderID] = @genderID, [Gender] = @Gender " +
                              "WHERE [ProfileID] = @ProfileID";
            }
            else
            {
                // Insert if profile does not exist
                queryGender = "INSERT INTO Gender ([genderID], [Gender], [ProfileID]) " +
                              "VALUES (@genderID, @Gender, @ProfileID)";
            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(queryGender, connection))
            {
                command.Parameters.AddWithValue("@genderID", genderRID);
                command.Parameters.AddWithValue("@Gender", gender ?? (object)DBNull.Value); // Use DBNull.Value for null values
                command.Parameters.AddWithValue("@ProfileID", profileID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void InsertProfileAddress(int profileID)
        {
            bool profileExists = CheckProfileExists(profileID);

            string queryAddress;
            if (profileExists)
            {
                // Update the existing address for the profile
                queryAddress = "UPDATE Address SET [City] = @City, [Province] = @Province " +
                               "WHERE [ProfileID] = @ProfileID";
            }
            else
            {
                // Insert a new address record
                queryAddress = "INSERT INTO Address ([City], [Province], [ProfileID]) " +
                               "VALUES (@City, @Province, @ProfileID)";
            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(queryAddress, connection))
            {
                command.Parameters.AddWithValue("@City", addressCity ?? (object)DBNull.Value); // Use DBNull.Value for null values
                command.Parameters.AddWithValue("@Province", addressProvince ?? (object)DBNull.Value); // Use DBNull.Value for null values
                command.Parameters.AddWithValue("@ProfileID", profileID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        private bool CheckProfileExists(int profileID)
        {
            string query = "SELECT COUNT(*) FROM Profile WHERE [ProfileID] = @ProfileID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileID);

                connection.Open();
                int countProfile = (int)command.ExecuteScalar();
                return countProfile > 0;
            }
        }


    }
}
