using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsEmail
{
    public class UpdateClientsEmailUseCase : UseCaseEndPointNodeBase<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateClientsEmailUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateClientsEmailResultContract>> Ask(
            IUpdateClientsEmailRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(_bus, input)
                .GetResponse();
        }
    }
}