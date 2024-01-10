using Common.MediatR;

namespace Contracts.Book
{
    public class DeleteBookCommand : ICommand<DeleteBookCommandResponse>
    {
        public int Id { get; set; }
    }
}