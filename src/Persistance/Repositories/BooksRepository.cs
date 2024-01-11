using Domain.Entities;
using Domain.Repositories.Book;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories
{
    public class BooksRepository : IQueryBookRepository, IBookRepository
    {
        private readonly LibraryContext _libraryContext;

        public BooksRepository(LibraryContext libraryContext)
        {
            this._libraryContext = libraryContext;
        }

        public async Task<BookEntity> CreateBookAsync(BookEntity book, CancellationToken cancellationToken = default)
        {
            book.Id = 0;
            await _libraryContext.Books.AddAsync(book, cancellationToken);
            await _libraryContext.SaveChangesAsync();
            return book;
        }

        public async Task<BookEntity?> DeleteBookAsync(int bookId, CancellationToken cancellationToken = default)
        {
            BookEntity? foundBook = await _libraryContext.Books.FindAsync(new object[] { bookId }, cancellationToken);
            if (foundBook == null)
            {
                return null;
            }

            _libraryContext.Books.Remove(foundBook);
            await _libraryContext.SaveChangesAsync();
            return foundBook;
        }

        public async Task<BookEntity?> GetBookAsync(int bookId, CancellationToken cancellationToken = default)
        {
            return await _libraryContext.Books
                .FirstOrDefaultAsync(e => e.Id == bookId, cancellationToken);
        }

        public async Task<List<BookEntity>> GetBooksAsync(int pageNumber = 0, int pageSize = 100, CancellationToken cancellationToken = default)
        {
            return await _libraryContext.Books
                .OrderBy(e => e.Id)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<BookEntity?> UpdateBookAsync(int bookId, BookEntity book, CancellationToken cancellationToken = default)
        {
            var foundBook = await _libraryContext.Books.FindAsync(new object[] { bookId }, cancellationToken);
            if (foundBook == null)
            {
                return null;
            }
            foundBook.Description = book.Description;
            foundBook.AuthorName = book.AuthorName;
            foundBook.PublishedOn = book.PublishedOn;
            foundBook.Name = book.Name;
            await _libraryContext.SaveChangesAsync();
            return book;
        }
    }
}