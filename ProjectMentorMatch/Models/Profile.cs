using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
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
        private int contactNumber;
        //private int userID;

        public int GetProfileID()
        {
            return profileID;
        }
        public string? GetBirthday()
        {
            return birthday;
        }
        public string? GetAboutMe()
        {
            return aboutMe;
        }
        public string? GetQualification()
        {
            return qualification;
        }
        public string? GetIsMentor()
        {
            return isMentor;
        }
        public int GetContactNumber()
        {
            return contactNumber;
        }

        public void SetProfileID(int profileID) {this.profileID = profileID;}
        public void SetBirthdayID(string birthday) { this.birthday = birthday;}
        public void SetAboutMe(string aboutMe) { this.aboutMe = aboutMe;}
        public void SetQualification(string qualification) { this.qualification = qualification; }
        public void SetIsMentor(string isMentor) { this.isMentor = isMentor; }
        public void SetContactNumber(int contactNumber) { this.contactNumber = contactNumber; }


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
        // NAME
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
        public void InsertProfileData(int userID)
        {
            string query = "INSERT INTO Profile (ProfileID, UserID, Birthday, ContactNumber, AboutMe, Qualification, IsMentor) " +
                "VALUES (@ProfileID, @UserID, @Birthday, @ContactNumber, @AboutMe, @Qualification, @IsMentor)";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProfileID", profileRID);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Birthday", birthday);
                command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                command.Parameters.AddWithValue("@AboutMe", aboutMe);
                command.Parameters.AddWithValue("@Qualification", qualification);
                command.Parameters.AddWithValue("@IsMentor", isMentor);

                //command.Parameters.AddWithValue("@Picture", picture);
                //picture is currently not included,yet.

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
