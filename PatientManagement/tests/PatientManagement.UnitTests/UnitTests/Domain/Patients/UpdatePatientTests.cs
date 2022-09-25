namespace PatientManagement.UnitTests.UnitTests.Domain.Patients;

using PatientManagement.SharedTestHelpers.Fakes.Patient;
using PatientManagement.Domain.Patients;
using PatientManagement.Domain.Patients.DomainEvents;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdatePatientTests
{
    private readonly Faker _faker;

    public UpdatePatientTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_patient()
    {
        // Arrange
        var fakePatient = FakePatient.Generate();
        var updatedPatient = new FakePatientForUpdateDto().Generate();
        
        // Act
        fakePatient.Update(updatedPatient);

        // Assert
        fakePatient.Should().BeEquivalentTo(updatedPatient, options =>
            options.ExcludingMissingMembers());
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakePatient = FakePatient.Generate();
        var updatedPatient = new FakePatientForUpdateDto().Generate();
        fakePatient.DomainEvents.Clear();
        
        // Act
        fakePatient.Update(updatedPatient);

        // Assert
        fakePatient.DomainEvents.Count.Should().Be(1);
        fakePatient.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(PatientUpdated));
    }
}