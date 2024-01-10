using Common.MediatR;

namespace Contracts.Book
{
    public class UpdateBookCommand : ICommand<UpdateBookCommandResponse>
    {
        public int Id { get; set; }
        public Book Book { get; set; }
    }
}