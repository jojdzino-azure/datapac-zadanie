using Common.MediatR;
using Contracts.Borrowing;
using Contracts.Mappers;
using Domain.Repositories.Book;
using Domain.Repositories.Borrowing;
using Domain.Repositories.User;

namespace Handlers.Borrowing
{
    public class CreateBorrowingCommandHandler : ICommandHandler<CreateBorrowingCommand, CreateBorrowingCommandResponse>
    {
        private readonly IBorrowingRepository _repo;
        private readonly IQueryBookRepository _queryBookRepository;
        private readonly IQueryUserRepository _userRepository;

        public CreateBorrowingCommandHandler(IBorrowingRepository repo, IQueryBookRepository queryBookRepository, IQueryUserRepository userRepository)
        {
            this._repo = repo;
            this._queryBookRepository = queryBookRepository;
            this._userRepository = userRepository;
        }
        public async Task<CreateBorrowingCommandResponse> Handle(CreateBorrowingCommand request, CancellationToken cancellationToken)
        {
            var book = await _queryBookRepository.GetBookAsync(request.BookId);
            var user = await _userRepository.GetUserAsync(request.UserGuid);
            var borrowing = await _repo.CreateBorrowing(new Domain.Entities.BorrowingEntity()
            {
                Book = book,
                BorrowedBy = user,
                BorrowedAt = DateTime.UtcNow,
                BorrowedForDays = request.BorrowedForDays
            });
            return new CreateBorrowingCommandResponse { Borrowing = borrowing.MapBorrowing() };
        }
    }
}
