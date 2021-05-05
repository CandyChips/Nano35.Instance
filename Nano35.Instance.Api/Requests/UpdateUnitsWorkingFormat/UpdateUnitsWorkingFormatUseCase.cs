using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatUseCase : UseCaseEndPointNodeBase<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateUnitsWorkingFormatUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateUnitsWorkingFormatResultContract>> Ask(IUpdateUnitsWorkingFormatRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>(_bus, input)
                .GetResponse();
        }
    }
}