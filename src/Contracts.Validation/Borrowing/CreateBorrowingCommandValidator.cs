using Contracts.Borrowing;
using Domain.Repositories.Book;
using Domain.Repositories.User;
using FluentValidation;

namespace Contracts.Validation.Borrowing
{
    public class CreateBorrowingCommandValidator : AbstractValidator<CreateBorrowingCommand>
    {
        public CreateBorrowingCommandValidator(IQueryBookRepository bookRepository, IQueryUserRepository userRepository)
        {
            RuleFor(b => b.BorrowedForDays).LessThan(31)
                .WithMessage("Maximum days for borrowing a book is 31.");
            RuleFor(e => e.BookId).MustAsync(async (bookId, token) =>
            {
                var foundBook = await bookRepository.GetBookAsync(bookId, token);
                return foundBook != null;
            }).WithMessage(e => $"Book with id {e.BookId} does not exist.");
            RuleFor(e => e.UserGuid).MustAsync(async (userGuid, token) =>
            {
                var foundUser = await userRepository.GetUserAsync(userGuid, token);
                return foundUser != null;
            }).WithMessage(e => $"User with guid: {e.UserGuid} does not exist.");

            //other data validations
        }
    }
}