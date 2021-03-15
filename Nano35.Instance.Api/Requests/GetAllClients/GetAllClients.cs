using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class GetAllClientsRequest : 
        MasstransitRequest
        <IGetAllClientsRequestContract, 
            IGetAllClientsResultContract,
            IGetAllClientsSuccessResultContract, 
            IGetAllClientsErrorResultContract>
    {
        public GetAllClientsRequest(IBus bus, IGetAllClientsRequestContract request) : base(bus, request) {}
    }
}