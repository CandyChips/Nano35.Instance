using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Models;

namespace Nano35.Instance.Processor.Models
{
    public class Region
    {
        public Guid Id {get;set;}
        public string Name {get;set;}
        public string CashType {get;set;}
        public double Rate {get;set;}
    }
    
    public partial class FluentContext
    {
        public static void Region(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Region>()
                .HasKey(u => u.Id);

            //Data
            modelBuilder.Entity<Region>()
                .Property(b => b.Name)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            modelBuilder.Entity<Region>()
                .Property(b => b.CashType)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            modelBuilder.Entity<Region>()
                .Property(b => b.Rate)
                .HasColumnType("real")
                .IsRequired();
        }
    }

    public class RegionAutoMapperProfile : Profile
    {
        public RegionAutoMapperProfile()
        {
            CreateMap<Region, IRegionViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name));
        }
    }
}