using Common.MediatR;

namespace Contracts.Borrowing
{
    public class ReturnBorrowingCommand : ICommand<ReturnBorrowingCommandResponse>
    {
        public int BorrowingId { get; set; }

        public Borrowing Borrowing { get; set; }
    }
}