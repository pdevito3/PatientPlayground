namespace PatientManagement.Domain.Patients.Dtos;

using SharedKernel.Dtos;

public sealed class PatientParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
