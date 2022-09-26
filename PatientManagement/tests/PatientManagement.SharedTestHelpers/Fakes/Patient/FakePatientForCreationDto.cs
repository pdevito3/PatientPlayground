namespace PatientManagement.SharedTestHelpers.Fakes.Patient;

using AutoBogus;
using Domain.Ethnicities;
using Domain.Races;
using Domain.Sexes;
using Lifespan;
using PatientManagement.Domain.Patients;
using PatientManagement.Domain.Patients.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public class FakePatientForCreationDto : AutoFaker<PatientForCreationDto>
{
    public FakePatientForCreationDto()
    {
        RuleFor(x => x.Lifespan, () => new FakeLifespanForCreationDto().Generate());
        RuleFor(x => x.Sex, f => f.PickRandom(Sex.ListNames()));
        RuleFor(x => x.Race, f => f.PickRandom(Race.ListNames()));
        RuleFor(x => x.Ethnicity, f => f.PickRandom(Ethnicity.ListNames()));
    }
}