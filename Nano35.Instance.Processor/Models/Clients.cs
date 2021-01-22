using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Clients.Models;

namespace Nano35.Instance.Processor.Models
{
    public class Client
    {
        //Primary key
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public Instance Instance { get; set; }
        //Data
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Deleted { get; set; }
        //Forgein keys
        public Guid SalleTypeId { get; set; }
        public SalleType SalleType {get;set;}
        
        public Guid ClientTypeId { get; set; }
        public ClientType ClientType { get; set; }
        
        public Guid ClientStateId { get; set; }
        public ClientState ClientState { get; set; }
        
        public Guid WorkerId { get; set; }
        public Worker Creator { get; set; }
    }

    public partial class FluentContext 
    {
        public static void Client(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Client>()
                .HasKey(u => new {u.Id, u.InstanceId});  
            
            modelBuilder.Entity<Client>()
                .HasOne(p => p.Instance)
                .WithMany()
                .HasForeignKey(p => p.InstanceId);
            
            //Data
            modelBuilder.Entity<Client>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Client>()
                .Property(b => b.Email)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Client>()
                .Property(b => b.Phone)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
            
            modelBuilder.Entity<Client>()
                .Property(b => b.Deleted)
                .IsRequired();
            
            //Forgein keys
            modelBuilder.Entity<Client>()
                .HasOne(p => p.SalleType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.SalleTypeId);
            
            modelBuilder.Entity<Client>()
                .HasOne(p => p.ClientType)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.ClientTypeId);
            
            modelBuilder.Entity<Client>()
                .HasOne(p => p.ClientState)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.ClientStateId);
            
            modelBuilder.Entity<Client>()
                .HasOne(p => p.Creator)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => new { p.WorkerId,  p.InstanceId });
        }
    }

    public class ClientsAutoMapperProfile : Profile
    {
        public ClientsAutoMapperProfile()
        {
            CreateMap<Models.Client, IClientViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name))
                .ForMember(dest => dest.Email, source => source
                    .MapFrom(source => source.Email))
                .ForMember(dest => dest.Phone, source => source
                    .MapFrom(source => source.Phone))
                .ForMember(dest => dest.ClientTypeId, source => source
                    .MapFrom(source => source.ClientType.Id))
                .ForMember(dest => dest.ClientType, source => source
                    .MapFrom(source => source.ClientType.Name))
                .ForMember(dest => dest.ClientStateId, source => source
                    .MapFrom(source => source.ClientStateId))
                .ForMember(dest => dest.ClientState, source => source
                    .MapFrom(source => source.ClientState.Name));
        }
    }
}
