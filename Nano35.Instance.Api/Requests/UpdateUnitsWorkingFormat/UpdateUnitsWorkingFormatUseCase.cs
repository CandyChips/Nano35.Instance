using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatUseCase : EndPointNodeBase<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateUnitsWorkingFormatUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<IUpdateUnitsWorkingFormatResultContract> Ask(IUpdateUnitsWorkingFormatRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitRequest<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract, IUpdateUnitsWorkingFormatSuccessResultContract, IUpdateUnitsWorkingFormatErrorResultContract>(_bus, input)
                .GetResponse();
        }
    }
}