namespace PatientManagement.Domain.Patients.DomainEvents;

public sealed class PatientUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            