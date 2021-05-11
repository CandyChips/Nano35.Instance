using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class Region
    {
        public Guid Id {get;set;}
        public string Name {get;set;}
        public ICollection<Instance> Instances { get; set; }
        public Region() => Instances = new List<Instance>();

        public class Configuration : IEntityTypeConfiguration<Region>
        {
            public void Configure(EntityTypeBuilder<Region> builder)
            {
                builder.ToTable("Regions");
                builder.HasKey(u => u.Id);
                builder.Property(b => b.Name)
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
            }
        }
    }
}