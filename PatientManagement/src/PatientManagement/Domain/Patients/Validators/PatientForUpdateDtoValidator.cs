namespace PatientManagement.Domain.Patients.Validators;

using PatientManagement.Domain.Patients.Dtos;
using FluentValidation;

public sealed class PatientForUpdateDtoValidator: PatientForManipulationDtoValidator<PatientForUpdateDto>
{
    public PatientForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}