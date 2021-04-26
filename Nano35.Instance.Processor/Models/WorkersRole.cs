using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts.Instance.Models;

namespace Nano35.Instance.Processor.Models
{
    public class WorkersRole
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string Name { get; set; }
        
        public ICollection<Worker> Workers { get; set; }

        public override string ToString() => Name;
        
        public WorkersRole()
        {
            Workers = new List<Worker>();
        }
        
        public class Configuration : IEntityTypeConfiguration<WorkersRole>
        {
            public void Configure(EntityTypeBuilder<WorkersRole> builder)
            {
                builder.ToTable("WorkerRoles");
                builder.HasKey(u => u.Id);
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
            }
        }
    }
}