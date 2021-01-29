using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Models;

namespace Nano35.Instance.Processor.Models
{
    public class UnitType
    {
        //Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Name { get; set; }
    }

    public partial class FluentContext 
    {
        public static void UnitType(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<UnitType>()
                .HasKey(u => u.Id);
            
            //Data
            modelBuilder.Entity<UnitType>()
                .Property(b => b.Name)    
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }

    public class UnitTypesAutoMapperProfile : Profile
    {
        public UnitTypesAutoMapperProfile()
        {
            CreateMap<Models.UnitType, IUnitTypeViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name));

        }
    }

}
