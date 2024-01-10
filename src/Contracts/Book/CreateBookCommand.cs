using Common.MediatR;

namespace Contracts.Book
{
    public class CreateBookCommand : ICommand<CreateBookCommandResponse>
    {
        public Book Book { get; set; }
    }
}