using Common.MediatR;

namespace Contracts.Borrowing
{
    public class CreateBorrowingCommand : ICommand<CreateBorrowingCommandResponse>
    {
        public Book.Book Book { get; set; }
        public User.User BorrowedBy { get; }
        public int BorrowedForDays { get; set; }
    }
}