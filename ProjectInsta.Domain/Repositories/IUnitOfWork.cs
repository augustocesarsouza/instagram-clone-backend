namespace ProjectInsta.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
