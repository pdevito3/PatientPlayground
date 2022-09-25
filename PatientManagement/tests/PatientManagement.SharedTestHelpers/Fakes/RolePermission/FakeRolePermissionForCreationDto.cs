namespace PatientManagement.SharedTestHelpers.Fakes.RolePermission;

using AutoBogus;
using PatientManagement.Domain;
using PatientManagement.Domain.RolePermissions.Dtos;
using PatientManagement.Domain.Roles;

public class FakeRolePermissionForCreationDto : AutoFaker<RolePermissionForCreationDto>
{
    public FakeRolePermissionForCreationDto()
    {
        RuleFor(rp => rp.Permission, f => f.PickRandom(Permissions.List()));
        RuleFor(rp => rp.Role, f => f.PickRandom(Role.ListNames()));
    }
}