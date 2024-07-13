namespace ProjectMentorMatch.Models
{
    //To use this randomID. 
    // Do polymorphism.
    /*
     RandomID randomID = new RandomID();

    then just call it by 

    randomID.chuchuSelected ID type;
    refer below kung unsa nga ID gamiton

    userID()            : 900000 - 999999
    profileID()         : 800000 - 899999
    analyticsID()       : 700000 - 799999
    messageID()         : 600000 - 699999
    notification()      : 500000 - 599999
    bookingID()         : 400000 - 499999
    sessionID()         : 300000 - 399999
    subject_taken()     : 260000 - 299999
    subject_taughtID()  : 200000 - 259999
    courseID()          : 160000 - 199999
    schedID()           : 100000 - 159999
     
     */
    public class RandomID 
    {
        private static readonly Random random = new Random();

        public static int userID()
        {
            return random.Next(900000, 999999);
        }
        public static int profileID()
        {
            return random.Next(800000, 899999);
        }
        public static int schedID()
        {
            return random.Next(100000, 159999);
        }
        public static int courseID()
        {
            return random.Next(160000, 199999);
        }
        public static int subject_taughtID()
        {
            return random.Next(200000, 259999);
        }
        public static int subject_takenID()
        {
            return random.Next(260000, 299999);
        }
        public static int analyticsID()
        {
            return random.Next(700000, 799999);
        }
        public static int messageID()
        {
            return random.Next(600000, 699999);
        }
        public static int notificationID()
        {
            return random.Next(500000, 599999);
        }
        public static int bookingID()
        {
            return random.Next(400000, 499999);
        }
        public static int sessionID()
        {
            return random.Next(300000, 399999);
        }
    }
}

