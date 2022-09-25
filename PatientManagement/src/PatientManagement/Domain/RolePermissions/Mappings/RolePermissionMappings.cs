namespace PatientManagement.Domain.RolePermissions.Mappings;

using PatientManagement.Domain.RolePermissions.Dtos;
using PatientManagement.Domain.RolePermissions;
using Mapster;

public sealed class RolePermissionMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RolePermission, RolePermissionDto>();
        config.NewConfig<RolePermissionForCreationDto, RolePermission>()
            .TwoWays();
        config.NewConfig<RolePermissionForUpdateDto, RolePermission>()
            .TwoWays();
    }
}