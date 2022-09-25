namespace PatientManagement.SharedTestHelpers.Fakes.User;

using AutoBogus;
using PatientManagement.Domain;
using PatientManagement.Domain.Users.Dtos;
using PatientManagement.Domain.Roles;

public class FakeUserForUpdateDto : AutoFaker<UserForUpdateDto>
{
    public FakeUserForUpdateDto()
    {
        RuleFor(u => u.Email, f => f.Person.Email);
    }
}