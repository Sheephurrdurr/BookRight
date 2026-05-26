using BookRight.Domain.Aggregates.TherapistAggregate;
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
            builder.Property(x => x.Id) 
                .ValueGeneratedNever(); // Db må ikke generere Id. I DDD styrer Domain dette.

            builder.OwnsOne(t => t.Name, name =>                            
            {
                name.Property(n => n.FirstName) 
                    .HasMaxLength(100)
                    .IsRequired();

                name.Property(n => n.LastName)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.OwnsOne(t => t.Email, email =>          
            {
                email.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsRequired();
            });

            builder.Property(t => t.Specialization)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasMany(t => t.Qualifications)
                .WithOne()
                .HasForeignKey(q => q.TherapistId) // Shadow property for FK
                .OnDelete(DeleteBehavior.Cascade); // Sletter kvalifikationer hvis terapeut slettes

            builder.OwnsOne(t => t.Authorization, authorization =>
            {
                authorization.Property(a => a.Type)
                    .HasMaxLength(100)
                    .IsRequired();

                authorization.Property(a => a.Number)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }
    }
}
