namespace Library.ApiServer.Emails.Smtp
{
    public interface ISmtpClient
    {
        public bool Send(SmtpClientEmail email, CancellationToken token = default);
    }
}