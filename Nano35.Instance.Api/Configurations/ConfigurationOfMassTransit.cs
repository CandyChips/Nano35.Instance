using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Configurations
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
                }));
                x.AddRequestClient<IGetUnitStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetClientStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetInstanceStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetWorkerStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<ICreateClientRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetAllWorkersRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWorkersRequestContract"));
                x.AddRequestClient<IGetAllUnitsRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllUnitsRequestContract"));
                x.AddRequestClient<IGetAllUnitTypesRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllUnitTypesRequestContract"));
                x.AddRequestClient<ICreateWorkerRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateWorkerRequestContract"));
                x.AddRequestClient<IGetAllInstancesRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllInstancesRequestContract"));
                x.AddRequestClient<ICreateInstanceRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateInstanceRequestContract"));
                x.AddRequestClient<ICreateUnitRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateUnitRequestContract"));
                x.AddRequestClient<IGetInstanceByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetInstanceByIdRequestContract"));
                x.AddRequestClient<IGetClientByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetClientByIdRequestContract"));
                x.AddRequestClient<IGetAllRegionsRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllRegionsRequestContract"));
                x.AddRequestClient<IGetAllInstanceTypesRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllInstanceTypesRequestContract")); 
                x.AddRequestClient<IGetAllWorkerRolesRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWorkerRolesRequestContract"));
                
                x.AddRequestClient<IUpdateClientsEmailRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateClientsEmailRequestContract"));
                x.AddRequestClient<IUpdateClientsNameRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateClientsNameRequestContract"));
                x.AddRequestClient<IUpdateClientsPhoneRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateClientsPhoneRequestContract"));
                x.AddRequestClient<IUpdateClientsSelleRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateClientsSelleRequestContract"));
                x.AddRequestClient<IUpdateClientsStateRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateClientsStateRequestContract"));
                x.AddRequestClient<IUpdateInstanceEmailRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateInstanceEmailRequestContract"));
                x.AddRequestClient<IUpdateInstanceInfoRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateInstanceInfoRequestContract"));
                x.AddRequestClient<IUpdateInstanceNameRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateInstanceNameRequestContract"));
                x.AddRequestClient<IUpdateInstancePhoneRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateInstancePhoneRequestContract"));
                x.AddRequestClient<IUpdateInstanceRegionRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateInstanceRegionRequestContract"));
                x.AddRequestClient<IUpdateInstanceRealNameRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateInstanceRealNameRequestContract"));
                x.AddRequestClient<IUpdateUnitsAddressRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateUnitsAddressRequestContract"));
                x.AddRequestClient<IUpdateUnitsNameRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateUnitsNameRequestContract"));
                x.AddRequestClient<IUpdateUnitsPhoneRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateUnitsPhoneRequestContract"));
                x.AddRequestClient<IUpdateUnitsTypeRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateUnitsTypeRequestContract"));
                x.AddRequestClient<IUpdateUnitsWorkingFormatRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateUnitsWorkingFormatRequestContract"));
                x.AddRequestClient<IUpdateWorkersCommentRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateWorkersCommentRequestContract"));
                x.AddRequestClient<IUpdateWorkersNameRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IUpdateWorkersNameRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}