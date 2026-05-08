using BookRight.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations
{
    public class TherapistConfiguration : IEntityTypeConfiguration<Therapist>
    {
        public void Configure(EntityTypeBuilder<Therapist> builder)
        {
            builder.ToTable("Therapists");
            // PK konfiguration
            builder.HasKey(t => t.Id); // Sig til EF Core; "Dette er PK"
            builder.Property(x => x.Id) // Fortæl EF Core hvad vi skal gøre med den PK property
                .HasConversion( // Oversætter BookingID (fra domain) til Guid (i db) og omvendt.
                    id => id.Value,
                    value => new TherapistId(value))
                .ValueGeneratedNever(); // Db må ikke generere Id. I DDD styrer Domain dette.

            builder.OwnsOne(t => t.Name, name =>                            
            {
                name.Property(n => n.FirstName) 
                    .HasColumnName("FirstName")
                    .HasMaxLength(100)
                    .IsRequired();

                name.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.OwnsOne(t => t.Email, email => 
                                                  
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            builder.Property(t => t.Specialization)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
