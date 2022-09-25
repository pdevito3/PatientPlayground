namespace PatientManagement.Domain.Patients.Features;

using PatientManagement.Domain.Patients.Dtos;
using PatientManagement.Domain.Patients.Services;
using SharedKernel.Exceptions;
using PatientManagement.Domain;
using HeimGuard;
using MapsterMapper;
using MediatR;

public static class GetPatient
{
    public sealed class Query : IRequest<PatientDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PatientDto>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IPatientRepository patientRepository, IMapper mapper, IHeimGuardClient heimGuard)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _heimGuard = heimGuard;
        }

        public async Task<PatientDto> Handle(Query request, CancellationToken cancellationToken)
        {
            await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanReadPatients);

            var result = await _patientRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<PatientDto>(result);
        }
    }
}