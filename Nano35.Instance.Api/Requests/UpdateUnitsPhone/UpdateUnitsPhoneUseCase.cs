using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneUseCase : UseCaseEndPointNodeBase<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateUnitsPhoneUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateUnitsPhoneResultContract>> Ask(IUpdateUnitsPhoneRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>(_bus, input)
                .GetResponse();

        }
    }
}