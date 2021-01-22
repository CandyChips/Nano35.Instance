using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Models;

namespace Nano35.Instance.Processor.Models
{
    public class InstanceType
    {
        //Primary key
        public Guid Id { get; set; }
        //Data
        public string Name { get; set; }
    }

    public partial class FluentContext 
    {
        public static void InstanceType(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<InstanceType>()
                .HasKey(u => u.Id);     
            
            //Data
            modelBuilder.Entity<InstanceType>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }

    public class InstanceTypesAutoMapperProfile : Profile
    {
        public InstanceTypesAutoMapperProfile()
        {
            CreateMap<Models.InstanceType, IInstanceTypeViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name));
        }
    }
}