using System;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Instance.Processor.Models
{
    public class InstanceType
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string Name { get; set; }
    }

    public partial class FluentContext 
    {
        public static void InstanceType(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<InstanceType>()
                .HasKey(u => u.Id);     
            
            //Data
            modelBuilder.Entity<InstanceType>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }
}