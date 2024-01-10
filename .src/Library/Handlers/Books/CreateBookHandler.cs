using Common.MediatR;
using Contracts.Book;
using Contracts.Mappers.Book;
using Domain.Repositories.Book;

namespace Handlers.Books
{
    public class CreateBookHandler : ICommandHandler<CreateBookCommand, CreateBookCommandResponse>

    {
        private readonly IBookRepository _repo;

        public CreateBookHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreateBookCommandResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            // validacia je riesenia vo ValidationBehaviour, ktore mi krasne poriesi aj vracanie objektu v JSON na response
            var createdBook = await _repo.CreateBookAsync(request.Book.MapBook(), cancellationToken);
            return new CreateBookCommandResponse()
            {
                Book = createdBook.MapBook()
            };
        }
    }
}