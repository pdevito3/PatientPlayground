namespace PatientManagement.Services;

using PatientManagement.Databases;

public interface IUnitOfWork : IPatientManagementService
{
    Task<int> CommitChanges(CancellationToken cancellationToken = default);
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly PatientManagementDbContext _dbContext;

    public UnitOfWork(PatientManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitChanges(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
