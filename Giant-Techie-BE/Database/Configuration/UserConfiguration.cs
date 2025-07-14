using Giant_Techie_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Giant_Techie_BE.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            // Set primary key
            builder.HasKey(m => m.Id);

            builder.Property(m => m.FullName)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(m => m.Email)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(m => m.CollegeName)
              .IsRequired()
               .HasMaxLength(100);

            builder.Property(m => m.Password)
                .IsRequired()
                .HasMaxLength(50);
        }

    }
}
