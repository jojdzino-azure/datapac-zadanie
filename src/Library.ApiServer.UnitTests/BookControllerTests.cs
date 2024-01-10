using Contracts.Book;
using Library.ApiServer.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.ApiServer.UnitTests
{
    public class BookControllerTests
    {
        [Fact]
        public async Task CrateBook_ShouldReturnValue()
        {
            var moqMediator = new Mock<IMediator>();
            var bookToSend = new Book { AuthorName = "TestAuthorName", Description = "Description test", Name = "Book test name", PublishedOn = DateTime.UtcNow.AddDays(-5) };
            var requestToSend = new CreateBookCommand() { Book = bookToSend };
            var bookToReturn = new Book { Id = 1, AuthorName = "TestAuthorName", Description = "Description test", Name = "Book test name", PublishedOn = DateTime.UtcNow.AddDays(-5) };
            var responseToReturn = new CreateBookCommandResponse { Book = bookToReturn };
            moqMediator.Setup(e => e.Send(It.IsAny<CreateBookCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(responseToReturn));

            var controller = new BooksController(moqMediator.Object);
            var result = await controller.CreateBook(requestToSend);

            var okResult = result as OkObjectResult;
            Assert.Equal(okResult.Value, responseToReturn);
        }
    }
}