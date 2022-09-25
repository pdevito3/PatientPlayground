namespace PatientManagement.Domain.Patients.Mappings;

using PatientManagement.Domain.Patients.Dtos;
using PatientManagement.Domain.Patients;
using Mapster;

public sealed class PatientMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Patient, PatientDto>();
        config.NewConfig<PatientForCreationDto, Patient>()
            .TwoWays();
        config.NewConfig<PatientForUpdateDto, Patient>()
            .TwoWays();
    }
}