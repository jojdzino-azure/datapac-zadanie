using Common.MediatR;

namespace Contracts.Borrowing
{
    public class CreateBorrowingCommand : ICommand<CreateBorrowingCommandResponse>
    {
        public int BookId { get; set; }
        public Guid UserGuid { get; set; }
        public int BorrowedForDays { get; set; }
    }
}