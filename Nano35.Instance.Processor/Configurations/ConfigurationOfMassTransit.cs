using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Instance.Processor.Consumers;

namespace Nano35.Instance.Processor.Configurations
{
    public class MassTransitConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
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
                    
                    cfg.ReceiveEndpoint("IGetInstanceByIdRequestContract", e =>
                    {
                        e.Consumer<GetInstanceByIdConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllWorkerRolesRequestContract", e =>
                    {
                        e.Consumer<GetAllWorkerRolesConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllUnitsResultContract", e =>
                    {
                        e.Consumer<GetAllUnitsConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllUnitTypesRequestContract", e =>
                    {
                        e.Consumer<GetAllUnitTypesConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateInstanceRequestContract", e =>
                    {
                        e.Consumer<CreateInstanceConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateWorkerRequestContract", e =>
                    {
                        e.Consumer<GetAllWorkersConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateUnitRequestContract", e =>
                    {
                        e.Consumer<CreateUnitConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateClientRequestContract", e =>
                    {
                        e.Consumer<CreateClientConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllClientsRequestContract", e =>
                    {
                        e.Consumer<GetAllClientsConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllClientStatesRequestContract", e =>
                    {
                        e.Consumer<GetAllClientStatesConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllClientTypesRequestContract", e =>
                    {
                        e.Consumer<GetAllClientTypesConsumer>(provider);
                    });
                }));
                x.AddConsumer<CreateClientConsumer>();
                x.AddConsumer<GetAllInstancesConsumer>();
                x.AddConsumer<CreateInstanceConsumer>();
                x.AddConsumer<GetAllUnitTypesConsumer>();
                x.AddConsumer<GetAllInstanceTypesConsumer>();
                x.AddConsumer<GetAllRegionsConsumer>();
                x.AddConsumer<GetInstanceByIdConsumer>();
                x.AddConsumer<CreateWorkerConsumer>();
                x.AddConsumer<GetAllWorkersConsumer>();
                x.AddConsumer<GetAllWorkerRolesConsumer>();
                x.AddConsumer<GetAllUnitsConsumer>();
                x.AddConsumer<GetAllClientsConsumer>();
                x.AddConsumer<GetAllClientStatesConsumer>();
                x.AddConsumer<GetAllClientTypesConsumer>();
                
                x.AddRequestClient<IGetUserByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUserByIdRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}