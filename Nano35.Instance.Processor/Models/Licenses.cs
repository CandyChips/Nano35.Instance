using System;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Instance.Processor.Models
{
    public class License
    {
        //Primary key
        public Guid Id { get; set; }
        
        //Data
        public DateTime Date {get;set;}
        public bool Deleted { get; set; }
            
        //Forgein keys
        public Guid LicenseTypeId { get; set; }
        public LicenseType LicenseType { get; set; }
        public Guid InstanceId { get; set; }
        public Models.Instance Instance { get; set; }
    }

    public partial class FluentContext 
    {
        public static void License(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<License>()
                .HasKey(u => u.Id);
            
            //Data
            modelBuilder.Entity<License>()
                .Property(b => b.Date)    
                .HasColumnType("date")
                .IsRequired();
            
            modelBuilder.Entity<License>()
                .Property(b => b.Deleted)    
                .HasColumnType("bit")
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<License>()
                .HasOne(p => p.Instance)
                .WithMany()
                .HasForeignKey(p => p.InstanceId);
            
            modelBuilder.Entity<License>()
                .HasOne(p => p.LicenseType)
                .WithMany()
                .HasForeignKey(p => p.LicenseTypeId);
        }
    }
}