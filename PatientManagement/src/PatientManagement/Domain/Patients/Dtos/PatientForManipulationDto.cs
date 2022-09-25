namespace PatientManagement.Domain.Patients.Dtos;

public abstract class PatientForManipulationDto 
{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }
        public string InternalId { get; set; }
}
