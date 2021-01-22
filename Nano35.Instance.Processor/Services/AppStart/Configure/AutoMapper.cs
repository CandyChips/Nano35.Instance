using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Services.AppStart.Configure
{
    public static class AutoMapperServiceConstructor 
    {
        public static void Construct(
            IServiceCollection services) 
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new InstancesAutoMapperProfile());
                mc.AddProfile(new ClientsAutoMapperProfile());
                mc.AddProfile(new ClientStatesAutoMapperProfile());
                mc.AddProfile(new ClientTypesAutoMapperProfile());
                mc.AddProfile(new InstanceTypesAutoMapperProfile());
                mc.AddProfile(new RegionAutoMapperProfile());
                mc.AddProfile(new UnitsAutoMapperProfile());
                mc.AddProfile(new UnitTypesAutoMapperProfile());
                mc.AddProfile(new WorkersAutoMapperProfile());
                mc.AddProfile(new WorkersRoleAutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            MappingPipe.Mapper = mapper;
            services.AddSingleton(mapper);
        }
    }
}