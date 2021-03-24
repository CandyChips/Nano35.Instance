using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitById
{
    public class GetUnitByIdRequest :
        MasstransitRequest
        <IGetUnitByIdRequestContract,
            IGetUnitByIdResultContract,
            IGetUnitByIdSuccessResultContract,
            IGetUnitByIdErrorResultContract>
    {
        public GetUnitByIdRequest(IBus bus, IGetUnitByIdRequestContract request) : base(bus, request) {}
    }
}