using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsSelle
{
    public class UpdateClientsSelleUseCase : UseCaseEndPointNodeBase<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>
    {
        private readonly IBus _bus;
        public UpdateClientsSelleUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateClientsSelleResultContract>> Ask(IUpdateClientsSelleRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(_bus, input).GetResponse();
        }
    }
}