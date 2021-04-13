using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Instance.Processor.UseCases.CreateClient;
using Nano35.Instance.Processor.UseCases.CreateInstance;
using Nano35.Instance.Processor.UseCases.CreateUnit;
using Nano35.Instance.Processor.UseCases.CreateWorker;
using Nano35.Instance.Processor.UseCases.GetAllClients;
using Nano35.Instance.Processor.UseCases.GetAllClientsStates;
using Nano35.Instance.Processor.UseCases.GetAllClientsTypes;
using Nano35.Instance.Processor.UseCases.GetAllInstances;
using Nano35.Instance.Processor.UseCases.GetAllInstanceTypes;
using Nano35.Instance.Processor.UseCases.GetAllRegions;
using Nano35.Instance.Processor.UseCases.GetAllUnits;
using Nano35.Instance.Processor.UseCases.GetAllUnitTypes;
using Nano35.Instance.Processor.UseCases.GetAllWorkerRoles;
using Nano35.Instance.Processor.UseCases.GetAllWorkers;
using Nano35.Instance.Processor.UseCases.GetClientById;
using Nano35.Instance.Processor.UseCases.GetClientStringById;
using Nano35.Instance.Processor.UseCases.GetInstanceById;
using Nano35.Instance.Processor.UseCases.GetInstanceStringById;
using Nano35.Instance.Processor.UseCases.GetUnitById;
using Nano35.Instance.Processor.UseCases.GetUnitStringById;
using Nano35.Instance.Processor.UseCases.GetWorkerStringById;

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
                    
                    cfg.ReceiveEndpoint("IGetAllWorkersRequestContract", e =>
                    {
                        e.Consumer<GetAllWorkersConsumer>(provider);
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
                        e.Consumer<CreateWorkerConsumer>(provider);
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
                    
                    cfg.ReceiveEndpoint("IGetClientByIdRequestContract", e =>
                    {
                        e.Consumer<GetClientByIdConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetUnitByIdRequestContract", e =>
                    {
                        e.Consumer<GetUnitByIdConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetWorkerStringByIdRequestContract", e =>
                    {
                        e.Consumer<GetWorkerStringByIdConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("IGetClientStringByIdRequestContract", e =>
                    {
                        e.Consumer<GetClientStringByIdConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("IGetUnitStringByIdRequestContract", e =>
                    {
                        e.Consumer<GetUnitStringByIdConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("IGetInstanceStringByIdRequestContract", e =>
                    {
                        e.Consumer<GetInstanceStringByIdConsumer>(provider);
                    });
                }));
                x.AddConsumer<GetWorkerStringByIdConsumer>();
                x.AddConsumer<GetClientStringByIdConsumer>();
                x.AddConsumer<GetUnitStringByIdConsumer>();
                x.AddConsumer<GetInstanceStringByIdConsumer>();
                x.AddConsumer<CreateUnitConsumer>();
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
                x.AddConsumer<GetClientByIdConsumer>();
                x.AddConsumer<GetUnitByIdConsumer>();
                
                x.AddRequestClient<IGetUserByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUserByIdRequestContract"));
                x.AddRequestClient<IRegisterRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IRegisterRequestContract"));
                x.AddRequestClient<ICreateUserRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateUserRequestContract"));

            });
            services.AddMassTransitHostedService();
        }
    }
}