using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsEmail
{
    public class UpdateClientsEmailUseCase : UseCaseEndPointNodeBase<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>
    {
        private readonly IBus _bus;
        public UpdateClientsEmailUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateClientsEmailResultContract>> Ask(
            IUpdateClientsEmailRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(_bus, input)
                .GetResponse();
        }
    }
}