using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientTypes
{
    public class GetAllClientTypesRequest :
        MasstransitRequest
        <IGetAllClientTypesRequestContract,
            IGetAllClientTypesResultContract,
            IGetAllClientTypesSuccessResultContract,
            IGetAllClientTypesErrorResultContract>
    {
        public GetAllClientTypesRequest(IBus bus, IGetAllClientTypesRequestContract request) : base(bus, request) {}
    }
}