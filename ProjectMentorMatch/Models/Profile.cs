using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string? GetAboutMe()
        {
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
        public string? GetAddressCity(int userID)
        {
            string query = "SELECT City FROM Profile WHERE ProfileID = @ProfileID";
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
        public string? GetAddressProvince(int userID)
        {
            string query = "SELECT Province FROM Profile WHERE ProfileID = @ProfileID";
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
        public void SetProfileID(int profileID) {this.profileID = profileID;}
        public void SetBirthday(DateTime? birthday) 
        { 
            this.birthday = birthday?.ToString("MM/dd/yyyy");
        }
        public void SetAboutMe(string aboutMe) { this.aboutMe = aboutMe;}
        public void SetQualification(string? qualification) { this.qualification = qualification; }
        public void SetIsMentor(string isMentor) { this.isMentor = isMentor; }
        public void SetContactNumber(string? contactNumber) { this.contactNumber = contactNumber; }
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
                query = "UPDATE Profile SET [Birthday] = @Birthday, [ContactNumber] = @ContactNumber, [Qualification] = @Qualification, [Gender] = @Gender, [City] = @City, [Province] = @Province " +
                        "WHERE [UserID] = @UserID";
            }
            else
            {
                // Insert a new profile
                query = "INSERT INTO Profile ([ProfileID], [UserID], [Birthday], [ContactNumber], [Qualification], [Gender], [City], [Province]) " +
                        "VALUES (@ProfileID, @UserID, @Birthday, @ContactNumber, @Qualification, @Gender, @City, @Province)";
            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileRID); 
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Birthday", GetparsedBirthday()); 
                command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                command.Parameters.AddWithValue("@Qualification", qualification);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@City", addressCity);
                command.Parameters.AddWithValue("@Province", addressProvince);

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
