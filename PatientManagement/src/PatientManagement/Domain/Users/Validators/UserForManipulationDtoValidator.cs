namespace PatientManagement.Domain.Users.Validators;

using PatientManagement.Domain.Users.Dtos;
using PatientManagement.Domain;
using FluentValidation;

public class UserForManipulationDtoValidator<T> : AbstractValidator<T> where T : UserForManipulationDto
{
    public UserForManipulationDtoValidator()
    {
        RuleFor(u => u.Identifier)
            .NotEmpty()
            .WithMessage("Please provide an identifier.");
    }
}