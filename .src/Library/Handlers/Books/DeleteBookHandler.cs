using Common.MediatR;
using Contracts.Book;
using Contracts.Mappers.Book;
using Domain.Repositories.Book;

namespace Handlers.Books
{
    public class DeleteBookHandler : ICommandHandler<DeleteBookCommand, DeleteBookCommandResponse>

    {
        private readonly IBookRepository _repo;

        public DeleteBookHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<DeleteBookCommandResponse> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            // validacia je riesenia vo ValidationBehaviour, ktore mi krasne poriesi aj vracanie objektu v JSON na response
            var domainBook = await _repo.DeleteBookAsync(request.Id, cancellationToken);
            return new DeleteBookCommandResponse()
            {
                Book = domainBook.MapBook()
            };
        }
    }
}