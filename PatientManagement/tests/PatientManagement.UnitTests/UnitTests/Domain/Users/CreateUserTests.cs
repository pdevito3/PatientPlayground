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
public class CreateUserTests
{
    private readonly Faker _faker;

    public CreateUserTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_user()
    {
        // Arrange
        var toCreate = new FakeUserForCreationDto().Generate();

        // Act
        var newUser = User.Create(toCreate);
        
        // Assert
        newUser.Should().NotBeNull();
    }
    
    [Test]
    public void can_NOT_create_user_without_identifier()
    {
        // Arrange
        var toCreate = new FakeUserForCreationDto().Generate();
        toCreate.Identifier = null;
        var newUser = () => User.Create(toCreate);

        // Act + Assert
        newUser.Should().Throw<FluentValidation.ValidationException>();
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeRecipe = FakeUser.Generate();

        // Assert
        fakeRecipe.DomainEvents.Count.Should().Be(1);
        fakeRecipe.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserCreated));
    }
}