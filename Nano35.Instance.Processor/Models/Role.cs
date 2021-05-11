using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<WorkersRole> WorkersRoles { get; set; }
        public Role() { WorkersRoles = new List<WorkersRole>(); }
        public class Configuration : IEntityTypeConfiguration<Role>
        {
            public void Configure(EntityTypeBuilder<Role> builder)
            {
                builder.ToTable("Roles");
                builder.HasKey(u => u.Id);
                builder.Property(b => b.Name).IsRequired();
            }
        }
    }
}