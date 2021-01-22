using System;
using Microsoft.EntityFrameworkCore;

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
    }

    public partial class FluentContext 
    {
        public static void LicenseType(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<LicenseType>()
                .HasKey(u => u.Id);
            
            //Data
            modelBuilder.Entity<LicenseType>()
                .Property(b => b.Name)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            modelBuilder.Entity<LicenseType>()
                .Property(b => b.Descriprion)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<LicenseType>()
                .Property(b => b.Price)    
                .HasColumnType("money")
                .IsRequired();
            
            modelBuilder.Entity<LicenseType>()
                .Property(b => b.Date)    
                .HasColumnType("date")
                .IsRequired();
            
            modelBuilder.Entity<LicenseType>()
                .Property(b => b.Deleted)    
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}