namespace Contracts.Mappers
{
    public static class UserMapper
    {
        public static User.User? MapUser(this Domain.Entities.UserEntity? user)
        {
            if (user == null) return null;
            return new User.User()
            {
                Guid = user.Guid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                RegisteredAt = user.RegisteredAt
            };
        }
    }
}