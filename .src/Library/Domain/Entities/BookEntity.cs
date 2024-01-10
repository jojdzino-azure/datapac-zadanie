namespace Domain.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string AuthorName { get; set; }

        public string InternalComment { get; set; }
    }
}