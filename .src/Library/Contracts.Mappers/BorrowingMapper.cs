using Contracts.Mappers.Book;

namespace Contracts.Mappers
{
    public static class BorrowingMapper
    {
        public static Borrowing.Borrowing MapUser(this Domain.Entities.BorrowingEntity borrowing)
        {
            return new Borrowing.Borrowing()
            {
                BorrowedAt = borrowing.BorrowedAt,
                Book = borrowing.Book.MapBook(),
                BorrowedForDays = borrowing.BorrowedForDays,
                ReturnedAt = borrowing.ReturnedAt,
                BorrowedBy = borrowing.BorrowedBy.MapUser(),
                Guid = borrowing.Guid
            };
        }
    }
}