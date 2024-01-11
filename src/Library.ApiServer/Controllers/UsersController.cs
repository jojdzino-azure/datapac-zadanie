using Contracts.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiServer.Controllers
{
    [Route("/users")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator sender) : base(sender)
        {
        }

        [HttpGet]
        public async Task<GetUsersQueryResponse> CreateBorrowing([FromQuery] GetUsersQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return result;
        }
    }
}