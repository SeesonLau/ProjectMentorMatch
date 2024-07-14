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
    courseID()          : 170000 - 199999
    schedID()           : 140000 - 169999
    addressID()         : 100000 - 139999
     
     */
    public class RandomID 
    {
        private static readonly Random random = new Random();
        public static int addressID()
        {
            return random.Next(100000, 139999);
        }

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
            return random.Next(140000, 169999);
        }
        public static int courseID()
        {
            return random.Next(170000, 199999);
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

    /*
     * 
            private void guna2Button4_Click(object sender, EventArgs e)
            {
                string refno = Generate8CharacterString(link);
                string query = "INSERT INTO LizzardpayData (ID,Status,Email,FullName,Amount,Remarks,Source,[OR No],[OR Date],RefNo)" +
                    "Values(@id,@status,@email,@name,@amount,@remarks,@source,@orno,@ordate,@refno)";
                int orno = GenerateUniqueOrno();
                using (OleDbConnection conn = new OleDbConnection(link))
                {
                    conn.Open();
                    string status = "Merchant has not confirmed transaction yet.";
                    double amount = Convert.ToDouble(ammount2.Text);
                    string source = source2.Text + $" - ONLINE PAYMENT ({orno})";
                    string ordate = GetDateNow();
                    stud3.Text = stud2.Text;
                    email3.Text = email2.Text;
                    name3.Text = name2.Text;
                    amount3.Text = amount.ToString("###,###,###.00");
                    remarks3.Text = remarks2.Text;
                    source3.Text = source;
                    ornotext.Text = orno.ToString();
                    ordatetext.Text = ordate.ToString();

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.Add("@id", stud2.Text);
                        cmd.Parameters.Add("@status", status);
                        cmd.Parameters.Add("@email", email2.Text);
                        cmd.Parameters.Add("@name", name2.Text);
                        cmd.Parameters.Add("@amount", amount);
                        cmd.Parameters.Add("@remarks", remarks2.Text);
                        cmd.Parameters.Add("@source", source);
                        cmd.Parameters.Add("@orno", orno);
                        cmd.Parameters.Add("@ordate", ordate);
                        cmd.Parameters.Add("@refno", refno);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LizardPayConfirmation.html");
                string htmlContent = File.ReadAllText(htmlFilePath);


                var replacements = new Dictionary<string, string>
                {
                { "{#RefNo}", refno },
                { "{#channel}", source2.Text },
                { "{#Merchant}", "PEOPLES ADVANCED TECHNOLOGY - UNIVERSITY" },
                { "{#Amount}", ammount2.Text },
                { "{#Status}", "SUCCESS" },
                { "{#Description}", ornotext.Text + " / " + stud2.Text + " / " + name2.Text + " / " + remarks2.Text }
                };
                string customizedContent = ReplacePlaceholders(htmlContent, replacements);

                SendHtmlEmail(email2.Text, "Payment Confirmation for Transaction Ref: " + refno, customizedContent);
                pay3.Visible = false;
                ProccessTimer.Start();
                ProcessingPB.Image = Resources.PROCESSING__1_;
            }
            public void SendHtmlEmail(string toAddress, string subject, string htmlContent)
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUser = "OnlineServiceJXIT@gmail.com";
                string smtpPassword = "auuq errs qyhe easi";
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUser, "Lizzard Pay");
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = htmlContent;
                mail.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                smtpClient.EnableSsl = true;

                try
                {
                    smtpClient.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
            public static string GetHtmlContent(string htmlFilePath)
            {
                return File.ReadAllText(htmlFilePath);
            }
    */
}

