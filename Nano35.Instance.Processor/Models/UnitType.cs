using System;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Instance.Processor.Models
{
    public class UnitType
    {
        //Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
    }

    public partial class FluentContext 
    {
        public static void UnitType(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<UnitType>()
                .HasKey(u => u.Id);
            
            //Data
            modelBuilder.Entity<UnitType>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }
}
