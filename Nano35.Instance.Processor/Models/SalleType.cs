using System;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Instance.Processor.Models
{
    public class SalleType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Salle { get; set; }
    }
        
    public partial class FluentContext
    {
        public static void SalleType(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<SalleType>()
                .HasKey(u => u.Id);

            //Data
            modelBuilder.Entity<SalleType>()
                .Property(b => b.Name)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }
}
