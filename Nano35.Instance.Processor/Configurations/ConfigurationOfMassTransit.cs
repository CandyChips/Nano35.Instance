using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Instance.Processor.UseCases.CreateClient;
using Nano35.Instance.Processor.UseCases.CreateInstance;
using Nano35.Instance.Processor.UseCases.CreateUnit;
using Nano35.Instance.Processor.UseCases.CreateWorker;
using Nano35.Instance.Processor.UseCases.DeleteClient;
using Nano35.Instance.Processor.UseCases.DeleteUnit;
using Nano35.Instance.Processor.UseCases.GetAllClients;
using Nano35.Instance.Processor.UseCases.GetAllClientsStates;
using Nano35.Instance.Processor.UseCases.GetAllClientsTypes;
using Nano35.Instance.Processor.UseCases.GetAllInstances;
using Nano35.Instance.Processor.UseCases.GetAllInstanceTypes;
using Nano35.Instance.Processor.UseCases.GetAllRegions;
using Nano35.Instance.Processor.UseCases.GetAllRolesByUser;
using Nano35.Instance.Processor.UseCases.GetAllUnits;
using Nano35.Instance.Processor.UseCases.GetAllUnitTypes;
using Nano35.Instance.Processor.UseCases.GetAllWorkerRoles;
using Nano35.Instance.Processor.UseCases.GetAllWorkers;
using Nano35.Instance.Processor.UseCases.GetClientById;
using Nano35.Instance.Processor.UseCases.GetClientStringById;
using Nano35.Instance.Processor.UseCases.GetClientStringsByIds;
using Nano35.Instance.Processor.UseCases.GetInstanceById;
using Nano35.Instance.Processor.UseCases.GetInstanceStringById;
using Nano35.Instance.Processor.UseCases.GetInstanceStringsByIds;
using Nano35.Instance.Processor.UseCases.GetUnitById;
using Nano35.Instance.Processor.UseCases.GetUnitStringById;
using Nano35.Instance.Processor.UseCases.GetUnitStringsByIds;
using Nano35.Instance.Processor.UseCases.GetWorkerById;
using Nano35.Instance.Processor.UseCases.GetWorkerStringById;
using Nano35.Instance.Processor.UseCases.GetWorkerStringsByIds;
using Nano35.Instance.Processor.UseCases.UpdateClientsEmail;
using Nano35.Instance.Processor.UseCases.UpdateClientsName;
using Nano35.Instance.Processor.UseCases.UpdateClientsState;
using Nano35.Instance.Processor.UseCases.UpdateClientsType;
using Nano35.Instance.Processor.UseCases.UpdateInstanceEmail;
using Nano35.Instance.Processor.UseCases.UpdateInstanceInfo;
using Nano35.Instance.Processor.UseCases.UpdateInstanceName;
using Nano35.Instance.Processor.UseCases.UpdateInstancePhone;
using Nano35.Instance.Processor.UseCases.UpdateInstanceRealName;
using Nano35.Instance.Processor.UseCases.UpdateInstanceRegion;
using Nano35.Instance.Processor.UseCases.UpdateUnitsAddress;
using Nano35.Instance.Processor.UseCases.UpdateUnitsName;
using Nano35.Instance.Processor.UseCases.UpdateUnitsPhone;
using Nano35.Instance.Processor.UseCases.UpdateUnitsType;
using Nano35.Instance.Processor.UseCases.UpdateUnitsWorkingFormat;
using Nano35.Instance.Processor.UseCases.UpdateWorkersComment;
using Nano35.Instance.Processor.UseCases.UpdateWorkersName;

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
                    
                    cfg.ReceiveEndpoint("IGetAllInstancesRequestContract", e => { e.Consumer<GetAllInstancesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllRegionsRequestContract", e => { e.Consumer<GetAllRegionsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllInstanceTypesRequestContract", e => { e.Consumer<GetAllInstanceTypesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetInstanceByIdRequestContract", e => { e.Consumer<GetInstanceByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllWorkerRolesRequestContract", e => { e.Consumer<GetAllWorkerRolesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllWorkersRequestContract", e => { e.Consumer<GetAllWorkersConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllUnitsResultContract", e => { e.Consumer<GetAllUnitsConsumer>(provider);});
                    cfg.ReceiveEndpoint("IGetAllUnitTypesRequestContract", e => { e.Consumer<GetAllUnitTypesConsumer>(provider); });
                    cfg.ReceiveEndpoint("ICreateInstanceRequestContract", e => { e.Consumer<CreateInstanceConsumer>(provider); });
                    cfg.ReceiveEndpoint("ICreateWorkerRequestContract", e => { e.Consumer<CreateWorkerConsumer>(provider); });
                    cfg.ReceiveEndpoint("ICreateUnitRequestContract", e =>{ e.Consumer<CreateUnitConsumer>(provider); });
                    cfg.ReceiveEndpoint("ICreateClientRequestContract", e => { e.Consumer<CreateClientConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllClientsRequestContract", e => { e.Consumer<GetAllClientsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllClientStatesRequestContract", e => { e.Consumer<GetAllClientStatesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllClientTypesRequestContract", e => { e.Consumer<GetAllClientTypesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetClientByIdRequestContract", e => { e.Consumer<GetClientByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetUnitByIdRequestContract", e => { e.Consumer<GetUnitByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetWorkerStringByIdRequestContract", e => { e.Consumer<GetWorkerStringByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetClientStringByIdRequestContract", e => { e.Consumer<GetClientStringByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetUnitStringByIdRequestContract", e => { e.Consumer<GetUnitStringByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetInstanceStringByIdRequestContract", e => { e.Consumer<GetInstanceStringByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IDeleteUnitRequestContract", e => { e.Consumer<DeleteUnitConsumer>(provider); });
                    cfg.ReceiveEndpoint("IDeleteClientRequestContract", e => { e.Consumer<DeleteClientConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllRolesByUserRequestContract", e => {e.Consumer<GetAllRolesByUserConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetClientStringsByIdsRequestContract", e => { e.Consumer<GetClientStringsByIdsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetInstanceStringsByIdsRequestContract", e => { e.Consumer<GetInstanceStringsByIdsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetUnitStringsByIdsRequestContract", e => { e.Consumer<GetUnitStringsByIdsConsumer>(provider); }); 
                    cfg.ReceiveEndpoint("IGetWorkerByIdRequestContract", e => { e.Consumer<GetWorkerByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetWorkerStringsByIdsRequestContract", e => { e.Consumer<GetWorkerStringsByIdsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateWorkersNameRequestContract", e => { e.Consumer<UpdateWorkersNameConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateWorkersCommentRequestContract", e => { e.Consumer<UpdateWorkersCommentConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateUnitsWorkingFormatRequestContract", e => { e.Consumer<UpdateUnitsWorkingFormatConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateUnitsTypeRequestContract", e => { e.Consumer<UpdateUnitsTypeConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateUnitsPhoneRequestContract", e => { e.Consumer<UpdateUnitsPhoneConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateUnitsNameRequestContract", e => { e.Consumer<UpdateUnitsNameConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateUnitsAddressRequestContract", e => { e.Consumer<UpdateUnitsAddressConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateInstanceRegionRequestContract", e => { e.Consumer<UpdateInstanceRegionConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateInstanceRealNameRequestContract", e => { e.Consumer<UpdateInstanceRealNameConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateInstancePhoneRequestContract", e => { e.Consumer<UpdateInstancePhoneConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateInstanceNameRequestContract", e => { e.Consumer<UpdateInstanceNameConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateInstanceInfoRequestContract", e => { e.Consumer<UpdateInstanceInfoConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateInstanceEmailRequestContract", e => { e.Consumer<UpdateInstanceEmailConsumer>(provider); }); 
                    cfg.ReceiveEndpoint("IUpdateClientsTypeRequestContract", e => { e.Consumer<UpdateClientsTypeConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateClientsStateRequestContract", e => { e.Consumer<UpdateClientsStateConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateClientsNameRequestContract", e => { e.Consumer<UpdateClientsNameConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateClientsEmailRequestContract", e => { e.Consumer<UpdateClientsEmailConsumer>(provider); });
                }));
                x.AddConsumer<UpdateClientsEmailConsumer>();
                x.AddConsumer<UpdateClientsNameConsumer>();
                x.AddConsumer<UpdateClientsStateConsumer>();
                x.AddConsumer<UpdateClientsTypeConsumer>();
                x.AddConsumer<UpdateInstanceEmailConsumer>();
                x.AddConsumer<UpdateInstanceInfoConsumer>();
                x.AddConsumer<UpdateInstanceNameConsumer>();
                x.AddConsumer<UpdateInstancePhoneConsumer>();
                x.AddConsumer<UpdateInstanceRealNameConsumer>();
                x.AddConsumer<UpdateInstanceRegionConsumer>();
                x.AddConsumer<UpdateUnitsAddressConsumer>();
                x.AddConsumer<UpdateUnitsNameConsumer>();
                x.AddConsumer<UpdateUnitsPhoneConsumer>();
                x.AddConsumer<UpdateUnitsTypeConsumer>();
                x.AddConsumer<UpdateUnitsWorkingFormatConsumer>();
                x.AddConsumer<UpdateWorkersCommentConsumer>();
                x.AddConsumer<UpdateWorkersNameConsumer>();
                x.AddConsumer<GetWorkerStringsByIdsConsumer>();
                x.AddConsumer<GetWorkerByIdConsumer>();
                x.AddConsumer<GetUnitStringsByIdsConsumer>();
                x.AddConsumer<GetInstanceStringsByIdsConsumer>();
                x.AddConsumer<GetClientStringsByIdsConsumer>();
                x.AddConsumer<DeleteUnitConsumer>();
                x.AddConsumer<DeleteClientConsumer>();
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
                x.AddConsumer<GetAllRolesByUserConsumer>();
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
                x.AddRequestClient<IRegisterCashboxRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IRegisterCashboxRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}