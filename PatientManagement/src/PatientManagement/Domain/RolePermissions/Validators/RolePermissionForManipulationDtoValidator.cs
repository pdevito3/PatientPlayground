namespace PatientManagement.Domain.RolePermissions.Validators;

using PatientManagement.Domain.RolePermissions.Dtos;
using PatientManagement.Domain;
using FluentValidation;

public class RolePermissionForManipulationDtoValidator<T> : AbstractValidator<T> where T : RolePermissionForManipulationDto
{
    public RolePermissionForManipulationDtoValidator()
    {
        RuleFor(rp => rp.Permission)
            .Must(BeAnExistingPermission)
            .WithMessage("Please use a valid permission.");
    }
    
    private static bool BeAnExistingPermission(string permission)
    {
        return Permissions.List().Contains(permission, StringComparer.InvariantCultureIgnoreCase);
    }
}