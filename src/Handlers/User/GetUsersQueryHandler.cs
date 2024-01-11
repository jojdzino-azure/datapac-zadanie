using Common.MediatR;
using Contracts.Mappers;
using Contracts.User;
using Domain.Repositories.User;

namespace Handlers.User
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersQueryResponse>
    {
        private readonly IQueryUserRepository _userRepository;

        public GetUsersQueryHandler(IQueryUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUsersAsync(request.PageNumber, request.PageSize, cancellationToken);
            return new GetUsersQueryResponse() { Users=result.Select(e=>e.MapUser()).ToList()};
        }
    }
}
