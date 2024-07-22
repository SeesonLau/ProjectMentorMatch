using Microsoft.Data.SqlClient;
using Microsoft.Maui.ApplicationModel.Communication;
using Syncfusion.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMentorMatch.ViewModels;
using static ProjectMentorMatch.ViewModels.SubjectsViewModel;
//using Windows.System;

namespace ProjectMentorMatch.Models
{
    public class ProfileModels : Account
    {
        private int profileRID = RandomID.profileID();
        private int profileID;

        private int userID;


        private string? birthday;
        private string? aboutMe;
        private string? qualification;
        private bool isMentor = false; 
        private string? contactNumber;
        private string? gender;
        private string? addressCity;
        private string? addressProvince;
        DateTime parsedBirthday;

        private string? fullName;



        public static List<ProfileModels> GetAllProfiles()
        {
            List<ProfileModels> profiles = new List<ProfileModels>();

            string query = @"
                SELECT 
                    p.[ProfileID], 
                    p.[UserID], 
                    p.[Birthday], 
                    p.[ContactNumber], 
                    p.[AboutMe], 
                    p.[Qualification], 
                    p.[IsMentor], 
                    p.[Gender], 
                    p.[City], 
                    p.[Province], 
                    p.[Academic], 
                    p.[NonAcademic], 
                    p.[Picture],
                    c.[FullName]
                FROM [Profile] p
                JOIN [CreateAccount] c ON p.[UserID] = c.[UserID]";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var profile = new ProfileModels
                            {
                                profileID = reader.GetInt32(reader.GetOrdinal("ProfileID")),
                                userID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                birthday = reader["Birthday"].ToString(),
                                contactNumber = reader["ContactNumber"].ToString(),
                                aboutMe = reader["AboutMe"].ToString(),
                                qualification = reader["Qualification"].ToString(),
                                 isMentor = reader.GetInt32(reader.GetOrdinal("IsMentor")) == 0,
                                gender = reader["Gender"].ToString(),
                                addressCity = reader["City"].ToString(),
                                addressProvince = reader["Province"].ToString(),
                                selectedImageBytes = reader["Picture"] != DBNull.Value ? (byte[])reader["Picture"] : null,
                                fullName = reader["FullName"].ToString()
                            };

                            profiles.Add(profile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                    Console.WriteLine(ex.Message);
                }
            }

            return profiles;
        }





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
        public int GetIsMentor(int userID)
        {
            int isMentor = 0;
            string query = "SELECT isMentor FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    isMentor = Convert.ToInt32(result);
                }
            }
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
        public void SetIsMentor(bool isMentor) { this.isMentor = isMentor; }
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
            string selectedAcademic = string.Join(", ", SubjectService.SelectedSub);
            string selectedNonAcademic = string.Join(", ", SubjectService.SelectedNonSub);

            // Check if a profile for the userID already exists
            bool profileExists = CheckProfileExists(userID);
            string query;
            if (profileExists)
            {
                // Update the existing profile
                query = "UPDATE Profile SET [Birthday] = @Birthday, [ContactNumber] = @ContactNumber, [AboutMe] = @AboutMe, [Qualification] = @Qualification, [isMentor] = @isMentor, [Gender] = @Gender, " +
                        "[City] = @City, [Province] = @Province, [Academic] = @Academic, [NonAcademic] = @NonAcademic " +
                        //",[Day] = @Day, [FromTime] = @FromTime, [ToTime] = @ToTime " +
                        "WHERE [UserID] = @UserID";
            }
            else
            {
                // Insert a new profile
                query = "INSERT INTO Profile ([ProfileID], [UserID], [Birthday], [ContactNumber], [AboutMe], [Qualification], [isMentor], [Gender], [City], [Province]," +
                        "[Academic], [NonAcademic]) " +
                        "VALUES (@ProfileID, @UserID, @Birthday, @ContactNumber, @AboutMe, @Qualification, @isMentor, @Gender, @City, @Province, @Academic, @NonAcademic)";
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
                command.Parameters.AddWithValue("@isMentor", isMentor);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@City", addressCity);
                command.Parameters.AddWithValue("@Province", addressProvince);
                command.Parameters.AddWithValue("@Academic", selectedAcademic);
                command.Parameters.AddWithValue("@NonAcademic", selectedNonAcademic);           

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
                    WHERE [UserID] = @userID";
                    }
                    else
                    {
                        // Insert a new profile
                        query = @"
                    INSERT INTO Profile ([Academic], [NonAcademic], [UserID])
                    VALUES (@academic, @nonAcademic, @userID)";
                    }

                    var command = connection.CreateCommand();
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@academic", selectedAcademic ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nonAcademic", selectedNonAcademic ?? (object)DBNull.Value);
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
        public async Task InsertScheduleMentee(int userID, List<DaySchedule> selectedSchedules)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();

                    foreach (var schedule in selectedSchedules)
                    {
                        // Check if the schedule already exists
                        var checkCommand = new SqlCommand("SELECT COUNT(*) FROM Profile WHERE UserID = @UserID AND Day = @Day AND FromTime = @FromTime AND ToTime = @ToTime", connection);
                        checkCommand.Parameters.AddWithValue("@UserID", userID);
                        checkCommand.Parameters.AddWithValue("@Day", schedule.Day);
                        checkCommand.Parameters.AddWithValue("@FromTime", schedule.FromTime);
                        checkCommand.Parameters.AddWithValue("@ToTime", schedule.ToTime);

                        int existingCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                        string query;

                        if (existingCount > 0)
                        {
                            // Update existing schedule
                            query = @"
                                UPDATE Profile 
                                SET [Day] = @Day, [FromTime] = @FromTime, [ToTime] = @ToTime 
                                WHERE [UserID] = @UserID AND [Day] = @Day AND [FromTime] = @FromTime AND [ToTime] = @ToTime";
                        }
                        else
                        {
                            // Insert new schedule
                            query = @"
                                INSERT INTO Profile (UserID, Day, FromTime, ToTime) 
                                VALUES (@UserID, @Day, @FromTime, @ToTime)";
                        }

                        var command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Day", schedule.Day);
                        command.Parameters.AddWithValue("@FromTime", schedule.FromTime);
                        command.Parameters.AddWithValue("@ToTime", schedule.ToTime);

                        await command.ExecuteNonQueryAsync();
                    }
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

        public void ApplyAsMentor(int userID)
        {
            string query = "UPDATE Profile SET isMentor = @isMentor WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@isMentor", 1);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
