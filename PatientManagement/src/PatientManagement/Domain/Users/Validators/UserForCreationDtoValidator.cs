namespace PatientManagement.Domain.Users.Validators;

using PatientManagement.Domain.Users.Dtos;
using FluentValidation;

public sealed class UserForCreationDtoValidator: UserForManipulationDtoValidator<UserForCreationDto>
{
    public UserForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}