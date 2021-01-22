using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Clients.Models;

namespace Nano35.Instance.Processor.Models
{
    public class ClientState
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string Name { get; set; }
    }

    public partial class FluentContext 
    {
        public static void ClientState(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<ClientState>()
                .HasKey(u => u.Id);        
            //Data
            modelBuilder.Entity<ClientState>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }

    public class ClientStatesAutoMapperProfile : Profile
    {
        public ClientStatesAutoMapperProfile()
        {
            CreateMap<ClientState, IClientStateViewModel>()
                .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source.MapFrom(source => source.Name));
        }
    }
}
