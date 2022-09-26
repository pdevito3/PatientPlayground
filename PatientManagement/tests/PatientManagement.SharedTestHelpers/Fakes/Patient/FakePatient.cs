namespace PatientManagement.SharedTestHelpers.Fakes.Patient;

using AutoBogus;
using PatientManagement.Domain.Patients;
using PatientManagement.Domain.Patients.Dtos;

public class FakePatient
{
    public static Patient Generate(PatientForCreationDto patientForCreationDto)
    {
        return Patient.Create(patientForCreationDto);
    }

    public static Patient Generate()
    {
        var temp = new FakePatientForCreationDto().Generate();
        return Patient.Create(temp);
    }
}