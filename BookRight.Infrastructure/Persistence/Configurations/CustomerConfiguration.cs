using BookRight.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            // PK konfiguration
            builder.HasKey(c => c.Id); // Sig til EF Core; "Dette er PK"
            builder.Property(x => x.Id)
                .ValueGeneratedNever(); // Db må ikke generere Id. I DDD styrer Domain dette.

            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                name.Property(n => n.LastName)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsRequired();
            });

            builder.OwnsOne(c => c.Phone, phone =>
            {
                phone.Property(p => p.Value)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }
    }
}
