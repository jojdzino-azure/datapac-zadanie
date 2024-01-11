using Common.MediatR;
using Contracts.Book;
using Contracts.Mappers.Book;
using Contracts.User;
using Domain.Repositories.Book;

namespace Handlers.Books
{
    public class GetBooksHandler : IQueryHandler<GetBooksQuery, GetBooksQueryResponse>
    {
        private readonly IQueryBookRepository _repo;

        public GetBooksHandler(IQueryBookRepository repo)
        {
            this._repo = repo;
        }

        public async Task<GetBooksQueryResponse> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            // validacia sa dokonca ani neriesi pre Query typy, ale len pre commandy, nakolko GET metody by nemali nic modifikovat, tak ich v principe netreba validovat
            var domainBooks = await _repo.GetBooksAsync(request.PageNumber, request.PageSize, cancellationToken);
            return new GetBooksQueryResponse()
            {
                Books = domainBooks.Select(e => e.MapBook()).ToList()
            };
        }
    }
}