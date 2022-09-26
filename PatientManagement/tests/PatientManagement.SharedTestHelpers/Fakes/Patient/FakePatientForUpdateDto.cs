namespace PatientManagement.SharedTestHelpers.Fakes.Patient;

using AutoBogus;
using Domain.Sexes;
using Lifespan;
using PatientManagement.Domain.Patients;
using PatientManagement.Domain.Patients.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public class FakePatientForUpdateDto : AutoFaker<PatientForUpdateDto>
{
    public FakePatientForUpdateDto()
    {
        RuleFor(x => x.Lifespan, () => new FakeLifespanForUpdateDto().Generate());
        RuleFor(rp => rp.Sex, f => f.PickRandom(Sex.ListNames()));
    }
}