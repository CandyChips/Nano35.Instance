using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsState
{
    public class UpdateClientsStateUseCase : UseCaseEndPointNodeBase<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>
    {
        private readonly IBus _bus;
        public UpdateClientsStateUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        public override async Task<UseCaseResponse<IUpdateClientsStateResultContract>> Ask(IUpdateClientsStateRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(_bus, input)
                .GetResponse();
        }
    }
}