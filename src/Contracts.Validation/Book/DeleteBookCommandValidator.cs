using Contracts.Book;
using Domain.Repositories.Borrowing;
using FluentValidation;

namespace Contracts.Validation.Book
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator(IQueryBorrowingRepository queryBorrowingRepository)
        {
            RuleFor(e => e.Id).MustAsync(async (id, token) =>
            {
                var borrowingForBookExists = await queryBorrowingRepository.GetBorrowings(0, 100);//better method should be used
                var nonReturnedBorrowings = borrowingForBookExists.Where(e => e.ReturnedAt == null);
                var nonReturnedBorrowingsForBookExists= nonReturnedBorrowings.Any(e => e.Book.Id == id);
                return !nonReturnedBorrowingsForBookExists;
            }).WithMessage("There is a non returned borrowing for a book, cannot delete.");
        }
    }
}