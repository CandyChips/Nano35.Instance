using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class Unit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string WorkingFormat { get; set; }
        public string Phone { get; set; }
        public DateTime Date {get;set;}
        public bool Deleted { get; set; }
        public Guid InstanceId { get; set; }
        public Guid UnitTypeId { get; set; }
        public Instance Instance { get; set; }
        public UnitType UnitType { get; set; }

        public override string ToString() => $@"{Adress} - {Name}";
        
        public class Configuration : IEntityTypeConfiguration<Unit>
        {
            public void Configure(EntityTypeBuilder<Unit> builder)
            {
                builder.ToTable("Units");
                builder.HasKey(u => new { u.Id });   
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.Adress)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.WorkingFormat)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.Phone)    
                    .HasColumnType("nvarchar(10)")
                    .IsRequired();
                builder.Property(b => b.Date)    
                    .HasColumnType("date")
                    .IsRequired();
                builder.HasOne(p => p.Instance)
                    .WithMany(p => p.Units)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => p.InstanceId)
                    .IsRequired();
                builder.HasOne(p => p.UnitType)
                    .WithMany(p => p.Units)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => p.UnitTypeId)
                    .IsRequired();
            }
        }
    }
}
