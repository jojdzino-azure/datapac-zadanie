using Common.MediatR;

namespace Contracts.User
{
    public class GetUsersQuery : IQuery<GetUsersQueryResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}