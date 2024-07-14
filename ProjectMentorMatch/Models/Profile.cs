using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Profile : Account
    {
        private int profileID = RandomID.profileID();
        private string? birthday;
        private string? aboutMe;
        private string? qualification;
        private string? isMentor; // can also be bool
        private int contactNumber;
        private int userID;

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


    }
}
