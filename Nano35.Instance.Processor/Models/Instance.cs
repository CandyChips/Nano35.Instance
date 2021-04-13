using System;
using Microsoft.EntityFrameworkCore;

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
    }

    public partial class FluentContext 
    {
        public static void Instance(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Instance>()
                .HasKey(u => u.Id);        
            
            //Data
            modelBuilder.Entity<Instance>()
                .Property(b => b.OrgName)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Instance>()
                .Property(b => b.OrgRealName)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Instance>()
                .Property(b => b.OrgEmail)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Instance>()
                .Property(b => b.CompanyInfo)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Instance>()
                .Property(b => b.Deleted) 
                .HasColumnType("bit")
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<Instance>()
                .HasOne(p => p.InstanceType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.InstanceTypeId);
            
            modelBuilder.Entity<Instance>()
                .HasOne(p => p.Region)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.RegionId);
        }
    }
}