using Contracts.Borrowing;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiServer.Controllers
{
    [Route("/borrowings")]
    public class BorrowingsController : BaseController
    {
        public BorrowingsController(IMediator sender) : base(sender)
        {
        }

        [HttpPost]
        public async Task<CreateBorrowingCommandResponse> CreateBorrowing([FromBody] CreateBorrowingCommand query, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<ReturnBorrowingCommandResponse> ReturnBorrowing([FromRoute]  int id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ReturnBorrowingCommand() { BorrowingId= id }, cancellationToken);

            return result;
        }
    }
}