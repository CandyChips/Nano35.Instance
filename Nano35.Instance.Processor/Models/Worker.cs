using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        public ICollection<Messenger> Messengers { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
        
        public Worker()
        {
            Messengers = new List<Messenger>();
        }

        public class Configuration : IEntityTypeConfiguration<Worker>
        {
            public void Configure(EntityTypeBuilder<Worker> builder)
            {
                builder.ToTable("Workers");
                builder.HasKey(u => new {u.Id, u.InstanceId});  
                builder.HasOne(p => p.Instance)
                       .WithMany()
                       .OnDelete(DeleteBehavior.NoAction)
                       .HasForeignKey(p => new { p.InstanceId });
                builder.Property(b => b.Name)    
                       .HasColumnType("nvarchar(MAX)")
                       .IsRequired();
                builder.Property(b => b.Comment)
                       .HasColumnType("nvarchar(MAX)")
                       .IsRequired();
                builder.HasOne(p => p.WorkersRole)
                       .WithMany()
                       .OnDelete(DeleteBehavior.NoAction)
                       .HasForeignKey(p => new { p.WorkersRoleId })
                       .IsRequired();
            }
        }
    }
}
