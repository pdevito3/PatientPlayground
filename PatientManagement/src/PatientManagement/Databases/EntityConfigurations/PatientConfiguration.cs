namespace PatientManagement.Databases.EntityConfigurations;

using Domain.Sexes;
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
        
        builder.OwnsOne(x => x.Lifespan, opts =>
        {
            opts.Property(x => x.DateOfBirth).HasColumnName("date_of_birth");
            opts.Property(x => x.Age).HasColumnName("age");
        }).Navigation(x => x.Lifespan);
        
        builder.Property(x => x.Sex)
            .HasConversion(x => x.Value, x => new Sex(x))
            .HasColumnName("sex");
    }
}