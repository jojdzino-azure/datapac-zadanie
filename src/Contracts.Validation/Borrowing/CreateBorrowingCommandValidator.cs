using Contracts.Borrowing;
using Domain.Repositories.Book;
using FluentValidation;

namespace Contracts.Validation.Borrowing
{
    public class CreateBorrowingCommandValidator : AbstractValidator<CreateBorrowingCommand>
    {
        public CreateBorrowingCommandValidator(IQueryBookRepository bookRepository)
        {
            RuleFor(b => b.BorrowedForDays).LessThan(31)
                .WithMessage("Maximum days for borrowing a book is 31.");
            RuleFor(e => e.BookId).MustAsync(async (bookId, token) =>
            {
                var foundBook = await bookRepository.GetBookAsync(bookId, token);
                return foundBook != null;
            }).WithMessage(e => $"Book with id {e.BookId} does not exist.");
            //other data validations
        }
    }

    public class ReturnBorrowingCommandValidator : AbstractValidator<ReturnBorrowingCommand>
    {
        public ReturnBorrowingCommandValidator()
        {
            //other data validations
        }
    }
}