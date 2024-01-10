using Common.MediatR;
using Contracts.Book;
using Contracts.Mappers.Book;
using Domain.Repositories.Book;

namespace Handlers.Books
{
    public class GetBookHandler : IQueryHandler<GetBookQuery, GetBookQueryResponse>
    {
        private readonly IQueryBookRepository _repo;

        public GetBookHandler(IQueryBookRepository repo)
        {
            this._repo = repo;
        }

        public async Task<GetBookQueryResponse> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            // validacia sa dokonca ani neriesi pre Query typy, ale len pre commandy, nakolko GET metody by nemali nic modifikovat, tak ich v principe netreba validovat
            var domainBook = await _repo.GetBookAsync(request.Id, cancellationToken);
            return new GetBookQueryResponse()
            {
                Book = domainBook.MapBook()
            };
        }
    }
}