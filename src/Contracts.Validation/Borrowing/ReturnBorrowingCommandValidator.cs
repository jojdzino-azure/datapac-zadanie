using Contracts.Borrowing;
using Domain.Repositories.Borrowing;
using FluentValidation;

namespace Contracts.Validation.Borrowing
{
    public class ReturnBorrowingCommandValidator : AbstractValidator<ReturnBorrowingCommand>
    {
        public ReturnBorrowingCommandValidator(IQueryBorrowingRepository borrowingRepository)
        {
            RuleFor(e => e.BorrowingId).MustAsync(async (borrowingId, token) =>
            {
                var foundBook = await borrowingRepository.GetBorrowing(borrowingId, token);
                return foundBook != null;
            }).WithMessage(e => $"Borrowing with id {e.BorrowingId} does not exist.");
        }
    }
}