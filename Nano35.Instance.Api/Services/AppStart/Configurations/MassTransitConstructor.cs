using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Services.AppStart.Configurations
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
                }));
                x.AddRequestClient<IGetAllWorkersRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWorkersRequestContract"));
                x.AddRequestClient<ICreateWorkerRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateWorkerRequestContract"));
                x.AddRequestClient<IGetAllInstancesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllInstancesRequestContract"));
                x.AddRequestClient<ICreateInstanceRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateInstanceRequestContract"));
                x.AddRequestClient<IGetInstanceByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetInstanceByIdRequestContract"));
                x.AddRequestClient<IGetAllRegionsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllRegionsRequestContract"));
                x.AddRequestClient<IGetAllInstanceTypesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllInstanceTypesRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}