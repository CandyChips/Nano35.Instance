using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatRequest:
        MasstransitRequest
        <IUpdateUnitsWorkingFormatRequestContract,
            IUpdateUnitsWorkingFormatResultContract,
            IUpdateUnitsWorkingFormatSuccessResultContract,
            IUpdateUnitsWorkingFormatErrorResultContract>
    {
        public UpdateUnitsWorkingFormatRequest(IBus bus, IUpdateUnitsWorkingFormatRequestContract request) : base(bus, request) {}
    }
}