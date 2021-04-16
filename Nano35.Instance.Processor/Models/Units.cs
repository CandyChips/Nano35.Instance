using System;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Instance.Processor.Models
{
    public class Unit
    {
        //Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
        public string Adress { get; set; }
        public string WorkingFormat { get; set; }
        public string Phone { get; set; }
        public DateTime Date {get;set;}
        public bool Deleted { get; set; }
        
        //Forgein keys
        public Guid CreatorId { get; set; }
        public Worker Creator { get; set; }
        
        public Guid InstanceId { get; set; }
        public Instance Instance { get; set; }
        
        public Guid UnitTypeId { get; set; }
        public UnitType UnitType { get; set; }

        public override string ToString()
        {
            return $@"{Adress} - {Name}";
        }
    }

    public partial class FluentContext 
    {
        public static void Unit(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Unit>()
                .HasKey(u => new { u.Id });   
            
            //Data
            modelBuilder.Entity<Unit>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Unit>()
                .Property(b => b.Adress)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Unit>()
                .Property(b => b.WorkingFormat)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Unit>()
                .Property(b => b.Phone)    
                .HasColumnType("nvarchar(10)")
                .IsRequired();
            
            modelBuilder.Entity<Unit>()
                .Property(b => b.Date)    
                .HasColumnType("date")
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<Unit>()
                .HasOne(p => p.Instance)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.InstanceId)
                .IsRequired();
            
            modelBuilder.Entity<Unit>()
                .HasOne(p => p.UnitType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.UnitTypeId)
                .IsRequired();
            
            modelBuilder.Entity<Unit>()
                .HasOne(p => p.Creator)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new { p.CreatorId, p.InstanceId })
                .IsRequired();
        }
    }
}
