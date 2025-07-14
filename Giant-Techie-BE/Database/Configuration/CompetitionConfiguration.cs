using Giant_Techie_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Giant_Techie_BE.Database.Configuration
{
    public class CompetitionConfiguration : IEntityTypeConfiguration<Competitions>
    {
        public void Configure(EntityTypeBuilder<Competitions> builder)
        {
            // Define table name
            builder.ToTable("Competitions");

            // Set primary key
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(m => m.Description)
               .IsRequired();

            builder.Property(m => m.StartDate)
              .IsRequired();

            builder.Property(m => m.EndDate)
                   .IsRequired();

            builder.Property(m => m.Status)
                  .IsRequired();

            // Configure Created and LastModified properties to be handled as immutable and modifiable timestamps
            builder.Property(m => m.Created)
               .IsRequired()
               .ValueGeneratedOnAdd();

            builder.Property(m => m.LastModified)
                   .IsRequired()
                   .ValueGeneratedOnUpdate();

          
        }
    }
}
