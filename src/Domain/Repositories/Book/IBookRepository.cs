using Domain.Entities;

namespace Domain.Repositories.Book
{
    /// <summary>
    /// Toto nie je uplne stastna implementacia, lebo ak by som chcel followovat CQRS patter uplne, tak by tieto metody vracali viac menej len true, false, ci sa podarilo
    /// Nakolko ale to robim cez jednu RW db tak preco rovno nieco nevratit.
    /// </summary>
    public interface IBookRepository
    {
        public Task<BookEntity?> UpdateBookAsync(int bookId, BookEntity book, CancellationToken cancellationToken = default);

        public Task<BookEntity> CreateBookAsync(BookEntity book, CancellationToken cancellationToken = default);

        public Task<BookEntity?> DeleteBookAsync(int bookId, CancellationToken cancellationToken = default);
    }
}