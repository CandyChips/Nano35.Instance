using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano35.Contracts.Instance.Models;

namespace Nano35.Instance.Processor.Models
{
    public class WorkersRole
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid WorkerId { get; set; }
        
        public Role Role { get; set; }
        public Worker Worker { get; set; }
        
        public class Configuration : IEntityTypeConfiguration<WorkersRole>
        {
            public void Configure(EntityTypeBuilder<WorkersRole> builder)
            {
                builder.ToTable("WorkerRoles");
                builder.HasKey(u => u.Id);
                
                builder.HasOne(p => p.Role)
                    .WithMany(p => p.WorkersRoles)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new { p.RoleId });
                
                builder.HasOne(p => p.Worker)
                    .WithMany(p => p.WorkersRoles)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new { p.WorkerId })
                    .IsRequired();
            }
        }
    }
}