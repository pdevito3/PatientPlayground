namespace PatientManagement.IntegrationTests.FeatureTests.Patients;

using PatientManagement.SharedTestHelpers.Fakes.Patient;
using PatientManagement.Domain.Patients.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class PatientQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_patient_with_accurate_props()
    {
        // Arrange
        var fakePatientOne = FakePatient.Generate(new FakePatientForCreationDto().Generate());
        await InsertAsync(fakePatientOne);

        // Act
        var query = new GetPatient.Query(fakePatientOne.Id);
        var patient = await SendAsync(query);

        // Assert
        patient.FirstName.Should().Be(fakePatientOne.FirstName);
        patient.LastName.Should().Be(fakePatientOne.LastName);
        patient.Lifespan.DateOfBirth.Should().Be(fakePatientOne.Lifespan.DateOfBirth);
        patient.Lifespan.Age.Should().Be(fakePatientOne.Lifespan.Age);
        patient.Race.Should().Be(fakePatientOne.Race);
        patient.Ethnicity.Should().Be(fakePatientOne.Ethnicity);
        patient.InternalId.Should().Be(fakePatientOne.InternalId);
    }

    [Test]
    public async Task get_patient_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetPatient.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}