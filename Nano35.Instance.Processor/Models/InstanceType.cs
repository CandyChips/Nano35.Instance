using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class InstanceType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Instance> Instances { get; set; }
        public InstanceType() => Instances = new List<Instance>();

        public class Configuration : IEntityTypeConfiguration<InstanceType>
        {
            public void Configure(EntityTypeBuilder<InstanceType> builder)
            {
                builder.ToTable("InstanceTypes");
                builder.HasKey(u => u.Id);        
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
            }
        }
    }
}