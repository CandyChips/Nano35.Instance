using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceEmail
{
    public class UpdateInstanceEmailUseCase : UseCaseEndPointNodeBase<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>
    {
        private readonly IBus _bus;
        public UpdateInstanceEmailUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IUpdateInstanceEmailResultContract>> Ask(IUpdateInstanceEmailRequestContract input) =>
            await new MasstransitUseCaseRequest<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(_bus, input)
                .GetResponse();
    }
}