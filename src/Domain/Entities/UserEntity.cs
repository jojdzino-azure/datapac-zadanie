namespace Domain.Entities
{
    public class UserEntity
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime BirthDate { get; set; }
    }
}