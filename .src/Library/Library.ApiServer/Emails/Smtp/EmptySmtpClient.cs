using Microsoft.Extensions.Options;

namespace Library.ApiServer.Emails.Smtp
{
    public class EmptySmtpClient : ISmtpClient
    {
        private SmtpSettings _settings;

        public EmptySmtpClient(IOptions<SmtpSettings> settings)
        {
            this._settings = settings.Value;
        }

        public bool Send(SmtpClientEmail email, CancellationToken token = default)
        {
            return true;
        }
    }
}