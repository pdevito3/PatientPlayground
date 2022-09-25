namespace PatientManagement.SharedTestHelpers.Fakes.RolePermission;

using AutoBogus;
using PatientManagement.Domain.RolePermissions;
using PatientManagement.Domain.RolePermissions.Dtos;

public class FakeRolePermission
{
    public static RolePermission Generate(RolePermissionForCreationDto rolePermissionForCreationDto)
    {
        return RolePermission.Create(rolePermissionForCreationDto);
    }

    public static RolePermission Generate()
    {
        return RolePermission.Create(new FakeRolePermissionForCreationDto().Generate());
    }
}