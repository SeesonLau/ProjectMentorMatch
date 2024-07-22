﻿using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMentorMatch.ViewModels;
using static ProjectMentorMatch.ViewModels.SubjectsViewModel;

namespace ProjectMentorMatch.Models
{
    public class ProfileModels : Account
    {
        private int profileRID = RandomID.profileID();
        private int profileID;
        private string? birthday;
        private string? aboutMe;
        private string? qualification;
        private string? isMentor; // can also be bool
        private string? contactNumber;
        private string? gender;
        private string? addressCity;
        private string? addressProvince;
        DateTime parsedBirthday;
        public int GetProfileID(int userID)
        {
            string profileIDQuery = "SELECT ProfileID FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(profileIDQuery, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    profileID = Convert.ToInt32(result);
                    SetProfileID(profileID);
                }
            }
            return profileID;
        }
        public DateTime? GetparsedBirthday()
        {
            if (DateTime.TryParse(birthday, out DateTime parsedBirthday))
            {
                return parsedBirthday;
            }
            else
            {
                return null; 
            }
        }
        public string? GetAboutMe(int userID)
        {
            string query = "SELECT [AboutMe] FROM Profile WHERE UserID = @UserID";

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
        public string? GetQualification(int userID)
        {
            string query = "SELECT [Qualification] FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    qualification = result.ToString();
                }
            }
            return qualification;
        }
        public string? GetIsMentor()
        {
            return isMentor;
        }
        public string? GetGender(int userID)
        {
            string? gender = "";

            string query = "SELECT Gender FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    gender = result.ToString();
                }
            }
            return gender;
        }
        public string? GetAddressCity(int userID)
        {
            string query = "SELECT City FROM Profile WHERE UserID = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    addressCity = result.ToString();
                }
            }
            return addressCity;
        }
        public string? GetAddressProvince(int userID)
        {
            string query = "SELECT Province FROM Profile WHERE UserID = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    addressProvince = result.ToString();
                }
            }
            return addressProvince;
        }
        public void SetProfileID(int profileID) {this.profileID = profileID;}
        public void SetBirthday(DateTime? birthday) 
        { 
            this.birthday = birthday?.ToString("MM/dd/yyyy");
        }
        public void SetAboutMe(string aboutMe) { this.aboutMe = aboutMe;}
        public void SetQualification(string? qualification) { this.qualification = qualification; }
        public void SetIsMentor(string isMentor) { this.isMentor = isMentor; }
        public void SetContactNumber(string? contactNumber) { this.contactNumber = contactNumber; }

        public void SetPicture(byte[]? selectedImageBytes) { this.selectedImageBytes = selectedImageBytes; }
        public void SetAddressCity(string addressCity)
        {
            this.addressCity = addressCity;
        }
        public void SetAddressProvince(string addressProvince)
        {
            this.addressProvince = addressProvince;
        }
        public void SetGender(string? gender) { this.gender = gender; }

        private byte[]? selectedImageBytes;

        /* REFERENCE FOR INSERTING AND SAVING IMAGE TO DATABASE
         
        private void btnCAchooseimage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openfileD = new OpenFileDialog();
                string mypictures = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openfileD.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif";
                openfileD.FileName = "Image file name";
                openfileD.Title = "Choose an image...";
                openfileD.AddExtension = true;
                openfileD.FilterIndex = 0;
                openfileD.Multiselect = false;
                openfileD.ValidateNames = true;
                openfileD.InitialDirectory = mypictures;
                openfileD.RestoreDirectory = true;
                if (openfileD.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openfileD.FileName;
                    selectedImageBytes = File.ReadAllBytes(imagePath);
                    pbCAphoto.Image = Image.FromFile(openfileD.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        */
        // Methods for Import and Export Data
        public string? GetFullName(int userID)
        {
            string? fullname = "";

            string query = "SELECT Fullname FROM CreateAccount WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    fullname = result.ToString();
                }
            }
            return fullname;
        }
        public string? GetEmail(int userID)
        {
            string? email = "";

            string query = "SELECT Email FROM CreateAccount WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    email = result.ToString();
                }
            }
            return email;
        }
        public string? GetContactNumber(int userID)
        {
            string? cN = "";

            string query = "SELECT [ContactNumber] FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    cN = result.ToString();
                }
            }
            return cN;
        }
        public DateTime? GetBirthday(int userID)
        {
            string query = "SELECT [Birthday] FROM Profile WHERE [UserID] = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                var result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    if (DateTime.TryParse(result.ToString(), out DateTime birthday))
                    {
                        return birthday;
                    }
                    else
                    {
                        return null; // Handle case where conversion to DateTime fails
                    }
                }

                return null; // Handle case where no birthday is found for the UserID
            }
        }

        public void InsertProfileData(int userID)
        {
            // Check if a profile for the userID already exists
            bool profileExists = CheckProfileExists(userID);
            string query;
            if (profileExists)
            {
                // Update the existing profile
                query = "UPDATE Profile SET [Birthday] = @Birthday, [ContactNumber] = @ContactNumber, [AboutMe] = @AboutMe, [Picture] = @Picture, [Qualification] = @Qualification, [Gender] = @Gender, [City] = @City, [Province] = @Province " +
                        "WHERE [UserID] = @UserID";
            }
            else
            {
                // Insert a new profile
                query = "INSERT INTO Profile ([ProfileID], [UserID], [Birthday], [ContactNumber], [AboutMe], [Qualification], [Picture], [Gender], [City], [Province]) " +
                         "VALUES (@ProfileID, @UserID, @Birthday, @ContactNumber, @AboutMe, @Qualification, @Picture, @Gender, @City, @Province)";
            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileRID); 
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Birthday", GetparsedBirthday()); 
                command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                command.Parameters.AddWithValue("@AboutMe", aboutMe);
                command.Parameters.AddWithValue("@Qualification", qualification);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@City", addressCity);
                command.Parameters.AddWithValue("@Province", addressProvince);

                // Add the picture parameter
                byte[] pictureData = selectedImageBytes ?? new byte[0];
                command.Parameters.AddWithValue("@Picture", pictureData);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private byte[] GetProfilePictureData(Stream imageStream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public async void InsertProfileSubject(int userID)
        {
            string selectedAcademic = string.Join(", ", SubjectService.SelectedSub);
            string selectedNonAcademic = string.Join(", ", SubjectService.SelectedNonSub);

            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();

                    // Check if the profile for the given userID already exists
                    var checkCommand = connection.CreateCommand();
                    checkCommand.CommandText = "SELECT COUNT(*) FROM Profile WHERE UserID = @userID";
                    checkCommand.Parameters.AddWithValue("@userID", userID);

                    int profileCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    string query;

                    if (profileCount > 0)
                    {
                        // Update the existing profile
                        query = @"
                    UPDATE Profile 
                    SET [Academic] = @academic, [NonAcademic] = @nonAcademic 
                    WHERE [UserID] = @userID
                    ";
                    }
                    else
                    {
                        // Insert a new profile
                        query = @"
                    INSERT INTO Profile ([Academic], [NonAcademic], [UserID])
                    VALUES (@academic, @nonAcademic, @userID)
                    ";
                    }

                    var command = connection.CreateCommand();
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@academic", selectedAcademic);
                    command.Parameters.AddWithValue("@nonAcademic", selectedNonAcademic);
                    command.Parameters.AddWithValue("@userID", userID);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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
        public async Task<(string[] Academic, string[] NonAcademic)> GetSubjectsAsync(int userID)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT [Academic], [NonAcademic] FROM Profile WHERE [UserID] = @userID";
                command.Parameters.AddWithValue("@userID", userID);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        string? academicSubjects = reader["Academic"].ToString();
                        string? nonAcademicSubjects = reader["NonAcademic"].ToString();

                        string[] academicArray = academicSubjects.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        string[] nonAcademicArray = nonAcademicSubjects.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                        return (academicArray, nonAcademicArray);
                    }
                    else
                    {
                        return (Array.Empty<string>(), Array.Empty<string>());
                    }
                }
            }
        }

        public byte[]? GetProfileImage(int userID)
        {
            string query = "SELECT Picture FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return (byte[])result;
                }
            }
            return null;
        }



    }
}
