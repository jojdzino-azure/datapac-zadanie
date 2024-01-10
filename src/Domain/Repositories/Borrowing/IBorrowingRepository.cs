using Domain.Entities;

namespace Domain.Repositories.Borrowing
{
    public interface IBorrowingRepository
    {
        public Task<BorrowingEntity> CreateBorrowing(BorrowingEntity borrowing, CancellationToken cancellationToken = default);

        public Task<List<BorrowingEntity>> ExpiringBorrowinsForUser(UserEntity user, CancellationToken cancellationToken = default);

        public Task<BorrowingEntity?> ReturnBorrowing(int borrowing, CancellationToken cancellationToken = default);
    }
}