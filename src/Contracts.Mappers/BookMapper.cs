namespace Contracts.Mappers.Book
{
    public static class BookMapper
    {
        public static Contracts.Book.Book MapBook(this Domain.Entities.BookEntity book)
        {
            return new Contracts.Book.Book()
            {
                Id = book.Id,
                Name = book.Name,
                AuthorName = book.AuthorName,
                Description = book.Description,
                PublishedOn = book.PublishedOn,
            };
        }

        public static Domain.Entities.BookEntity MapBook(this Contracts.Book.Book book)
        {
            return new Domain.Entities.BookEntity()
            {
                Id = book.Id,
                Name = book.Name,
                AuthorName = book.AuthorName,
                Description = book.Description,
                PublishedOn = book.PublishedOn,
            };
        }
    }
}