using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public Guid ClientStateId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Deleted { get; set; }
        public Instance Instance { get; set; }
        public ClientState ClientState { get; set; }
        public ClientProfile ClientProfile { get; set; }
        
        public class Configuration : IEntityTypeConfiguration<Client>
        {
            public void Configure(EntityTypeBuilder<Client> builder)
            {
                builder.ToTable("Clients");
                builder.HasKey(u => new { u.Id, u.InstanceId });  
                builder.Property(b => b.InstanceId).IsRequired();
                builder.Property(b => b.Name).HasColumnType("nvarchar(MAX)").IsRequired();
                builder.Property(b => b.Email).HasColumnType("nvarchar(MAX)").IsRequired();
                builder.Property(b => b.Deleted).IsRequired();
                builder.Property(b => b.ClientStateId).IsRequired();
                
                builder.HasOne(p => p.ClientProfile)
                    .WithMany(p => p.Clients)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new { p.Id });
                
                builder.HasOne(p => p.ClientState)
                    .WithMany(p => p.Clients)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new { p.ClientStateId });
                
                builder.HasOne(p => p.Instance)
                    .WithMany(p => p.Clients)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new { p.InstanceId });
            }
        }
    }
}
