using Domain.Entities;

namespace Domain.Repositories.Book
{
    public interface IQueryBookRepository
    {
        public Task<BookEntity?> GetBookAsync(int bookId, CancellationToken cancellationToken = default);

        public Task<List<BookEntity>> GetBooksAsync(int pageNumber = 0, int pageSize = 100, CancellationToken cancellationToken = default);
    }
}