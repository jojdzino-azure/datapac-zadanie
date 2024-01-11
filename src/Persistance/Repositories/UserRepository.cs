using Domain.Entities;
using Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories
{
    public class UserRepository : IQueryUserRepository
    {
        private LibraryContext _libraryContext;

        public UserRepository(LibraryContext libraryContext)
        {
            this._libraryContext = libraryContext;
        }

        public async Task<UserEntity?> GetUserAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            var result = await _libraryContext.Users
                .Where(e=>e.Guid==guid)
                .FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        public async Task<List<UserEntity>> GetUsersAsync(int page = 0, int pageSize = 1000, CancellationToken cancellationToken = default)
        {
            var result = await _libraryContext.Users
                .OrderBy(e => e.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return result;
        }
    }
}