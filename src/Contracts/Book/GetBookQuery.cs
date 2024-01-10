using Common.MediatR;

namespace Contracts.Book
{
    public class GetBookQuery : IQuery<GetBookQueryResponse>
    {
        public int Id { get; set; }
    }
}