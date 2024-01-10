namespace Library.ApiServer.Emails.Smtp
{
    public class SmtpClientEmail
    {
        public string From { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}