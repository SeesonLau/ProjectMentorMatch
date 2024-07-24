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
using Microsoft.Maui.Controls;

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
        //private bool isMentor = false; 
        private int isMentor;
        private string? contactNumber;
        private string? gender;
        private string? addressCity;
        private string? addressProvince;
        DateTime parsedBirthday;
        private string? eduback;
        private string? fullName;
        TimeSpan FromTime;
        TimeSpan ToTime;

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

                               // isMentor = reader.GetInt32(reader.GetOrdinal("IsMentor")) == 0,

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

        // GET
        public string? GetFullName(int userID)
        {
            string? fullname = "";

            string query = "SELECT [Fullname] FROM [CreateAccount] WHERE [UserID] = @UserID";

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

            string query = "SELECT [Email] FROM [CreateAccount] WHERE [UserID] = @UserID";

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

            string query = "SELECT [ContactNumber] FROM [Profile] WHERE [UserID] = @UserID";

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
        public string? GetEducBack(int userID)
        {
            string query = "SELECT [EducationalBackground] FROM [Profile] WHERE [UserID] = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    eduback = result.ToString();
                }
            }
            return eduback;
        }
        public string? GetQualification(int userID)
        {
            string query = "SELECT [Qualification] FROM [Profile] WHERE [UserID] = @UserID";

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
        public string? GetGender(int userID)
        {
            string? gender = "";

            string query = "SELECT [Gender] FROM [Profile] WHERE [UserID] = @UserID";

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
            string query = "SELECT [City] FROM [Address] WHERE [UserID] = @UserID";
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
            string query = "SELECT [Province] FROM [Address] WHERE [UserID] = @UserID";
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
        public async Task<(string[] Academic, string[] NonAcademic)> GetSubjectMenteeAsync(int userID)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT [Academic], [Nonacademic] FROM SubjectMentee WHERE [UserID] = @UserID";
                command.Parameters.AddWithValue("@UserID", userID);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        string? academicSubjects = reader["Academic"].ToString();
                        string? nonAcademicSubjects = reader["Nonacademic"].ToString();

                        string[] academicArray = (academicSubjects ?? string.Empty).Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                        string[] nonAcademicArray = (nonAcademicSubjects ?? string.Empty).Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                        return (academicArray, nonAcademicArray);
                    }
                    else
                    {
                        return (Array.Empty<string>(), Array.Empty<string>());
                    }
                }
            }
        }


        public string? GetAboutMe(int userID)
        {
            string query = "SELECT [AboutMe] FROM [Profile] WHERE [UserID] = @UserID";

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

        // SET
        public void SetProfileID(int profileID) {this.profileID = profileID;}
        public void SetBirthday(DateTime? birthday) 
        { 
            this.birthday = birthday?.ToString("MM/dd/yyyy");
        }
        public void SetAboutMe(string aboutMe) { this.aboutMe = aboutMe;}
        public void SetQualification(string? qualification) { this.qualification = qualification; }
        public void SetIsMentor(int isMentor) { this.isMentor = isMentor; }
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

        // INSERT
        public void InsertProfile(int userID)
        {
            bool profileExists = CheckProfileExists(userID);
            string query;
            if (profileExists)
            {
                query = "UPDATE Profile SET [Birthday] = @Birthday, [Gender] = @Gender, [ContactNumber] = @ContactNumber, [EducationalBackground] = @EducationalBackground, [Qualification] = @Qualification, [isMentor] = @isMentor " +
                        "WHERE [UserID] = @UserID";
            }
            else
            {
                query = "INSERT INTO Profile ([UserID], [Birthday], [Gender], [ContactNumber], [EducationalBackground], [Qualification], [isMentor])" +

                        "VALUES (@UserID, @Birthday, @Gender, @ContactNumber, @EducationalBackground, @Qualification, @isMentor)";
            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Birthday", GetparsedBirthday());
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                command.Parameters.AddWithValue("@EducationalBackground", aboutMe);
                command.Parameters.AddWithValue("@Qualification", qualification);
                command.Parameters.AddWithValue("@isMentor", 0);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void InsertAddress(int userID)
        {
            // Check if a profile for the userID already exists
            bool profileExists = CheckAddressExist(userID);
            string query;
            if (profileExists)
            {
                // Update the existing profile
                query = "UPDATE [Address] SET [City] = @City, [Province] = @Province " +
                        "WHERE [UserID] = @UserID";
            }
            else
            {
                // Insert a new profile
                query = "INSERT INTO [Address] ([UserID], [City], [Province])" +

                        "VALUES (@UserID, @City, @Province)";
            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                //command.Parameters.AddWithValue("@ProfileID", profileRID);
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@City", addressCity);
                command.Parameters.AddWithValue("@Province", addressProvince);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        /*
        public void InsertSubject(int userID)
        {
            string selectedAcademic = string.Join(", ", SubjectService.SelectedSub);
            string selectedNonAcademic = string.Join(", ", SubjectService.SelectedNonSub);

            bool profileExists = CheckSubjectMenteeExist(userID);
            string query;
            if (profileExists)
            {
                query = "UPDATE [SubjectMentee] SET [Academic] = @Academic, [Nonacademic] = @Nonacademic " +
                        "WHERE [UserID] = @UserID";
            }
            else
            {
                query = "INSERT INTO [SubjectMentee] ([UserID], [Academic], [Nonacademic]) " +
                        "VALUES (@UserID, @Academic, @Nonacademic)";
            }
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Academic", selectedAcademic);
                command.Parameters.AddWithValue("@Nonacademic", selectedNonAcademic);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }*/
       /* public void  InsertSchedules( int userID)
        {
            ScheduleViewModel svm = new ScheduleViewModel();

            // Call the methods to get selected days, from times, and to times
            List<string> selectedDays = svm.GetSelectedDaysNames();
            List<TimeSpan> selectedFromTimes = svm.GetSelectedFromTimes();
            List<TimeSpan> selectedToTimes = svm.GetSelectedToTimes();

            bool profileExists = CheckSubjectMenteeExist(userID);
            string query;
            if (profileExists)
            {
                query = "UPDATE [ScheduleMentee] SET [Day] = @Day, [FromTime] = @FromTime, [ToTime] = @ToTime " +
                        "WHERE [UserID] = @UserID";
            }
            else
            {
                query = "INSERT INTO [ScheduleMentee] ([UserID], [Day], [FromTime], [ToTime]) " +
                        "VALUES (@UserID, @Day, @FromTime, @ToTime)";
            }

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Day", selectedDays);
                command.Parameters.AddWithValue("@FromTime", selectedFromTimes);
                command.Parameters.AddWithValue("@ToTime", selectedToTimes);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }*/

        //OLD
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
        }        private byte[] GetProfilePictureData(Stream imageStream)
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

        //CHECK
        public bool CheckProfileExists(int userID)
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
        public bool CheckAddressExist(int userID)
        {
            string query = "SELECT COUNT(*) FROM [Address] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
        public bool CheckSubjectMenteeExist(int userID)
        {
            string query = "SELECT COUNT(*) FROM [SubjectMentee] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
        public bool CheckScheduleMenteeExist(int userID)
        {
            string query = "SELECT COUNT(*) FROM [ScheduleMentee] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        public void DeleteScheduleMentee(int userID)
        {
            string query = "DELETE FROM [ScheduleMentee] WHERE [UserID] = @UserID";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void InsertScheduleMentee(int userID, List<DaySchedule> selectedSchedules)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                foreach (var schedule in selectedSchedules)
                {
                    var command = new SqlCommand("INSERT INTO ScheduleMentee (UserID, Day, FromTime, ToTime) VALUES (@UserID, @Day, @FromTime, @ToTime)", connection);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Day", schedule.Day);
                    command.Parameters.AddWithValue("@FromTime", schedule.FromTime);
                    command.Parameters.AddWithValue("@ToTime", schedule.ToTime);

                    command.ExecuteNonQuery();
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

        public void WithdrewAsMentor(int userID)
        {
            string query = "UPDATE Profile SET isMentor = @isMentor WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@isMentor", 0);
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
