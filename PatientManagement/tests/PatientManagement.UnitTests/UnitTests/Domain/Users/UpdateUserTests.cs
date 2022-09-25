namespace PatientManagement.UnitTests.UnitTests.Domain.Users;

using PatientManagement.Domain.Users.DomainEvents;
using PatientManagement.Domain;
using PatientManagement.Domain.Users;
using PatientManagement.Wrappers;
using PatientManagement.Domain.Users.Dtos;
using PatientManagement.SharedTestHelpers.Fakes.User;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

[Parallelizable]
public class UpdateUserTests
{
    private readonly Faker _faker;

    public UpdateUserTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_user()
    {
        // Arrange
        var fakeUser = FakeUser.Generate();
        var updatedUser = new FakeUserForUpdateDto().Generate();
        
        // Act
        fakeUser.Update(updatedUser);

        // Assert
        fakeUser.Should().BeEquivalentTo(updatedUser, options =>
            options.ExcludingMissingMembers()
                .Excluding(x => x.Email));
        fakeUser.Email.Value.Should().Be(updatedUser.Email);
    }
    
    [Test]
    public void can_NOT_update_user_without_identifier()
    {
        // Arrange
        var fakeUser = FakeUser.Generate();
        var updatedUser = new FakeUserForUpdateDto().Generate();
        updatedUser.Identifier = null;
        var newUser = () => fakeUser.Update(updatedUser);

        // Act + Assert
        newUser.Should().Throw<FluentValidation.ValidationException>();
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeUser = FakeUser.Generate();
        var updatedUser = new FakeUserForUpdateDto().Generate();
        fakeUser.DomainEvents.Clear();
        
        // Act
        fakeUser.Update(updatedUser);

        // Assert
        fakeUser.DomainEvents.Count.Should().Be(1);
        fakeUser.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserUpdated));
    }
}