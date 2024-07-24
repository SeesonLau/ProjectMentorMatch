using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using ProjectMentorMatch.ViewModels;
//using Intents;
//using MetalPerformanceShaders;

namespace ProjectMentorMatch.Models
{
    public class Account : Database
    {
        RandomID randomID = new RandomID();
        private int userID = RandomID.userID();
        public int ProfileID { get; set; }


        private string? fullname;
        private string? email;
        private string? password;

        public string? AddressCity;

        private int retuserID;

 

        public int GetUserID()
        {
            return retuserID;
        }
        public string? GetFullname()
        {
            return fullname;
        }
        public string? GetEmail()
        {
            return email;
        }
        public string? GetPassword()
        {
            return password;
        }

        public string? GetAddressCity()
        {
            return AddressCity;
        }

        public void SetUserID(int retuserID) {this.retuserID = retuserID; }
        public void SetFullname(string fullname) {this.fullname = fullname;}  
        public void SetEmail(string email) {this.email = email;}
        public void SetPassword(string password) {this.password = password;}

        public void SetAddressCity(string AddressCity) {this.AddressCity = AddressCity;}

        public string? GetCityByUserID(int userID)
        {
            string query = "SELECT City FROM Address WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }

            return null; // Return null if no city is found
        }

        public string? GetProvinceByUserID(int userID)
        {
            string query = "SELECT Province FROM Address WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }

            return null; // Return null if no city is found
        }

        public string? GetQualificationByUserID(int userID)
        {
            string query = "SELECT Qualification FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }

            return null; // Return null if no city is found
        }

        public string? GetEmailByUserID(int userID)
        {
            string query = "SELECT Email FROM CreateAccount WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }

            return null; // Return null if no city is found
        }


        public (string? Academic, string? Nonacademic) GetSubjectsByUserID(int userID)
        {
            string query = "SELECT Academic, Nonacademic FROM SubjectMentee WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string? academic = reader["Academic"] as string;
                        string? nonacademic = reader["Nonacademic"] as string;
                        return (academic, nonacademic);
                    }
                }
            }

            return (null, null); // Return null tuple if no subjects are found
        }

         public string? GetImageFilenameByUserID(int userID)
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
                    return result.ToString();
                }
            }

            return null; // Return null if no image is found
        }



        public string? GetAboutMeByUserID(int userID)
        {
            string query = "SELECT AboutMe FROM Profile WHERE UserID = @UserID";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
            }

            return null; // Return null if no city is found
        }









        public void SignUp()
        {
            /*
            if (email != null && CheckEmailIsTaken(email))
            {
                throw new Exception("Email already exists. Please use a different email.");
            }

            string query = "INSERT INTO CreateAccount (userID, fullname, email, password) VALUES (@UserID, @Fullname, @Email, @Password)";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Fullname", fullname);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                command.ExecuteNonQuery();
            }*/
            string query = "INSERT INTO CreateAccount (fullname, email, password) VALUES (@Fullname, @Email, @Password)";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Fullname", fullname);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        // THE COOLER BOOL LOGIN

        public bool LogIn(string email, string password)
        {
            string countQuery = "SELECT COUNT(*) FROM [CreateAccount] WHERE [Email] = @Email AND [Password] = @Password"; 
            string userIDQuery = "SELECT UserID FROM CreateAccount WHERE Email = @Email";
            string profileIDQuery = "SELECT ProfileID FROM Profile WHERE UserID = @UserID";
 
            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand countCommand = new SqlCommand(countQuery, connection))
                {
                    countCommand.Parameters.AddWithValue("@Email", email);
                    countCommand.Parameters.AddWithValue("@Password", password);

                    int count = (int)countCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        using (SqlCommand userIDCommand = new SqlCommand(userIDQuery, connection))
                        {
                            userIDCommand.Parameters.AddWithValue("@Email", email);

                            object result = userIDCommand.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                int userID = Convert.ToInt32(result);
                                SetUserID(userID);                         
                                
                                using (SqlCommand profileIDCommand = new SqlCommand(profileIDQuery, connection))
                                {
                                    profileIDCommand.Parameters.AddWithValue("@UserID", userID);
                                    object profileResult = profileIDCommand.ExecuteScalar();
                                    if(profileResult != null && profileResult != DBNull.Value)
                                    {
                                        ProfileInformation profileInfo = new ProfileInformation();
                                        int profileID = Convert.ToInt32(profileResult);
                                        profileInfo.SetProfileID(profileID);
                                    }
                                }
                                return true;
                            }
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool CheckEmailIsTaken(string email)
        {
            string query = "SELECT COUNT(*) FROM CreateAccount WHERE email = @Email";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int emailCount = (int)command.ExecuteScalar();

                return emailCount > 0;
            }
        }

        // Fetching data from the database
        public static List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();

            string query = "SELECT fullname, email, password FROM CreateAccount";
            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(new Account
                        {
                            fullname = reader["fullname"].ToString(),
                            email = reader["email"].ToString(),
                            password = reader["password"].ToString()
                        });
                    }
                }
            }

            return accounts;
        }

        public static List<Account> GetAllMentors()
        {
            List<Account> accounts = new List<Account>();

            string query = @"
             SELECT 
              p.ProfileID, 
               c.Fullname         
                 FROM 
        Profile p
    INNER JOIN 
        CreateAccount c ON p.UserID = c.UserID
    WHERE 
        p.IsMentor = 1";

            using (var connection = GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(new Account
                        {
                            ProfileID = (int)reader["ProfileID"],
                            fullname = reader["Fullname"].ToString()
                        });
                    }
                }
            }

            return accounts;
        }



        //sends email confirmation upon successful login; check CreateAccount.xaml.cs, under OnSignUpClicked
        public void sendEmail(string email)
        {

            string fromMail = "mentormatch4dmins@gmail.com";
            string fromPassword = "rakd vjox ydwh rxen";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Account Confirmation";
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> Your account has been successfully confirmed. </body></html>";
            message.IsBodyHtml = true;


            /* Can't get picture to work yet lah
             * 
            string imagePath = FileSystem.Current.AppDataDirectory;
            imagePath = Path.Combine(imagePath, "welcome_email.png");

            Attachment inlineImage = new Attachment(imagePath);
            inlineImage.ContentId = "EmbeddedImage";

            if (inlineImage.ContentDisposition != null)
            {
                inlineImage.ContentDisposition.Inline = true;
                inlineImage.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            }
            //embeds the pic ooga booga
            message.Attachments.Add(inlineImage);
            */
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);


        }
    }
}
