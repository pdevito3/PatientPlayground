namespace PatientManagement.SharedTestHelpers.Fakes.Lifespan;

using AutoBogus;
using Domain.Lifespans.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public class FakeLifespanForUpdateDto : AutoFaker<LifespanForUpdateDto>
{
    public FakeLifespanForUpdateDto()
    {
        RuleFor(x => x.DateOfBirth, f=> f.Date.PastDateOnly());
    }
}