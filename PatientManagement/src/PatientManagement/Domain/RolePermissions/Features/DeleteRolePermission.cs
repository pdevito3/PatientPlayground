namespace PatientManagement.Domain.RolePermissions.Features;

using PatientManagement.Domain.RolePermissions.Services;
using PatientManagement.Services;
using SharedKernel.Exceptions;
using PatientManagement.Domain;
using HeimGuard;
using MediatR;

public static class DeleteRolePermission
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;

        public Command(Guid rolePermission)
        {
            Id = rolePermission;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IRolePermissionRepository rolePermissionRepository, IUnitOfWork unitOfWork, IHeimGuardClient heimGuard)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
            _heimGuard = heimGuard;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanDeleteRolePermissions);

            var recordToDelete = await _rolePermissionRepository.GetById(request.Id, cancellationToken: cancellationToken);

            _rolePermissionRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}