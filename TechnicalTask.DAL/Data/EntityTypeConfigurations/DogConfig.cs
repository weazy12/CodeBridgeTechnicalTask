using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalTask.DAL.Entities;

namespace TechnicalTask.DAL.Data.EntityTypeConfigurations
{
    public class DogConfig : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> entity)
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            entity.Property(d => d.Name)
                   .IsRequired()
                   .HasColumnName("name")
                   .HasMaxLength(100);

            entity.Property(d => d.Color)
                   .HasColumnName("color")
                   .IsRequired()
                   .HasMaxLength(100);

            entity.Property(d => d.TailLength)
                   .HasColumnName("tail_length")
                   .IsRequired();

            entity.Property(d => d.Weight)
                    .HasColumnName("weight")
                   .IsRequired();

            entity.HasIndex(d => d.Name).IsUnique();
        }
    }
}
