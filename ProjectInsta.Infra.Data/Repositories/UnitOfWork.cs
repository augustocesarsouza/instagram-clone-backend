using Microsoft.EntityFrameworkCore.Storage;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _ctx.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose() 
        {
            _transaction?.Dispose();
        }
    }
}
