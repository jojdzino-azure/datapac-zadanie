using Common.MediatR;
using Contracts.Book;
using Contracts.Mappers.Book;
using Domain.Repositories.Book;

namespace Handlers.Books
{
    public class UpdateBookHandler : ICommandHandler<UpdateBookCommand, UpdateBookCommandResponse>

    {
        private readonly IBookRepository _repo;

        public UpdateBookHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<UpdateBookCommandResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            // validacia je riesenia vo ValidationBehaviour, ktore mi krasne poriesi aj vracanie objektu v JSON na response
            var domainBook = await _repo.UpdateBookAsync(request.Id, request.Book.MapBook(), cancellationToken);
            if (domainBook == null)
            {
                throw new ArgumentException("Book was not found");
            }
            return new UpdateBookCommandResponse()
            {
                Book = domainBook.MapBook()
            };
        }
    }
}