using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsName
{
    public class UpdateClientsNameUseCase : UseCaseEndPointNodeBase<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>
    {
        private readonly IBus _bus;
        public UpdateClientsNameUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateClientsNameResultContract>> Ask(IUpdateClientsNameRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(_bus, input).GetResponse();
        }
    }
}