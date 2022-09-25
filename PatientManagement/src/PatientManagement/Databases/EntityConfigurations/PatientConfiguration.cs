namespace PatientManagement.Databases.EntityConfigurations;

using PatientManagement.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resources;

public sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    /// <summary>
    /// The database configuration for Patients. 
    /// </summary>
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(o => o.InternalId)
            .HasDefaultValueSql($"concat('{Consts.DatabaseSequences.PatientInternalIdPrefix}', nextval('\"{Consts.DatabaseSequences.PatientInternalIdPrefix}\"'))")
            .IsRequired();
    }
}