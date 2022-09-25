namespace PatientManagement.Domain.Patients.Services;

using PatientManagement.Domain.Patients;
using PatientManagement.Databases;
using PatientManagement.Services;

public interface IPatientRepository : IGenericRepository<Patient>
{
}

public sealed class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    private readonly PatientManagementDbContext _dbContext;

    public PatientRepository(PatientManagementDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
