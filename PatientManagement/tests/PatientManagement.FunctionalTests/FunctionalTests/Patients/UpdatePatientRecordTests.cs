namespace PatientManagement.FunctionalTests.FunctionalTests.Patients;

using PatientManagement.SharedTestHelpers.Fakes.Patient;
using PatientManagement.FunctionalTests.TestUtilities;
using PatientManagement.Domain;
using SharedKernel.Domain;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdatePatientRecordTests : TestBase
{
    [Test]
    public async Task put_patient_returns_nocontent_when_entity_exists_and_auth_credentials_are_valid()
    {
        // Arrange
        var fakePatient = FakePatient.Generate(new FakePatientForCreationDto().Generate());
        var updatedPatientDto = new FakePatientForUpdateDto { }.Generate();

        var user = await AddNewSuperAdmin();
        _client.AddAuth(user.Identifier);
        await InsertAsync(fakePatient);

        // Act
        var route = ApiRoutes.Patients.Put.Replace(ApiRoutes.Patients.Id, fakePatient.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedPatientDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
            
    [Test]
    public async Task put_patient_returns_unauthorized_without_valid_token()
    {
        // Arrange
        var fakePatient = FakePatient.Generate(new FakePatientForCreationDto().Generate());
        var updatedPatientDto = new FakePatientForUpdateDto { }.Generate();

        await InsertAsync(fakePatient);

        // Act
        var route = ApiRoutes.Patients.Put.Replace(ApiRoutes.Patients.Id, fakePatient.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedPatientDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task put_patient_returns_forbidden_without_proper_scope()
    {
        // Arrange
        var fakePatient = FakePatient.Generate(new FakePatientForCreationDto().Generate());
        var updatedPatientDto = new FakePatientForUpdateDto { }.Generate();
        _client.AddAuth();

        await InsertAsync(fakePatient);

        // Act
        var route = ApiRoutes.Patients.Put.Replace(ApiRoutes.Patients.Id, fakePatient.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedPatientDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}