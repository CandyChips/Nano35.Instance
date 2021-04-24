using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class LicenseType
    {
        //Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name {get;set;}
        public string Descriprion {get;set;}
        public decimal Price {get;set;}
        public DateTime Date {get;set;}
        public bool Deleted { get; set; }
        public class Configuration : IEntityTypeConfiguration<LicenseType>
        {
            public void Configure(EntityTypeBuilder<LicenseType> builder)
            {
                builder.ToTable("Licenses");
                builder.HasKey(u => u.Id);
                builder.Property(b => b.Name)
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.Descriprion)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.Price)    
                    .HasColumnType("money")
                    .IsRequired();
                builder.Property(b => b.Date)    
                    .HasColumnType("date")
                    .IsRequired();
                builder.Property(b => b.Deleted)    
                    .HasColumnType("bit")
                    .IsRequired();
            }
        }
    }
}