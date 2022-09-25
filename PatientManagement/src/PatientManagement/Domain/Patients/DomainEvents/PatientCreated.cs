namespace PatientManagement.Domain.Patients.DomainEvents;

public sealed class PatientCreated : DomainEvent
{
    public Patient Patient { get; set; } 
}
            