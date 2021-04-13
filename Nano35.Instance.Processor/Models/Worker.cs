using System;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Instance.Processor.Models
{
    public class Worker
    {
        //Primary key
        public Guid Id {get;set;}
        public Guid InstanceId { get; set; }
        public Instance Instance { get; set; }
        
        //Data
        public string Name { get; set; }
        public string Comment { get; set; }
        
        //Forein keys
        public Guid WorkersRoleId { get; set; }
        public WorkersRole WorkersRole { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    public partial class FluentContext 
    {
        public static void Worker(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Worker>()
                .HasKey(u => new {u.Id, u.InstanceId});  
            
            modelBuilder.Entity<Worker>()
                .HasOne(p => p.Instance)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new { p.InstanceId });
            
            //Data
            modelBuilder.Entity<Worker>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Worker>()
                .Property(b => b.Comment)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<Worker>()
                .HasOne(p => p.WorkersRole)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new { p.WorkersRoleId })
                .IsRequired();
        }
    }
}
