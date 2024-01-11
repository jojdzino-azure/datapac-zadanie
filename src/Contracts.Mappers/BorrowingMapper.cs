using Contracts.Book;
using Contracts.Mappers.Book;

namespace Contracts.Mappers
{
    public static class BorrowingMapper
    {
        public static Borrowing.Borrowing? MapBorrowing(this Domain.Entities.BorrowingEntity borrowing)
        {
            if (borrowing == null) return null;
            return new Borrowing.Borrowing()
            {
                BorrowedAt = borrowing.BorrowedAt,
                Book = borrowing.Book.MapBook(),
                BorrowedForDays = borrowing.BorrowedForDays,
                ReturnedAt = borrowing.ReturnedAt,
                BorrowedBy = borrowing.BorrowedBy.MapUser(),
                Id = borrowing.Id
            };
        }
    }
}