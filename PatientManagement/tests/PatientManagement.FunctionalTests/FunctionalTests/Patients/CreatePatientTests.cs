namespace PatientManagement.FunctionalTests.FunctionalTests.Patients;

using PatientManagement.SharedTestHelpers.Fakes.Patient;
using PatientManagement.FunctionalTests.TestUtilities;
using PatientManagement.Domain;
using SharedKernel.Domain;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreatePatientTests : TestBase
{
    [Test]
    public async Task create_patient_returns_created_using_valid_dto_and_valid_auth_credentials()
    {
        // Arrange
        var fakePatient = new FakePatientForCreationDto { }.Generate();

        var user = await AddNewSuperAdmin();
        FactoryClient.AddAuth(user.Identifier);

        // Act
        var route = ApiRoutes.Patients.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakePatient);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
            
    [Test]
    public async Task create_patient_returns_unauthorized_without_valid_token()
    {
        // Arrange
        var fakePatient = new FakePatientForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Patients.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakePatient);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task create_patient_returns_forbidden_without_proper_scope()
    {
        // Arrange
        var fakePatient = new FakePatientForCreationDto { }.Generate();
        FactoryClient.AddAuth();

        // Act
        var route = ApiRoutes.Patients.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakePatient);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}