namespace PatientManagement.SharedTestHelpers.Fakes.Patient;

using AutoBogus;
using Bogus;
using Domain.Lifespans;
using PatientManagement.Domain.Patients;
using PatientManagement.Domain.Patients.Dtos;

// or replace 'AutoFaker' with 'Faker' along with your own rules if you don't want all fields to be auto faked
public class FakePatientDto : AutoFaker<PatientDto>
{
    public FakePatientDto()
    {
        RuleFor(x => x.Lifespan.DateOfBirth, f=> f.Date.PastDateOnly());
    }
}