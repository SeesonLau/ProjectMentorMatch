using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class BookingModels :  Database
    {
        // private int bookingID = RandomID.bookingID();
       
       // private string? modeOfLearning;
     //   private int profileID;

        private int BookingID;
        public string Picture;
        public string Fullname;
        public float MentorFee;
        public string Subject;
        public string Address;
        public float Ratings;
        public int HeartReact;
        public string Setup;

        // GET

        public int GetBookingID() {  return BookingID; }
        public string GetPicture() { return Picture; }

        public string GetFullname() { return Fullname; }    

        public float GetMentorFee() { return MentorFee; }

        public string GetSubject() { return Subject; }

        public string GetAddress() { return Address; }

        public float GetRatings() { return Ratings; }   

        public int GetHeartReact() { return HeartReact; }

        public string GetSetup() { return Setup; }

        // SET

        public void SetBookingID (int bookingID) { BookingID = bookingID; }

        public void SetPicture(string picture) { Picture = picture; }

        public void SetFullname(string fullname) { Fullname = fullname; }

        public void SetMentorFee(float mentorFee) { MentorFee = mentorFee; }

        public void SetSubject(string subject) { Subject = subject; }

        public void SetAddress(string address) { Address = address; }

        public void SetRatings(float rating) { Ratings = rating; }

        public void SetHeartReact(int heartReact) { HeartReact = heartReact; }

        public void SetSetup(string setup) { Setup = setup; }


        public void InsertBooking()
        {
            string query = @"
                INSERT INTO Booking (Picture, Fullname, MentorFee, Subject, Address, Ratings, HeartReact, Setup)
                VALUES (@Picture, @Fullname, @MentorFee, @Subject, @Address, @Ratings, @HeartReact, @Setup);
            ";

            using (SqlConnection connection = GetConnection()) 
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Picture", Picture);
                    command.Parameters.AddWithValue("@Fullname", Fullname);
                    command.Parameters.AddWithValue("@MentorFee", MentorFee);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Ratings", Ratings);
                    command.Parameters.AddWithValue("@HeartReact", HeartReact);
                    command.Parameters.AddWithValue("@Setup", Setup);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }





    }
}
