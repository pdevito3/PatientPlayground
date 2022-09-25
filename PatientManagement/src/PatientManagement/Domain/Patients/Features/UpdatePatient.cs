namespace PatientManagement.Domain.Patients.Features;

using PatientManagement.Domain.Patients;
using PatientManagement.Domain.Patients.Dtos;
using PatientManagement.Domain.Patients.Validators;
using PatientManagement.Domain.Patients.Services;
using PatientManagement.Services;
using SharedKernel.Exceptions;
using PatientManagement.Domain;
using HeimGuard;
using MapsterMapper;
using MediatR;

public static class UpdatePatient
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly PatientForUpdateDto PatientToUpdate;

        public Command(Guid patient, PatientForUpdateDto newPatientData)
        {
            Id = patient;
            PatientToUpdate = newPatientData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IHeimGuardClient heimGuard)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _heimGuard = heimGuard;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanUpdatePatients);

            var patientToUpdate = await _patientRepository.GetById(request.Id, cancellationToken: cancellationToken);

            patientToUpdate.Update(request.PatientToUpdate);
            _patientRepository.Update(patientToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}