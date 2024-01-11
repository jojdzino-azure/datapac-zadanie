using Domain.Entities;
using Domain.Repositories.Borrowing;
using Domain.Repositories.User;
using Library.ApiServer.Emails.Smtp;

namespace Library.ApiServer.Emails
{
    public class EmailSender
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IQueryUserRepository _userRepository;
        private readonly ISmtpClient _client;
        private readonly SmtpSettings _settings;

        public EmailSender(IBorrowingRepository repository, IQueryUserRepository userRepository, ISmtpClient client)
        {
            this._borrowingRepository = repository;
            this._userRepository = userRepository;
            this._client = client;
        }

        public async Task SendMails()
        {
            var users = new List<UserEntity>();
            int page = 0, pageSize = 100;
            do
            {
                users = await _userRepository.GetUsersAsync(page++, pageSize);
                foreach (var user in users)
                {
                    var allExpiringBorrowings = await _borrowingRepository.ExpiringBorrowinsForUser(user);
                    var emailText = GenerateTemplatingText(allExpiringBorrowings, user);
                    _client.Send(new SmtpClientEmail() { Body = emailText });
                }
            } while (users.Any());
        }

        private string GenerateTemplatingText(List<BorrowingEntity> allExpiringBorrowings, UserEntity user)
        {
            return "";// vygenerovat pomocou nejakeho templating enginu, napr razor engine, alebo nieco ine
        }
    }
}