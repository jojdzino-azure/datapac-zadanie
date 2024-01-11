using Common.MediatR;
using Contracts.Borrowing;
using Contracts.Mappers;
using Domain.Repositories.Borrowing;

namespace Handlers.Borrowing
{
    public class ReturnBorrowingCommandHandler : ICommandHandler<ReturnBorrowingCommand, ReturnBorrowingCommandResponse>
    {
        private readonly IBorrowingRepository _repo;

        public ReturnBorrowingCommandHandler(IBorrowingRepository repo)
        {
            this._repo = repo;
        }
        public async Task<ReturnBorrowingCommandResponse> Handle(ReturnBorrowingCommand request, CancellationToken cancellationToken)
        {
            var borrowing = await _repo.ReturnBorrowing(request.BorrowingId);
            return new ReturnBorrowingCommandResponse { Borrowing = borrowing.MapBorrowing() };
        }
    }
}
