using System;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Instance.Processor.Models
{
    public class CashOperation
    {
        //Primary key
        public Guid Id { get; set; }
        public Guid UnitId { get; set; }
        public Guid InstanceId { get; set; }
        //Data
        public int Type { get; set; }
        public string Description { get; set; }
        public double Cash { get; set; }
        public DateTime Date { get; set; }
        public Guid WorkerId { get; set; }
        //Forgein keys
        public Instance Instance { get; set; }
        public Instance Unit { get; set; }
        public Instance Worker { get; set; }
    }

    public partial class FluentContext 
    {
        public static void CashOperation(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<CashOperation>()
                .HasKey(u => new {u.Id, u.UnitId});  
            
            //Data
            modelBuilder.Entity<CashOperation>()
                .Property(b => b.Type)    
                .IsRequired();
            modelBuilder.Entity<CashOperation>()
                .Property(b => b.Description)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            modelBuilder.Entity<CashOperation>()
                .Property(b => b.Cash)    
                .HasColumnType("money")
                .IsRequired();
            modelBuilder.Entity<CashOperation>()
                .Property(b => b.Date)    
                .HasColumnType("datetime")
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<CashOperation>()
                .HasOne(p => p.Instance)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.InstanceId});
            modelBuilder.Entity<CashOperation>()
                .HasOne(p => p.Unit)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.UnitId});
            modelBuilder.Entity<CashOperation>()
                .HasOne(p => p.Worker)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new {p.WorkerId});
        }
    }
}