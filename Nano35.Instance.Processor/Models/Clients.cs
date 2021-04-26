using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nano35.Instance.Processor.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Deleted { get; set; }
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        public Guid WorkerId { get; set; }
        
        public Instance Instance { get; set; }
        public ClientType ClientType { get; set; }
        public ClientState ClientState { get; set; }
        public Worker Creator { get; set; }
        
        public class Configuration : IEntityTypeConfiguration<Client>
        {
            public void Configure(EntityTypeBuilder<Client> builder)
            {
                builder.ToTable("Clients");
                builder.HasKey(u => new {u.Id});  
                builder.Property(b => b.InstanceId)    
                    .IsRequired();
                builder.Property(b => b.Name)    
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.Email)
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.Phone)
                    .HasColumnType("nvarchar(MAX)")
                    .IsRequired();
                builder.Property(b => b.Deleted)
                    .IsRequired();
                builder.HasOne(p => p.ClientType)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.ClientTypeId});
                builder.HasOne(p => p.ClientState)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new {p.ClientStateId});
                builder.HasOne(p => p.Creator)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey(p => new { p.WorkerId });
                builder.HasOne(p => p.Instance)
                    .WithMany()
                    .HasForeignKey(p => new {p.InstanceId});
            }
        }
    }
}
