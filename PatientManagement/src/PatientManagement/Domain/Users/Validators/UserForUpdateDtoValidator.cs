namespace PatientManagement.Domain.Users.Validators;

using PatientManagement.Domain.Users.Dtos;
using FluentValidation;

public sealed class UserForUpdateDtoValidator: UserForManipulationDtoValidator<UserForUpdateDto>
{
    public UserForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}