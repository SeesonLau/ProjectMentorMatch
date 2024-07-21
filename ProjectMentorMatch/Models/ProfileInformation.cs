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
        private int courseRID = RandomID.courseID();
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

        public string? GetSubjectTaught()
        {
            return subjectTaught;
        }
        public string? GetSubjectTakern()
        {
            return subjectTaken;
        }
        public string? GetCourseName(int profileID)
        {
            string? cN = "";

            string query = "SELECT [courseName] FROM EducationalBackground WHERE ProfileID = @ProfileID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    cN = result.ToString();
                }
            }
            return cN;
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
        public void InsertProfileEducationalBackground (int profileID)
        {
            bool profileExists = CheckProfileExists(profileID);

            string queryEB;
            if (profileExists)
            {
                queryEB = "UPDATE EducationalBackground SET [courseID] = @courseID, [courseName] = @courseName " +
                        "WHERE [ProfileID] = @ProfileID";
            }
            else
            {
                queryEB = "INSERT INTO EducationalBackground ([courseID], [courseName], [ProfileID]) " +
                        "VALUES (@courseID, @courseName,  @ProfileID)";
            }
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(queryEB, connection))
            {
                command.Parameters.AddWithValue("@courseID", courseRID);
                command.Parameters.AddWithValue("@courseName", courseName);
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
