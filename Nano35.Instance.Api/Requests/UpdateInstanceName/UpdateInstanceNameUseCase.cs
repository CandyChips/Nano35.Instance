using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceName
{
    public class UpdateInstanceNameUseCase : UseCaseEndPointNodeBase<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>
    {
        private readonly IBus _bus;
        public UpdateInstanceNameUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IUpdateInstanceNameResultContract>> Ask(IUpdateInstanceNameRequestContract input) =>
            await new MasstransitUseCaseRequest<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>(_bus, input)
                .GetResponse();
    }
}