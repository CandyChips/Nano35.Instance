using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class Worker
    {
        public Guid Id {get;set;}
        public Guid InstanceId { get; set; }
        public Instance Instance { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool? Deleted { get; set; }
        public ICollection<WorkersRole> WorkersRoles { get; set; }
        public override string ToString() => Name;

        public class Configuration : IEntityTypeConfiguration<Worker>
        {
            public void Configure(EntityTypeBuilder<Worker> builder)
            {
                builder.ToTable("Workers");
                builder.HasKey(u => new {u.Id});  
                builder.Property(b => b.Name)
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.InstanceId)
                    .IsRequired();
                builder.Property(b => b.Comment)
                   .HasColumnType("nvarchar(MAX)")
                   .IsRequired();
                builder.HasOne(p => p.Instance)
                    .WithMany(p => p.Workers)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new { p.InstanceId });
            }
        }
    }
}
