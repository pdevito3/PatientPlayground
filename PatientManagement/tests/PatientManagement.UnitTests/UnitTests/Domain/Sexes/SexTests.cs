namespace PatientManagement.UnitTests.UnitTests.Domain.Sexes;

using Bogus;
using FluentAssertions;
using NUnit.Framework;
using PatientManagement.Domain.Sexes;

[Parallelizable]
public class SexTests
{
    private readonly Faker _faker;

    public SexTests()
    {
        _faker = new Faker();
    }
    
    [TestCase("Male", "Male")]
    [TestCase("male", "Male")]
    [TestCase("m", "Male")]
    [TestCase("Female", "Female")]
    [TestCase("female", "Female")]
    [TestCase("f", "Female")]
    [TestCase(null, "Unknown")]
    [TestCase("random", "Unknown")]
    public void can_get_expected_sex(string givenSex, string expectedSex)
    {
        var sex = new Sex(givenSex);
        sex.Value.Should().Be(expectedSex);
    }
}