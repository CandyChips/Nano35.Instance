using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class License
    {
        public Guid Id { get; set; }
        public DateTime Date {get;set;}
        public bool Deleted { get; set; }
        public Guid LicenseTypeId { get; set; }
        public LicenseType LicenseType { get; set; }
        public Guid InstanceId { get; set; }
        public Models.Instance Instance { get; set; }
        
        public class Configuration : IEntityTypeConfiguration<License>
        {
            public void Configure(EntityTypeBuilder<License> builder)
            {
                builder.ToTable("Licenses");
                builder.HasKey(u => u.Id);       
                builder.Property(b => b.Date)    
                    .HasColumnType("date")
                    .IsRequired();
                builder.Property(b => b.Deleted)    
                    .HasColumnType("bit")
                    .IsRequired();
                builder.HasOne(p => p.Instance)
                    .WithMany()
                    .HasForeignKey(p => p.InstanceId);
                builder.HasOne(p => p.LicenseType)
                    .WithMany()
                    .HasForeignKey(p => p.LicenseTypeId);
            }
        }
    }
}