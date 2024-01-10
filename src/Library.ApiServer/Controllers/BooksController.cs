using Contracts.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiServer.Controllers
{
    [Route("/books")]
    public class BooksController : BaseController
    {
        public BooksController(IMediator sender) : base(sender)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var query = new GetBookQuery() { Id = id };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 100, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetBooksQuery() { PageNumber = pageNumber, PageSize = pageSize }, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
            {
                return BadRequest("Body is empty");
            }
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
            {
                return BadRequest("Body is empty");
            }
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook([FromBody] DeleteBookCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
            {
                return BadRequest("Body is empty");
            }
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}