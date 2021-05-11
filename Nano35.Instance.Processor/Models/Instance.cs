using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class Instance
    {
        public Guid Id { get; set; }
        public string OrgName { get; set; }
        public string OrgRealName { get; set; }
        public string OrgEmail { get; set; }
        public string CompanyInfo { get; set; }
        public bool Deleted { get; set; }
        public Guid InstanceTypeId { get; set; }
        public Guid RegionId { get; set; }
        public InstanceType InstanceType { get; set; }
        public Region Region { get; set; }
        public ICollection<Unit> Units { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Worker> Workers { get; set; }

        public Instance()
        {
            Units = new List<Unit>();
            Clients = new List<Client>();
            Workers = new List<Worker>();
        }
        
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
                    .WithMany(p => p.Instances)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => p.InstanceTypeId);
                builder.HasOne(p => p.Region)
                    .WithMany(p => p.Instances)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => p.RegionId);
            }
        }
    }
}