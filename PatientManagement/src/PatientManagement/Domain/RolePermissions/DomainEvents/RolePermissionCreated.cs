namespace PatientManagement.Domain.RolePermissions.DomainEvents;

public sealed class RolePermissionCreated : DomainEvent
{
    public RolePermission RolePermission { get; set; } 
}
            