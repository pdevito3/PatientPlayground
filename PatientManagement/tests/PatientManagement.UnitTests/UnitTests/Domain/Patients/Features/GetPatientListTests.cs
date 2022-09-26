namespace PatientManagement.UnitTests.UnitTests.Domain.Patients.Features;

using PatientManagement.SharedTestHelpers.Fakes.Patient;
using PatientManagement.Domain.Patients;
using PatientManagement.Domain.Patients.Dtos;
using PatientManagement.Domain.Patients.Mappings;
using PatientManagement.Domain.Patients.Features;
using PatientManagement.Domain.Patients.Services;
using MapsterMapper;
using FluentAssertions;
using HeimGuard;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetPatientListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IPatientRepository> _patientRepository;
    private readonly Mock<IHeimGuardClient> _heimGuard;

    public GetPatientListTests()
    {
        _patientRepository = new Mock<IPatientRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
        _heimGuard = new Mock<IHeimGuardClient>();
    }
    
    [Test]
    public async Task can_get_paged_list_of_patient()
    {
        //Arrange
        var fakePatientOne = FakePatient.Generate();
        var fakePatientTwo = FakePatient.Generate();
        var fakePatientThree = FakePatient.Generate();
        var patient = new List<Patient>();
        patient.Add(fakePatientOne);
        patient.Add(fakePatientTwo);
        patient.Add(fakePatientThree);
        var mockDbData = patient.AsQueryable().BuildMock();
        
        var queryParameters = new PatientParametersDto() { PageSize = 1, PageNumber = 2 };

        _patientRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetPatientList.Query(queryParameters);
        var handler = new GetPatientList.Handler(_patientRepository.Object, _mapper, _sieveProcessor, _heimGuard.Object);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }
}