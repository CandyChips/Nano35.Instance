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
                x.AddRequestClient<UseCaseResponse<IGetUnitStringByIdRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetClientStringByIdRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetInstanceStringByIdRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetWorkerStringByIdRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<UseCaseResponse<ICreateClientRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetAllWorkersRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWorkersRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetAllUnitsRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllUnitsRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetAllUnitTypesRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllUnitTypesRequestContract"));
                x.AddRequestClient<UseCaseResponse<ICreateWorkerRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateWorkerRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetAllInstancesRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllInstancesRequestContract"));
                x.AddRequestClient<UseCaseResponse<ICreateInstanceRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateInstanceRequestContract"));
                x.AddRequestClient<UseCaseResponse<ICreateUnitRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateUnitRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetInstanceByIdRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetInstanceByIdRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetClientByIdSuccessResultContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetClientByIdSuccessResultContract"));
                x.AddRequestClient<UseCaseResponse<IGetAllRegionsRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllRegionsRequestContract"));
                x.AddRequestClient<UseCaseResponse<IGetAllInstanceTypesRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllInstanceTypesRequestContract")); 
                x.AddRequestClient<UseCaseResponse<IGetAllWorkerRolesRequestContract>>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWorkerRolesRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}