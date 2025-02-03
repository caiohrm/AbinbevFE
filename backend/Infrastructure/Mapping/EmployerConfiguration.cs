using System;
using CrossCutting.Enums;
using CrossCutting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
	public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {
		
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            //builder.ToTable("Employer");

            builder.HasKey(u => u.Id);
            //builder.Property(u => u.Id).HasColumnType("int");

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
            builder.Property(u => u.DocNumber).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Role).HasConversion<string>().HasMaxLength(50);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(250);
            builder.Property(u => u.BirthDate).IsRequired();
            builder.HasIndex("DocNumber").IsUnique();

        }
    }
}

