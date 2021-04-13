using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Models;

namespace Nano35.Instance.Processor.Models
{
    public class WorkersRole
    {
        //Primary key
        public Guid Id {get;set;}
        //Data
        public string Name { get; set; }
    }

    public partial class FluentContext 
    {
        public static void WorkersRole(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<WorkersRole>()
                .HasKey(u => u.Id);  
            
            //Data
            modelBuilder.Entity<WorkersRole>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }
}