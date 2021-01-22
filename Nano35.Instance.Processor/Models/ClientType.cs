using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Clients.Models;

namespace Nano35.Instance.Processor.Models
{
    public class ClientType
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string Name { get; set; }
    }

    public partial class FluentContext 
    {
        public static void ClientType(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<ClientType>()
                .HasKey(u => u.Id);        
            //Data
            modelBuilder.Entity<ClientType>()
                .Property(b => b.Name)  
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }

    public class ClientTypesAutoMapperProfile : Profile
    {
        public ClientTypesAutoMapperProfile()
        {
            CreateMap<ClientType, IClientTypeViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name));
        }
    }
}
