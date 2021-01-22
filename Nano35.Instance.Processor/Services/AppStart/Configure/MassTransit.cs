using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Instance.Processor.Services.MassTransit.Consumers;

namespace Nano35.Instance.Processor.Services.AppStart.Configure
{
    public static class MassTransitServiceConstructor
    {
        public static void Construct(
            IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri($"{ContractBase.RabbitMqLocation}/"), h =>
                    {
                        h.Username(ContractBase.RabbitMqUsername);
                        h.Password(ContractBase.RabbitMqPassword);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllInstancesRequestContract", e =>
                    {
                        e.Consumer<GetAllInstancesConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllRegionsRequestContract", e =>
                    {
                        e.Consumer<GetAllRegionsConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllInstanceTypesRequestContract", e =>
                    {
                        e.Consumer<GetAllInstanceTypesConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateInstanceRequestContract", e =>
                    {
                        e.Consumer<CreateInstanceConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetInstanceByIdRequestContract", e =>
                    {
                        e.Consumer<GetInstanceByIdConsumer>(provider);
                    });
                }));
                x.AddConsumer<GetAllInstancesConsumer>();
                x.AddConsumer<CreateInstanceConsumer>();
                x.AddConsumer<GetAllInstanceTypesConsumer>();
                x.AddConsumer<GetAllRegionsConsumer>();
                x.AddConsumer<GetInstanceByIdConsumer>();
            });
            services.AddMassTransitHostedService();
        }
    }
}