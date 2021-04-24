using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class Instance
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string OrgName { get; set; }
        public string OrgRealName { get; set; }
        public string OrgEmail { get; set; }
        public string CompanyInfo { get; set; }
        public bool Deleted { get; set; }
        //Forgein keys
        public Guid InstanceTypeId { get; set; }
        public InstanceType InstanceType { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        
        public class Configuration : IEntityTypeConfiguration<Instance>
        {
            public void Configure(EntityTypeBuilder<Instance> builder)
            {
                builder.ToTable("Instances");
                builder.HasKey(u => u.Id);        
                builder.Property(b => b.OrgName)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.OrgRealName)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.OrgEmail)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.CompanyInfo)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.Deleted) 
                    .HasColumnType("bit")
                    .IsRequired();
                builder.HasOne(p => p.InstanceType)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => p.InstanceTypeId);
                builder.HasOne(p => p.Region)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => p.RegionId);
            }
        }
    }
}