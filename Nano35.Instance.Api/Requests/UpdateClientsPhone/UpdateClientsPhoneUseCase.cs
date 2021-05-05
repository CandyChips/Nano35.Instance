using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsPhone
{
    public class UpdateClientsPhoneUseCase : UseCaseEndPointNodeBase<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateClientsPhoneUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateClientsPhoneResultContract>> Ask(IUpdateClientsPhoneRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(_bus, input).GetResponse();
        }
    }
}