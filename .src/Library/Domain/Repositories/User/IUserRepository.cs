using Domain.Entities;

namespace Domain.Repositories.User
{
    public interface IUserRepository
    {
        public Task<List<UserEntity>> GetUsers(int page = 0, int pageSize = 1000, CancellationToken cancellationToken = default);
    }
}