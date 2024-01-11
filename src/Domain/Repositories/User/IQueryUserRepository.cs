using Domain.Entities;

namespace Domain.Repositories.User
{
    public interface IQueryUserRepository
    {
        public Task<List<UserEntity>> GetUsersAsync(int page = 0, int pageSize = 1000, CancellationToken cancellationToken = default);
        public Task<UserEntity> GetUserAsync(Guid guid, CancellationToken cancellationToken = default);
    }
}