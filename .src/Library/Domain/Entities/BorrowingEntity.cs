namespace Domain.Entities
{
    public class BorrowingEntity
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime BorrowedAt { get; set; }
        public int BorrowedForDays { get; set; }
        public DateTime ReturnedAt { get; set; }
        public bool Notified { get; set; }
        public BookEntity Book { get; set; }
        public UserEntity BorrowedBy { get; set; }
    }
}