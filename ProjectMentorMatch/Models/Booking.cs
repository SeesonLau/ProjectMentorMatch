using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Booking : ProfileInformation
    {
        private int bookingID = RandomID.bookingID();
        private string? modeOfLearning;
        private int profileID;

        public int GetBookingID()
        {
            return bookingID;
        }
        public string? GetModeOfLearning()
        {
            return modeOfLearning;
        }
        /*public int GetProfileID()
        {
            return profileID;
        }*/
        public void SetProfileID(string modeOfLearning)
        {
            this.modeOfLearning = modeOfLearning;
        }
    }
}
