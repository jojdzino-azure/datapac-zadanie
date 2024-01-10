using Domain.Entities;

namespace Domain.Repositories.Borrowing
{
    public interface IQueryBorrowingRepository
    {
        public Task<List<BorrowingEntity>> GetBorrowings(int pageNumber = 0, int pageSize = 100, CancellationToken cancellationToken = default);
    }
}