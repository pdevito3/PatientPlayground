namespace PatientManagement.Domain.RolePermissions.Services;

using PatientManagement.Domain.RolePermissions;
using PatientManagement.Databases;
using PatientManagement.Services;

public interface IRolePermissionRepository : IGenericRepository<RolePermission>
{
}

public sealed class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
{
    private readonly PatientManagementDbContext _dbContext;

    public RolePermissionRepository(PatientManagementDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
