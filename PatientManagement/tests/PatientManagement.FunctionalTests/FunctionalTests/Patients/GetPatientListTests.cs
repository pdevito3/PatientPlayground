namespace PatientManagement.FunctionalTests.FunctionalTests.Patients;

using PatientManagement.SharedTestHelpers.Fakes.Patient;
using PatientManagement.FunctionalTests.TestUtilities;
using PatientManagement.Domain;
using SharedKernel.Domain;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetPatientListTests : TestBase
{
    [Test]
    public async Task get_patient_list_returns_success_using_valid_auth_credentials()
    {
        // Arrange
        

        var user = await AddNewSuperAdmin();
        _client.AddAuth(user.Identifier);

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Patients.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
            
    [Test]
    public async Task get_patient_list_returns_unauthorized_without_valid_token()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Patients.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task get_patient_list_returns_forbidden_without_proper_scope()
    {
        // Arrange
        _client.AddAuth();

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Patients.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}