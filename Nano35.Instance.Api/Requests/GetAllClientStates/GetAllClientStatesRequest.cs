using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class GetAllClientStatesRequest : 
        MasstransitRequest
        <IGetAllClientStatesRequestContract, 
            IGetAllClientStatesResultContract,
            IGetAllClientStatesSuccessResultContract, 
            IGetAllClientStatesErrorResultContract>
    {
        public GetAllClientStatesRequest(IBus bus, IGetAllClientStatesRequestContract request) : base(bus, request) {}
    }
}