using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class UpdateClientsTypeUseCase : UseCaseEndPointNodeBase<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>
    {
        private readonly IBus _bus;
        public UpdateClientsTypeUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateClientsTypeResultContract>> Ask(IUpdateClientsTypeRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(_bus, input)
                .GetResponse();
        }
    }
}