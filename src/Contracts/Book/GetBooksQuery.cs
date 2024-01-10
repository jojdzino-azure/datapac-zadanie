using Common.MediatR;

namespace Contracts.Book
{
    public class GetBooksQuery : IQuery<GetBooksQueryResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}