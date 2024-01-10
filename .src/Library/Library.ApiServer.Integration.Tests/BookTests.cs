using Contracts.Book;
using Library.ApiServer.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

namespace Library.ApiServer.Integration.Tests
{
    public class BookTests : BaseIntegrationTest
    {
        public BookTests(IntegrationTestsWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task CreateBook_ShouldCreateBookInDB()
        {
            var command = new CreateBookCommand()
            {
                Book = new Book()
                {
                    AuthorName = "Charles D",
                    Description = "A well written book",
                    Name = "Tale of two cities",
                    PublishedOn = DateTime.UtcNow.AddDays(-30000)
                }
            };

            var response = await mediator.Send(command);
            //also get  book from DB
            var dbContext = this._scope.ServiceProvider.GetRequiredService<LibraryContext>();
            var book = dbContext.Books.Where(x => x.Id == response.Book.Id).FirstOrDefault();

            Assert.NotNull(response);
            Assert.True(response.Book.Id > 0);
            Assert.NotNull(book);
            Assert.True(book.Id == response.Book.Id);
        }
    }
}