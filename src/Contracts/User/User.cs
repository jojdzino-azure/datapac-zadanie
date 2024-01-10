namespace Contracts.User
{
    public class User
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime BirthDate { get; set; }
    }
}