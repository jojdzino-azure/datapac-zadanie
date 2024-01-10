namespace Contracts.Book
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string AuthorName { get; set; }
    }
}