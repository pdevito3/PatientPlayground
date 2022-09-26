namespace PatientManagement.Domain.Patients.Features;

using PatientManagement.Domain.Patients.Dtos;
using PatientManagement.Domain.Patients.Services;
using PatientManagement.Wrappers;
using SharedKernel.Exceptions;
using PatientManagement.Domain;
using HeimGuard;
using MapsterMapper;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

public static class GetPatientList
{
    public sealed class Query : IRequest<PagedList<PatientDto>>
    {
        public readonly PatientParametersDto QueryParameters;

        public Query(PatientParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IPatientRepository patientRepository, IMapper mapper, SieveProcessor sieveProcessor, IHeimGuardClient heimGuard)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _sieveProcessor = sieveProcessor;
            _heimGuard = heimGuard;
        }

        public async Task<PagedList<PatientDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanReadPatients);

            var collection = _patientRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<PatientDto>();

            return await PagedList<PatientDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}