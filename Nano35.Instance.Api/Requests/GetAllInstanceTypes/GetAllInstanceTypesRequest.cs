using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class GetAllInstanceTypesRequest:
        MasstransitRequest
        <IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract,
            IGetAllInstanceTypesSuccessResultContract,
            IGetAllInstanceTypesErrorResultContract>
    {
        public GetAllInstanceTypesRequest(IBus bus, IGetAllInstanceTypesRequestContract request) : base(bus, request) {}
    }
}