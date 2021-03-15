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
                x.AddRequestClient<IGetUnitStringByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetClientStringByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetInstanceStringByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetWorkerStringByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<ICreateClientRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetAllWorkersRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWorkersRequestContract"));
                x.AddRequestClient<IGetAllUnitsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllUnitsRequestContract"));
                x.AddRequestClient<IGetAllUnitTypesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllUnitTypesRequestContract"));
                x.AddRequestClient<ICreateWorkerRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateWorkerRequestContract"));
                x.AddRequestClient<IGetAllInstancesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllInstancesRequestContract"));
                x.AddRequestClient<ICreateInstanceRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateInstanceRequestContract"));
                x.AddRequestClient<ICreateUnitRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateUnitRequestContract"));
                x.AddRequestClient<IGetInstanceByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetInstanceByIdRequestContract"));
                x.AddRequestClient<IGetAllRegionsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllRegionsRequestContract"));
                x.AddRequestClient<IGetAllInstanceTypesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllInstanceTypesRequestContract")); 
                x.AddRequestClient<IGetAllWorkerRolesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWorkerRolesRequestContract"));
                x.AddRequestClient<ICreateCashOutputRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateCashOutputRequestContract")); 
                x.AddRequestClient<ICreateCashInputRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateCashInputRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}